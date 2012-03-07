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
using System.Text;
using System.Configuration.Provider;
using Wybecom.TalkPortal.Cisco.AXL.Proxy;
using System.Xml;
using log4net;

namespace Wybecom.TalkPortal.Providers
{
    public class CiscoPrivacyProvider : PrivacyProvider
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _applicationName;
        private AXLAPIService _aas;
        private bool _saveInCache;

        public CiscoPrivacyProvider()
        {
            _aas = new AXLAPIService();
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

        public override bool SaveInCache
        {
            get { return _saveInCache; }
        }

        public override bool IsPrivate(string dn)
        {
            bool isprivate = false;
            if (System.Web.HttpContext.Current.Cache.Get(dn + "_privacy") == null)
            {
                ExecuteSQLQueryReq query = new ExecuteSQLQueryReq();
                query.sql = "select name from dirgroup dg inner join enduserdirgroupmap eudgm";
                query.sql += " on dg.pkid = eudgm.fkdirgroup inner join enduser eu on eu.pkid = ";
                query.sql += "eudgm.fkenduser where eu.pkid in (select distinct eu.pkid from enduser eu";
                query.sql += " inner join endusernumplanmap eunpm on eu.pkid = eunpm.fkenduser ";
                query.sql += "inner join numplan np on np.pkid = eunpm.fknumplan where dnorpattern in ('";
                query.sql += dn + "'))";
                ExecuteSQLQueryRes response = _aas.executeSQLQuery(query);
                if (response != null && response.@return != null)
                {
                    foreach (XmlNode[] nodes in response.@return)
                    {
                        if (nodes.Length == 1)
                        {
                            if (System.Web.Configuration.WebConfigurationManager.AppSettings["CiscoPrivacyGroup"] == nodes[0].InnerText)
                            {
                                isprivate = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    log.Error("Unable to retreive group membership from " + dn);
                }
                System.Web.HttpContext.Current.Cache.Add(dn + "_privacy", isprivate, null, DateTime.Now.AddHours(10), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, OnCacheSuppress);
            }
            else
            {
                isprivate = (bool)System.Web.HttpContext.Current.Cache.Get(dn + "_privacy");
            }
            return isprivate;
        }

        public override void SetPrivacy(string dn, bool isprivate)
        {
            System.Web.HttpContext.Current.Cache.Add(dn + "_privacy", isprivate, null, DateTime.Now.AddHours(10), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, OnCacheSuppress);
            ExecuteSQLUpdateReq query = new ExecuteSQLUpdateReq();
            if (isprivate)
            {
                query.sql = "insert into enduserdirgroupmap (fkenduser, fkdirgroup) select eu.pkid, dg.pkid from enduser eu, dirgroup dg where eu.userid in (";
                query.sql = "select distinct eu.pkid from enduser eu";
                query.sql += " inner join endusernumplanmap eunpm on eu.pkid = eunpm.fkenduser ";
                query.sql += "inner join numplan np on np.pkid = eunpm.fknumplan where dnorpattern in ('";
                query.sql += dn + "') and dg.name = '" + System.Web.Configuration.WebConfigurationManager.AppSettings["CiscoPrivacyGroup"] + "'";
            }
            else
            {
                query.sql = "delete from enduserdirgroupmap where fkenduser in (select pkid from enduser where userid = in (";
                query.sql = "select distinct eu.pkid from enduser eu";
                query.sql += " inner join endusernumplanmap eunpm on eu.pkid = eunpm.fkenduser ";
                query.sql += "inner join numplan np on np.pkid = eunpm.fknumplan where dnorpattern in ('";
                query.sql += dn + "') and fkdirgroup in (select pkid from dirgroup where name = '" + System.Web.Configuration.WebConfigurationManager.AppSettings["CiscoPrivacyGroup"] + "')"; 
            }
            _aas.executeSQLUpdate(query);
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "CiscoPrivacyProvider";
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Cisco Privacy Provider");
            }
            base.Initialize(name, config);
            _applicationName = config["applicationName"];

            if (String.IsNullOrEmpty(_applicationName))
                _applicationName = "/";
            config.Remove("applicationName");

            string save = config["saveInCache"];
            config.Remove("saveInCache");

            if (String.IsNullOrEmpty(save))
            {
                _saveInCache = true;
            }
            else
            {
                _saveInCache = bool.Parse(save);
            }

            if (config.Count > 0)
            {
                string attr = config.Get(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized atrtibute: " + attr);
            }
        }

        private static void OnCacheSuppress(string key, object value, System.Web.Caching.CacheItemRemovedReason reason)
        {
            log.Debug("Privacy Cache deleted: " + key + " reason: " + reason.ToString());
        }
    }
}
