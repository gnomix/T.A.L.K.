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
    public class DirectoryNumberAnalysorService
    {
        private static DirectoryNumberAnalysorProvider _provider = null;
        private static DirectoryNumberAnalysorProviderCollection _providers = null;
        private static object _lock = new object();
        static DirectoryNumberAnalysorService()
        {
            LoadProviders();
        }
        public DirectoryNumberAnalysorProvider Provider
        {
            get { return _provider; }
        }
        public DirectoryNumberAnalysorProviderCollection Providers
        {
            get { return _providers; }
        }

        public static string Analyse(string dn)
        {
            return _provider.Analyse(dn);
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
                        DirectoryNumberAnalysorServiceSection section = (DirectoryNumberAnalysorServiceSection)
                            WebConfigurationManager.GetSection
                            ("directoryNumberAnalysorService");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new DirectoryNumberAnalysorProviderCollection();
                        ProvidersHelper.InstantiateProviders
                            (section.Providers, _providers,
                            typeof(DirectoryNumberAnalysorProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException
                                ("Unable to load default DirectoryNumberAnalysorProvider");

                    }
                }
            }
        }

    }
}
