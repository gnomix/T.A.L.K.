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
using System.Text;
using System.Configuration.Provider;
using Wybecom.TalkPortal.CTI.Proxy;

namespace Wybecom.TalkPortal.Providers
{
    public class TalkCTIProvider : CTIProvider
    {
        private string _applicationName;
        private CTIServerService _css;

        public TalkCTIProvider()
        {
            _css = new CTIServerService();
        }
        public override string Call(string caller, string callee){
            return _css.Call(caller, callee);
        }
        public override bool UnHook(string callee, string callid){
            bool result;
            bool returnSpecified;
            _css.UnHook(callee, callid,out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }
        public override bool HangUp(string caller, string callid){
            bool result;
            bool returnSpecified;
            _css.HangUp(caller, callid,out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }
        public override bool Forward(string caller, string destination){
            bool result;
            bool returnSpecified;
            _css.Forward(caller, destination, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }

        public override bool Hold(string callid, string caller){
            bool result;
            bool returnSpecified;
            _css.Hold(callid, caller, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }

        public override bool UnHold(string callid, string caller){
            bool result;
            bool returnSpecified;
            _css.UnHold(callid, caller, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }
        public override bool DoNotDisturb(string caller){
            bool result;
            bool returnSpecified;
            _css.DoNotDisturb(caller, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }

        public override bool Transfer(string callid, string caller, string destination){
            bool result;
            bool returnSpecified;
            _css.Transfer(callid, caller,destination, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }
        public override bool Transfer(string caller)
        {
            bool result;
            bool returnSpecified;
            _css.Transfer(null, caller, null, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }
        public override bool ConsultTransfer(string callid, string callee, string destination)
        {
            bool result;
            bool returnSpecified;
            _css.ConsultTransfer(callid, callee, destination, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }
        public override bool Monitor(string monitorer, string monitored)
        {
            bool result;
            bool returnSpecified;
            _css.Monitor(monitorer,monitored, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }
        public override bool Divert(string callid, string caller)
        {
            bool result;
            bool returnSpecified;
            _css.Divert(callid, caller, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }
        
        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "TalkCTIProvider";
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Talk CTI Provider");
            }
            base.Initialize(name, config);
            _applicationName = config["applicationName"];

            if (String.IsNullOrEmpty(_applicationName))
                _applicationName = "/";
            config.Remove("applicationName");

            if (config.Count > 0)
            {
                string attr = config.Get(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized atrtibute: " + attr);
            }
        }
        public override bool Login(string agentid, string pwd, string extension)
        {
            bool result;
            bool returnSpecified;
            _css.Login(agentid, pwd, extension, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }
        public override bool Logoff(string agentid)
        {
            bool result;
            bool returnSpecified;
            _css.Logoff(agentid, out result, out returnSpecified);
            if (returnSpecified)
            {
                return @result;
            }
            else
            {
                return true;
            }
        }
    }
}
