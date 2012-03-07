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
using Wybecom.TalkPortal.CTI.ACD;
using Wybecom.TalkPortal.Providers;

namespace Wybecom.TalkPortal.CTI
{
    [WebService(Namespace = "http://wybecom.org/talkportal/cti/stateserver")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class StateServer : System.Web.Services.WebService
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [WebMethod(MessageName = "SetLineControl", EnableSession = false)]
        public bool SetLineControl(LineControl lc)
        {
            bool success = false;
            try
            {
                if (Global.cacheMgr != null)
                {
                    log.Debug("Adding or update linecontrol: " + lc.ToString());
                    if (Global.cacheMgr.Contains(lc.directoryNumber))
                    {
                        object linecontrol = Global.cacheMgr.GetData(lc.directoryNumber);
                        if (linecontrol is AgentLineControl)
                        {
                            AgentLineControl currentAgentLineControl = ((AgentLineControl)Global.cacheMgr.GetData(lc.directoryNumber));
                            currentAgentLineControl.doNotDisturb = lc.doNotDisturb;
                            currentAgentLineControl.forward = lc.forward;
                            currentAgentLineControl.lineControlConnection = lc.lineControlConnection;
                            currentAgentLineControl.mwiOn = lc.mwiOn;
                            currentAgentLineControl.status = lc.status;
                            currentAgentLineControl.monitored = lc.monitored;
                            Global.cacheMgr.Add(lc.directoryNumber, currentAgentLineControl);
                        }
                        else
                        {
                            LineControl currentLineControl = ((LineControl)Global.cacheMgr.GetData(lc.directoryNumber));
                            currentLineControl.doNotDisturb = lc.doNotDisturb;
                            currentLineControl.forward = lc.forward;
                            currentLineControl.lineControlConnection = lc.lineControlConnection;
                            currentLineControl.mwiOn = lc.mwiOn;
                            currentLineControl.status = lc.status;
                            currentLineControl.monitored = lc.monitored;
                            Global.cacheMgr.Add(lc.directoryNumber, currentLineControl);
                        }
                        
                    }
                    else
                    {
                        Global.cacheMgr.Add(lc.directoryNumber, lc);
                    }
                    success = true;
                }
                return success;
            }
            catch (Exception e)
            {
                log.Error("Error while setting linecontrol : " + e.Message);
                return success;
            }
        }

        [WebMethod(MessageName = "SetAgentLineControl", EnableSession = false)]
        public bool SetAgentLineControl(string extension, string agentid, AgentState state, CallCenterCall ccc)
        {
            bool success = false;
            try
            {
                if (Global.cacheMgr != null)
                {
                    log.Debug("Adding or update agentlinecontrol: " + extension + state.ToString());
                    if (Global.cacheMgr.Contains(extension) && Global.cacheMgr.GetData(extension) is AgentLineControl)
                    {
                        AgentLineControl currentAgentLineControl = ((AgentLineControl)Global.cacheMgr.GetData(extension));
                        currentAgentLineControl.agentstate = state;
                        currentAgentLineControl.callcentercall = ccc;
                        Global.cacheMgr.Add(extension, currentAgentLineControl);
                    }
                    else
                    {
                        AgentLineControl alc = new AgentLineControl();
                        LineControl lc = ((LineControl)Global.cacheMgr.GetData(extension));
                        alc.agentid = agentid;
                        alc.agentstate = state;
                        alc.callcentercall = ccc;
                        alc.directoryNumber = lc.directoryNumber;
                        alc.doNotDisturb = lc.doNotDisturb;
                        alc.forward = lc.forward;
                        alc.lineControlConnection = lc.lineControlConnection;
                        alc.mwiOn = lc.mwiOn;
                        alc.status = lc.status;
                        alc.monitored = lc.monitored;
                        Global.cacheMgr.Add(extension, alc);
                    }
                    success = true;
                }
                return success;
            }
            catch (Exception e)
            {
                log.Error("Error while setting agentlinecontrol : " + e.Message);
                return success;
            }
        }

        [WebMethod(MessageName = "AddCallLogs", EnableSession = false)]
        public bool AddCallLogs(string dn, Call call)
        {
            bool success = false;
            try
            {
                LineControl lc = null;
                if (Global.cacheMgr != null)
                {
                    if (Global.cacheMgr.Contains(dn))
                    {
                        lc = ((LineControl)Global.cacheMgr.GetData(dn));
                    }
                    else
                    {
                        lc = new LineControl();
                        lc.directoryNumber = dn;
                        lc.status = Status.unknown;
                        lc.doNotDisturb = false;
                        lc.forward = "";
                        lc.mwiOn = false;
                        lc.monitored = "";
                    }
                    if (call.endTime == null || call.endTime.Year == 1)
                    {
                        call.endTime = DateTime.Now;
                    }
                    log.Debug("Adding call to cache, " + dn + ": " + call.ToString());
                    lc.AddCall(call);
                    Global.cacheMgr.Add(dn, lc);
                    if (Global.codif != null)
                    {
                        
                        log.Debug("Adding call to the database...");
                        
                        CodificationService.AddCall(call, dn);
                    }

                    success = true;
                }
                return success;
            }
            catch (Exception e)
            {
                log.Error("Error while adding call log: " + e.Message + " inner: " + e.InnerException.ToString());
                return success;
            }
        }
    }
}
