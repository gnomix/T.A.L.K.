 ///
 ///  WYBECOM T.A.L.K. -- Telephony Application Library Kit
 ///  Copyright (C) 2010 WYBECOM
 ///
 ///  Yohann BARRE <y.barre@wybecom.com>
 ///
 ///  This program is free software: you can redistribute it and/or modify
 ///  it under the terms of the GNU General Public License as published by
 ///  the Free Software Foundation, either version 3 of the License, or
 ///  (at your option) any later version.
 ///
 ///  This program is distributed in the hope that it will be useful,
 ///  but WITHOUT ANY WARRANTY; without even the implied warranty of
 ///  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 ///  GNU General Public License for more details.
 ///
 ///  You should have received a copy of the GNU General Public License
 ///  along with this program.  If not, see <http:///www.gnu.org/licenses/>.
 ///
 ///  T.A.L.K. is based upon:
 ///  - Sun JTAPI http:///java.sun.com/products/jtapi/
 ///  - JulMar TAPI http:///julmar.com/
 ///  - Asterisk.Net http:///sourceforge.net/projects/asterisk-dotnet/
 ///
 
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Asterisk.NET.Manager;
using Asterisk.NET.Manager.Response;
using Asterisk.NET.Manager.Action;
using Asterisk.NET.Manager.Event;
using Wybecom.TalkPortal.CTI.Proxy;
using System.ServiceModel;
using log4net;

namespace Wybecom.TalkPortal.Connectors.Asterisk
{
    public partial class AsteriskConnector : ServiceBase
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string dnToFind = "";
        private static string callToFind = "";
        private ManagerConnection manager = null;
        private ServiceHost host;
        private StateServer ss = null;
        private List<PeerEntryEvent> peers = new List<PeerEntryEvent>();
        private List<LineControl> linecontrols = new List<LineControl>();
        private List<Call> missedCalls = new List<Call>();
        private Hashtable channels = null;
        private AsteriskCTIService acs = null;
        public AsteriskConnector()
        {
            InitializeComponent();
            try
            {
                //Trace.Listeners.Add(new TextWriterTraceListener("asteriskconnector.log"));
                log.Debug("Initializing asterisk connector...");
                string address = Properties.Settings.Default.AsteriskServer;
                int port = Convert.ToInt32(Properties.Settings.Default.AsteriskPort);
                string user = Properties.Settings.Default.AsteriskUser;
                string password = Properties.Settings.Default.AsteriskUserPassword;
                log.Debug("Creating new manager connection: " + address + ":" + port + " as " + user);
                manager = new ManagerConnection(address, port, user, password);
                manager.PingInterval = Properties.Settings.Default.AsteriskPingInterval;
                manager.KeepAlive = true;
                manager.FireAllEvents = true;
                manager.UnhandledEvent += new ManagerEventHandler(manager_UnhandledEvent);
                
                log.Debug("Creating StateServer Proxy: ");
                ss = new StateServer(Properties.Settings.Default.StateServerUrl);
                
                log.Debug("Creating AsteriskCTIService...");
                acs = new AsteriskCTIService(manager);
                host = new ServiceHost(acs);
                log.Debug("Hosting AsteriskCTIService...");
                host.Open();
            }
            catch (Exception e)
            {
                log.Debug("Error while initializing AsteriskConnector: " + e.Message);
            }
        }

