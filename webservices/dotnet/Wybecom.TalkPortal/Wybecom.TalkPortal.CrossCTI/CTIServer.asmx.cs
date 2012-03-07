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
using log4net;
namespace Wybecom.TalkPortal.CrossCTI
{
    /// <summary>
    /// Third party call control web service for AJAX clients
    /// </summary>
    [WebService(Namespace = "http://wybecom.org/talkportal/crosscti/ctiserver")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    [System.Web.Script.Services.GenerateScriptType(typeof(AuthenticationExpiredException))]
    [System.Web.Script.Services.GenerateScriptType(typeof(AuthenticationMismatchException))]
    [System.Web.Script.Services.ScriptService]
    public class CTIServer : System.Web.Services.WebService
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Calls a destination. 
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="DirectoryNumberAnalysorService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="caller">The caller extension</param>
        /// <param name="callee">The destinationt</param>
        /// <param name="token">Authentication token</param>
        /// <example>
        /// To call 1001 from 1000
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.Call("1000", "1001", token);
        /// </code>
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession=false)]
        public CTIResponse Call(string caller, string callee, string token)
        {
            log.Debug("Call: " + caller + ", " + callee);
            string callid = "";
            if (ValidateToken(token, caller))
            {
                callee = DirectoryNumberAnalysorService.Analyse(callee);
                callid = CTIService.Call(caller, callee);
            }

            return new CTIResponse(AuthenticationService.UpdateToken(token),true,callid);
        }

        /// <summary>
        /// Takes a call. 
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="callee">The called extension</param>
        /// <param name="callid">The call id</param>
        /// <param name="token">Authentication token</param>
        /// <example>
		/// To unhook a the call 505050 from line 1000
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.UnHook("1000", "505050", token);
        /// </code>
        /// 
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession=false)]
        public CTIResponse UnHook(string callee, string callid, string token)
        {
            log.Debug("UnHook: " + callee + ", " + callid);
            bool success = false;
            if (ValidateToken(token, callee))
            {
                success = CTIService.UnHook(callee, callid);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Hangs up.
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="caller">The hanged up extension</param>
        /// <param name="callid">The call id</param>
        /// <param name="token">The authentication token</param>
        /// <example>
        /// To hang up the call 505050 from line 1000
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.HangUp("1000", "505050", "encryptedString");
        /// </code>
        /// 
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession=false)]
        public CTIResponse HangUp(string caller, string callid, string token)
        {
            log.Debug("HangUp: " + caller + ", " + callid);
            bool success = false;
            if (ValidateToken(token, caller))
            {
                success = CTIService.HangUp(caller, callid);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Unconditional forward.
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="caller">The extension to forward</param>
        /// <param name="destination">The forward destination</param>
        /// <param name="token">The authentication token</param>
        /// <example>
        /// To forward line 1000 to line 1001
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.Forward("1000", "1001", token);
        /// </code>
        /// 
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession=false)]
        public CTIResponse Forward(string caller, string destination, string token)
        {
            log.Debug("Forward: " + caller + ", " + destination);
            destination = DirectoryNumberAnalysorService.Analyse(destination);
            bool success = false;
            if (ValidateToken(token, caller))
            {
                success = CTIService.Forward(caller, destination);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Puts a call on hold.
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="caller">The called party that puts the call on hold</param>
        /// <param name="callid">The call id</param>
        /// <param name="token">The authentication token</param>
        /// <example>
        /// Put the call 505050 from the 1000 extension
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.Hold("1000", "505050", "encryptedString");
        /// </code>
        /// 
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession=false)]
        public CTIResponse Hold(string callid, string caller, string token)
        {
            log.Debug("Hold: " + caller + ", " + callid);
            bool success = false;
            if (ValidateToken(token, caller))
            {
                success = CTIService.Hold(callid, caller);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Unholds a call. 
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="caller">The caller extension</param>
        /// <param name="callid">The call id</param>
        /// <param name="token">The authentication token</param>
        /// <example>
        /// Unholds the call 505050 from the 1000 extension
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.UnHold("1000", "505050", "encryptedString");
        /// </code>
        /// 
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession = false)]
        public CTIResponse UnHold(string callid, string caller, string token)
        {
            log.Debug("UnHold: " + caller + ", " + callid);
            bool success = false;
            if (ValidateToken(token, caller))
            {
                success = CTIService.UnHold(callid, caller);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Enables or disables DND features.
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="caller">The caller extension</param>
        /// <param name="token">The authentication token</param>
        /// <example>
        /// To enable or disable DND from line 1000
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.DoNotDisturb("1000", "encryptedString");
        /// </code>
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession = false)]
        public CTIResponse DoNotDisturb(string caller, string token)
        {
            log.Debug("DoNotDisturb: " + caller);
            bool success = false;
            if (ValidateToken(token, caller))
            {
                success = CTIService.DoNotDisturb(caller);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Direct transfer. 
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="destination">Transfer destination</param>
        /// <param name="caller">The transferring party</param>
        /// <param name="callid">The call id to transfer</param>
        /// <param name="token">The authentication token</param>
        /// <example>
        /// To transfer the call 505050 from 1000 to 1001
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.Transfer("505050", "1000", "1001", "encryptedString");
        /// </code>
        /// 
        /// To "commit" a consult transfer
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// CTIResponse cr = cs.Transfer(null, "1000", null, "encryptedString"); 
        /// </code>
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession = false)]
        public CTIResponse Transfer(string callid, string caller, string destination, string token)
        {
            log.Debug("Transfer: " + caller + ", " + callid + ", " + destination);
            destination = DirectoryNumberAnalysorService.Analyse(destination);
            bool success = false;
            if (ValidateToken(token, caller))
            {
                if (callid != null && destination != null)
                {
                    log.Debug("Transfer requested");
                    success = CTIService.Transfer(callid, caller, destination);
                }
                else
                {
                    log.Debug("Transfer following consult transfer requested");
                    success = CTIService.Transfer(caller);
                }
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Consult transfer.
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="destination">Transfer destination</param>
        /// <param name="caller">The transferring party</param>
        /// <param name="callid">The call id</param>
        /// <param name="token">The authentication token</param>
        /// <example>
        /// To transfer the call 505050 from 1000 to 1001
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.ConsultTransfer("505050", "1000", "1001", "encryptedString");
        /// </code>
        /// 
        /// To ends the consult transfer.
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// CTIResponse cr = cs.Transfer(null, "1000", null, "encryptedString");  
        /// </code>
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession = false)]
        public CTIResponse ConsultTransfer(string callid, string caller, string destination, string token)
        {
            log.Debug("ConsultTransfer: " + caller + ", " + callid + ", " + destination);
            destination = DirectoryNumberAnalysorService.Analyse(destination);
            bool success = false;
            if (ValidateToken(token, caller))
            {
                success = CTIService.ConsultTransfer(callid, caller, destination);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Diverts a call.
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="caller">The called party</param>
        /// <param name="callid">The call id</param>
        /// <param name="token">The authentication token</param>
        /// <example>
        /// To divert the call 505050 from 1000
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.Divert("505050", "1000", "encryptedString");
        /// </code>
        /// 
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession = false)]
        public CTIResponse Divert(string callid, string caller, string token)
        {
            log.Debug("Divert: " + caller + ", " + callid);
            bool success = false;
            if (ValidateToken(token, caller))
            {
                success = CTIService.Divert(callid, caller);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Silent monitoring. 
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="monitorer">The monitor line</param>
        /// <param name="monitored">The monitored line</param>
        /// <param name="token">The authentication token</param>
        /// <example>
        /// Starts a monitor session from 1000 to 1001
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.Monitor("1000", "1001", "encryptedString");
        /// </code>
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession = false)]
        public CTIResponse Monitor(string monitorer, string monitored, string token)
        {
            log.Debug("Monitor: " + monitorer + ", " + monitored );
            bool success = false;
            if (ValidateToken(token, monitorer))
            {
                success = CTIService.Monitor(monitorer, monitored);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Logs an agent.
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="agentid">Agent login</param>
        /// <param name="pwd">Agent password</param>
        /// <param name="extension">Agent extension</param>
        /// <param name="token">The authentication yoken</param>
        /// <example>
        /// Logs toto (1000)
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.Login("toto", "password", "1000", "encryptedString");
        /// </code>
        /// 
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession = false)]
        public CTIResponse Login(string agentid, string pwd, string extension, string token)
        {
            log.Debug("Login agent: " + agentid + ", " + extension);
            bool success = false;
            if (ValidateToken(token, extension))
            {
                success = CTIService.Login(agentid, pwd, extension);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Logs off an agent.
        /// </summary>
        /// <seealso cref="CTIService"/>
        /// <seealso cref="AuthenticationService"/>
        /// <param name="agentid">Agent login</param>
        /// <param name="extension">Agent extension</param>
        /// <param name="token">The authentication token</param>
        /// <example>
        /// To log off toto (1000)
        /// <code>
        /// CTIServer cs = new CTIServer();
        /// string token = cs.Authenticate("1000", "toto", "password");
        /// CTIResponse cr = cs.Logoff("toto", "1000", "encryptedString");
        /// </code>
        /// 
        /// 
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// <code>
        /// <section name="ctiService" type="Wybecom.TalkPortal.Providers.CTIServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
		/// <section name="directoryNumberAnalysorService" type="Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <section name="authenticationService" type="Wybecom.TalkPortal.Providers.AuthenticationServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <ctiService defaultProvider="TalkCTIProvider">
        ///    <providers>
        ///        <add name="TalkCTIProvider" type="Wybecom.TalkPortal.Providers.TalkCTIProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </ctiService>
        /// <directoryNumberAnalysorService defaultProvider="TalkDirectoryNumberAnalysorProvider">
        ///    <providers>
        ///        <add name="TalkDirectoryNumberAnalysorProvider" type="Wybecom.TalkPortal.Providers.TalkDirectoryNumberAnalysorProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///    </providers>
        /// </directoryNumberAnalysorService>
        /// <authenticationService defaultProvider="TalkAuthenticationProvider">
		///     <providers>
		///     	<add name="TalkAuthenticationProvider" type="Wybecom.TalkPortal.Providers.CiscoAuthenticationProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" tokenExpiration="10"/>
		///     </providers>
	    /// </authenticationService>
        /// </code>
        /// T.A.L.K. providers several providers:
        /// - "TalkCTIProvider" for JTAPI environment
        /// - "TAPICTIProvider" for TAPI environment
        /// - "TalkDirectoryNumberAnalysorProvider"
        /// - "TalkAuthenticationProvider" delivering a trivial authentication mecanism
        /// - "CiscoAuthenticationProvider" to use the Cisco the cisco authentication mecanism
        /// <seealso cref="TalkCTIProvider"/>
        /// <seealso cref="TalkDirectoryNumberAnalysorProvider"/>
        /// <seealso cref="TalkAuthenticationProvider"/>
        /// <seealso cref="CiscoAuthenticationProvider"/>
        /// If you want to implement your own provider, it must inherits : 
        ///  - Wybecom.TalkPortal.Providers.CallLogsProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.DirectoryNumberAnalysorProvider
        ///  or
        ///  - Wybecom.TalkPortal.Providers.AuthenticationProvider
        ///  depending on the role needed
        /// <seealso cref="CTIProvider"/>
        /// <seealso cref="DirectoryNumberAnalysorProvider"/>
        /// <seealso cref="AuthenticationProvider"/>
        /// </example>
        /// <returns>
        /// A CTIResponse containing the result of the operation and a new token
        /// <seealso cref="CTIResponse"/>
        /// </returns>
        /// <exception cref="AuthenticationFailedException">The user or password is incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user can't request this extension</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(EnableSession = false)]
        public CTIResponse Logoff(string agentid, string extension, string token)
        {
            log.Debug("Logoff agent: " + agentid );
            bool success = false;
            if (ValidateToken(token, extension))
            {
                success = CTIService.Logoff(agentid);
            }
            return new CTIResponse(AuthenticationService.UpdateToken(token), success);
        }

        /// <summary>
        /// Authenticate an AJAX client
        /// </summary>
        /// <param name="dn">The user extension</param>
        /// <param name="user">the user login</param>
        /// <param name="password">The user password</param>
        /// <returns>
        /// An authentication token
        /// </returns>
        /// <exception cref="AuthenticationFailedException">Bad user name or bad password</exception>
        /// <exception cref="AuthenticationMismatchException">This user can't request this line</exception>
        [WebMethod(EnableSession = false)]
        public string Authenticate(string dn, string user, string password)
        {
            string token = "";
            if (password != null && password != "")
            {
                token = AuthenticationService.Authenticate(dn, user, password);
            }
            else
            {
                token = AuthenticationService.Authenticate(dn, user);
            }
            return token;
        }

        private bool ValidateToken(string token, string dn)
        {
            bool isValid = false;
            log.Debug("Validate token: " + token);
            try
            {
                isValid = AuthenticationService.Validate(token, dn);
            }
            catch (AuthenticationExpiredException aee)
            {
                log.Error("Token is invalid: token as expired");
                throw aee;
            }
            catch (AuthenticationMismatchException ame)
            {
                log.Error("Token is invalid: token does not match dn");
                throw ame;
            }
            log.Debug("Token is valid: " + isValid.ToString());
            return isValid;
        }

        /// <summary>
        /// Message containing the result of an operation and a new authentication token
        /// </summary>
        public class CTIResponse
        {
            public string token;
            public bool success;
            public string message;

            public CTIResponse()
            {
                token = "";
                success = false;
                message = "";
            }

            public CTIResponse(string tok)
            {
                token = tok;
                success = false;
                message = "";
            }

            public CTIResponse(string tok, bool suc)
            {
                token = tok;
                success = suc;
                message = "";
            }

            public CTIResponse(string tok, bool suc, string mes)
            {
                token = tok;
                success = suc;
                message = mes;
            }
        }
    }
}
