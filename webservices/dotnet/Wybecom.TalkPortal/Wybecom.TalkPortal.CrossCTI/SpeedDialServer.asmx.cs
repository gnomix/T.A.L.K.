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
    /// Not used
    /// </summary>
    [WebService(Namespace = "http://wybecom.org/talkportal/crosscti/speeddialserver")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.GenerateScriptType(typeof(SpeedDial))]
    public class SpeedDialServer : System.Web.Services.WebService
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        
        [WebMethod(MessageName = "GetSpeedDials", EnableSession = false)]
        public SpeedDial[] GetSpeedDials(string extension, string token)
        {
            log.Debug("Get SpeedDials from " + extension);
            SpeedDial[] sds = null;
            if (ValidateToken(token, extension))
            {
                sds = SpeedDialService.GetSpeedDials(extension);
            }
            return sds;
        }

        
        [WebMethod(MessageName = "AddSpeedDial", EnableSession = false)]
        public void AddSpeedDial(string extension, SpeedDial speeddial, string token)
        {
            log.Debug("Adding speeddial: " + speeddial.displayName + " from " + extension);
            if (ValidateToken(token, extension))
            {
                SpeedDialService.AddSpeedDial(extension, speeddial);
            }
        }

        
        [WebMethod(MessageName = "RemoveSpeedDial", EnableSession = false)]
        public void RemoveSpeedDial(string extension, SpeedDial speeddial, string token)
        {
            log.Debug("Removing speeddial: " + speeddial.displayName + " from " + extension);
            if (ValidateToken(token, extension))
            {
                SpeedDialService.RemoveSpeedDial(extension, speeddial);
            }
        }

        
        [WebMethod(MessageName = "EditSpeedDial", EnableSession = false)]
        public void EditSpeedDial(string extension, SpeedDial newspeeddial, SpeedDial exspeeddial, string token)
        {
            log.Debug("Edit speeddial: " + exspeeddial.displayName + " from " + extension);
            if (ValidateToken(token, extension))
            {
                SpeedDialService.EditSpeedDial(extension, newspeeddial,exspeeddial);
            }
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
    }
}
