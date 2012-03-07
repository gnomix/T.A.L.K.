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
using System.Data;
using System.Text;
using System.Data.Odbc;
using log4net;

namespace Wybecom.TalkPortal.DMD
{
    /// <summary>
    /// SQL browser
    /// </summary>
    /// <seealso cref="DirectoryType"/>
    public class SQL
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Search method
        /// </summary>
        /// <param name="directory">Search settings<seealso cref="DirectoryType"/></param>
        /// <returns>A dataset conform to the FieldFormatters specified<seealso cref="FieldFormatter"/></returns>
        public static DataSet Search(DirectoryType directory)
        {
            DataSet results = new DataSet();
            DataTable dt = results.Tables.Add();
            OdbcConnection odbc = new OdbcConnection();
            OdbcDataReader reader = null;
            OdbcCommand command = null;
            try
            {
                string dsn = "DSN=";
                dsn += ((SqlDatasourceType)directory.Item).dsn;
                dsn += ";Uid=";
                dsn += ((SqlDatasourceType)directory.Item).uid;
                dsn += ";Pwd=";
                dsn += ((SqlDatasourceType)directory.Item).pwd;
                odbc.ConnectionString = dsn;
                log.Debug("Opening ODBC connection...");
                odbc.Open();

                string sql = ((SqlDatasourceType)directory.Item).command + " " + ((SqlDatasourceType)directory.Item).sqlFilter;
                log.Debug("Initializing ODBC command: " + sql);
                command = odbc.CreateCommand();
                command.CommandText = sql;

                log.Debug("Loading data in memory...");
                reader = command.ExecuteReader();

                DataTable schema = reader.GetSchemaTable();
                foreach (DataRow dr in schema.Rows)
                {
                    dt.Columns.Add((string)dr[0], System.Type.GetType( ((Type)dr[5]).FullName));
                }
                object[] values = new object[dt.Columns.Count];
                while (reader.Read())
                {
                    reader.GetValues(values);
                    dt.Rows.Add(values);
                }

                return results;
            }
            catch (Exception e)
            {
                throw new Exception("Request failed!", e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (odbc != null)
                {
                    odbc.Close();
                    odbc.Dispose();
                }
            }
        }
    }
}
