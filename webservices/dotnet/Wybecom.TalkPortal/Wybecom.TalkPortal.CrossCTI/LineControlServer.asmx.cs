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
using Wybecom.TalkPortal.CTI.Proxy.LCS;
using log4net;

namespace Wybecom.TalkPortal.CrossCTI
{
    /// <summary>
    /// The presence informers for AJAX clients
    /// </summary>
    [WebService(Namespace = "http://wybecom.org/talkportal/crosscti/linecontrolserver")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.GenerateScriptType(typeof(LineStatus))]
    [System.Web.Script.Services.GenerateScriptType(typeof(LineControl))]
    [System.Web.Script.Services.GenerateScriptType(typeof(LineControlResponse))]
    [System.Web.Script.Services.GenerateScriptType(typeof(Status))]
    [System.Web.Script.Services.GenerateScriptType(typeof(LineControlConnection))]
    [System.Web.Script.Services.GenerateScriptType(typeof(ConnectionState))]
    [System.Web.Script.Services.GenerateScriptType(typeof(TerminalState))]
    [System.Web.Script.Services.GenerateScriptType(typeof(Call))]
    public class LineControlServer : System.Web.Services.WebService
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Retreives line & presence informations. 
        /// </summary>
        /// <seealso cref="LineControlService"/>
        /// <param name="lc">
        /// Describe the status of a line
        /// <seealso cref="LineControl"/>
        /// </param>
        /// <param name="token">Authentication token</param>
        /// <example>
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="LineControlProvider"/>
        /// <code>
        /// <section name="lineControlService" type="Wybecom.TalkPortal.Providers.LineControlServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <lineControlService defaultProvider="TalkLineControlProvider">
		///     <providers>
		///     	<add name="TalkLineControlProvider" type="Wybecom.TalkPortal.Providers.TalkLineControlProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
		///     </providers>
	    /// </lineControlService>
        /// </code>
        /// T.A.L.K. provides Wybecom.TalkPortal.Providers.TalkLineControlProvider
        /// <seealso cref="TalkLineControlProvider"/>
        /// If you want to implement your own provider, it must inherits from Wybecom.TalkPortal.Providers.LineControlProvider
        /// <seealso cref="LineControlProvider"/>
        /// </example>
        /// <returns>
        /// The status & presence informations of a line and the authentication token
        /// </returns>
        /// <exception cref="AuthenticationFailedException">User or password are incorrect</exception>
        /// <exception cref="AuthenticationMismatchException">The user is not associated with the line requested</exception>
        /// <exception cref="AuthenticationExpiredException">The token has expired</exception>
        [WebMethod(MessageName = "GetLineControl", EnableSession = false)]
        public LineControlResponse GetLineControl(LineControl lc, string token)
        {
            log.Debug("GetLineControl: " + lc.ToString());
            LineControl newlc = null;
            if (ValidateToken(token, lc.directoryNumber))
            {
                log.Debug("Token is valid...Retreive line control ("+lc.directoryNumber+") from LineControlService");
                try
                {
                    newlc = LineControlService.GetLineControl(lc);
                }
                catch (Exception e)
                {
                    log.Error("Unable to retreive lineControl from LineControlService: " + e.Message);
                }
            }
            return new LineControlResponse(newlc, AuthenticationService.UpdateToken(token));
        }

        /// <summary>
        /// Retreives line & presence informations from several lines
        /// </summary>
        /// <param name="lines">
        /// An array of LineStatus object
        /// <seealso cref="LineStatus"/>
        /// </param>
        /// 
        /// <example>
        /// You can choose the provider according to your environment in the web.config file
        /// <seealso cref="LineControlProvider"/>
        /// <code>
        /// <section name="lineControlService" type="Wybecom.TalkPortal.Providers.LineControlServiceSection, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca" requirePermission="false" restartOnExternalChanges="true"/>
        /// <lineControlService defaultProvider="TalkLineControlProvider">
        ///     <providers>
        ///     	<add name="TalkLineControlProvider" type="Wybecom.TalkPortal.Providers.TalkLineControlProvider, Wybecom.TalkPortal.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6d3aec9c1101eeca"/>
        ///     </providers>
        /// </lineControlService>
        /// </code>
        /// T.A.L.K. provides Wybecom.TalkPortal.Providers.TalkLineControlProvider
        /// <seealso cref="TalkLineControlProvider"/>
        /// If you want to implement your own provider, it must inherits from Wybecom.TalkPortal.Providers.LineControlProvider
        /// <seealso cref="LineControlProvider"/>
        /// </example>
        /// <returns>
        /// An array of LineStatus
        /// <seealso cref="LineStatus"/>
        /// </returns>
        [WebMethod(MessageName = "GetLinesStatus", EnableSession = false)]
        public LineStatus[] GetLinesStatus(LineStatus[] lines)
        {
            log.Debug("GetLinesStatus...");
            return LineControlService.GetLinesStatus(lines);
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
        /// Message containing status & presence information and an authentication token
        /// </summary>
        public class LineControlResponse
        {
            public LineControl linecontrol;
            public string token;

            public LineControlResponse(LineControl lc, string tok)
            {
                linecontrol = lc;
                token = tok;
            }

            public LineControlResponse()
            {
                linecontrol = null;
                token = null;
            }
        }
    }
}
