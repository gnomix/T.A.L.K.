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
using System.Web;
using System.Web.Services;
using Wybecom.TalkPortal.Providers;
using Wybecom.TalkPortal.CTI.Proxy.CLS;

namespace Wybecom.TalkPortal.CrossCTI
{
    /// <summary>
    /// Retreives call logs
    /// </summary>
    [WebService(Namespace = "http://wybecom.org/talkportal/crosscti/calllogsserver")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.GenerateScriptType(typeof(Call))]
    [System.Web.Script.Services.GenerateScriptType(typeof(CallType))]
    public class CallLogsServer : System.Web.Services.WebService
    {
        /// <summary>
        /// Retreives missed call logs.
        /// </summary>
        /// <seealso cref="CallLogsService"/>
        /// <param name="dn">The extension concerned</param>
        /// <param name="sort">
        /// The sorted field
        /// </param>
        /// <example>
        /// Retreives missed calls logs from the 1000 extension sorted by:
		/// - date
		/// - caller
		/// - callee
        /// <code>
        /// CallLogsServer cls = new CallLogsServer();
        /// Call[] missedCallsByDate = cls.GetMissedCalls("1000", "startTime");
        /// Call[] missedCallsByCaller = cls.GetMissedCalls("1000", "caller");
        /// Call[] missedCallsByCallee = cls.GetMissedCalls("1000", "callee");
        /// </code>
        /// 
        /// You can choose the provider from the web.config file
        /// <seealso cref="CallLogsProvider"/>
        /// <code>
        /// <section name="callLogsService" type="Wybecom.TalkPortal.Providers.CallLogsServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <callLogsService defaultProvider="TalkCallLogsProvider">
		///    <providers>
		///	    <add name="TalkCallLogsProvider" type="Wybecom.TalkPortal.Providers.TalkCallLogsProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
		///    </providers>
	    /// </callLogsService>
        /// </code>
        /// T.A.L.K. provides the "TalkCallLogsProvider" provider
        /// <seealso cref="TalkCallLogsProvider"/>
        /// If you want to implement your own provider, it must inherit Wybecom.TalkPortal.Providers.CallLogsProvider
        /// <seealso cref="CallLogsProvider"/>
        /// </example>
        /// <returns>
        /// An array of calls
        /// <seealso cref="Wybecom.TalkPortal.CTI.Proxy.CLS.Call"/>
        /// </returns>
        [WebMethod(MessageName = "GetMissedCalls",EnableSession=false)]
        public Call[] GetMissedCalls(string dn, string sort)
        {
            return CallLogsService.GetMissedCalls(dn,sort);
        }

        /// <summary>
        /// Retreives the passed calls.
        /// </summary>
        /// <seealso cref="CallLogsService"/>
        /// <param name="dn">The extension concerned</param>
        /// <param name="sort">
        /// The sorted field
        /// </param>
        /// <example>
        /// Retreives passed calls logs from the 1000 extension sorted by:
		/// - date
		/// - caller
		/// - callee
        /// <code>
        /// CallLogsServer cls = new CallLogsServer();
        /// Call[] placedCallsByDate = cls.GetPlacedCalls("1000", "startTime");
        /// Call[] placedCallsByCaller = cls.GetPlacedCalls("1000", "caller");
        /// Call[] placedCallsByCallee = cls.GetPlacedCalls("1000", "callee");
        /// </code>
        /// 
        /// You can choose the provider from the web.config file
        /// <seealso cref="CallLogsProvider"/>
        /// <code>
        /// <section name="callLogsService" type="Wybecom.TalkPortal.Providers.CallLogsServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <callLogsService defaultProvider="TalkCallLogsProvider">
		///    <providers>
		///	    <add name="TalkCallLogsProvider" type="Wybecom.TalkPortal.Providers.TalkCallLogsProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
		///    </providers>
	    /// </callLogsService>
        /// </code>
        /// T.A.L.K. provides the "TalkCallLogsProvider" provider
        /// <seealso cref="TalkCallLogsProvider"/>
        /// If you want to implement your own provider, it must inherit Wybecom.TalkPortal.Providers.CallLogsProvider
        /// <seealso cref="CallLogsProvider"/>
        /// </example>
        /// <returns>
        /// An array of calls
        /// <seealso cref="Wybecom.TalkPortal.CTI.Proxy.CLS.Call"/>
        /// </returns>
        [WebMethod(MessageName = "GetPlacedCalls", EnableSession = false)]
        public Call[] GetPlacedCalls(string dn, string sort)
        {
            return CallLogsService.GetPlacedCalls(dn, sort);
        }

        /// <summary>
        /// Retreives the received calls.
        /// </summary>
        /// <seealso cref="CallLogsService"/>
        /// <param name="dn">The extension concerned</param>
        /// <param name="sort">
        /// The sorted field
        /// </param>
        /// <example>
        /// Retreives received calls logs from the 1000 extension sorted by:
		/// - date
		/// - caller
		/// - callee
        /// <code>
        /// CallLogsServer cls = new CallLogsServer();
        /// Call[] receivedCallsByDate = cls.GetReceivedCalls("1000", "startTime");
        /// Call[] receivedCallsByCaller = cls.GetReceivedCalls("1000", "caller");
        /// Call[] receivedCallsByCallee = cls.GetReceivedCalls("1000", "callee");
        /// </code>
        /// 
        /// You can choose the provider from the web.config file
        /// <seealso cref="CallLogsProvider"/>
        /// <code>
        /// <section name="callLogsService" type="Wybecom.TalkPortal.Providers.CallLogsServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <callLogsService defaultProvider="TalkCallLogsProvider">
		///    <providers>
		///	    <add name="TalkCallLogsProvider" type="Wybecom.TalkPortal.Providers.TalkCallLogsProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
		///    </providers>
	    /// </callLogsService>
        /// </code>
        /// T.A.L.K. provides the "TalkCallLogsProvider" provider
        /// <seealso cref="TalkCallLogsProvider"/>
        /// If you want to implement your own provider, it must inherit Wybecom.TalkPortal.Providers.CallLogsProvider
        /// <seealso cref="CallLogsProvider"/>
        /// </example>
        /// <returns>
        /// An array of calls
        /// <seealso cref="Wybecom.TalkPortal.CTI.Proxy.CLS.Call"/>
        /// </returns>
        [WebMethod(MessageName = "GetReceivedCalls", EnableSession = false)]
        public Call[] GetReceivedCalls(string dn, string sort)
        {
            return CallLogsService.GetReceivedCalls(dn, sort);
        }

        [WebMethod(MessageName = "GetCodification", EnableSession = false)]
        public string[] GetCodification()
        {
            return CodificationService.GetCodif(true);
        }

        [WebMethod(MessageName = "CodifCall", EnableSession = false)]
        public int CodifCall(string callid, string extension, string codif)
        {
            return CodificationService.CodifCall(callid, extension, codif);
        }

        [WebMethod(MessageName = "GetCall", EnableSession = false)]
        public Call GetCall(string callid, string extension)
        {
            return CodificationService.GetCall(callid, extension);
        }
    }
}
