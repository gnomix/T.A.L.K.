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

namespace Wybecom.TalkPortal.Providers
{
    public class AuthenticationService
    {
        private static AuthenticationProvider _provider = null;
        private static AuthenticationProviderCollection _providers = null;
        private static object _lock = new object();

        static AuthenticationService()
        {
            LoadProviders();
        }

        public AuthenticationProvider Provider
        {
            get { return _provider; }
        }
        public AuthenticationProviderCollection Providers
        {
            get { return _providers; }
        }

        public static string Authenticate(string dn, string user, string password)
        {
            return _provider.Authenticate(dn, user, password);
        }

        public static string Authenticate(string dn, string user)
        {
            return _provider.Authenticate(dn, user);
        }

        public static bool Validate(string token, string dn)
        {
            return _provider.Validate(token, dn);
        }

        public static string UpdateToken(string token)
        {
            return _provider.UpdateToken(token);
        }

        public static string Encrypt(string token)
        {
            return _provider.Encrypt(token);
        }

        public static string getDN()
        {
            return _provider.GetDN();
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
                        AuthenticationServiceSection section = (AuthenticationServiceSection)
                            WebConfigurationManager.GetSection
                            ("authenticationService");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new AuthenticationProviderCollection();
                        ProvidersHelper.InstantiateProviders
                            (section.Providers, _providers,
                            typeof(AuthenticationProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException
                                ("Unable to load default AuthenticationProvider");

                    }
                }
            }
        }
    }
}
