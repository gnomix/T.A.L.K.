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
using Wybecom.TalkPortal.CTI.Proxy.CLS;
using Wybecom.TalkPortal.CTI;

namespace Wybecom.TalkPortal.Providers
{
    public abstract class CodificationProvider : ProviderBase
    {
        public abstract string ApplicationName { get; set; }
        public abstract void AddCall(Wybecom.TalkPortal.CTI.Call call, string extension);
        public abstract Wybecom.TalkPortal.CTI.Proxy.CLS.Call GetCall(string callid, string extension);
        public abstract int CodifCall(string callid, string extension, string codif );
        public abstract string[] GetCodif(bool active);
        public abstract void AddCodif(string codif);
        public abstract void DeleteCodif(string codif);
        public abstract void EditCodif(string oldcodif, string newcodif);
    }

    public class CodificationProviderCollection : ProviderCollection
    {
        public new CodificationProvider this[string name]
        {
            get { return (CodificationProvider)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (!(provider is CodificationProvider))
                throw new ArgumentException("Invalid provider type", "provider");
            base.Add(provider);
        }
    }
}
