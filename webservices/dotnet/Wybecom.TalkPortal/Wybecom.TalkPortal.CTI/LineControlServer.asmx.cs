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
using System.Web.Configuration;
using System.Threading;
using Wybecom.TalkPortal.Providers;
using Wybecom.TalkPortal.CTI.ACD;
using log4net;

namespace Wybecom.TalkPortal.CTI
{
    [WebService(Namespace = "http://wybecom.org/talkportal/cti/linecontrolserver")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class LineControlServer : System.Web.Services.WebService
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate LineControl LengthyGetLineControlAsyncStub(LineControl lc);

        public delegate LineStatus[] LengthyGetLinesStatusAsyncStub(LineStatus[] lines);

        public LineControl LengthyGetLineControl(LineControl lc)
        {
            LineControl lControl = lc;
            log.Debug("Received GetLineControl: " + lc.ToString());
            if (Global.cacheMgr != null)
            {
                if (Global.cacheMgr.Contains(lc.directoryNumber))
                {
                    log.Debug("This line is in cache: " + lc.directoryNumber);
                    lControl = (LineControl)Global.cacheMgr.GetData(lc.directoryNumber);
                    int compteur = 0;
                    while (lControl.Equals(lc) && compteur <= int.Parse(WebConfigurationManager.AppSettings.Get("LineControlServerTimeOut")))
                    {
                        Thread.Sleep(int.Parse(WebConfigurationManager.AppSettings.Get("LineControlServerSleepTime")));
                        lControl = (LineControl)Global.cacheMgr.GetData(lc.directoryNumber);
                        compteur++;
                    }
                }
            }
            else
            {
                log.Debug("Cache Manager is null");
            }
            log.Debug("Return LineControl: " + lControl.ToString());
            return lControl;
        }

        public LineStatus[] LengthyGetLinesStatus(LineStatus[] lines)
        {
            LineStatus[] currentLines = new LineStatus[lines.Length];
            try
            {
                if (Global.cacheMgr != null)
                {
                    GetCurrentLines(lines, ref currentLines);
                    int compteur = 0;
                    while (Compare(currentLines, lines) && compteur <= int.Parse(WebConfigurationManager.AppSettings.Get("LineControlServerTimeOut")))
                    {
                        Thread.Sleep(int.Parse(WebConfigurationManager.AppSettings.Get("LineControlServerSleepTime")));
                        GetCurrentLines(lines, ref currentLines);
                        compteur++;
                    }
                }
                else
                {
                    log.Debug("Cache Manager is null");
                }
                return currentLines;
            }
            catch (Exception e)
            {
                log.Error("Error while getting lines status: " + e.Message);
                return currentLines;
            }
        }

        public class GetLineControlState
        {
            public object previousstate;
            public LengthyGetLineControlAsyncStub stub;
        }
        public class GetLinesStatusState
        {
            public object previousstate;
            public LengthyGetLinesStatusAsyncStub stub;
        }

        [WebMethod(MessageName = "GetLineControl", EnableSession = false)]
        public IAsyncResult BeginGetLineControl(LineControl lc, AsyncCallback cb, object s)
        {
            LengthyGetLineControlAsyncStub stub = new LengthyGetLineControlAsyncStub(LengthyGetLineControl);
            GetLineControlState glcs = new GetLineControlState();
            glcs.previousstate = s;
            glcs.stub = stub;
            return stub.BeginInvoke(lc, cb, glcs);
        }

        [WebMethod(MessageName = "GetLineControl", EnableSession = false)]
        public LineControl EndGetLineControl(IAsyncResult call)
        {
            GetLineControlState glcs = (GetLineControlState)call.AsyncState;
            return glcs.stub.EndInvoke(call);
        }

        [WebMethod(MessageName = "GetLinesStatus", EnableSession = false)]
        public IAsyncResult BeginGetLinesStatus(LineStatus[] lines, AsyncCallback cb, object s)
        {
            LengthyGetLinesStatusAsyncStub stub = new LengthyGetLinesStatusAsyncStub(LengthyGetLinesStatus);
            GetLinesStatusState glss = new GetLinesStatusState();
            glss.previousstate = s;
            glss.stub = stub;
            return stub.BeginInvoke(lines, cb, glss);
        }

        [WebMethod(MessageName = "GetLinesStatus", EnableSession = false)]
        public LineStatus[] EndGetLinesStatus(IAsyncResult call)
        {
            GetLinesStatusState glss = (GetLinesStatusState)call.AsyncState;
            return glss.stub.EndInvoke(call);
        }

        [WebMethod(MessageName = "GetAgentLineControl", EnableSession = false)]
        public AgentLineControl GetAgentLineControl(AgentLineControl ag)
        {
            AgentLineControl lControl = ag;
            log.Debug("Received GetAgentLineControl: " + ag.ToString());
            if (Global.cacheMgr != null)
            {
                if (Global.cacheMgr.Contains(ag.directoryNumber))
                {
                    log.Debug("This agent is in cache: " + ag.directoryNumber);
                    lControl = (AgentLineControl)Global.cacheMgr.GetData(ag.directoryNumber);
                    int compteur = 0;
                    while (lControl.Equals(ag) && compteur <= int.Parse(WebConfigurationManager.AppSettings.Get("LineControlServerTimeOut")))
                    {
                        Thread.Sleep(int.Parse(WebConfigurationManager.AppSettings.Get("LineControlServerSleepTime")));
                        lControl = (AgentLineControl)Global.cacheMgr.GetData(ag.directoryNumber);
                        compteur++;
                    }
                }
            }
            else
            {
                log.Debug("Cache Manager is null");
            }
            log.Debug("Return AgentLineControl: " + lControl.ToString());
            return lControl;
        }


        private void GetCurrentLines(LineStatus[] reference, ref LineStatus[] currentLines)
        {
            int currentLineCompteur = 0;
            foreach (LineStatus ls in reference)
            {
                LineStatus newLine = new LineStatus();
                LineStatus cacheLine = null;
                if (ls.directoryNumber != "" && Global.cacheMgr.Contains(ls.directoryNumber))
                {
                    cacheLine = Global.cacheMgr.GetData(ls.directoryNumber) as LineStatus;
                }
                newLine.directoryNumber = ls.directoryNumber;
                if (cacheLine == null)
                {
                    newLine.status = Status.unknown;
                    newLine.doNotDisturb = false;
                    newLine.forward = "";
                    newLine.mwiOn = false;
                    newLine.monitored = "";
                }
                else
                {
                    if (PrivacyService.IsPrivate(cacheLine.directoryNumber))
                    {
                        newLine.status = Status.hidden;
                    }
                    else
                    {
                        newLine.status = cacheLine.status;
                    }
                    newLine.doNotDisturb = cacheLine.doNotDisturb;
                    newLine.forward = cacheLine.forward;
                    newLine.mwiOn = cacheLine.mwiOn;
                    newLine.monitored = cacheLine.monitored;
                }
                currentLines[currentLineCompteur] = newLine;
                currentLineCompteur++;
            }
        }

        private bool Compare(LineStatus[] currentLine, LineStatus[] cacheLine)
        {
            bool result = true;
            for (int i = 0; i < currentLine.Length; i++)
            {
                if (!currentLine[i].Equals(cacheLine[i]))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
