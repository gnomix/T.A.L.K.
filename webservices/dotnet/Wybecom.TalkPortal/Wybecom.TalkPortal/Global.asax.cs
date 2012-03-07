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
using System.Web.Security;
using System.Web.SessionState;
using Wybecom.TalkPortal.DMD;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;
using System.Data;

namespace Wybecom.TalkPortal
{
    public class Global : System.Web.HttpApplication
    {
        public static List<DirectoryType> directoryConfiguration;
        private static string configFile = "dmd.config";
        private static string currentDirectoryName = "";
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            InitConfiguration();
            InitDirectoryCache();
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
        }

        void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            log.Debug("Loading assembly: " + args.LoadedAssembly.FullName);
        }

        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            log.Debug("Assembly not found: " + args.Name);
            return null;
        }

        

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private void InitConfiguration()
        {
            string datasourceConfigFile = HttpRuntime.AppDomainAppPath + "\\" + configFile;
            Stream s = null;
            try
            {
                if (File.Exists(datasourceConfigFile))
                {
                    log.Debug("Opening DMD config file...");
                    s = File.Open(datasourceConfigFile, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    directoryConfiguration = (List<DirectoryType>)bf.Deserialize(s);
                    log.Debug("DMD config file loaded");
                }
                else
                {
                    log.Debug("No DMD config file found...");
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                if (s != null)
                {
                    s.Flush();
                    s.Close();
                    s.Dispose();
                }
            }
        }

        private static void InitDirectoryCache()
        {
            if (directoryConfiguration != null)
            {
                
                    foreach (DirectoryType dt in directoryConfiguration)
                    {
                        log.Debug("DirectoryType: " + dt.ToString()); 
                        try
                        {
                            DataSet ds = null;
                            FieldFormatter[] ffs = null;
                            if (dt.Item is SqlDatasourceType)
                            {
                                ds = SQL.Search(dt);
                                
                                ffs = ((SqlDatasourceType)dt.Item).fieldFormatters;
                            }
                            else if (dt.Item is LdapDatasourceType)
                            {
                                ds = LDAP.Search(dt);
                                ffs = ((LdapDatasourceType)dt.Item).fieldFormatters;
                            }
                            else if (dt.Item is CiscoDatasourceType)
                            {
                                ds = CiscoDataSource.Search(dt);
                                ffs = ((CiscoDatasourceType)dt.Item).fieldFormatters;
                            }
                            //HttpRuntime.Cache.Insert(dt.name, ds, null, DateTime.Now.AddHours(10), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, OnCacheSuppress);
                            HttpRuntime.Cache.Insert(dt.name, ds, null, DateTime.Now.AddMinutes(Double.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("DMDRefreshTimer"))), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, OnCacheSuppress);
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                log.Debug("Result dataset contains " + ds.Tables.Count.ToString() + " table: " + ds.Tables[0].TableName);
                                DataTable calcTable = ds.Tables[0];

                                List<string> cols = new List<string>();

                                foreach (FieldFormatter ff in ffs)
                                {
                                    cols.Add(ff.fieldName);
                                    DataColumn dc = new DataColumn();
                                    dc.DataType = typeof(string);
                                    dc.ColumnName = ff.fieldName;
                                    dc.Expression = ff.value;
                                    calcTable.Columns.Add(dc);
                                }
                                DataView sortedView = calcTable.AsDataView();
                                if (cols.Count > 0)
                                {
                                    sortedView.Sort = cols[0];
                                }
                                calcTable = sortedView.ToTable("Results", false, cols.ToArray());
                                //HttpRuntime.Cache.Insert(dt.name + "_formated", calcTable, null, DateTime.Now.AddHours(10), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, OnCacheSuppress);
                                HttpRuntime.Cache.Insert(dt.name + "_formated", calcTable, null, DateTime.Now.AddMinutes(Double.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("DMDRefreshTimer"))), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, OnCacheSuppress);
                            }
                        }
                        catch (Exception e)
                        {
                            log.Error("Unable to search the directory " + dt.name + ": " + e.Message );
                        }
                        
                    }
            }
            else
            {
                log.Error("Configuration file is empty or does not exist");
            }
        }

        public static DirectoryType GetDirectory(string directoryName)
        {
            DirectoryType dir = null;
            try
            {
                if (directoryConfiguration != null)
                {
                    currentDirectoryName = directoryName;
                    dir = directoryConfiguration.Find(FindDirectoryFromName);
                }
                else
                {
                    log.Debug("DMD config file empty or inexistant");
                }
                return dir;
            }
            catch (Exception e)
            {
                log.Error("GetDirectory failed: " + e.Message);
                return dir;
            }
        }

        private static bool FindDirectoryFromName(DirectoryType dir)
        {
            bool found = false;
            if (currentDirectoryName != "")
            {
                if (dir.name == currentDirectoryName)
                {
                    found = true;
                }
            }
            else
            {
                log.Debug("Looking for a directory with an empty name field is not allowed");
            }
            return found;
        }

        public static DirectoryType[] GetDirectoriesList()
        {
            if (directoryConfiguration != null)
            {
                return directoryConfiguration.ToArray();
            }
            else
            {
                return null;
            }
        }

        public static void AddDirectory(DirectoryType dir)
        {
            try
            {
                if (directoryConfiguration != null)
                {
                    bool canSave = true;
                    foreach (DirectoryType dt in directoryConfiguration)
                    {
                        if (dt.name == dir.name)
                        {
                            canSave = false;
                            break;
                        }
                    }
                    if (canSave)
                    {
                        directoryConfiguration.Add(dir);
                    }
                }
                else
                {
                    directoryConfiguration = new List<DirectoryType>();
                    directoryConfiguration.Add(dir);
                }
                SaveConfigFile();
            }
            catch (Exception e)
            {
                log.Error("Cannot add directory...: " + e.Message);
            }
        }

        public static void UpdateDirectory(DirectoryType dir)
        {
            try
            {
                if (directoryConfiguration != null)
                {
                    foreach (DirectoryType dt in directoryConfiguration)
                    {
                        if (dt.name == dir.name)
                        {
                            directoryConfiguration.Remove(dt);
                            directoryConfiguration.Add(dir);
                            break;
                        }
                    }
                    
                }
                else
                {
                    log.Error("Cannot update a directory in an empty configuration file...");
                }
                SaveConfigFile();
            }
            catch (Exception e)
            {
                log.Error("Cannot update directory...: " + e.Message);
            }
        }

        private static void SaveConfigFile()
        {
            string datasourceConfigFile = HttpRuntime.AppDomainAppPath + "\\" + configFile;
            Stream s = null;
            try
            {
                log.Debug("Creating new config file...");
                s = File.Open(datasourceConfigFile, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                log.Debug("Serializing configuration...");
                bf.Serialize(s, directoryConfiguration);
                InitDirectoryCache();
            }
            catch (Exception e)
            {
                log.Error("Cannot write configuration...: " + e.Message);
            }
            finally
            {
                if (s != null)
                {
                    s.Flush();
                    s.Close();
                    s.Dispose();
                }
            }
        }

        public static void DeleteDirectory (string name)
        {
            if (directoryConfiguration != null)
            {
                foreach (DirectoryType dt in directoryConfiguration)
                {
                    if (dt.name == name)
                    {
                        directoryConfiguration.Remove(dt);
                        SaveConfigFile();
                        break;
                    }
                }
            }
            else
            {
                log.Error("Config file is null or empty...");
            }
        }

        private static void OnCacheSuppress(string key, object value, System.Web.Caching.CacheItemRemovedReason reason)
        {
            log.Debug("Cache deleted: " + key + " reason: " + reason.ToString());
            try
            {
                DirectoryType dt = GetDirectory(key);
                if (dt != null)
                {
                    log.Debug("Reinit directory cache...");
                    if (HttpRuntime.Cache.Get(dt.name) == null)
                    {
                        InitDirectoryCache();
                    }
                }
            }
            catch (Exception e)
            {
                log.Debug("Unable to reinsert " + key + " object " + e.Message);
            }
        }
    }
}