        void manager_UnhandledEvent(object sender, ManagerEvent e)
        {
            log.Debug("New unhandled event received: " + e.GetType().Name);
            LineControl lc = null;
            
            //StateServer
            switch (e.GetType().Name)
            {
                case "AGIExecEvent":
                    AGIExecEvent agievent = e as AGIExecEvent;
                    break;
                case "AlarmClearEvent":
                    AlarmClearEvent alarmclear = e as AlarmClearEvent;
                    break;
                case "AlarmEvent":
                    AlarmEvent alarmevent = e as AlarmEvent;
                    break;
                case "AsyncAGIEvent":
                    AsyncAGIEvent asyncagievent = e as AsyncAGIEvent;
                    break;
                case "BridgeEvent":
                    BridgeEvent bridgeevent = e as BridgeEvent;
                    break;
                case "CdrEvent":
                    CdrEvent cdrevent = e as CdrEvent;
                    break;
                case "ChannelReloadEvent":
                    ChannelReloadEvent channelreload = e as ChannelReloadEvent;
                    break;
                case "ChannelUpdateEvent":
                    ChannelUpdateEvent channelupdate = e as ChannelUpdateEvent;
                    break;
                case "ConnectEvent":
                    ConnectEvent connectevent = e as ConnectEvent;
                    break;
                case "ConnectionStateEvent":
                    ConnectionStateEvent connectionstate = e as ConnectionStateEvent;
                    break;
                case "DBGetResponseEvent":
                    DBGetResponseEvent dbget = e as DBGetResponseEvent;
                    log.Debug("DBGet response: " + dbget.ToString());
                    switch (dbget.Family)
                    {
                        case "DND":
                            ss.SetLineControl(setLineControlDND(dbget.Key, true));
                            break;
                        case "CF":
                            ss.SetLineControl(setLineControlForward(dbget.Key, dbget.Val));
                            break;
                    }
                    break;
                case "DialEvent":
                    DialEvent dial = e as DialEvent;
                    log.Debug("Dial event: " + dial.ToString());
                    break;
                case "DisconnectEvent":
                    DisconnectEvent disconnect = e as DisconnectEvent;
                    log.Debug("Disconnect event: " + disconnect.ToString());
                    break;
                case "DNDStateEvent":
                    DNDStateEvent dndstate = e as DNDStateEvent;
                    log.Debug("DND state event: " + dndstate.ToString());
                    break;
                case "ExtensionStatusEvent":
                    ExtensionStatusEvent extensionstatus = e as ExtensionStatusEvent;
                    log.Debug("Extension status event: " + extensionstatus.ToString() + ", status: " + extensionstatus.Status + ", hint: " + extensionstatus.Hint);
                    ss.SetLineControl(getLineControlFromExtensionStatusEvent(extensionstatus));
                    break;
                case "FaxReceivedEvent":
                    FaxReceivedEvent faxreceived = e as FaxReceivedEvent;
                    break;
                case "HangupEvent":
                    HangupEvent hangup = e as HangupEvent;
                    log.Debug("Hangup event: " + hangup.ToString() + " callerid: " + hangup.CallerId + " calleridnum: " + hangup.CallerIdNum);
                    //line control
                    if (channels.Contains(hangup.Channel))
                    {

                        lc = getLineControl((string)channels[hangup.Channel]);
                        int hi = 0;
                        LineControlConnection[] newLCC = null;
                        if (lc.lineControlConnection.Length > 1)
                        {
                            newLCC = new LineControlConnection[lc.lineControlConnection.Length - 1];
                            foreach (LineControlConnection hlcc in lc.lineControlConnection)
                            {
                                if (hlcc.callid != hangup.Channel)
                                {
                                    newLCC[hi] = hlcc;
                                    hi++;
                                }
                            }
                        }
                        lc.lineControlConnection = newLCC;
                        ss.SetLineControl(lc);
                        channels.Remove(hangup.Channel);
                    }
                    
                    //missed calls
                    callToFind = hangup.UniqueId.Substring(0, 6) + "," + hangup.UniqueId.Substring(6);
                    Call mCall = missedCalls.Find(FindCall);
                    if (mCall != null)
                    {
                        log.Debug("Missed call finded for callid: " + hangup.UniqueId);
                        AddCallLogs(mCall.callee, mCall);
                        if (missedCalls.Remove(mCall))
                        {
                            log.Debug("Call " + mCall.callId + " successfully removed from missedcall cache");
                        }
                        else
                        {
                            log.Debug("Call " + mCall.callId + " cannot be removed from missedcall cache");
                        }
                    }
                    break;
                case "HoldedCallEvent":
                    HoldedCallEvent holdedcall = e as HoldedCallEvent;
                    break;
                case "HoldEvent":
                    HoldEvent holdevent = e as HoldEvent;
                    break;
                case "JabberEvent":
                    JabberEvent jabberevent = e as JabberEvent;
                    break;
                case "JitterBufStatsEvent":
                    JitterBufStatsEvent jitter = e as JitterBufStatsEvent;
                    break;
                case "JoinEvent":
                    JoinEvent join = e as JoinEvent;
                    break;
                case "LeaveEvent":
                    LeaveEvent leave = e as LeaveEvent;
                    break;
                case "LinkEvent":
                    LinkEvent link = e as LinkEvent;
                    log.Debug("Link event: " + link.ToString());
                    lc = getLineControl(link.CallerId1);
                    if (lc != null)
                    {
                        foreach (LineControlConnection linklcc in lc.lineControlConnection)
                        {
                            if (linklcc.callid == link.Channel1)
                            {
                                linklcc.contact = link.CallerId2;
                                ss.SetLineControl(lc);
                                break;
                            }
                        }
                    }
                    lc = getLineControl(link.CallerId2);
                    if (lc != null)
                    {
                        foreach (LineControlConnection linklcc in lc.lineControlConnection)
                        {
                            if (linklcc.callid == link.Channel2)
                            {
                                linklcc.contact = link.CallerId1;
                                ss.SetLineControl(lc);
                                break;
                            }
                        }
                    }
                    break;
                case "LogChannelEvent":
                    LogChannelEvent logchannel = e as LogChannelEvent;
                    break;
                case "ManagerEvent":
                    ManagerEvent managerevent = e;
                    break;
                case "MeetmeEndEvent":
                    MeetmeEndEvent meetmeend = e as MeetmeEndEvent;
                    break;
                case "MeetmeJoinEvent":
                    MeetmeJoinEvent meetmejoin = e as MeetmeJoinEvent;
                    break;
                case "MeetmeLeaveEvent":
                    MeetmeLeaveEvent meetmeleave = e as MeetmeLeaveEvent;
                    break;
                case "MeetmeMuteEvent":
                    MeetmeMuteEvent meetmemute = e as MeetmeMuteEvent;
                    break;
                case "MeetmeStopTalkingEvent":
                    MeetmeStopTalkingEvent meetmestoptalking = e as MeetmeStopTalkingEvent;
                    break;
                case "MeetmeTalkingEvent":
                    MeetmeTalkingEvent meetmetalking = e as MeetmeTalkingEvent;
                    break;
                case "MeetmeTalkRequestEvent":
                    MeetmeTalkRequestEvent meetmetalkrequest = e as MeetmeTalkRequestEvent;
                    break;
                case "MessageWaitingEvent":
                    MessageWaitingEvent messagewaiting = e as MessageWaitingEvent;
                    log.Debug("Message waiting event: " + messagewaiting.ToString());
                    lc = getLineControl(messagewaiting.Mailbox.Substring(0, messagewaiting.Mailbox.IndexOf("@")));
                    if (lc != null)
                    {
                        if (messagewaiting.Waiting > 0)
                        {
                            lc.mwiOn = true;
                        }
                        else
                        {
                            lc.mwiOn = false;
                        }
                        ss.SetLineControl(lc);
                    }
                    break;
                case "MobileStatusEvent":
                    MobileStatusEvent mobilestatus = e as MobileStatusEvent;
                    break;
                case "ModuleLoadReportEvent":
                    ModuleLoadReportEvent moduleload = e as ModuleLoadReportEvent;
                    break;
                case "MonitorStartEvent":
                    MonitorStartEvent monitorstart = e as MonitorStartEvent;
                    break;
                case "MonitorStopEvent":
                    MonitorStopEvent monitorstop = e as MonitorStopEvent;
                    break;
                case "NewAccountCodeEvent":
                    NewAccountCodeEvent newaccountcode = e as NewAccountCodeEvent;
                    break;
                case "NewCallerIdEvent":
                    NewCallerIdEvent newcallerid = e as NewCallerIdEvent;
                    log.Debug("New caller id envent: " + newcallerid.ToString());
                    break;
                case "NewChannelEvent":
                    NewChannelEvent newchannel = e as NewChannelEvent;
                    log.Debug("New Channel event: " + newchannel.ToString());
                    CommandAction ca = new CommandAction("core show channel " + newchannel.Channel);
                    CommandResponse cr = (CommandResponse)manager.SendAction(ca, 10000);
                    log.Debug("Channel info: " + cr.ToString());
                    string dn = newchannel.CallerIdNum;
                    log.Debug("Retreive call information...");
                    bool callerIdUnknown = true;
                    if (newchannel.CallerIdNum != "<unknown>")
                    {
                        callerIdUnknown = false;
                    }
                    else
                    {
                        foreach (string s in cr.Result)
                        {
                            if (s.Contains("Caller ID:"))
                            {
                                dn = s.Substring(s.LastIndexOf(" "));
                                break;
                            }
                        }
                    }
                    Call newOutboundCall = getOutboundCallFromChannelInfo(cr.Result, callerIdUnknown);
                    if (newOutboundCall != null)
                    {
                        Call missedCall = newOutboundCall;
                        AddCallLogs(dn, newOutboundCall);
                        dnToFind = newOutboundCall.callee;
                        if (linecontrols.Find(FindLineControl) != null)
                        {
                            
                            log.Debug("This call will be put in missedcall cache: " + missedCall.callId);
                            missedCall.type = CallType.missed;
                            missedCalls.Add(missedCall);
                        }
                        
                    }
                    break;
                case "NewExtenEvent":
                    NewExtenEvent newexten = e as NewExtenEvent;
                    log.Debug("New exten event: " + newexten.ToString());
                    string channel = "";
                    char[] splitter = { '/' };
                    string[] splitchannel = newexten.Channel.Split(splitter);
                    splitter[0] = '-';
                    splitchannel = splitchannel[1].Split(splitter);
                    channel = splitchannel[0];
                    //DND?
                    if (newexten.Extension == Properties.Settings.Default.FeatureCodeDNDToggle && newexten.Application == "Playback")
                    {
                        switch (newexten.AppData)
                            {
                                case "do-not-disturb&activated":
                                    log.Debug("Successfully activate dnd for channel: " + channel);
                                    ss.SetLineControl(setLineControlDND(channel, true));
                                    break;
                                case "do-not-disturb&de-activated":
                                    log.Debug("Successfully deactivate dnd for channel: " + channel);
                                    ss.SetLineControl(setLineControlDND(channel, false));
                                    break;
                            }
                    }
                        //Forward all?
                    else if (newexten.Extension.Contains(Properties.Settings.Default.FeatureCodeCallForwardAllActivate) && newexten.Application == "Playback" && newexten.AppData == "call-fwd-unconditional&for&extension") 
                    {
                        string forward = newexten.Extension.Substring(Properties.Settings.Default.FeatureCodeCallForwardAllActivate.Length);
                        log.Debug("Call forward all from channel: " + channel + " to " + forward);
                        ss.SetLineControl(setLineControlForward(channel, forward));
                    }
                    // UnForwardAll
                    else if (newexten.Extension == Properties.Settings.Default.FeatureCodeCallForwardAllDeactivate && newexten.Application == "Playback" && newexten.AppData == "call-fwd-unconditional&de-activated")
                    {
                        log.Debug("Call unforward all from channel: " + channel);
                        ss.SetLineControl(setLineControlForward(channel,""));
                    }
                    break;
                case "NewStateEvent":
                    NewStateEvent newstate = e as NewStateEvent;
                    log.Debug("New State event: " + newstate.ToString());
                    LineControl newstateLc = getLineControl(newstate.CallerId);
                    LineControlConnection[] newStateLccs = null;
                    int i = 0;
                    if (newstateLc.lineControlConnection != null)
                    {
                        bool isContained = false;
                        foreach (LineControlConnection elcc in newstateLc.lineControlConnection)
                        {
                            if (elcc.callid == newstate.Channel)
                            {
                                isContained = true;
                                newStateLccs = newstateLc.lineControlConnection;
                                break;
                            }
                            i++;
                        }
                        if (!isContained)
                        {
                            i = 0;
                            newStateLccs = new LineControlConnection[newstateLc.lineControlConnection.Length + 1];
                            foreach (LineControlConnection newstateLcc in newstateLc.lineControlConnection)
                            {
                                newStateLccs[i] = newstateLcc;
                                i++;
                            }
                        }
                    }
                    else
                    {
                        newStateLccs = new LineControlConnection[1];
                        newStateLccs[0] = new LineControlConnection();
                    }
                    try
                    {
                        switch (newstate.State)
                        {
                            case "Up":
                                //received call?
                                callToFind = newstate.UniqueId;
                                Call rCall = missedCalls.Find(FindCall);
                                if (rCall != null)
                                {
                                    log.Debug("Missed call finded: " + callToFind + ", this call will be received");
                                    rCall.type = CallType.received;
                                    AddCallLogs(rCall.callee, rCall);
                                    missedCalls.Remove(rCall);
                                }
                                if (newStateLccs != null)
                                {
                                    if (!channels.Contains(newstate.Channel))
                                    {
                                        channels.Add(newstate.Channel, newstate.CallerId);
                                    }
                                    newStateLccs[i].callid = newstate.Channel;
                                    newStateLccs[i].remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                                    newStateLccs[i].state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                                    newStateLccs[i].terminalState = TerminalState.talking;
                                }
                                break;
                            case "Ringing":
                                if (newStateLccs != null)
                                {
                                    if (!channels.Contains(newstate.Channel))
                                    {
                                        channels.Add(newstate.Channel, newstate.CallerId);
                                    }
                                    newStateLccs[i].callid = newstate.Channel;
                                    newStateLccs[i].remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                                    newStateLccs[i].state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.alerting;
                                    newStateLccs[i].terminalState = TerminalState.ringing;
                                }
                                break;
                            case "Ring":
                                if (newStateLccs != null)
                                {
                                    if (!channels.Contains(newstate.Channel))
                                    {
                                        channels.Add(newstate.Channel, newstate.CallerId);
                                    }
                                    newStateLccs[i].callid = newstate.Channel;
                                    newStateLccs[i].remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.alerting;
                                    newStateLccs[i].state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.dialing;
                                    newStateLccs[i].terminalState = TerminalState.inuse;
                                }
                                break;
                        }
                    }
                    catch (Exception stateException)
                    {
                        log.Debug("NewState exception: " + stateException.Message);
                    }
                    if (newstateLc != null)
                    {
                        newstateLc.lineControlConnection = newStateLccs;
                        ss.SetLineControl(newstateLc);
                    }
                    break;
                case "OriginateResponseEvent":
                    OriginateResponseEvent originateresponse = e as OriginateResponseEvent;
                    break;
                case "ParkedCallEvent":
                    ParkedCallEvent parkedcall = e as ParkedCallEvent;
                    break;
                case "ParkedCallGiveUpEvent":
                    ParkedCallGiveUpEvent parkedcallgiveup = e as ParkedCallGiveUpEvent;
                    break;
                case "ParkedCallsCompleteEvent":
                    ParkedCallsCompleteEvent parkedcallscomplete = e as ParkedCallsCompleteEvent;
                    break;
                case "ParkedCallTimeOutEvent":
                    ParkedCallTimeOutEvent parkedcalltimeout = e as ParkedCallTimeOutEvent;
                    break;
                case "PeerEntryEvent":
                    log.Debug("SipAction: one peer entry event received, " + e.ToString());
                    PeerEntryEvent peerentry = e as PeerEntryEvent;
                    peers.Add(peerentry);
                    ss.SetLineControl(getLineControlFromPeerEntry(peerentry));
                    break;
                case "PeerlistCompleteEvent":
                    log.Debug("SipAction: peer list completed " + e.ToString());
                    PeerlistCompleteEvent peerlistcomplete = e as PeerlistCompleteEvent;
                    acs.setPeers(peers);
                    break;
                case "PeerStatusEvent":
                    PeerStatusEvent peerstatus = e as PeerStatusEvent;
                    log.Debug("Peer status: " + peerstatus.ToString());
                    break;
                case "PRIEvent":
                    PRIEvent pri = e as PRIEvent;
                    break;
                case "RegistryEvent":
                    RegistryEvent registry = e as RegistryEvent;
                    break;
                case "ReloadEvent":
                    ReloadEvent reload = e as ReloadEvent;
                    break;
                case "RenameEvent":
                    RenameEvent rename = e as RenameEvent;
                    break;
                case "ResponseEvent":
                    ResponseEvent response = e as ResponseEvent;
                    break;
                case "RTCPReceivedEvent":
                    RTCPReceivedEvent rtcpreceived = e as RTCPReceivedEvent;
                    break;
                case "RTCPSentEvent":
                    RTCPSentEvent rtcpsent = e as RTCPSentEvent;
                    break;
                case "RTPReceiverStatEvent":
                    RTPReceiverStatEvent rtpreceiver = e as RTPReceiverStatEvent;
                    break;
                case "RTPSenderStatEvent":
                    RTPSenderStatEvent rtpsender = e as RTPSenderStatEvent;
                    break;
                case "ShowDialPlanCompleteEvent":
                    ShowDialPlanCompleteEvent showdialplan = e as ShowDialPlanCompleteEvent;
                    break;
                case "ShutdownEvent":
                    ShutdownEvent shutdown = e as ShutdownEvent;
                    break;
                case "StatusCompleteEvent":
                    StatusCompleteEvent statuscomplete = e as StatusCompleteEvent;
                    break;
                case "StatusEvent":
                    StatusEvent status = e as StatusEvent;
                    break;
                case "TransferEvent":
                    TransferEvent transfer = e as TransferEvent;
                    break;
                case "UnholdEvent":
                    UnholdEvent unhold = e as UnholdEvent;
                    break;
                case "UnknownEvent":
                    UnknownEvent unknown = e as UnknownEvent;
                    break;
                case "UnlinkEvent":
                    UnlinkEvent unlink = e as UnlinkEvent;
                    log.Debug("Unlink event : " + unlink.ToString());
                    LineControlConnection[] lccs = null;
                    i = 0;
                    lc = getLineControl(unlink.CallerId1);
                    if (lc != null)
                    {
                        if (lc.lineControlConnection.Length > 1)
                        {
                            lccs = new LineControlConnection[lc.lineControlConnection.Length - 1];
                            foreach (LineControlConnection linklcc in lc.lineControlConnection)
                            {
                                if (linklcc.callid != unlink.Channel1)
                                {
                                    lccs[i] = linklcc;
                                    i++;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            lc.lineControlConnection = null;
                        }
                        ss.SetLineControl(lc);
                    }
                    i = 0;
                    lc = getLineControl(unlink.CallerId2);
                    if (lc != null)
                    {
                        if (lc.lineControlConnection.Length > 1)
                        {
                            lccs = new LineControlConnection[lc.lineControlConnection.Length - 1];
                            foreach (LineControlConnection linklcc in lc.lineControlConnection)
                            {
                                if (linklcc.callid != unlink.Channel2)
                                {
                                    lccs[i] = linklcc;
                                    i++;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            lc.lineControlConnection = null;
                        }
                        ss.SetLineControl(lc);
                    }
                    break;
                case "UnparkedCallEvent":
                    UnparkedCallEvent unparked = e as UnparkedCallEvent;
                    break;
                case "UserEvent":
                    UserEvent user = e as UserEvent;
                    break;
                case "VarSetEvent":
                    VarSetEvent varset = e as VarSetEvent;
                    break;
                case "ZapShowChannelsCompleteEvent":
                    ZapShowChannelsCompleteEvent zapshowchannelscomplete = e as ZapShowChannelsCompleteEvent;
                    break;
                case "ZapShowChannelsEvent":
                    ZapShowChannelsEvent zapshowchannels = e as ZapShowChannelsEvent;
                    break;
            }
            //ACDConnector
            switch (e.GetType().Name)
            {
                case "AgentCallbackLoginEvent":
                    AgentCallbackLoginEvent agentcallbacklogin = e as AgentCallbackLoginEvent;
                    break;
                case "AgentCallbackLogoffEvent":
                    AgentCallbackLogoffEvent agentcallbacklogoff = e as AgentCallbackLogoffEvent;
                    break;
                case "AgentCalledEvent":
                    AgentCalledEvent agentcalled = e as AgentCalledEvent;
                    break;
                case "AgentCompleteEvent":
                    AgentCompleteEvent agentcomplete = e as AgentCompleteEvent;
                    break;
                case "AgentConnectEvent":
                    AgentConnectEvent agentconnect = e as AgentConnectEvent;
                    break;
                case "AgentDumpEvent":
                    AgentDumpEvent agentdump = e as AgentDumpEvent;
                    break;
                case "AgentLoginEvent":
                    AgentLoginEvent agentlogin = e as AgentLoginEvent;
                    break;
                case "AgentLogoffEvent":
                    AgentLogoffEvent agentlogoff = e as AgentLogoffEvent;
                    break;
                case "AgentsCompleteEvent":
                    AgentsCompleteEvent agentscomplete = e as AgentsCompleteEvent;
                    break;
                case "AgentsEvent":
                    AgentsEvent agentevent = e as AgentsEvent;
                    break;
                case "QueueCallerAbandonEvent":
                    QueueCallerAbandonEvent queuecallerabandon = e as QueueCallerAbandonEvent;
                    break;
                case "QueueEntryEvent":
                    QueueEntryEvent queueentry = e as QueueEntryEvent;
                    break;
                case "QueueEvent":
                    QueueEvent queue = e as QueueEvent;
                    break;
                case "QueueMemberEvent":
                    QueueMemberEvent queuemember = e as QueueMemberEvent;
                    break;
                case "QueueMemberPausedEvent":
                    QueueMemberPausedEvent queuememberpaused = e as QueueMemberPausedEvent;
                    break;
                case "QueueMemberPenaltyEvent":
                    QueueMemberPenaltyEvent queuememberpenalty = e as QueueMemberPenaltyEvent;
                    break;
                case "QueueMemberRemovedEvent":
                    QueueMemberRemovedEvent queuememberremoved = e as QueueMemberRemovedEvent;
                    break;
                case "QueueMemberStatusEvent":
                    QueueMemberStatusEvent queuememberstatus = e as QueueMemberStatusEvent;
                    break;
                case "QueueParamsEvent":
                    QueueParamsEvent queueparams = e as QueueParamsEvent;
                    break;
                case "QueueStatusCompleteEvent":
                    QueueStatusCompleteEvent queuestatuscomplete = e as QueueStatusCompleteEvent;
                    break;
            }
        }

        protected override void OnStart(string[] args)
        {
            log.Debug("Starting AsteriskConnector...");
            try
            {
                manager.Login();
                log.Debug("Getting sip peers...");
                SIPPeersAction sipaction = new SIPPeersAction();
                manager.SendAction(sipaction);
                CommandAction ca = new CommandAction("sip show users");
                CommandResponse cr = (CommandResponse)manager.SendAction(ca, 10000);
                char[] splitter = { ' ' };
                
                foreach (string user in cr.Result)
                {
                    string[] usersplit = user.Split(splitter);
                    List<string> userprop = new List<string>();
                    foreach (string split in usersplit)
                    {
                        if (split.Length != 0)
                        {
                            userprop.Add(split);
                        }
                    }
                    if (userprop.Count == 5)
                    {
                        log.Debug("User " + userprop.ToArray()[0] + " password: " + userprop.ToArray()[1]);
                        acs.addUser(userprop.ToArray()[0], userprop.ToArray()[1]);
                    }
                }
                channels = new Hashtable();
            }
            catch (Exception e)
            {
                log.Debug("Exception while starting AsteriskConnector: " + e.Message);
            }
        }

        protected override void OnStop()
        {
            log.Debug("Stopping AsteriskConnector...");
            try
            {
                foreach (LineControl l in linecontrols)
                {
                    l.status = Status.unknown;
                    l.lineControlConnection = null;
                    ss.SetLineControl(l);
                }
                manager.Logoff();
            }
            catch (Exception e)
            {
                log.Debug("Exception while stopping AsteriskConnector: " + e.Message);
            }
        }

        private bool getMWI(string dn)
        {
            log.Debug("Retreiving mwi status for " + dn);
            MailboxStatusAction msa = new MailboxStatusAction(dn);
            MailboxStatusResponse msr = (MailboxStatusResponse)manager.SendAction(msa, 10000);
            log.Debug("Mwi status for " + dn + ": " + msr.ToString());
            return msr.Waiting;
        }

        private void getForward(string dn)
        {
            DBGetAction dbga = new DBGetAction();
            dbga.Family = "CF";
            dbga.Key = dn;
            manager.SendAction(dbga, 10000);
        }

        private void getDND(string dn)
        {
            DBGetAction dbga = new DBGetAction();
            dbga.Family = "DND";
            dbga.Key = dn;
            manager.SendAction(dbga, 10000);
        }

        private LineControl getLineControlFromPeerEntry(PeerEntryEvent pee)
        {
            log.Debug("Peer entry channel: " + pee.Channel + ", status:" + pee.Status);
            LineControl lc = null;
            lc = new LineControl();
            //lc.directoryNumber = pee.ChannelType + "/" + pee.ObjectName;
            lc.directoryNumber = pee.ObjectName;
            lc.doNotDisturb = false;
            lc.forward = "";
            lc.mwiOn = getMWI(pee.ObjectName);
            lc.lineControlConnection = null;
            switch (pee.Status)
            {
                case "UNKNOWN":
                    lc.status = Status.inactive;
                    break;
                default:
                    //OK
                    lc.status = Status.available;
                    break;
            }
            linecontrols.Add(lc);
            getDND(lc.directoryNumber);
            getForward(lc.directoryNumber);
            return lc;
        }

        private LineControl setLineControlForward(string dn, string forward)
        {
            LineControl lc = getLineControl(dn);
            lc.forward = forward;
            return lc;
        }

        private LineControl setLineControlDND(string dn, bool dnd)
        {
            LineControl lc = getLineControl(dn);
            lc.doNotDisturb = dnd;
            return lc;
        }

        private LineControl getLineControlFromExtensionStatusEvent(ExtensionStatusEvent ese)
        {
            log.Debug("Extension status event: " + ese.Exten + ", "  + ese.Channel + ", status:" + ese.Status.ToString());
            LineControl lc = getLineControl(ese.Exten);
            
            switch (ese.Status)
            {
                case 0:
                    if (lc.lineControlConnection == null)
                    {
                        lc.status = Status.available;
                    }
                    break;
                case 1:
                    lc.status = Status.busy;
                    break;
                case 8:
                    lc.status = Status.busy;
                    break;
                case 4:
                    lc.status = Status.inactive;
                    lc.lineControlConnection = null;
                    break;
            }
            return lc;
        }

        private LineControl getLineControl(string dn)
        {
            LineControl lc = null;
            dnToFind = dn;
            log.Debug("Looking for LineControl: " + dn);
            lc = linecontrols.Find(FindLineControl);
            if (lc == null)
            {
                log.Debug("LineControl: " + dn + " not exists");
                lc = new LineControl();
                lc.directoryNumber = dn;
                lc.doNotDisturb = false;
                lc.forward = "";
                lc.mwiOn = false;
                lc.lineControlConnection = null;
            }
            return lc;
        }

        private LineControlConnection getLineControlConnectionFromChannelInfo(NewChannelEvent ev)
        {
            LineControlConnection lcc = new LineControlConnection();
            lcc.callid = ev.UniqueId;
            lcc.contact = "";
            lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.unknown;
            lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.unknown;
            lcc.terminalState = TerminalState.unknown;
            return lcc;
        }

        private LineControlConnection getLineControlConnectionFromNewState(NewStateEvent state)
        {
            LineControlConnection lcc = new LineControlConnection();
            lcc.callid = state.UniqueId;
            lcc.contact = "";
            lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.unknown;
            lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.alerting;
            lcc.terminalState = TerminalState.ringing;
            return lcc;
        }

        private static bool FindLineControl(LineControl lc)
        {
            if (lc.directoryNumber == dnToFind)
            {
                return true;
            }
            {
                return false;
            }
        }

        private static bool FindCall(Call c)
        {
            log.Debug("Comparing call " + c.callId + " with " + callToFind + "...");
            if (c.callId == callToFind)
            {
                return true;
            }
            {
                return false;
            }
        }

        private Call getOutboundCallFromChannelInfo(List<string> result, bool callerIdUnknown)
        {
            char[] splitter = { '=' };
            Call c = null;
            try
            {
                c = new Call();
                c.type = CallType.placed;
                c.startTime = DateTime.Now;
                foreach (string s in result)
                {

                    if (s.StartsWith("level 1: ") && !callerIdUnknown)
                    {

                        string cdrvarkey = "";
                        string cdrvarvalue = "";
                        string cdrvar = s.Substring(9);
                        log.Debug(s + ", " + cdrvar);
                        cdrvarkey = cdrvar.Split(splitter)[0];
                        cdrvarvalue = cdrvar.Split(splitter)[1];
                        switch (cdrvarkey)
                        {
                            case "src":
                                c.caller = cdrvarvalue;
                                break;
                            case "dst":
                                c.callee = cdrvarvalue;
                                break;
                            case "uniqueid":
                                c.callId = cdrvarvalue;
                                break;
                        }
                    }
                    else
                    {
                        if (s.Contains("UniqueID:"))
                        {
                            c.callId = s.Substring(s.LastIndexOf(" ") + 1);
                        }
                        else if (s.Contains("Caller ID:"))
                        {
                            c.caller = s.Substring(s.LastIndexOf(" ") + 1);
                        }
                        else if (s.Contains("EXTTOCALL="))
                        {
                            c.callee = s.Substring(s.LastIndexOf("=") + 1);
                        }
                    }
                }
                if (c.callee == null || c.caller == null || c.callee.StartsWith("*") || c.caller == ""  || c.callee == "s")
                {
                    c = null;
                }
            }
            catch (Exception callException)
            {
                log.Debug(callException.Message);
            }
            return c;
        }

        private void AddCallLogs(string dn,Call c)
        {
            //adjust callid for compatibility issue with TALK Gadget:
            try
            {
                if (c.type != CallType.missed)
                {
                    c.callId = c.callId.Substring(0, 6) + "," + c.callId.Substring(6);
                }
                ss.AddCallLogs(dn, c);
            }
            catch (Exception e)
            {
                log.Debug("Unable to add call logs:" + e.Message);
            }
        }
    }
}
