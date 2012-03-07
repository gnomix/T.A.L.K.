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
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using log4net;
using Wybecom.TalkPortal.CTI.Proxy;
using System.ServiceModel;
using JulMar.Atapi;
using System.Runtime.InteropServices;

namespace Wybecom.TalkPortal.Connectors.TAPI
{
    
    public partial class TAPIConnector : ServiceBase
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TapiManager manager = null;
        private ServiceHost host;
        private ServiceHost snapshotHost;
        private StateServer ss = null;
        private Hashtable Calls;
        
        private bool isInitialized = false;
        public TAPIConnector()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //Trace.Listeners.Add(new TextWriterTraceListener("asteriskconnector.log"));
                log.Debug("Initializing tapi connector...");
                Calls = new Hashtable();
                manager = new TapiManager("TALK TAPI CTI Service");
                if (manager.Initialize())
                {
                    log.Debug("Creating LineControlServer Proxy: ");
                    ss = new StateServer(Properties.Settings.Default.LineControlServerUrl);

                    log.Debug("Creating TAPICTIService...");
                    host = new ServiceHost(new TAPICTIService(manager));
                    log.Debug("Hosting TAPICTIService...");
                    host.Open();
                    log.Debug("Creating TAPISnapshotService...");
                    snapshotHost = new ServiceHost(new TAPISnapshotServer(manager));
                    log.Debug("Hosting TAPISnapshotService...");
                    snapshotHost.Open();
                    isInitialized = true;
                }
                else
                {
                    log.Debug("Unable to initialize TAPI Manager");
                }
            }
            catch (Exception e)
            {
                log.Debug("Error while initializing TAPIConnector: " + e.Message);
            }
            log.Debug("TAPI Connector starting");
            if (isInitialized)
            {
                manager.AddressChanged += new EventHandler<AddressInfoChangeEventArgs>(manager_AddressChanged);
                manager.CallInfoChanged += new EventHandler<CallInfoChangeEventArgs>(manager_CallInfoChanged);
                manager.CallStateChanged += new EventHandler<CallStateEventArgs>(manager_CallStateChanged);
                manager.LineAdded += new EventHandler<LineAddedEventArgs>(manager_LineAdded);
                manager.LineChanged += new EventHandler<LineInfoChangeEventArgs>(manager_LineChanged);
                manager.LineRemoved += new EventHandler<LineRemovedEventArgs>(manager_LineRemoved);
                manager.LineRinging += new EventHandler<RingEventArgs>(manager_LineRinging);
                manager.NewCall += new EventHandler<NewCallEventArgs>(manager_NewCall);
                manager.PhoneAdded += new EventHandler<PhoneAddedEventArgs>(manager_PhoneAdded);
                manager.PhoneRemoved += new EventHandler<PhoneRemovedEventArgs>(manager_PhoneRemoved);
                manager.ReinitRequired += new EventHandler(manager_ReinitRequired);
                
                foreach (TapiLine line in manager.Lines)
                {
                    line.Changed += new EventHandler<LineInfoChangeEventArgs>(line_Changed);
                    line.AddressChanged += new EventHandler<AddressInfoChangeEventArgs>(line_AddressChanged);
                    line.CallInfoChanged += new EventHandler<CallInfoChangeEventArgs>(line_CallInfoChanged);
                    line.CallStateChanged += new EventHandler<CallStateEventArgs>(line_CallStateChanged);
                    line.NewCall += new EventHandler<NewCallEventArgs>(line_NewCall);
                    line.Ringing += new EventHandler<RingEventArgs>(line_Ringing);
                    
                    

                    if (!line.IsOpen)
                    {
                        try
                        {
                            log.Debug("Opening line: " + line.ToString() + " name: " + line.Name);
                            line.Open(line.Capabilities.MediaModes);
                            
                            try
                            {
                                log.Debug("Parsing " + line.ToString() +" adresses");
                                foreach (TapiAddress address in line.Addresses)
                                {
                                    address.CallInfoChanged += new EventHandler<CallInfoChangeEventArgs>(address_CallInfoChanged);
                                    address.CallStateChanged += new EventHandler<CallStateEventArgs>(address_CallStateChanged);
                                    address.Changed += new EventHandler<AddressInfoChangeEventArgs>(address_Changed);
                                    address.NewCall += new EventHandler<NewCallEventArgs>(address_NewCall);
                                    
                                    ss.SetLineControl(GetLineControl(address));
                                }
                            }
                            catch (Exception e)
                            {
                                log.Error("Error while parsing " + line.ToString() + " addresses: " + e.Message);
                            }

                            //linedevspecific etrali
                            try
                            {
                                log.Debug(line.ToString() + " device specific data : " + System.Text.Encoding.ASCII.GetString(line.Status.DeviceSpecificData));
                                line.NegotiateExtensions(Properties.Settings.Default.DevSpecificMinVersion, Properties.Settings.Default.DevSpecificMaxVersion, new EventHandler<DeviceSpecificEventArgs>(line_DevSpecific));
                            }
                            catch (Exception lne)
                            {
                                log.Error("Unable to negotiate dev specific extensions for " + line.ToString() + " " + lne.ToString());
                            }
                        }
                        catch
                        {
                            log.Error("Error while opening line: " + line.ToString());
                        }
                    }
                }
            }
        }

        private LineControl GetLineControl(TapiAddress address)
        {
            LineControl lc = new LineControl();
            lc.directoryNumber = address.Address;
            lc.doNotDisturb = false;
            lc.forward = GetForward(address);
            lc.mwiOn = false;
            lc.monitored = "";
            if (address.Calls.Length > 0)
            {
                List<LineControlConnection> lcs = new List<LineControlConnection>();
                foreach (TapiCall tc in address.Calls)
                {
                    if (tc.CallState != CallState.Idle && tc.CallState != CallState.Unknown)
                    {
                        LineControlConnection lcc = new LineControlConnection();
                        lcc.callid = tc.Id.ToString();
                        if (tc.CallOrigin == CallOrigins.Outbound)
                        {
                            lcc.contact = tc.CalledId;
                        }
                        else 
                        {
                            lcc.contact = tc.CallerId;
                        }
                        SetLineControlConnection(lcc, tc.CallState);
                        lcs.Add(lcc);
                    }
                }
                lc.lineControlConnection = lcs.ToArray();
                if (address.Calls.Length > 1)
                {
                    lc.status = Status.busy;
                }
                else
                {
                    switch (address.Calls[0].CallState)
                    {
                        case CallState.Dialing:
                            lc.status = Status.dialing;
                            break;
                        case CallState.Offering:
                            lc.status = Status.ringing;
                            break;
                        case CallState.Idle:
                            lc.status = Status.available;
                            break;
                        case CallState.Disconnected:
                            lc.status = Status.available;
                            break;
                        case CallState.None:
                            lc.status = Status.available;
                            break;
                        case CallState.Unknown:
                            lc.status = Status.available;
                            break;
                        case CallState.Accepted:
                            lc.status = Status.busy;
                            break;
                        case CallState.Busy:
                            lc.status = Status.busy;
                            break;
                        default:
                            lc.status = Status.busy;
                            break;
                    }
                }
            }
            else
            {
                lc.status = Status.available;
            }
            return lc;
        }

        private string GetForward(TapiAddress address)
        {
            string forward = "";
            foreach (ForwardInfo fi in address.Status.ForwardingInformation)
            {
                if (fi.ForwardMode == ForwardingMode.Unconditional)
                {
                    forward = fi.DestinationAddress;
                    break;
                }
            }
            return forward;
        }

        private void SetLineControlConnection(LineControlConnection lcc, CallState cs)
        {
            switch (cs)
            {
                case CallState.Accepted:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.terminalState = TerminalState.talking;
                    break;
                case CallState.Busy:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.terminalState = TerminalState.talking;
                    break;
                case CallState.Conferenced:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.terminalState = TerminalState.bridged;
                    break;
                case CallState.Connected:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.terminalState = TerminalState.talking;
                    break;
                case CallState.Dialing:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.alerting;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.dialing;
                    lcc.terminalState = TerminalState.inuse;
                    break;
                case CallState.Dialtone:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.alerting;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.dialing;
                    lcc.terminalState = TerminalState.inuse;
                    break;
                case CallState.Disconnected:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.disconnected;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.disconnected;
                    lcc.terminalState = TerminalState.unknown;
                    break;
                case CallState.Idle:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.disconnected;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.disconnected;
                    lcc.terminalState = TerminalState.idle;
                    break;
                case CallState.None:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.terminalState = TerminalState.inuse;
                    break;
                case CallState.Offering:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.dialing;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.offered;
                    lcc.terminalState = TerminalState.inuse;
                    break;
                case CallState.OnHold:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.queued;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.established;
                    lcc.terminalState = TerminalState.held;
                    break;
                case CallState.Ringback:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.alerting;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.dialing;
                    lcc.terminalState = TerminalState.inuse;
                    break;
                case CallState.Unknown:
                    lcc.remoteState = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.unknown;
                    lcc.state = Wybecom.TalkPortal.CTI.Proxy.ConnectionState.unknown;
                    lcc.terminalState = TerminalState.unknown;
                    break;
            }
        }

        void address_NewCall(object sender, NewCallEventArgs e)
        {
            NewCall(e, "Address");
        }

        void NewCall(NewCallEventArgs e, string from)
        {
            log.Debug(from + " new call: " + e.Call.ToString() + " under " + e.Privilege.ToString() + " privilege. Extension: " + e.Call.Address.ToString() + " , state: " + e.Call.CallState.ToString());
            ss.SetLineControl(GetLineControl(e.Call.Address));
        }

        void address_Changed(object sender, AddressInfoChangeEventArgs e)
        {
            log.Debug("Address changed: " + e.Address.ToString() + " changes: " + e.Change.ToString());
            ss.SetLineControl(GetLineControl(e.Address));
        }

        void address_CallStateChanged(object sender, CallStateEventArgs e)
        {
            CallStateChanged(e, "Address");
        }

        void CallStateChanged(CallStateEventArgs e, string from)
        {
            log.Debug(from + " Call state changed: " + e.Call.ToString() + ", old state:" + e.OldCallState.ToString() + ", new call state:" + e.CallState.ToString() + " Extension: " + e.Call.Address.ToString());
            ss.SetLineControl(GetLineControl(e.Call.Address));
        }

        void address_CallInfoChanged(object sender, CallInfoChangeEventArgs e)
        {
            CallInfoChanged(e, "Address");
        }

        void CallInfoChanged(CallInfoChangeEventArgs e, string from)
        {
            log.Debug(from + " Call info changed: " + e.Call.ToString() + " Extension: " + e.Call.Address.ToString() + " changes: " + e.Change.ToString());
            ss.SetLineControl(GetLineControl(e.Call.Address));
            Call c = getCall(e.Call.Address.Address, e.Call.Id.ToString());
            if (c != null)
            {
                switch (e.Change)
                {
                    case CallInfoChangeTypes.CalledId:
                        break;
                    case CallInfoChangeTypes.CallerId:
                        break;
                    case CallInfoChangeTypes.RedirectionId:
                        log.Debug("Call redirectionId changed for " + e.Call.ToString() + ": " + e.Call.RedirectionId.ToString());
                        if (c.type == CallType.missed)
                        {
                            c.callee = e.Call.RedirectionId;
                        }
                        break;
                    case CallInfoChangeTypes.RelatedId:
                        log.Debug("Call relatedId changed for " + e.Call.ToString() + ": " + e.Call.RelatedId.ToString());
                        break;
                }
            }
        }

        void manager_ReinitRequired(object sender, EventArgs e)
        {
            log.Debug("Manager reinit required!");
        }

        void manager_PhoneRemoved(object sender, PhoneRemovedEventArgs e)
        {
            log.Debug("Phone removed: " + e.Phone.ToString());
        }

        void manager_PhoneAdded(object sender, PhoneAddedEventArgs e)
        {
            log.Debug("Phone added: " + e.Phone.ToString());
        }

        void manager_NewCall(object sender, NewCallEventArgs e)
        {
            Call c = new Call();
            c.callee = e.Call.CalledId;
            c.caller = e.Call.CallerId;
            c.callId = e.Call.Id.ToString();
            c.startTime = DateTime.Now;
            switch (e.Call.CallState)
            {
                case CallState.Accepted:
                    //first missed
                    c.type = CallType.missed;
                    break;
                case CallState.Dialing:
                    //placed 
                    c.type = CallType.placed;
                    break;
                case CallState.Dialtone:
                    //placed
                    c.type = CallType.placed;
                    break;
                case CallState.Offering:
                    //first missed
                    c.type = CallType.missed;
                    break;
            }
            putCall(c, e.Call.Address.Address);
            NewCall(e, "Manager");
        }

        void manager_LineRinging(object sender, RingEventArgs e)
        {
            log.Debug("Manager Line ringing: " + e.Line.ToString() + ", " + e.RingCount.ToString() + " times");
        }

        void manager_LineRemoved(object sender, LineRemovedEventArgs e)
        {
            foreach (TapiAddress ad in e.Line.Addresses)
            {
                LineControl lc = new LineControl();
                lc.directoryNumber = ad.Address;
                lc.doNotDisturb = false;
                lc.forward = GetForward(ad);
                lc.mwiOn = false;
                lc.monitored = "";
                lc.status = Status.inactive;
                lc.lineControlConnection = null;
                ss.SetLineControl(lc);
            }
            
            log.Debug("Manager line removed: " + e.Line.ToString());
        }

        void manager_LineChanged(object sender, LineInfoChangeEventArgs e)
        {
            LineChanged(e, "Manager");
        }

        void LineChanged(LineInfoChangeEventArgs e, string from)
        {
            log.Debug(from + " line changed: " + e.Line.ToString() + " changes: " + e.Change.ToString());
            foreach (TapiAddress address in e.Line.Addresses)
            {
                ss.SetLineControl(GetLineControl(address));
            }
        }

        void manager_LineAdded(object sender, LineAddedEventArgs e)
        {
            if (!e.Line.IsOpen)
            {
                e.Line.Open(e.Line.Capabilities.MediaModes);
                e.Line.AddressChanged += new EventHandler<AddressInfoChangeEventArgs>(line_AddressChanged);
                e.Line.CallInfoChanged += new EventHandler<CallInfoChangeEventArgs>(line_CallInfoChanged);
                e.Line.CallStateChanged += new EventHandler<CallStateEventArgs>(line_CallStateChanged);
                e.Line.Changed += new EventHandler<LineInfoChangeEventArgs>(line_Changed);
                e.Line.NewCall += new EventHandler<NewCallEventArgs>(line_NewCall);
                e.Line.Ringing += new EventHandler<RingEventArgs>(line_Ringing);
                foreach (TapiAddress ad in e.Line.Addresses)
                {
                    ss.SetLineControl(GetLineControl(ad));
                }
                log.Debug("Manager line added: " + e.Line);
            }
            
        }

        void manager_CallStateChanged(object sender, CallStateEventArgs e)
        {
            Call c = getCall(e.Call.Address.Address, e.Call.Id.ToString());
            if (c != null)
            {
                switch (e.CallState)
                {
                    case CallState.Connected:
                        switch (c.type)
                        {
                            case CallType.missed:
                                c.type = CallType.received;
                                putCall(c, e.Call.Address.Address);
                                break;
                            case CallType.placed:
                                c.callee = e.Call.CalledId;
                                putCall(c, e.Call.Address.Address);
                                break;
                        }
                        break;
                    case CallState.Disconnected:
                        c.endTime = DateTime.Now;
                        AddCall(c, e.Call.Address.Address);
                        break;
                    case CallState.Idle:
                        if (e.OldCallState != CallState.Idle && e.OldCallState != CallState.Disconnected)
                        {
                            c.endTime = DateTime.Now;
                            AddCall(c, e.Call.Address.Address);
                        }
                        break;
                }
            }
            else
            {
                c = new Call();
                c.callee = e.Call.CalledId;
                c.caller = e.Call.CallerId;
                c.callId = e.Call.Id.ToString();
                c.startTime = DateTime.Now;
                switch (e.Call.CallState)
                {
                    case CallState.Accepted:
                        //first missed
                        c.type = CallType.missed;
                        break;
                    case CallState.Dialing:
                        //placed 
                        c.type = CallType.placed;
                        break;
                    case CallState.Dialtone:
                        //placed
                        c.type = CallType.placed;
                        break;
                    case CallState.Offering:
                        //first missed
                        c.type = CallType.missed;
                        break;
                }
                putCall(c, e.Call.Address.Address);
            }
            CallStateChanged(e, "Manager");
        }

        void manager_CallInfoChanged(object sender, CallInfoChangeEventArgs e)
        {
            CallInfoChanged(e, "Manager");
        }

        void manager_AddressChanged(object sender, AddressInfoChangeEventArgs e)
        {
            log.Debug("Manager Address changed: " + e.Address.ToString());
            ss.SetLineControl(GetLineControl(e.Address));
        }

        void line_Ringing(object sender, RingEventArgs e)
        {
            log.Debug("Line ringing: " + e.Line.ToString() + ", " + e.RingCount.ToString() + " times");
        }

        void line_DevSpecific(object sender, DeviceSpecificEventArgs e)
        {
            try
            {
                log.Debug("Specific event: " + Marshal.PtrToStringAuto(e.Param1) + " " + Marshal.PtrToStringAuto(e.Param2) + " " + Marshal.PtrToStringAuto(e.Param3));
            }
            catch (Exception ex)
            {
                log.Error("Specific event: " + ex.ToString());
            }
        }

        void line_NewCall(object sender, NewCallEventArgs e)
        {
            NewCall(e, "Line");
        }

        void line_CallStateChanged(object sender, CallStateEventArgs e)
        {
            CallStateChanged(e, "Line");
        }

        void line_CallInfoChanged(object sender, CallInfoChangeEventArgs e)
        {
            CallInfoChanged(e, "Line");
        }

        void line_AddressChanged(object sender, AddressInfoChangeEventArgs e)
        {
            log.Debug("Line Address changed: " + e.Address.ToString());
            ss.SetLineControl(GetLineControl(e.Address));
        }

        void line_Changed(object sender, LineInfoChangeEventArgs e)
        {
            LineChanged(e, "Line");
        }

        private void putCall(Call c, string address)
        {
            try
            {
                Hashtable addressCalls = new Hashtable(); 
                if (Calls.ContainsKey(address))
                {
                    addressCalls = (Hashtable)Calls[address];
                    if (addressCalls != null)
                    {
                        if (addressCalls.Contains(c.callId))
                        {
                            addressCalls[c.callId] = c;
                        }
                        else
                        {
                            addressCalls.Add(c.callId, c);
                        }
                    }
                    else
                    {
                        addressCalls = new Hashtable();
                        addressCalls.Add(c.callId, c);
                    }
                    log.Debug("Adding call " + c.callId + " from " + address);
                    Calls[address] = addressCalls;
                }
                else
                {
                    addressCalls.Add(c.callId, c);
                    log.Debug("Adding call " + c.callId + " from " + address);
                    Calls.Add(address, addressCalls);
                }
            }
            catch (Exception e)
            {
                log.Error("Unable to put call " + c.callId + " from " + address + " in queue: " + e.Message);
            }
        }

        private Call getCall(string address, string callid)
        {
            Call c = null;
            try
            {
                Hashtable addressCalls = new Hashtable();
                if (Calls.ContainsKey(address))
                {
                    addressCalls = (Hashtable)Calls[address];
                    if (addressCalls != null)
                    {
                        if (addressCalls.Contains(callid))
                        {
                            c = (Call)addressCalls[callid];
                        }
                    }
                }
                return c;
            }
            catch (Exception e)
            {
                log.Error("Unable to get call " + callid + " from " + address + ": " + e.Message);
                return c;
            }
        }

        private void AddCall(Call c, string address)
        {
            try
            {
                if (((Hashtable)Calls[address]).ContainsKey(c.callId))
                {
                    log.Debug("Adding call for " + address + ": " + c.ToString());
                    ss.AddCallLogs(address, c);
                    shiftCall(c.callId, address);
                }
            }
            catch (Exception e)
            {
                log.Error("Unable to add call " + c.callId + " from " + address + ": " + e.Message);
            }
        }

        private void shiftCall(string callid, string address)
        {
            try
            {
                log.Debug("Shifting call " + callid + " from " + address);
                ((Hashtable)Calls[address]).Remove(callid);
            }
            catch (Exception e)
            {
                log.Error("Unable to shift call " + callid + " from " + address + ": " + e.Message);
            }
        }

        protected override void OnStop()
        {
            foreach (TapiLine line in manager.Lines)
            {
                if (line.IsOpen)
                {
                    log.Debug("Closing line: " + line.ToString());
                    line.Close();
                }
            }
            manager.Shutdown();
        }
    }
}
