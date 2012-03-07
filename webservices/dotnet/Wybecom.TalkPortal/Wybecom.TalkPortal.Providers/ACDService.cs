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
using System.Web.Configuration;
using System.Web;
using Wybecom.TalkPortal.CTI.ACD;

namespace Wybecom.TalkPortal.Providers
{
    public class ACDService
    {
        private static ACDProvider _provider = null;
        private static ACDProviderCollection _providers = null;
        private static object _lock = new object();

        static ACDService()
        {
            LoadProviders();
        }
        public ACDProvider Provider
        {
            get { return _provider; }
        }
        public ACDProviderCollection Providers
        {
            get { return _providers; }
        }

        public static string AgentLogin(string agent, string dn, string pwd)
        {
            return _provider.AgentLogin(agent, dn, pwd);
        }

        public static bool AgentLogoff(string agent, string dn, string pwd)
        {
            return _provider.AgentLogoff(agent, dn, pwd);
        }

        public static bool ChangeAgentState(string agent, string dn, string pwd, ushort code, ushort state)
        {
            return _provider.ChangeAgentState(agent, dn, pwd, code, state);
        }

        public static Agent[] GetAgents()
        {
            return _provider.GetAgents();
        }

        public static CSQ[] GetCSQs()
        {
            return _provider.GetCSQs();
        }

        public static Application[] GetApplications()
        {
            return _provider.GetApplications();
        }

        public static SummaryStatistics GetSummaryStatistics()
        {
            return _provider.GetSummaryStatistics();
        }

        public static AgentStatistics GetAgentStatistics(string agentid)
        {
            return _provider.GetAgentStatistics(agentid);
        }

        public static AgentStatistics[] GetAgentStatistics(string[] agentid)
        {
            return _provider.GetAgentStatistics(agentid);
        }

        public static QueueStatistics GetQueueStatistics(int csqid)
        {
            return _provider.GetQueueStatistics(csqid);
        }

        public static QueueStatistics[] GetQueueStatistics(int[] csqid)
        {
            return _provider.GetQueueStatistics(csqid);
        }

        public static QueueStatistics[] GetQueueStatistics(string agentid)
        {
            return _provider.GetQueueStatistics(agentid);
        }

        public static bool Validate(string token, string extension)
        {
            return _provider.Validate(token, extension);
        }

        public static string UpdateToken(string token)
        {
            return _provider.UpdateToken(token);
        }

        public static string Encrypt(string token)
        {
            return _provider.Encrypt(token);
        }

        public static string GetAgentIdFromToken(string token)
        {
            return _provider.GetAgentIdFromToken(token);
        }

        public static string GetExtensionFromToken(string token)
        {
            return _provider.GetExtensionFromToken(token);
        }

        public static string GetPwdFromToken(string token)
        {
            return _provider.GetPwdFromToken(token);
        }

        public static ReasonCode[] GetLogoffReasonCodes()
        {
            return _provider.GetLogoffReasonCode();
        }

        public static ReasonCode[] GetNotReadyReasonCodes()
        {
            return _provider.GetNotReadyReasonCode();
        }

        public static void LoadProviders()
        {
            if (_provider == null)
            {
                lock (_lock)
                {
                    if (_provider == null)
                    {
                        // Get a reference to the <acdService> section
                        ACDServiceSection section = (ACDServiceSection)
                            WebConfigurationManager.GetSection
                            ("acdService");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new ACDProviderCollection();
                        ProvidersHelper.InstantiateProviders
                            (section.Providers, _providers,
                            typeof(ACDProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException
                                ("Unable to load default ACDProvider");

                    }
                }
            }
        }
    }
}
