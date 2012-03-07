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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Asterisk.NET.Manager;
using Asterisk.NET.Manager.Action;
using Asterisk.NET.Manager.Response;
using Asterisk.NET.Manager.Event;
using log4net;

namespace Wybecom.TalkPortal.Connectors.Asterisk
{
    // REMARQUE : si vous modifiez le nom de classe « AsteriskCTIService » ici, vous devez également mettre à jour la référence à « AsteriskCTIService » dans App.config.
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single, ConcurrencyMode=ConcurrencyMode.Multiple)]
    public class AsteriskCTIService : IAsteriskCTIService
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ManagerConnection _manager = null;
        private List<PeerEntryEvent> peers = null;
        private Hashtable users = new Hashtable();
        
        public AsteriskCTIService(ManagerConnection manager)
        {
            _manager = manager;
        }

        public void setPeers(List<PeerEntryEvent> pee)
        {
            peers = pee;
        }

        public void addUser(string username, string password)
        {
            users.Add(username, password);
        }

        #region IAsteriskCTIService Membres

        public string Call(string caller, string callee)
        {
            //throw new NotImplementedException();
            log.Debug("Make call from " + caller + " to " + callee);
            OriginateAction newCall = new OriginateAction();
            newCall.CallerId = caller;
            //newCall.Channel = "SIP/1000";
            newCall.Channel = "SIP/" + caller;
            //newCall.Context = "app-dialvm";
            newCall.Context = Properties.Settings.Default.DefaultContext;
            newCall.Priority = 1;
            //newCall.Exten = "*98";
            newCall.Exten = callee;
            newCall.Timeout = 30000;
            newCall.Async = false;
            string result = _manager.SendAction(newCall, 10000).ToString();
            log.Debug("Result: " + result);
            return result;
        }

        public bool UnHook(string callee, string callid)
        {
            
            throw new NotImplementedException();
        }

        public bool HangUp(string caller, string callid)
        {
            log.Debug("Hanging up call " + callid + " from " + caller);
            HangupAction ha = new HangupAction(callid);
            return _manager.SendAction(ha, 10000).IsSuccess();
        }

        public bool Forward(string caller, string destination)
        {

            log.Debug("Forward all from " + caller + " to " + destination);
            OriginateAction newCall = new OriginateAction();
            newCall.CallerId = caller;
            //newCall.Channel = "SIP/1000";
            newCall.Channel = "SIP/" + caller;
            //newCall.Context = "app-dialvm";
            newCall.Context = Properties.Settings.Default.DefaultContext;
            newCall.Priority = 1;
            //newCall.Exten = "*98";
            if (destination != "")
            {
                newCall.Exten = Properties.Settings.Default.FeatureCodeCallForwardAllActivate + destination;
            }
            else
            {
                newCall.Exten = Properties.Settings.Default.FeatureCodeCallForwardAllDeactivate;
            }
            newCall.Timeout = 30000;
            newCall.Async = false;
            return _manager.SendAction(newCall, 10000).IsSuccess();
        }

        public bool Hold(string callid, string caller)
        {
            throw new NotImplementedException();
        }

        public bool UnHold(string callid, string caller)
        {
            throw new NotImplementedException();
        }

        public bool DoNotDisturb(string caller)
        {
            log.Debug("DND Toggle " + caller);
            OriginateAction newCall = new OriginateAction();
            newCall.CallerId = caller;
            //newCall.Channel = "SIP/1000";
            newCall.Channel = "SIP/" + caller;
            //newCall.Context = "app-dialvm";
            newCall.Context = Properties.Settings.Default.DefaultContext;
            newCall.Priority = 1;
            //newCall.Exten = "*98";
            newCall.Exten = Properties.Settings.Default.FeatureCodeDNDToggle;
            newCall.Timeout = 30000;
            newCall.Async = false;
            return _manager.SendAction(newCall, 10000).IsSuccess();
        }

        public bool Transfer(string callid, string caller, string destination)
        {
            RedirectAction ra = new RedirectAction();
            ra.Channel = caller;
            ra.Context = Properties.Settings.Default.DefaultContext;
            ra.Priority = 1;
            ra.Exten = destination;
            
            ManagerResponse mr = _manager.SendAction(ra, 10000);
            return mr.IsSuccess();
        }

        public bool Divert(string callid, string caller)
        {
            throw new NotImplementedException();
        }

        public bool AgentLogin(string agent, string dn)
        {
            AgentCallbackLoginAction acla = new AgentCallbackLoginAction(agent, dn);
            acla.AckCall = false;
            ManagerResponse mr = (ManagerResponse)_manager.SendAction(acla, 10000);
            return mr.IsSuccess();
        }

        public bool AgentLogoff(string agent)
        {
            AgentLogoffAction ala = new AgentLogoffAction(agent);
            ManagerResponse mr = (ManagerResponse)_manager.SendAction(ala, 10000);
            return mr.IsSuccess();
        }

        public bool Authenticate(string user, string password)
        {
            log.Debug("Authenticating " + user + "...");
            
            if (users.Contains(user))
            {
                log.Debug("Comparing password for " + user + "...");
                string pass = (string)users[user];
                if (pass == password)
                {
                    return true;
                }
                {
                    return false;
                }
            }
            {
                return false;
            }
        }

        #endregion
    }
}
