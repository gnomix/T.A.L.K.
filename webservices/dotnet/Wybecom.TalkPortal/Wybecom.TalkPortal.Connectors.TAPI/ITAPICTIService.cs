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
using System.ServiceModel.Web;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Wybecom.TalkPortal.Connectors.TAPI
{
    // REMARQUE : si vous modifiez le nom d’interface « ITAPICTIService » ici, vous devez également mettre à jour la référence à « ITAPICTIService » dans App.config.
    [ServiceContract(Namespace="http://wybecom.org/talkportal/cti/tapictiservice")]
    public interface ITAPICTIService
    {
        [OperationContract]
        [WebGet()]
        string Call(string caller, string callee);

        [OperationContract]
        [WebGet()]
        bool UnHook(string callee, string callid);

        [OperationContract]
        [WebGet()]
        bool HangUp(string caller, string callid);

        [OperationContract]
        [WebGet()]
        bool Forward(string caller, string destination);

        [OperationContract]
        [WebGet()]
        bool Hold(string callid, string caller);

        [OperationContract]
        [WebGet()]
        bool UnHold(string callid, string caller);

        [OperationContract]
        [WebGet()]
        bool DoNotDisturb(string caller);

        [OperationContract]
        [WebGet()]
        bool Transfer(string callid, string caller, string destination);

        [OperationContract]
        [WebGet()]
        bool Divert(string callid, string caller);

        [OperationContract]
        [WebGet()]
        bool AgentLogin(string agent, string dn);

        [OperationContract]
        [WebGet()]
        bool AgentLogoff(string agent);
    }
}
