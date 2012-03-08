﻿ ///
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
using System.Security.Cryptography;
using System.IO;
using Wybecom.TalkPortal.Cisco.WebDialer.Proxy;
using Wybecom.TalkPortal.Cisco.AXL.Proxy;
using log4net;
using System.Web.Configuration;

namespace Wybecom.TalkPortal.Providers
{
    public class CiscoAuthenticationProvider : AuthenticationProvider
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _applicationName;
        private WebdialerSoap _wds;
        private AXLAPIService _aas;
        private int _tokenExpiration = 20;
        private static readonly byte[] encryptionKeyBytes =
            new byte[] { 0x36, 0x37, 0x46, 0x31, 0x42, 0x41, 0x39, 0x43 };

        public CiscoAuthenticationProvider()
        {
            _wds = new WebdialerSoap();
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

        public int tokenExpiration
        {
            get
            {
                return _tokenExpiration;
            }
            set
            {
                _tokenExpiration = value;
            }
        }

        public override string Authenticate(string dn, string user)
        {
            log.Debug("Token request from user: " + user);
            user = formatUser(user);
            string token = "";

            if (_wds.isClusterUserSoap(user))
            {
                if (userCanMonitoring(dn, user))
                {
                    token = GenerateToken(dn);
                }
                else
                {
                    throw new AuthenticationMismatchException();
                }
            }
            else
            {
                throw new AuthenticationFailedException();
            }
            return token;
        }

        public override string Authenticate(string dn, string user, string password)
        {
            log.Debug("Token request from user: " + user);
            string token = "";
            AuthenticateUserReq aur = new AuthenticateUserReq();
            aur.ItemElementName = ItemChoiceType42.password;
            aur.Item = password;
            aur.userid = user;
            AuthenticateUserResponse aures = _aas.doAuthenticateUser(aur);
            if (aures.@return.userAuthenticated)
            {
                if (userCanMonitoring(dn, user))
                {
                    token = GenerateToken(dn);
                }
                else
                {
                    throw new AuthenticationMismatchException();
                }
            }
            else
            {
                throw new AuthenticationFailedException();
            }
            return token;
        }

        private bool userCanMonitoring(string dn, string user)
        {
            bool can = false;
            
            string[] lines = null;
            lines = (string[])System.Web.HttpRuntime.Cache.Get(user);
            if (lines == null)
            {
                GetUserReq request = new GetUserReq();
                request.userid = user;
                GetUserRes gur = _aas.getUser(request);
                List<string> userLines = new List<string>();
                if (gur.@return.user.primaryExtension != null && gur.@return.user.primaryExtension != "")
                {
                    userLines.Add(gur.@return.user.primaryExtension);
                }
                if (gur.@return.user.iaqExtension != null && gur.@return.user.iaqExtension != "")
                {
                    userLines.Add(gur.@return.user.iaqExtension);
                }
                lines = userLines.ToArray();
                System.Web.HttpRuntime.Cache.Add(user, lines, null, DateTime.Now.AddMinutes(2), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
            }
            foreach (string line in lines)
            {
                if (line == dn)
                {
                    can = true;
                    break;
                }
            }
            return can;
        }

        private string GenerateToken(string dn)
        {
            string token = dn + "_" + DateTime.Now.AddMinutes(_tokenExpiration).ToString();
            token = Encrypt(token);
            log.Debug("Generated token: " + token);
            return token;
        }

        public override string UpdateToken(string token)
        {
            string newToken = Decrypt(token);
            string[] tokens = newToken.Split('_');
            if (tokens.Length > 0)
            {
                if (tokens.Length != 2)
                {
                    newToken = token;
                }
                else
                {
                    DateTime dt = DateTime.Now;
                    if (DateTime.TryParse(tokens[1], out dt))
                    {
                        log.Debug("Extends token lifetime...");
                        dt = dt.AddMinutes(_tokenExpiration);
                        newToken = Encrypt(tokens[0] + "_" + dt.ToString());
                    }
                    else
                    {
                        newToken = token;
                    }
                }
            }
            else
            {
                newToken = token;
            }
            return newToken;
        }

		public override bool Validate(string token, string dn)
        {
            bool isValid = false;
            token = Decrypt(token);
            // trusted?
            //if (token == dn + " is trusted.")
            if (token.Contains(" is trusted."))
            {
                log.Debug("Current connection run under trusted authentication mode");
                isValid = true;
            }
            else
            {
                string[] tokens = token.Split('_');
                if (tokens.Length != 2)
                {
                    return isValid;
                }
                else if (tokens[0] == dn && DateTime.Parse(tokens[1]).CompareTo(DateTime.Now) >= 0)
                {
                    log.Debug("Current connection run under sso or manual mode");
                    isValid = true;
                }
                else
                {
                    if (tokens[0] != dn)
                    {
                        throw new AuthenticationMismatchException();
                    }
                    else
                    {
                        throw new AuthenticationExpiredException();
                    }
                }
            }
            return isValid;
        }

        private string Decrypt(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException
                   ("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                    (Convert.FromBase64String(token));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(encryptionKeyBytes, encryptionKeyBytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();

        }


        public override string Encrypt(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(encryptionKeyBytes, encryptionKeyBytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(token);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);

        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "CiscoAuthenticationProvider";
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Cisco Authentication Provider");
            }
            base.Initialize(name, config);
            _applicationName = config["applicationName"];

            if (String.IsNullOrEmpty(_applicationName))
                _applicationName = "/";
            config.Remove("applicationName");

            try
            {
                _tokenExpiration = int.Parse(config["tokenExpiration"]);
            }
            catch (Exception e)
            {
                log.Error("Unable to parse tokenExpiration attribute: " + e.Message);
                _tokenExpiration = 20;
            }

            if (_tokenExpiration <= 0)
            {
                _tokenExpiration = 20;
            }
            config.Remove("tokenExpiration");

            if (config.Count > 0)
            {
                string attr = config.Get(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized atrtibute: " + attr);
            }
        }

        public override string GetDN()
        {
            AuthenticationSection authsec = (AuthenticationSection)WebConfigurationManager.GetSection("system.web/authentication");
            if (authsec.Mode != AuthenticationMode.Windows)
            {
                throw new Exception("Windows authentication mode is required!");
            }
            else
            {
                return "";
            }
        }
    }
}