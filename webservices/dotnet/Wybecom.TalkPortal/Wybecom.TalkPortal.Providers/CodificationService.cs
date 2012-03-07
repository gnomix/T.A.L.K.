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
using Wybecom.TalkPortal.CTI.Proxy.CLS;
using Wybecom.TalkPortal.CTI;

namespace Wybecom.TalkPortal.Providers
{
    public class CodificationService
    {
        private static CodificationProvider _provider = null;
        private static CodificationProviderCollection _providers = null;
        private static object _lock = new object();
        
        static CodificationService()
        {
            LoadProviders();
        }
        public CodificationProvider Provider
        {
            get { return _provider; }
        }
        public CodificationProviderCollection Providers
        {
            get { return _providers; }
        }

        public static void AddCall(Wybecom.TalkPortal.CTI.Call call, string extension)
        {
            _provider.AddCall(call, extension);
        }
        public static Wybecom.TalkPortal.CTI.Proxy.CLS.Call GetCall(string callid, string extension)
        {
           return  _provider.GetCall(callid, extension);
        }
        public static int CodifCall(string callid, string extension, string codif)
        {
            return _provider.CodifCall(callid, extension, codif);
        }
        public static string[] GetCodif(bool active)
        {
            return _provider.GetCodif(active);
        }
        public static void AddCodif(string codif)
        {
            _provider.AddCodif(codif);
        }
        public static void DeleteCodif(string codif)
        {
            _provider.DeleteCodif(codif);
        }
        public static void EditCodif(string oldcodif, string newcodif)
        {
            _provider.EditCodif(oldcodif, newcodif);
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
                        CodificationServiceSection section = (CodificationServiceSection)
                            WebConfigurationManager.GetSection
                            ("codifService");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new CodificationProviderCollection();
                        ProvidersHelper.InstantiateProviders
                            (section.Providers, _providers,
                            typeof(CodificationProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException
                                ("Unable to load default CodificationProvider");

                    }
                }
            }
        }
    }
}
