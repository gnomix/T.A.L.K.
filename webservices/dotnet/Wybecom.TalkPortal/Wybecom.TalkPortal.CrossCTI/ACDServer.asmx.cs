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
using Wybecom.TalkPortal.CTI.ACD;

using log4net;

namespace Wybecom.TalkPortal.CrossCTI
{
    /// <summary>
    /// 
    /// </summary>
    [WebService(Namespace = "http://wybecom.org/talkportal/crosscti/ctiserver")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ACDServer : System.Web.Services.WebService
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [WebMethod(EnableSession=false)]
        public ACDResponse Login(string agentId, string pwd, string extension)
        {
            string token = "";
            token = ACDService.AgentLogin(agentId, extension, pwd);
            return new ACDResponse(token, true);
        }

        [WebMethod(EnableSession = false)]
        public ACDResponse Logoff(string token, string extension)
        {
            
            bool success = false;
            if (ValidateACDToken(token, extension))
            {
                log.Debug("Logoff agent: " + ACDService.GetAgentIdFromToken(token));
                success = ACDService.AgentLogoff(ACDService.GetAgentIdFromToken(token), ACDService.GetExtensionFromToken(token), ACDService.GetPwdFromToken(token));
            }
            return new ACDResponse(ACDService.UpdateToken(token), success);
        }

        [WebMethod(EnableSession = false)]
        public ACDResponse SetAgentState(string token, string extension, ushort reasoncode, ushort state)
        {

            bool success = false;
            if (ValidateACDToken(token, extension))
            {
                log.Debug("Set agent state: " + ACDService.GetAgentIdFromToken(token));
                success = ACDService.ChangeAgentState(ACDService.GetAgentIdFromToken(token), ACDService.GetExtensionFromToken(token), ACDService.GetPwdFromToken(token), reasoncode, state);
            }
            return new ACDResponse(ACDService.UpdateToken(token), success);
        }

        [WebMethod(EnableSession = false)]
        public AgentStatistics GetAgentStatistics(string token, string extension)
        {
            AgentStatistics ast = null;
            if (ValidateACDToken(token, extension))
            {
                log.Debug("Get agent statistics: " + ACDService.GetAgentIdFromToken(token));
                ast = ACDService.GetAgentStatistics(ACDService.GetAgentIdFromToken(token));
            }
            return ast;
        }

        [WebMethod(EnableSession = false)]
        public QueueStatistics[] GetAgentQueueStatistics(string token, string extension)
        {
            QueueStatistics[] ast = null;
            if (ValidateACDToken(token, extension))
            {
                log.Debug("Get agent statistics: " + ACDService.GetAgentIdFromToken(token));
                ast = ACDService.GetQueueStatistics(ACDService.GetAgentIdFromToken(token));
            }
            return ast;
        }

        [WebMethod(EnableSession = false)]
        public ReasonCode[] GetLogoffReasonCodes()
        {
            return ACDService.GetLogoffReasonCodes();
        }

        [WebMethod(EnableSession = false)]
        public ReasonCode[] GetNotReadyReasonCodes()
        {
            return ACDService.GetNotReadyReasonCodes();
        }

        private bool ValidateACDToken(string token, string extension)
        {
            bool isValid = false;
            log.Debug("Validate acd token: " + token);
            try
            {
                isValid = ACDService.Validate(token, extension);
            }
            catch (AuthenticationExpiredException aee)
            {
                log.Error("ACD Token is invalid: token as expired");
                throw aee;
            }
            catch (AuthenticationMismatchException ame)
            {
                log.Error("ACD Token is invalid: token does not match dn");
                throw ame;
            }
            log.Debug("ACD Token is valid: " + isValid.ToString());
            return isValid;
        }

        public class ACDResponse
        {
            public string token;
            public bool success;

            public ACDResponse()
            {
                token = "";
                success = false;
            }

            public ACDResponse(string tok)
            {
                token = tok;
                success = false;
            }

            public ACDResponse(string tok, bool suc)
            {
                token = tok;
                success = suc;
            }
        }
    }
}
