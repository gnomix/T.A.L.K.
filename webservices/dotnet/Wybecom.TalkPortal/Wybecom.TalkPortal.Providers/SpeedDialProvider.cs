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

namespace Wybecom.TalkPortal.Providers
{
    public abstract class SpeedDialProvider : ProviderBase
    {
        public abstract string ApplicationName { get; set; }

        public abstract SpeedDial[] GetSpeedDials(string extension);
        public abstract void AddSpeedDial(string extension, SpeedDial speeddial);
        public abstract void RemoveSpeedDial(string extension, SpeedDial speeddial);

        public abstract void EditSpeedDial(string extension, SpeedDial newspeeddial, SpeedDial exspeeddial);
    }
    public class SpeedDialProviderCollection : ProviderCollection
    {
        public new SpeedDialProvider this[string name]
        {
            get { return (SpeedDialProvider)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (!(provider is SpeedDialProvider))
                throw new ArgumentException("Invalid provider type", "provider");
            base.Add(provider);
        }
    }
}