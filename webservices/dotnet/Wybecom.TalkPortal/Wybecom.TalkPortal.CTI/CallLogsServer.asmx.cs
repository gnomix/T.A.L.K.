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
using log4net;

namespace Wybecom.TalkPortal.CTI
{
    [WebService(Namespace = "http://wybecom.org/talkportal/cti/calllogsserver")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class CallLogsServer : System.Web.Services.WebService
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [WebMethod(MessageName = "GetMissedCalls", EnableSession = false)]
        public Call[] GetMissedCalls(string dn, string sort)
        {
            Call[] calls = null;
            try
            {
                if (Global.cacheMgr != null)
                {
                    if (Global.cacheMgr.Contains(dn))
                    {
                        LineControl lc = (LineControl)Global.cacheMgr.GetData(dn);
                        calls = lc.GetCalls(CallType.missed, sort);
                    }
                }
                else
                {
                    log.Debug("Cache Manager is null");
                }
                return calls;
            }
            catch (Exception e)
            {
                log.Error("Error while retreiving missed calls: " + e.Message);
                return calls;
            }
        }

        [WebMethod(MessageName = "GetPlacedCalls", EnableSession = false)]
        public Call[] GetPlacedCalls(string dn, string sort)
        {
            Call[] calls = null;
            try
            {
                if (Global.cacheMgr != null)
                {
                    if (Global.cacheMgr.Contains(dn))
                    {
                        LineControl lc = (LineControl)Global.cacheMgr.GetData(dn);
                        calls = lc.GetCalls(CallType.placed, sort);
                    }
                }
                else
                {
                    log.Debug("Cache Manager is null");
                }
                return calls;
            }
            catch (Exception e)
            {
                log.Error("Error while retreiving placed calls: " + e.Message);
                return calls;
            }
        }

        [WebMethod(MessageName = "GetReceivedCalls", EnableSession = false)]
        public Call[] GetReceivedCalls(string dn, string sort)
        {
            Call[] calls = null;
            try
            {
                if (Global.cacheMgr != null)
                {
                    if (Global.cacheMgr.Contains(dn))
                    {
                        LineControl lc = (LineControl)Global.cacheMgr.GetData(dn);
                        calls = lc.GetCalls(CallType.received, sort);
                    }
                }
                else
                {
                    log.Debug("Cache Manager is null");
                }
                return calls;
            }
            catch (Exception e)
            {
                log.Error("Error while retreiving received calls: " + e.Message);
                return calls;
            }
        }
    }
}
