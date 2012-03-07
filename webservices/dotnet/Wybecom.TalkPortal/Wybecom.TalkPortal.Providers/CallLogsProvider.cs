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
using Wybecom.TalkPortal.CTI.Proxy.CLS;

namespace Wybecom.TalkPortal.Providers
{
    public abstract class CallLogsProvider : ProviderBase
    {
        public abstract string ApplicationName { get; set; }

        public abstract Call[] GetMissedCalls(string dn, string sort);
        public abstract Call[] GetPlacedCalls(string dn, string sort);
        public abstract Call[] GetReceivedCalls(string dn, string sort);
    }

    public class CallLogsProviderCollection : ProviderCollection
    {
        public new CallLogsProvider this[string name]
        {
            get { return (CallLogsProvider)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (!(provider is CallLogsProvider))
                throw new ArgumentException("Invalid provider type", "provider");
            base.Add(provider);
        }
    }
}
