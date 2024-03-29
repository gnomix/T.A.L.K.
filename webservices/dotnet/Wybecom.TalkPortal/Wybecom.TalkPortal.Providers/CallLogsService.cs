﻿ ///
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
using Wybecom.TalkPortal.CTI.Proxy.CLS;

namespace Wybecom.TalkPortal.Providers
{
    public class CallLogsService
    {
        private static CallLogsProvider _provider = null;
        private static CallLogsProviderCollection _providers = null;
        private static object _lock = new object();

        static CallLogsService()
        {
            LoadProviders();
        }
        public CallLogsProvider Provider
        {
            get { return _provider; }
        }
        public CallLogsProviderCollection Providers
        {
            get { return _providers; }
        }

        public static Call[] GetMissedCalls(string dn, string sort)
        {
            return _provider.GetMissedCalls(dn,sort);
        }

        public static Call[] GetPlacedCalls(string dn, string sort)
        {
            return _provider.GetPlacedCalls(dn,sort);
        }

        public static Call[] GetReceivedCalls(string dn, string sort)
        {
            return _provider.GetReceivedCalls(dn,sort);
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
                        CallLogsServiceSection section = (CallLogsServiceSection)
                            WebConfigurationManager.GetSection
                            ("callLogsService");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new CallLogsProviderCollection();
                        ProvidersHelper.InstantiateProviders
                            (section.Providers, _providers,
                            typeof(CallLogsProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException
                                ("Unable to load default CallLogsProvider");

                    }
                }
            }
        }
    }
}
