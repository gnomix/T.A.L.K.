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
using System.Configuration;
using System.Configuration.Provider;
using Wybecom.TalkPortal.CTI.ACD;

namespace Wybecom.TalkPortal.Providers
{
    public abstract class ACDProvider : ProviderBase
    {
        public abstract string ApplicationName { get; set; }

        public abstract string AgentLogin(string agent, string dn, string pwd, string teamextension);

        public abstract string AgentLogin(string agent, string dn, string pwd);

        public abstract bool AgentLogoff(string agent, string dn, string pwd);

        public abstract bool AgentLogoff(string agent, string dn, string pwd, string teamextension);

        public abstract bool ChangeAgentState(string agent, string dn, string pwd, ushort code, ushort state);

        public abstract Agent[] GetAgents();

        public abstract CSQ[] GetCSQs();

        public abstract Application[] GetApplications();

        public abstract SummaryStatistics GetSummaryStatistics();

        public abstract AgentStatistics GetAgentStatistics(string agentid);

        public abstract AgentStatistics[] GetAgentStatistics(string[] agentid);

        public abstract QueueStatistics GetQueueStatistics(int csqid);

        public abstract QueueStatistics[] GetQueueStatistics(int[] csqid);

        public abstract QueueStatistics[] GetQueueStatistics(string agentid);

        public abstract string UpdateToken(string token);

        public abstract bool Validate(string token, string extension);

        public abstract string Encrypt(string token);

        public abstract string GetAgentIdFromToken(string token);

        public abstract string GetExtensionFromToken(string token);

        public abstract string GetPwdFromToken(string token);

        public abstract ReasonCode[] GetLogoffReasonCode();

        public abstract ReasonCode[] GetNotReadyReasonCode();

    }

    public class ACDProviderCollection : ProviderCollection
    {
        public new ACDProvider this[string name]
        {
            get { return (ACDProvider)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (!(provider is AuthenticationProvider))
                throw new ArgumentException("Invalid provider type", "provider");
            base.Add(provider);
        }
    }
}
