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
    public class ExtensionMobilityService
    {
        private static ExtensionMobilityProvider _provider = null;
        private static ExtensionMobilityProviderCollection _providers = null;
        private static object _lock = new object();

        static ExtensionMobilityService()
        {
            LoadProviders();
        }

        public static string[] getPhones(string[] users)
        {
            return _provider.getPhones(users);
        }

        public static void Login(string user, string phone, string profile)
        {
            _provider.Login(user, phone, profile);
        }

        public static void LoginFromLine(string user, string extension, string profile)
        {
            _provider.LoginFromLine(user, extension, profile);
        }

        public static void Logout(string phone)
        {
            _provider.Logout(phone);
        }

        public static string[] getUsers(string[] phones)
        {
            return _provider.getUsers(phones);
        }

        public ExtensionMobilityProvider Provider
        {
            get { return _provider; }
        }

        public ExtensionMobilityProviderCollection Providers
        {
            get { return _providers; }
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
                        ExtensionMobilityServiceSection section = (ExtensionMobilityServiceSection)
                            WebConfigurationManager.GetSection
                            ("extensionMobilityService");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new ExtensionMobilityProviderCollection();
                        ProvidersHelper.InstantiateProviders
                            (section.Providers, _providers,
                            typeof(ExtensionMobilityProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException
                                ("Unable to load default ExtensionMobilityProvider");

                    }
                }
            }
        }
    }
}
