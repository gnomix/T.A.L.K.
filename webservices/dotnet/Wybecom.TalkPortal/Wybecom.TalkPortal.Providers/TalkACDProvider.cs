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
using log4net;
using System.IO;

namespace Wybecom.TalkPortal.Providers
{
    public class TalkACDProvider : ACDProvider
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _applicationName;

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

        public override string AgentLogin(string agent, string dn, string pwd)
        {
            log.Debug("Agent login request: " + agent + " with extension " + dn);
            return "";
        }

        public override bool AgentLogoff(string agent, string dn, string pwd)
        {
            log.Debug("Agent logoff request: " + agent);
            return true;
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "TalkACDProvider";
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Talk ACD Provider");
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
                    throw new ProviderException("Unrecognized attribute: " + attr);
            }
        }

        public override bool ChangeAgentState(string agent, string dn, string pwd, ushort code, ushort state)
        {
            throw new NotImplementedException();
        }

        public override Wybecom.TalkPortal.CTI.ACD.Agent[] GetAgents()
        {
            throw new NotImplementedException();
        }

        public override Wybecom.TalkPortal.CTI.ACD.CSQ[] GetCSQs()
        {
            throw new NotImplementedException();
        }

        public override Wybecom.TalkPortal.CTI.ACD.Application[] GetApplications()
        {
            throw new NotImplementedException();
        }

        public override Wybecom.TalkPortal.CTI.ACD.SummaryStatistics GetSummaryStatistics()
        {
            throw new NotImplementedException();
        }

        public override Wybecom.TalkPortal.CTI.ACD.AgentStatistics GetAgentStatistics(string agentid)
        {
            throw new NotImplementedException();
        }

        public override Wybecom.TalkPortal.CTI.ACD.QueueStatistics GetQueueStatistics(int csqid)
        {
            throw new NotImplementedException();
        }

        public override string UpdateToken(string token)
        {
            throw new NotImplementedException();
        }

        public override bool Validate(string token, string extension)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string token)
        {
            throw new NotImplementedException();
        }

        public override string GetAgentIdFromToken(string token)
        {
            throw new NotImplementedException();
        }

        public override string GetExtensionFromToken(string token)
        {
            throw new NotImplementedException();
        }

        public override string GetPwdFromToken(string token)
        {
            throw new NotImplementedException();
        }

        public override ReasonCode[] GetLogoffReasonCode()
        {
            throw new NotImplementedException();
        }

        public override ReasonCode[] GetNotReadyReasonCode()
        {
            throw new NotImplementedException();
        }

        public override Wybecom.TalkPortal.CTI.ACD.AgentStatistics[] GetAgentStatistics(string[] agentid)
        {
            throw new NotImplementedException();
        }

        public override Wybecom.TalkPortal.CTI.ACD.QueueStatistics[] GetQueueStatistics(int[] csqid)
        {
            throw new NotImplementedException();
        }

        public override Wybecom.TalkPortal.CTI.ACD.QueueStatistics[] GetQueueStatistics(string agentid)
        {
            throw new NotImplementedException();
        }

        public override string AgentLogin(string agent, string dn, string pwd, string teamextension)
        {
            throw new NotImplementedException();
        }

        public override bool AgentLogoff(string agent, string dn, string pwd, string teamextension)
        {
            throw new NotImplementedException();
        }
    }
}
