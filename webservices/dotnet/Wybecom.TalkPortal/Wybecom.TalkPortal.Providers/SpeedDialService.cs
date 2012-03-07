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
    public class SpeedDialService
    {
        private static SpeedDialProvider _provider = null;
        private static SpeedDialProviderCollection _providers = null;
        private static object _lock = new object();
        static SpeedDialService()
        {
            LoadProviders();
        }
        public SpeedDialProvider Provider
        {
            get { return _provider; }
        }
        public SpeedDialProviderCollection Providers
        {
            get { return _providers; }
        }
        public static SpeedDial[] GetSpeedDials(string extension){
            return _provider.GetSpeedDials(extension);
        }
        public static void AddSpeedDial(string extension, SpeedDial speeddial){
            _provider.AddSpeedDial(extension, speeddial);
        }
        public static void RemoveSpeedDial(string extension, SpeedDial speeddial)
        {
            _provider.RemoveSpeedDial(extension, speeddial);
        }
        public static void EditSpeedDial(string extension, SpeedDial newspeeddial, SpeedDial exspeeddial)
        {
            _provider.EditSpeedDial(extension,newspeeddial,exspeeddial);
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
                        SpeedDialServiceSection section = (SpeedDialServiceSection)
                            WebConfigurationManager.GetSection
                            ("speeddialService");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new SpeedDialProviderCollection();
                        ProvidersHelper.InstantiateProviders
                            (section.Providers, _providers,
                            typeof(SpeedDialProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException
                                ("Unable to load default SpeedDialProvider");

                    }
                }
            }
        }
    }
}
