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
using System.Text;
using System.Web.Configuration;
using System.Configuration.Provider;
using Wybecom.TalkPortal.Cisco.AXL.Proxy;
using System.Xml;
using log4net;

namespace Wybecom.TalkPortal.Providers
{
    public class CiscoSpeedDialProvider : SpeedDialProvider
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _applicationName;
        private string defaultContext;
        private AXLAPIService _aas;

        public CiscoSpeedDialProvider()
        {
            _aas = new AXLAPIService();
            defaultContext = WebConfigurationManager.AppSettings.Get("DefaultLineContext");
        }

        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        public override SpeedDial[] GetSpeedDials(string extension)
        {
            log.Debug("GetSpeedDial from " + extension);
            List<SpeedDial> speeddials = new List<SpeedDial>();
            string sql = "select label, speeddialindex, speeddialnumber from speeddial where fkdevice in (select  pkid from device d where pkid in (select fkdevice from devicenumplanmap where fknumplan in (select pkid from numplan where dnorpattern = '" + extension + "'";
            if (defaultContext != "")
            {
                sql += " and fkroutepartition in (select pkid from routepartition where name = '"+defaultContext+"')";
            }
            sql += ")))";
            ExecuteSQLQueryReq esqr = new ExecuteSQLQueryReq();
            esqr.sql = sql;
            ExecuteSQLQueryRes esqres = _aas.executeSQLQuery(esqr);
            if (esqres.@return != null)
            {
                if (esqres.@return.Length > 0)
                {
                    foreach (XmlNode[] nodes in esqres.@return)
                    {
                        SpeedDial sd = new SpeedDial();
                        sd.displayName = nodes[0].InnerText;
                        sd.directoryNumber = nodes[2].InnerText;
                        sd.position = Convert.ToInt32(nodes[1].InnerText);
                        speeddials.Add(sd);
                    }
                }
                else
                {
                    log.Debug("No speeddial available for " + extension);
                }
            }
            return speeddials.ToArray();
        }

        public override void AddSpeedDial(string extension, SpeedDial speeddial)
        {
            log.Debug("Adding speedial " + speeddial.displayName + " from " + extension);
            string sql = "insert into speeddial (label, labelascii, speeddialindex, speeddialnumber,fkdevice) select '" + speeddial.displayName + "','" + speeddial.displayName + "', ";
            sql += "(select max(speeddialindex) from speeddial where fkdevice in (select fkdevice from devicenumplanmap where fknumplan in (select pkid from numplan where dnorpattern = '"+extension+"')))";
            sql += ", '" + speeddial.directoryNumber + "', pkid from device where pkid in (select fkdevice from devicenumplanmap where fknumplan in (select pkid from numplan where dnorpattern = '" + extension + "'";
            if (defaultContext != "")
            {
                sql += " and fkroutepartition in (select pkid from routepartition where name = '" + defaultContext + "')";
            }
            sql += ")))";
            ExecuteSQLUpdateReq esur = new ExecuteSQLUpdateReq();
            esur.sql = sql;
            log.Debug(_aas.executeSQLUpdate(esur).@return.rowsUpdated + " speeddial(s) added from " + extension);
        }

        public override void RemoveSpeedDial(string extension, SpeedDial speeddial)
        {
            log.Debug("Removing speeddial " + speeddial.displayName + " from " + extension);
            string sql = "delete from speeddial where fkdevice in (select  pkid from device d where pkid in (select fkdevice from devicenumplanmap where fknumplan in (select pkid from numplan where dnorpattern = '" + extension + "'";
            if (defaultContext != "")
            {
                sql += " and fkroutepartition in (select pkid from routepartition where name = '" + defaultContext + "')";
            }
            sql += "))) and label = '" + speeddial.displayName + "' and speeddialindex = " + speeddial.position + " and speeddialnumber = '" + speeddial.directoryNumber + "'";
            ExecuteSQLUpdateReq esur = new ExecuteSQLUpdateReq();
            esur.sql = sql;
            log.Debug(_aas.executeSQLUpdate(esur).@return.rowsUpdated + " speeddial(s) removed from " + extension); 
        }

        public override void EditSpeedDial(string extension, SpeedDial newspeeddial, SpeedDial exspeeddial)
        {
            log.Debug("Editing speeddial " + exspeeddial.displayName + " from " + extension);
            string sql = "update speeddial set label = '" + newspeeddial.displayName + "', labelascii = '" + newspeeddial.displayName + "', speeddialindex = " + newspeeddial.position + ", speeddialnumber = '" + newspeeddial.directoryNumber + "' where fkdevice in (select  pkid from device d where pkid in (select fkdevice from devicenumplanmap where fknumplan in (select pkid from numplan where dnorpattern = '" + extension + "'";
            if (defaultContext != "")
            {
                sql += " and fkroutepartition in (select pkid from routepartition where name = '" + defaultContext + "')";
            }
            sql += "))) and label = '" + exspeeddial.displayName + "' and speeddialindex = " + exspeeddial.position + " and speeddialnumber = '" + exspeeddial.directoryNumber + "'";
            ExecuteSQLUpdateReq esur = new ExecuteSQLUpdateReq();
            esur.sql = sql;
            log.Debug(_aas.executeSQLUpdate(esur).@return.rowsUpdated + " speeddial(s) updated from " + extension); 
        }
    }
}
