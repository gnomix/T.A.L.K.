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
using System.DirectoryServices.Protocols;
using System.Net;
using log4net;
using System.Xml;

namespace Wybecom.TalkPortal.DMD
{
    /// <summary>
    /// LDAP browser
    /// </summary>
    /// <seealso cref="DirectoryType"/>
    public class LDAP
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static LdapConnection ldapConnection;
        static string ldapServer;
        static NetworkCredential credential;
        static string targetOU;
        static string ldapFilter;

        static int pageSize; 
        static int numberOfPages;

        private static AuthType AuthTypeFromString(string authtype)
        {
            AuthType aType = AuthType.Anonymous;
            switch (authtype)
            {
                case "Anonymous":
                    aType = AuthType.Anonymous;
                    break;
                case "Basic":
                    aType = AuthType.Basic;
                    break;
                case "Digest":
                    aType = AuthType.Digest;
                    break;
                case "Dpa":
                    aType = AuthType.Dpa;
                    break;
                case "External":
                    aType = AuthType.External;
                    break;
                case "Kerberos":
                    aType = AuthType.Kerberos;
                    break;
                case "Msn":
                    aType = AuthType.Msn;
                    break;
                case "Negotiate":
                    aType = AuthType.Negotiate;
                    break;
                case "Ntlm":
                    aType = AuthType.Ntlm;
                    break;
                case "Sicily":
                    aType = AuthType.Sicily;
                    break;
                default:
                    aType = AuthType.Anonymous;
                    break;
            }
            return aType;
        }
        /// <summary>
        /// Search method
        /// </summary>
        /// <param name="dir">Search settings<seealso cref="DirectoryType"/></param>
        /// <returns>A dataset conform to the specified FieldFormatters<seealso cref="FieldFormatter"/></returns>
        public static DataSet Search(DirectoryType dir)
        {
            DataSet results = new DataSet();
            DataTable dt = results.Tables.Add();
            LdapDatasourceType ldt = (LdapDatasourceType)dir.Item;
            try
            {
                Init(ldt.server, ldt.authenticationType, ldt.user, ldt.userPassword, ldt.targetOU, ldt.ldapFilter, ldt.pageSize, ldt.nbPages);
                log.Debug("Search " + ldapFilter + " from " + targetOU + " on " + ldapServer);
                PageResultRequestControl prc = new PageResultRequestControl(pageSize);
                prc.IsCritical = false;
                PageResultResponseControl cookie;
                SearchResponse sr;

                SearchRequest request = new SearchRequest(targetOU, ldapFilter, SearchScope.Subtree,ldt.ldapAttributes);
                request.Controls.Add(prc);

                foreach (string attribut in ldt.ldapAttributes)
                {
                    dt.Columns.Add(attribut, typeof(string));
                }
                

                int pageCount = 0;
                int count;
                while (true)
                {
                    pageCount++;

                    sr = (SearchResponse)ldapConnection.SendRequest(request);
                    log.Debug("Page " + pageCount.ToString() + ": " + sr.Entries.Count + " entries");

                    count = 0;
                    foreach (SearchResultEntry entry in sr.Entries)
                    {
                        count++;
                        log.Debug("Entry " + count.ToString() + ": " + entry.DistinguishedName);
                        List<object> values = new List<object>();
                        
                        foreach (string attribut in ldt.ldapAttributes)
                        {
                            DirectoryAttribute da = entry.Attributes[attribut];
                            if (da != null && da.GetValues(typeof(string)).Length > 0)
                            {
                                values.Add((string)da.GetValues(typeof(string))[0]);
                            }
                            else
                            {
                                values.Add("");
                            }
                        }
                        dt.Rows.Add(values.ToArray());
                    }

                    if (sr.Controls.Length != 1 || !(sr.Controls[0] is PageResultResponseControl))
                    {
                        log.Debug("Weird response...");
                        ldapConnection.Dispose();
                        return results;
                    }

                    cookie = (PageResultResponseControl)sr.Controls[0];

                    if (cookie.Cookie.Length == 0) break;

                    prc.Cookie = cookie.Cookie;
                }

                ldapConnection.Dispose();
                log.Debug("Search ended");
                return results;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return results;
            }
        }

