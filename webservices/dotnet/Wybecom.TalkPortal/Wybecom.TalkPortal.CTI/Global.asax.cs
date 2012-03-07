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
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;
using System.IO;
using Wybecom.TalkPortal.Providers;

namespace Wybecom.TalkPortal.CTI
{
    public class Global : System.Web.HttpApplication
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static CacheManager cacheMgr;
        public static CodifEntities codif;

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Debug("CTI Web Services Starting...");
            InitCache();
            if (System.Web.Configuration.WebConfigurationManager.AppSettings.Get("CallsInDatabase") == "true")
            {
                log.Debug("Initialize Codif database...");
                codif = new CodifEntities();
                
            }
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
            //SaveCache();
        }

        private void InitCache()
        {
            cacheMgr = (CacheManager)CacheFactory.GetCacheManager();
            
            //string file = Path.Combine(HttpRuntime.AppDomainAppPath, "stateserver.state");
            //Stream s = null;
            //try
            //{
            //    if (File.Exists(file))
            //    {
            //        log.Debug("Analyzing StateServer persistance...");
            //        s = File.Open(file, FileMode.Open);
            //        BinaryFormatter bf = new BinaryFormatter();
            //        List<LineControl> lineControls = (List<LineControl>)bf.Deserialize(s);
            //        log.Debug(lineControls.Count + " lines retreived...");
                foreach (LineControl lc in SnapshotService.GetSnapshot())
                    {
                        log.Debug("Adding " + lc.directoryNumber + " to the cache.");
                        cacheMgr.Add(lc.directoryNumber, lc);
                    }
            //        s.Close();
            //        s.Dispose();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Unable to initialize cache: " + ex.Message);
            //}
            //finally
            //{
            //    if (s != null)
            //    {
            //        s.Close();
            //        s.Dispose();
            //    }
            //}
        }

        private void SaveCache()
        {
            //if (cacheMgr != null)
            //{
            //    string file = Path.Combine(HttpRuntime.AppDomainAppPath, "stateserver.state");
            //    Stream s = null;
            //    try
            //    {
            //        log.Debug("Analyzing StateServer persistance...");
            //        s = File.Open(file, FileMode.Create);
            //        BinaryFormatter bf = new BinaryFormatter();
            //        List<LineControl> lineControls = new List<LineControl>();
            //        System.Collections.Hashtable ht = cacheMgr.GetHashtable();
            //        System.Collections.IDictionaryEnumerator de = ht.GetEnumerator();
            //        while (de.MoveNext())
            //        {
            //            if (((CacheItem)de.Value).Value is LineControl)
            //            {
            //                lineControls.Add((LineControl)((CacheItem)de.Value).Value);
            //            }
            //        }
            //        log.Debug("Serialisation du cache");
            //        s.Seek(0, SeekOrigin.Begin);
            //        bf.Serialize(s, lineControls);
            //        s.Close();
            //        s.Dispose();
            //    }
            //    catch (Exception ex)
            //    {
            //        log.Error("Unable to initialize cache: " + ex.Message);
            //    }
            //    finally
            //    {
            //        if (s != null)
            //        {
            //            s.Close();
            //            s.Dispose();
            //        }
            //    }
            //}
            //else
            //{
            //    log.Debug("CacheManager is null");
            //}
        }
    }
}