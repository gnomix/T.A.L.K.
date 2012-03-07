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
using log4net;
using Wybecom.TalkPortal.Cisco.AXL.Proxy;
using System.Data;
using System.Xml;

namespace Wybecom.TalkPortal.DMD
{
    public class CiscoDataSource
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static AXLAPIService _axl;

        public static DataSet Search(DirectoryType dir)
        {
            DataSet results = new DataSet();
            DataTable dt = results.Tables.Add();
            CiscoDatasourceType ldt = (CiscoDatasourceType)dir.Item;
            try
            {
                Init(ldt.server, ldt.axluser, ldt.axluserpwd);
                ExecuteSQLQueryReq query = new ExecuteSQLQueryReq();
                query.sql = "select firstname, lastname, telephonenumber, department from enduser";
                ExecuteSQLQueryRes response = _axl.executeSQLQuery(query);
                dt.Columns.Add("firstname");
                dt.Columns.Add("lastname");
                dt.Columns.Add("telephonenumber");
                dt.Columns.Add("department");
                if (response != null && response.@return != null && response.@return.Length > 0)
                {

                    foreach (object o in response.@return)
                    {
                        List<object> values = new List<object>();
                        values.Add(((XmlNode[])o)[0].InnerText);
                        values.Add(((XmlNode[])o)[1].InnerText);
                        values.Add(((XmlNode[])o)[2].InnerText);
                        values.Add(((XmlNode[])o)[3].InnerText);
                        dt.Rows.Add(values.ToArray());
                    }
                }
                else
                {
                    log.Error("No result retreive from AXL Server");
                }
                return results;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return results;
            }
        }

        static void Init(string server, string user, string password)
        {
            _axl = new AXLAPIService(server, user, password);
        }
    }
}