        static void Init(string server, string authtype, string user, string password, string targetou, string filter, int pagesize, int nbpages)
        {
            ldapServer = server;
            ldapConnection = new LdapConnection(ldapServer);
            ldapConnection.AuthType = AuthTypeFromString(authtype);
            if (ldapConnection.AuthType != AuthType.Anonymous)
            {
                credential = new NetworkCredential(user, password);
                ldapConnection.Credential = credential;
            }
            ldapConnection.AutoBind = true;
            ldapConnection.SessionOptions.ProtocolVersion = 3;
            log.Debug("LDAP connection created");
            targetOU = targetou;
            ldapFilter = filter;
            pageSize = pagesize;
            numberOfPages = nbpages;
        }

        static void Init(string server, string authtype, string user, string password, string targetou, string filter)
        {
            ldapServer = server;
            ldapConnection = new LdapConnection(ldapServer);
            ldapConnection.AuthType = AuthTypeFromString(authtype);
            if (ldapConnection.AuthType != AuthType.Anonymous)
            {
                credential = new NetworkCredential(user, password);
                ldapConnection.Credential = credential;
            }
            ldapConnection.AutoBind = true;
            ldapConnection.SessionOptions.ProtocolVersion = 3;
            log.Debug("LDAP connection created");
            targetOU = targetou;
            ldapFilter = filter;
        }

        public static bool Authorize(string user, string telephoneNumber, string idAttribute, string telephoneAttribute, string server, string authtype, string ldapUser, string password, string targetou, string filter)
        {
            bool authenticated = false;
            if (telephoneNumber == getDN(user, idAttribute, telephoneAttribute, server, authtype, ldapUser, password, targetou, filter))
            {
                authenticated = true;
            }
            return authenticated;
        }

        public static bool AuthenticateAndAuthorize(string user, string password, string telephoneNumber, string idAttribute, string telephoneAttribute, string server, string targetou, string filter, string authtype)
        {
            bool authenticated = false;
            Init(server, "Basic", user, password, targetou, filter);
            log.Debug("Search " + user + " from " + targetOU + " on " + ldapServer);

            SearchResponse response;
            ldapFilter = "(&(" + idAttribute + "=" + user + ")" + ldapFilter + ")";
            SearchRequest request = new SearchRequest(targetOU, ldapFilter, SearchScope.Subtree, new string[1] { telephoneAttribute });
            response = (SearchResponse)ldapConnection.SendRequest(request);
            if (response.Entries.Count == 1)
            {
                DirectoryAttribute da = response.Entries[0].Attributes[telephoneAttribute];
                if (da != null && da.GetValues(typeof(string)).Length > 0)
                {
                    if (telephoneNumber == (string)da.GetValues(typeof(string))[0])
                    {
                        authenticated = true;
                    }
                }
                else
                {
                    log.Debug("The attribute " + telephoneAttribute + " is not defined for " + user);
                }
            }
            else
            {
                log.Debug("0 or more than 1 result retreived...");
            }
            return authenticated;
        }

        public static string getDN(string user, string idAttribute, string telephoneAttribute, string server, string authtype, string ldapUser, string password, string targetou, string filter)
        {
            string dn = "";
            Init(server, authtype, ldapUser, password, targetou, filter);
            log.Debug("Search " + user + " from " + targetOU + " on " + ldapServer);

            SearchResponse response;
            ldapFilter = "(&(" + idAttribute + "=" + user + ")" + ldapFilter + ")";
            SearchRequest request = new SearchRequest(targetOU, ldapFilter, SearchScope.Subtree, new string[1] { telephoneAttribute });
            response = (SearchResponse)ldapConnection.SendRequest(request);
            if (response.Entries.Count == 1)
            {
                DirectoryAttribute da = response.Entries[0].Attributes[telephoneAttribute];
                if (da != null && da.GetValues(typeof(string)).Length > 0)
                {
                    dn = (string)da.GetValues(typeof(string))[0];
                }
                else
                {
                    log.Debug("The attribute " + telephoneAttribute + " is not defined for " + user);
                }
            }
            else
            {
                log.Debug("0 or more than 1 result retreived...");
            }
            return dn;
        }
    }
}
