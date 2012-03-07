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
    // REMARQUE : si vous modifiez le nom de classe « TAPICTIService » ici, vous devez également mettre à jour la référence à « TAPICTIService » dans App.config.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, Namespace = "http://wybecom.org/talkportal/cti/tapictiservice")]
    public class TAPICTIService : ITAPICTIService
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TapiManager _manager = null;
        private List<TapiAddress> _addresses = new List<TapiAddress>();
        
        public TAPICTIService(TapiManager manager)
        {
            _manager = manager;
            _manager.LineAdded += new EventHandler<LineAddedEventArgs>(manager_LineAdded);
            _manager.LineRemoved += new EventHandler<LineRemovedEventArgs>(manager_LineRemoved);
            foreach (TapiLine line in manager.Lines)
            {
                foreach (TapiAddress address in line.Addresses)
                {
                    _addresses.Add(address);
                }
            }
        }

        

        TapiAddress GetAddress(string extension)
        {
            TapiAddress address = null;
            foreach (TapiAddress ad in _addresses)
            {
                if (ad.Address == extension)
                {
                    address = ad;
                    break;
                }
            }
            return address;
        }

        void manager_LineRemoved(object sender, LineRemovedEventArgs e)
        {
            log.Debug("Manager line removed: " + e.Line.ToString());
        }

        void manager_LineAdded(object sender, LineAddedEventArgs e)
        {
            log.Debug("Manager line added: " + e.Line);
        }


        #region ITAPICTIService Membres


        public string Call(string caller, string callee)
        {
            TapiAddress ad = GetAddress(caller);
            TapiCall call = null;
            if (ad != null)
            {
                log.Debug("Make call from " + ad.ToString() + " to " + callee);
                call = ad.MakeCall(callee);
                log.Debug("Call from " + ad.ToString() + " to " + callee + ": " + call.ToString());
            }
            return call.Id.ToString();
        }

        public bool UnHook(string callee, string callid)
        {
            TapiCall call = null;
            TapiAddress address = GetAddress(callee);
            bool success = false;
            if (address != null)
            {
                foreach (TapiCall tc in address.Calls)
                {
                    if (tc.Id.ToString() == callid)
                    {
                        call = tc;
                        break;
                    }
                }
                if (call != null)
                {
                    log.Debug("Answering call " + callid + " from " + callee);
                    call.Answer();
                    success = true;
                }
            }
            else
            {
                log.Debug("Can't get address: " + callee);
            }
            return success;
        }

        public bool HangUp(string caller, string callid)
        {
            TapiCall call = null;
            TapiAddress address = GetAddress(caller);
            bool success = false;
            if (address != null)
            {
                foreach (TapiCall tc in address.Calls)
                {
                    if (tc.Id.ToString() == callid)
                    {
                        call = tc;
                        break;
                    }
                }
                if (call != null)
                {
                    log.Debug("Hanging up call " + callid + " from " + caller);
                    call.Drop();
                    success = true;
                }
            }
            return success;
        }

        public bool Forward(string caller, string destination)
        {
            TapiAddress ad = GetAddress(caller);
            bool success = false;
            if (ad != null)
            {
                if (destination != "")
                {
                    log.Debug("Unconditional forward from " + ad.ToString() + " to " + destination);
                    ForwardInfo[] fis = new ForwardInfo[1];
                    fis[0] = new ForwardInfo(ForwardingMode.Unconditional, "", 0, destination);
                    log.Debug("Forwarding " + ad.ToString() + ": " + fis[0].ToString());
                    try
                    {
                        MakeCallParams mcp = new MakeCallParams();
                        mcp.NoAnswerTimeout = 0;
                        if (ad.Forward(fis, 3, mcp) != null)
                        {
                            success = true;
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error("Unable to forward " + ad.ToString() + " " + e.Message);
                    }
                }
                else
                {
                    ad.CancelForward();
                    success = true;
                }
            }
            return success;
        }

        public bool Hold(string callid, string caller)
        {
            TapiCall call = null;
            TapiAddress address = GetAddress(caller);
            bool success = false;
            log.Debug("Hold call " + callid + " from " + caller);
            if (address != null)
            {
                log.Debug("Parsing " + address.Calls.Length.ToString() + " from " + address.ToString());
                foreach (TapiCall tc in address.Calls)
                {
                    log.Debug("Call " + tc.ToString() + " from " + caller + " is being compared:" + tc.Id.ToString() + ": " + callid);
                    if (tc.Id.ToString() == callid)
                    {
                        call = tc;
                        break;
                    }
                }
                if (call != null)
                {
                    log.Debug("Holding call " + callid + " from " + caller);
                    call.Hold();
                    success = true;
                }
            }
            return success;
        }

        public bool UnHold(string callid, string caller)
        {
            TapiCall call = null;
            TapiAddress address = GetAddress(caller);
            bool success = false;
            if (address != null)
            {
                foreach (TapiCall tc in address.Calls)
                {
                    if (tc.Id.ToString() == callid)
                    {
                        call = tc;
                        break;
                    }
                }
                if (call != null)
                {
                    log.Debug("Unholding call " + callid + " from " + caller);
                    call.Unhold();
                    success = true;
                }
            }
            return success;
        }

        public bool DoNotDisturb(string caller)
        {
            throw new NotImplementedException();
        }

        public bool Transfer(string callid, string caller, string destination)
        {
            TapiCall call = null;
            TapiAddress address = GetAddress(caller);
            bool success = false;
            if (address != null)
            {
                foreach (TapiCall tc in address.Calls)
                {
                    if (tc.Id.ToString() == callid)
                    {
                        call = tc;
                        break;
                    }
                }
                if (call != null)
                {
                    log.Debug("Transfering call " + callid + " from " + caller + " to " + destination);
                    try
                    {
                        call.BlindTransfer(destination, 0);
                        success = true;
                    }
                    catch (Exception e)
                    {
                        log.Debug("Unable to setup transfer, " + e.Message);
                    }
                }
            }
            return success;
        }

        public bool Divert(string callid, string caller)
        {
            TapiCall call = null;
            TapiAddress address = GetAddress(caller);
            bool success = false;
            if (address != null)
            {
                foreach (TapiCall tc in address.Calls)
                {
                    if (tc.Id.ToString() == callid)
                    {
                        call = tc;
                        break;
                    }
                }
                if (call != null)
                {
                    log.Debug("Divert call " + callid + " from " + caller);
                    try
                    {
                        call.Drop();
                        success = true;
                    }
                    catch (Exception e)
                    {
                        log.Debug("Unable to drop call, " + e.Message);
                    }
                }
            }
            return success;
        }

        public bool AgentLogin(string agent, string dn)
        {
            throw new NotImplementedException();
        }

        public bool AgentLogoff(string agent)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
