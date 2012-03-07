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
using log4net;
using Wybecom.TalkPortal.Cisco;
using Wybecom.TalkPortal.Cisco.AXL.Proxy;
using System.Xml;

namespace Wybecom.TalkPortal.Providers
{
    public class CiscoExtensionMobilityProvider : ExtensionMobilityProvider
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _applicationName;
        private EMAPI _emapi;
        private AXLAPIService _axl;
        private string _emserver = "";
        private string _emuser = "";
        private string _empassword = "";
        private string _sharedLinePartition = "Delog";

        public CiscoExtensionMobilityProvider()
        {
            _emapi = new EMAPI(_emserver, _emuser, _empassword);
            _axl = new AXLAPIService();
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

        public override string[] getPhones(string[] users)
        {
            List<string> devices = new List<string>();
            Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.responseType rt = _emapi.getPhones(users);
            if (rt.Item is Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.deviceUserResultsType)
            {
                foreach (Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.deviceType dt in ((Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.deviceUserResultsType)rt.Item).device)
                {
                    devices.Add(dt.name);
                }
            }
            else if (rt.Item is Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.failureType)
            {
                throw new Exception(((Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.failureType)rt.Item).errorMessage.Value);
            }
            else
            {
                throw new Exception("Invalid response");
            }
            return devices.ToArray();
        }

        public override void Login(string user, string phone, string profile)
        {
            Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.responseType rt = _emapi.Login(user, phone, profile);
            if (rt.Item is Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.failureType)
            {
                throw new Exception( ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.failureType)rt.Item).error.Text[0]);
            }
            else if (rt.Item is Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.successType)
            {
                log.Debug("Successfully log " + user + " from " + phone + " with " + profile);
            }
            else
            {
                throw new Exception("Invalid response");
            }
        }

        public override void LoginFromLine(string user, string extension, string profile)
        {

            string phone = GetPhoneFromLine(extension);
            if (phone != null && phone != "")
            {

                Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.responseType rt = _emapi.Login(user, phone, profile);
                if (rt.Item is Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.failureType)
                {
                    throw new Exception(((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.failureType)rt.Item).error.Text[0]);
                }
                else if (rt.Item is Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.successType)
                {
                    log.Debug("Successfully log " + user + " from " + extension + " with " + profile);
                }
                else
                {
                    throw new Exception("Invalid response");
                }
            }
            else
            {
                throw new Exception("No phones retreived from this line, unable to process request");
            }
        }

        public override void Logout(string device)
        {
            Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.responseType rt = _emapi.Logout(device);
            if (rt.Item is Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.failureType)
            {
                throw new Exception(((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.failureType)rt.Item).error.Text[0]);
            }
            else if (rt.Item is Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.successType)
            {
            }
            else
            {
                throw new Exception("Invalid response");
            }
        }

        public override string[] getUsers(string[] phones)
        {
            List<string> users = new List<string>();
            Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.responseType rt = _emapi.getUsers(phones);
            if (rt.Item is Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.userDevicesResultsType)
            {
                foreach (Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.userType dt in ((Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.userDevicesResultsType)rt.Item).user)
                {
                    users.Add(dt.id);
                }
            }
            else if (rt.Item is Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.failureType)
            {
                throw new Exception(((Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.failureType)rt.Item).errorMessage.Value);
            }
            else
            {
                throw new Exception("Invalid response");
            }
            return users.ToArray();
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "CiscoExtensionMobilityProvider";
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Cisco Extension Mobility Provider");
            }
            base.Initialize(name, config);
            _applicationName = config["applicationName"];

            if (String.IsNullOrEmpty(_applicationName))
                _applicationName = "/";
            config.Remove("applicationName");

            try
            {
                _emserver = config["emserver"];
            }
            catch (Exception e)
            {
                log.Error("Unable to parse emserver attribute: " + e.Message);
            }

            
            config.Remove("emserver");

            try
            {
                _emuser = config["emuser"];
            }
            catch (Exception e)
            {
                log.Error("Unable to parse emuser attribute: " + e.Message);
            }


            config.Remove("emuser");

            try
            {
                _empassword = config["empassword"];
            }
            catch (Exception e)
            {
                log.Error("Unable to parse empassword attribute: " + e.Message);
            }


            config.Remove("empassword");

            try
            {
                _sharedLinePartition = config["sharedlinepartition"];
            }
            catch (Exception e)
            {
                log.Error("Unable to parse sharedlinepartition attribute: " + e.Message);
            }

            
            config.Remove("sharedlinepartition");

            _emapi.empassword = _empassword;
            _emapi.emserver = _emserver;
            _emapi.emuser = _emuser;

            if (config.Count > 0)
            {
                string attr = config.Get(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized attribute: " + attr);
            }
        }

        private string GetPhoneFromLine(string line)
        {
            string phone = "";
            ExecuteSQLQueryReq sql = new ExecuteSQLQueryReq();
            sql.sql = "select name from device d inner join devicenumplanmap dnpm on dnpm.fkdevice = d.pkid inner join numplan np on np.pkid = dnpm.fknumplan where dnorpattern = '"+line+"' and fkroutepartition in (select pkid from routepartition where name = '"+_sharedLinePartition+"')";
            if (_axl != null)
            {
                ExecuteSQLQueryRes res = _axl.executeSQLQuery(sql);
                if (res != null)
                {
                    if (res.@return.Length > 0)
                    {
                        if (res.@return.Length > 1)
                        {
                            string[] device = new string[res.@return.Length];
                            int compteur = 0;
                            foreach (object o in res.@return)
                            {
                                XmlNode[] element = (XmlNode[])o;
                                device[compteur] = element[0].InnerText;
                                compteur++;
                            }
                            throw new SharedLineException(line);
                        }
                        else
                        {
                            XmlNode[] element = (XmlNode[])res.@return[0];
                            phone = element[0].InnerText;
                        }
                        return phone;
                    }
                    else
                    {
                        log.Error("Line " + line + " doesn't exist.");
                        throw new Exception("No phone device founded from this line");
                    }
                }
                else
                {
                    throw new Exception("No result retreived.");
                }
            }
            else
            {
                throw new System.Exception("Impossible d'éxécuter l'ordre sans client AXL");
            }
        }
    }
}
