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
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using JulMar.Atapi;
using Wybecom.TalkPortal.CTI.Proxy;
using log4net;

namespace Wybecom.TalkPortal.Connectors.TAPI
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, Namespace = "http://wybecom.org/talkportal/cti/tapisnapshotservice")]
    public class TAPISnapshotServer : ITAPISnapshotServer
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TapiManager _manager = null;

        public TAPISnapshotServer(TapiManager mgr)
        {
            _manager = mgr;
        }

        #region ITAPISnapshotServer Membres

        public LineControl[] GetSnapshot()
        {
            log.Debug("Snapshot requested..." + _manager.Lines.Length + " lines to generate"); 
            List<LineControl> linecontrols = new List<LineControl>();
            foreach (TapiLine line in _manager.Lines)
            {
                foreach (TapiAddress address in line.Addresses)
                {
                    linecontrols.Add(GetLineControl(address));
                }
            }
            return linecontrols.ToArray();
        }

        #endregion

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
    }
}
