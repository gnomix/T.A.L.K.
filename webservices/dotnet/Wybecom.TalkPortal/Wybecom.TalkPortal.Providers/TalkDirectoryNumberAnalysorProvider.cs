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
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Configuration.Provider;
using log4net;

namespace Wybecom.TalkPortal.Providers
{
    public class TalkDirectoryNumberAnalysorProvider : DirectoryNumberAnalysorProvider
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _applicationName;
        private XmlDocument xDoc;
        public TalkDirectoryNumberAnalysorProvider()
        {
            xDoc = new XmlDocument();
            try
            {
                xDoc.Load(Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "Regex.xml"));
            }
            catch (Exception e)
            {
                log.Error("Unable to load regular expression pattern file, provider will not work: " + e.Message);
            }
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

        public override string Analyse(string dn)
        {
            string result = dn;
            if (xDoc != null)
            {
                try
                {
                    foreach (XmlNode node in xDoc.SelectNodes("//regex"))
                    {
                        log.Debug("Application de la règle: " + node.Attributes["pattern"].Value + " qui devient " + node.InnerText);
                        result = Regex.Replace(result, node.Attributes["pattern"].Value, node.InnerText);
                    }
                    
                    if (result.Length == 10 && result.StartsWith("0"))
                    {
                        result = "0" + result;
                    }
                    log.Debug("Le numéro final est le " + result);
                }
                catch (Exception e)
                {
                    log.Error("Error while analysing " + dn + ": " + e.Message);
                }
            }
            return result;
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "TalkDirectoryNumberAnalysorProvider";
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Talk Directory Number Analysor Provider");
            }
            base.Initialize(name, config);
            _applicationName = config["applicationName"];

            if (String.IsNullOrEmpty(_applicationName))
                _applicationName = "/";
            config.Remove("applicationName");

            if (config.Count > 0)
            {
                string attr = config.Get(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized atrtibute: " + attr);
            }
        }
    }
}
