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
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web;

namespace Wybecom.TalkPortal.Providers
{
    public class CTIService
    {
        private static CTIProvider _provider = null;
        private static CTIProviderCollection _providers = null;
        private static object _lock = new object();

        static CTIService()
        {
            LoadProviders();
        }
        public CTIProvider Provider
        {
            get { return _provider; }
        }
        public CTIProviderCollection Providers
        {
            get { return _providers; }
        }

        public static string Call(string caller, string callee){
            return _provider.Call(caller, callee);
        }
        public static bool UnHook(string callee, string callid){
            return _provider.UnHook(callee, callid);
        }
        public static bool HangUp(string caller, string callid){
            return _provider.HangUp(caller, callid);
        }
        public static bool Forward(string caller, string destination){
            return _provider.Forward(caller, destination);
        }
        public static bool Hold(string callid, string caller){
            return _provider.Hold(callid,caller);
        }
        public static bool UnHold(string callid, string caller){
            return _provider.UnHold(callid, caller);
        }
        public static bool DoNotDisturb(string caller){
            return _provider.DoNotDisturb(caller);
        }
        public static bool Transfer(string callid, string caller, string destination){
            return _provider.Transfer(callid, caller,destination);
        }
        public static bool Transfer(string caller)
        {
            return _provider.Transfer(caller);
        }
        public static bool ConsultTransfer(string callid, string callee, string destination)
        {
            return _provider.ConsultTransfer(callid, callee, destination);
        }
        public static bool Monitor(string monitorer, string monitored)
        {
            return _provider.Monitor(monitorer, monitored);
        }
        public static bool Login(string agentid, string pwd, string extension)
        {
            return _provider.Login(agentid, pwd, extension);
        }
        public static bool Logoff(string agentid)
        {
            return _provider.Logoff(agentid);
        }
        public static bool Divert(string callid, string caller)
        {
            return _provider.Divert(callid, caller);
        }

        public static void LoadProviders()
        {
            if (_provider == null)
            {
                lock (_lock)
                {
                    if (_provider == null)
                    {
                        // Get a reference to the <imageService> section
                        CTIServiceSection section = (CTIServiceSection)
                            WebConfigurationManager.GetSection
                            ("ctiService");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new CTIProviderCollection();
                        ProvidersHelper.InstantiateProviders
                            (section.Providers, _providers,
                            typeof(CTIProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException
                                ("Unable to load default CTIProvider");

                    }
                }
            }
        }
    }
}
