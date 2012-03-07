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
using Wybecom.TalkPortal.Connectors.TAPI.Client;

namespace Wybecom.TalkPortal.Providers
{
    public class TAPICTIProvider: CTIProvider
    {
        private string _applicationName;
        private TAPICTIService _tapiservice;

        public TAPICTIProvider()
        {
            _tapiservice = new TAPICTIService();
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

        public override string Call(string caller, string callee)
        {
            return _tapiservice.Call(caller, callee);
        }

        public override bool UnHook(string callee, string callid)
        {
            bool success = false;
            bool isSpecified = true;
            _tapiservice.UnHook(callee, callid,out success, out isSpecified);
            return success;
        }

        public override bool HangUp(string caller, string callid)
        {
            bool success = false;
            bool isSpecified = true;
            _tapiservice.HangUp(caller, callid, out success, out isSpecified);
            return success;
        }

        public override bool Forward(string caller, string destination)
        {
            bool success = false;
            bool isSpecified = true;
            _tapiservice.Forward(caller,destination, out success, out isSpecified);
            return success;
        }

        public override bool Hold(string callid, string caller)
        {
            bool success = false;
            bool isSpecified = true;
            _tapiservice.Hold(callid, caller, out success, out isSpecified);
            return success;
        }

        public override bool UnHold(string callid, string caller)
        {
            bool success = false;
            bool isSpecified = true;
            _tapiservice.UnHold(callid, caller, out success, out isSpecified);
            return success;
        }
        public override bool DoNotDisturb(string caller)
        {
            bool success = false;
            bool isSpecified = true;
            _tapiservice.DoNotDisturb(caller, out success, out isSpecified);
            return success;
        }

        public override bool Transfer(string callid, string caller, string destination)
        {
            bool success = false;
            bool isSpecified = true;
            _tapiservice.Transfer(callid, caller,destination, out success, out isSpecified);
            return success;
        }

        public override bool Transfer(string caller)
        {
            bool success = false;
            bool isSpecified = true;
            _tapiservice.Transfer("", caller,"", out success, out isSpecified);
            return success;
        }
        public override bool ConsultTransfer(string callid, string callee, string destination)
        {
            throw new NotImplementedException();
        }
        public override bool Monitor(string monitorer, string monitored)
        {
            throw new NotImplementedException();
        }
        public override bool Divert(string callid, string caller)
        {
            throw new NotImplementedException();
        }
        
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "TAPICTIProvider";
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "TAPI CTI Provider");
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
            throw new NotImplementedException();
        }

        public override bool Logoff(string agentid)
        {
            throw new NotImplementedException();
        }
    }
}
