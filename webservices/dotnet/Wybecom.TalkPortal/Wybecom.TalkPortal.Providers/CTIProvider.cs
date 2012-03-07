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
using System.Configuration.Provider;

namespace Wybecom.TalkPortal.Providers
{
    public abstract class CTIProvider : ProviderBase
    {
        public abstract string ApplicationName { get; set; }
        
        public abstract string Call(string caller, string callee);
        public abstract bool UnHook(string callee, string callid);
        public abstract bool HangUp(string caller, string callid);
        public abstract bool Forward(string caller, string destination);
        public abstract bool Hold(string callid, string caller);
        public abstract bool UnHold(string callid, string caller);
        public abstract bool DoNotDisturb(string caller);
        public abstract bool Transfer(string callid, string caller, string destination);
        public abstract bool Transfer(string caller);
        public abstract bool ConsultTransfer(string callid, string callee, string destination);
        public abstract bool Monitor(string monitorer, string monitored);
        public abstract bool Login(string agentid, string pwd, string extension);
        public abstract bool Logoff(string agentid);
        public abstract bool Divert(string callid, string caller);
    }
    public class CTIProviderCollection : ProviderCollection
    {
        public new CTIProvider this[string name]
        {
            get { return (CTIProvider)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (!(provider is CTIProvider))
                throw new ArgumentException("Invalid provider type", "provider");
            base.Add(provider);
        }
    }
}
