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
using System.Web.Services;
using System.EnterpriseServices;
using System.Data;
using System.Collections;
using log4net;
using Wybecom.TalkPortal.Cisco;

namespace Wybecom.TalkPortal
{
    /// <summary>
    /// Description résumée de DMDWS
    /// </summary>
    [WebService(Namespace = "http://wybecom.org/", Name="DMDWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    [System.Web.Script.Services.ScriptService()]
    [System.Web.Script.Services.GenerateScriptType(typeof(FieldType))]
    public class DMDWS : System.Web.Services.WebService
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [WebMethod(Description = "Get meta data from directory", CacheDuration = 30, BufferResponse = false, MessageName = "GetMetaData", TransactionOption = TransactionOption.Disabled)]
        public FieldType[] GetMetaData(string directoryName)
        {
            log.Debug("GetMetaData from " + directoryName + " directory");
            List<FieldType> results = new List<FieldType>();
            foreach (DirectoryType dt in Global.directoryConfiguration)
            {
                if (dt.name == directoryName)
                {
                    FieldFormatter[] ffs = null;
                    if (dt.Item is SqlDatasourceType)
                    {
                        ffs = ((SqlDatasourceType)dt.Item).fieldFormatters;
                    }
                    else if (dt.Item is LdapDatasourceType)
                    {
                        ffs = ((LdapDatasourceType)dt.Item).fieldFormatters;
                    }
                    else if (dt.Item is CiscoDatasourceType)
                    {
                        ffs = ((CiscoDatasourceType)dt.Item).fieldFormatters;
                    }
                    foreach (FieldFormatter ff in ffs)
                    {
                        results.Add(ff.fieldType);
                    }
                }
            }
            log.Debug(results.Count.ToString() + " fieldType(s) retreived from " + directoryName + " directory");
            return results.ToArray();
        }

        [WebMethod(Description="Get a dataset from a directory",CacheDuration=30,BufferResponse=false,MessageName="Search",TransactionOption=TransactionOption.Disabled)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat=System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString=true)]
        public DataTable Search(string directoryName, string filter)
        {
            DataTable results = null;
            if (HttpRuntime.Cache.Get(directoryName + "_" + filter) != null)
            {
                results = (DataTable)HttpRuntime.Cache.Get(directoryName + "_" + filter);
            }
            else
            {
                if (HttpRuntime.Cache.Get(directoryName) != null)
                {
                    DataSet fromCache = (DataSet)HttpRuntime.Cache.Get(directoryName);
                    DataView dv = null;
                    try
                    {
                        if (fromCache != null)
                        {
                            dv = fromCache.Tables[0].AsDataView();
                            dv.RowFilter = filter;
                            DataTable calcTable = dv.ToTable("CalcTable");
                            foreach (DirectoryType dt in Global.directoryConfiguration)
                            {
                                if (dt.name == directoryName)
                                {
                                    FieldFormatter[] ffs = null;
                                    List<string> cols = new List<string>();
                                    if (dt.Item is SqlDatasourceType)
                                    {
                                        ffs = ((SqlDatasourceType)dt.Item).fieldFormatters;
                                    }
                                    else if (dt.Item is LdapDatasourceType)
                                    {
                                        ffs = ((LdapDatasourceType)dt.Item).fieldFormatters;
                                    }
                                    else if (dt.Item is CiscoDatasourceType)
                                    {
                                        ffs = ((CiscoDatasourceType)dt.Item).fieldFormatters;
                                    }
                                    foreach (FieldFormatter ff in ffs)
                                    {
                                        cols.Add(ff.fieldName);
                                        if (!calcTable.Columns.Contains(ff.fieldName))
                                        {
                                            DataColumn dc = new DataColumn();
                                            dc.DataType = typeof(string);
                                            dc.ColumnName = ff.fieldName;
                                            dc.Expression = ff.value;
                                            calcTable.Columns.Add(dc);
                                        }
                                    }
                                    DataView sortedView = calcTable.AsDataView();
                                    if (cols.Count > 0)
                                    {
                                        sortedView.Sort = cols[0];
                                    }
                                    results = sortedView.ToTable("Results", false, cols.ToArray());
                                    HttpRuntime.Cache.Insert(directoryName + "_" + filter, results, null, DateTime.Now.AddHours(10), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                                    break;
                                }
                            }

                        }
                        else
                        {
                            log.Debug("Cache is null, no data to retreive...");
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error("Error while searching: " + e.Message);
                    }
                }
            }
            return results;
        }

        [WebMethod(Description = "Get a dataset from a directory", CacheDuration = 30, BufferResponse = false, MessageName = "SortSearch", TransactionOption = TransactionOption.Disabled)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml, XmlSerializeString = true)]
        public DataTable SortSearch(string directoryName, string filter, int sort)
        {
            DataTable results = null;
            if (HttpRuntime.Cache.Get(directoryName + "_" + filter + "_" + sort) != null)
            {
                results = (DataTable)HttpRuntime.Cache.Get(directoryName + "_" + filter + "_" + sort);
            }
            else
            {
                if (HttpRuntime.Cache.Get(directoryName) != null)
                {
                    DataSet fromCache = (DataSet)HttpRuntime.Cache.Get(directoryName);
                    DataView dv = null;

                    try
                    {
                        if (fromCache != null)
                        {
                            dv = fromCache.Tables[0].AsDataView();
                            dv.RowFilter = filter;
                            DataTable calcTable = dv.ToTable("CalcTable");
                            foreach (DirectoryType dt in Global.directoryConfiguration)
                            {
                                if (dt.name == directoryName)
                                {
                                    FieldFormatter[] ffs = null;
                                    List<string> cols = new List<string>();
                                    if (dt.Item is SqlDatasourceType)
                                    {
                                        ffs = ((SqlDatasourceType)dt.Item).fieldFormatters;
                                    }
                                    else if (dt.Item is LdapDatasourceType)
                                    {
                                        ffs = ((LdapDatasourceType)dt.Item).fieldFormatters;
                                    }
                                    else if (dt.Item is CiscoDatasourceType)
                                    {
                                        ffs = ((CiscoDatasourceType)dt.Item).fieldFormatters;
                                    }
                                    foreach (FieldFormatter ff in ffs)
                                    {
                                        cols.Add(ff.fieldName);

                                        if (!calcTable.Columns.Contains(ff.fieldName))
                                        {
                                            DataColumn dc = new DataColumn();
                                            dc.DataType = typeof(string);
                                            dc.ColumnName = ff.fieldName;
                                            dc.Expression = ff.value;
                                            calcTable.Columns.Add(dc);
                                        }
                                    }
                                    DataView sortedView = calcTable.AsDataView();
                                    if (cols.Count >= sort)
                                    {
                                        sortedView.Sort = cols[sort];
                                    }
                                    else if (cols.Count > 0)
                                    {
                                        sortedView.Sort = cols[0];
                                    }
                                    results = sortedView.ToTable("Results", false, cols.ToArray());
                                    HttpRuntime.Cache.Insert(directoryName + "_" + filter + "_" + sort, results, null, DateTime.Now.AddHours(10), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                                    break;
                                }
                            }

                        }
                        else
                        {
                            log.Debug("Cache is null, no data to retreive...");
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error("Error while searching: " + e.Message);
                    }
                }
            }
            return results;
        }

        [WebMethod(Description = "Lookup directory number in all directory")]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = true)]
        public string Lookup(string dirNum)
        {
            string identity = "Inconnu";
            try
            {
                foreach (DirectoryType dt in Global.directoryConfiguration)
                {
                    FieldFormatter[] ffs = null;
                    if (dt.Item is SqlDatasourceType)
                    {
                        ffs = ((SqlDatasourceType)dt.Item).fieldFormatters;
                    }
                    else if (dt.Item is LdapDatasourceType)
                    {
                        ffs = ((LdapDatasourceType)dt.Item).fieldFormatters;
                    }
                    else if (dt.Item is CiscoDatasourceType)
                    {
                        ffs = ((CiscoDatasourceType)dt.Item).fieldFormatters;
                    }
                    List<string> filterCol = new List<string>();
                    string filter = "";
                    string identityCol = "";
                    foreach (FieldFormatter ff in ffs)
                    {
                        if (ff.fieldType == FieldType.Telephone)
                        {
                            filterCol.Add(ff.fieldName);
                            if (filter != "")
                            {
                                filter += " OR " + ff.fieldName + " = '" + dirNum + "'";
                            }
                            else
                            {
                                filter += ff.fieldName + " = '" + dirNum + "'";
                            }
                        }
                        else if (ff.fieldType == FieldType.Identity)
                        {
                            identityCol = ff.fieldName;
                        }
                    }

                    DataTable dtb = null;
                    if (HttpRuntime.Cache.Get(dt.name + "_formated") != null)
                    {
                        dtb = (DataTable)HttpRuntime.Cache.Get(dt.name + "_formated");
                        DataView dtbv = dtb.AsDataView();
                        dtbv.RowFilter = filter;
                        var name = from dtbvname in dtbv.ToTable().AsEnumerable()
                                   select dtbvname.Field<string>(identityCol);
                        identity = name.First();
                    }
                }
                return identity;
            }
            catch (Exception e)
            {
                log.Error("Unable to lookup " + dirNum + ": " + e.Message);
                return identity;
            }
            
        }

        [WebMethod(Description = "Lookup for several directory number in all directory")]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = true)]
        public LookupResponse[] MultiLookup(LookupResponse[] lookup)
        {
            LookupResponse[] response = new LookupResponse[lookup.Length];
            int i = 0;
            foreach (LookupResponse l in lookup)
            {
                LookupResponse lr = new LookupResponse();
                lr.dirNum = l.dirNum;
                lr.callids = l.callids;
                string identity = "Inconnu";
                try
                {
                    foreach (DirectoryType dt in Global.directoryConfiguration)
                    {
                        FieldFormatter[] ffs = null;
                        if (dt.Item is SqlDatasourceType)
                        {
                            ffs = ((SqlDatasourceType)dt.Item).fieldFormatters;
                        }
                        else if (dt.Item is LdapDatasourceType)
                        {
                            ffs = ((LdapDatasourceType)dt.Item).fieldFormatters;
                        }
                        else if (dt.Item is CiscoDatasourceType)
                        {
                            ffs = ((CiscoDatasourceType)dt.Item).fieldFormatters;
                        }
                        List<string> filterCol = new List<string>();
                        string filter = "";
                        string identityCol = "";
                        foreach (FieldFormatter ff in ffs)
                        {
                            if (ff.fieldType == FieldType.Telephone)
                            {
                                filterCol.Add(ff.fieldName);
                                if (filter != "")
                                {
                                    filter += " OR " + ff.fieldName + " = '" + l.dirNum + "'";
                                }
                                else
                                {
                                    filter += ff.fieldName + " = '" + l.dirNum + "'";
                                }
                            }
                            else if (ff.fieldType == FieldType.Identity)
                            {
                                identityCol = ff.fieldName;
                            }
                        }

                        DataTable dtb = null;
                        if (HttpRuntime.Cache.Get(dt.name + "_formated") != null)
                        {
                            dtb = (DataTable)HttpRuntime.Cache.Get(dt.name + "_formated");
                            DataView dtbv = dtb.AsDataView();
                            dtbv.RowFilter = filter;
                            var name = from dtbvname in dtbv.ToTable().AsEnumerable()
                                       select dtbvname.Field<string>(identityCol);
                            identity = name.First();
                        }
                    }
                    lr.name = identity;
                    response[i] = lr;
                    i++;
                }
                catch (Exception e)
                {
                    log.Error("Unable to lookup " + l.dirNum + ": " + e.Message);
                    lr.name = identity;
                    response[i] = lr;
                    i++;
                }
            }
            return response;
        }

        [WebMethod(Description = "Autocomplete filter text box method")]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] AutoCompleteFilter(string prefixText, int count, string contextKey)
        {
            log.Debug("Autocomplete request: " + prefixText + " for " + contextKey);
            if (count == 0)
            {
                count = 10;
            }
            List<string> s = new List<string>();
            string[] context = contextKey.Split(',');
            try
            {
                foreach (DirectoryType dt in Global.directoryConfiguration)
                {
                    if (dt.name == context[0])
                    {
                        DataSet ds = (DataSet)HttpRuntime.Cache.Get(dt.name);
                        if (ds != null)
                        {
                            string filter = context[1] + " LIKE '" + prefixText + "*'";
                            if (context.Length > 2)
                            {
                                for (int i = 2; i < context.Length; i++)
                                {
                                    filter += " AND " + context[i];
                                }
                            }
                            foreach (DataRow dr in ds.Tables[0].Select(filter, context[1] + " ASC"))
                            {
                                s.Add(dr.Field<string>(context[1]));
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Unable to lookup for autocomplete with " + prefixText + " in " + contextKey + ": " + e.Message);
            }
            return s.ToArray();
        }

		[WebMethod(Description = "Get list of directories for CiscoIPPhone")]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet=true, XmlSerializeString=true)]
        public void GetDirectoriesForCiscoIPPhone()
        {
            CiscoIPPhoneMenuType menu = new CiscoIPPhoneMenuType();
            menu.Prompt = "Sélectionner un répertoire";
            menu.Title = "Répertoires";
            try
            {
                List<CiscoIPPhoneMenuItemType> menus = new List<CiscoIPPhoneMenuItemType>();
                
                foreach (DirectoryType dt in Global.directoryConfiguration)
                {
                    CiscoIPPhoneMenuItemType menuitem = null;
                    bool isIPPhoneCompliant = false;
                    if (dt.Item is SqlDatasourceType)
                    {
                        SqlDatasourceType sdt = dt.Item as SqlDatasourceType;
                        if (sdt.ipphonefilter != null)
                        {
                            isIPPhoneCompliant = true;
                        }
                    }
                    else if (dt.Item is LdapDatasourceType)
                    {
                        LdapDatasourceType ldt = dt.Item as LdapDatasourceType;
                        if (ldt.ipphonefilter != null)
                        {
                            isIPPhoneCompliant = true;
                        }
                    }
                    else if (dt.Item is CiscoDatasourceType)
                    {
                        CiscoDatasourceType cdt = dt.Item as CiscoDatasourceType;
                        if (cdt.ipphonefilter != null)
                        {
                            isIPPhoneCompliant = true;
                        }
                    }
                    if (isIPPhoneCompliant)
                    {
                        menuitem = new CiscoIPPhoneMenuItemType();
                        menuitem.Name = dt.name;
                        menuitem.URL = this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "SearchForCiscoIPPhone?directory=" + System.Web.HttpUtility.UrlEncode(dt.name);
                        menus.Add(menuitem);
                    }
                }
                if (menus.Count > 0)
                {
                    menu.MenuItem = menus.ToArray();
                }
                else
                {
                    this.Context.Response.Redirect(this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "Error?error=" + System.Web.HttpUtility.UrlEncode("No ipphone directories finded"), false);
                    //return menu;
                }
                //return menu;
                CiscoIPPhoneMenuTypeSerializer xml = new CiscoIPPhoneMenuTypeSerializer();
                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();;
                settings.Encoding = System.Text.Encoding.UTF8;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(ms, settings);
                System.Xml.Serialization.XmlSerializerNamespaces xmlnsEmpty = new System.Xml.Serialization.XmlSerializerNamespaces();
                xmlnsEmpty.Add("", "");
                xml.Serialize(xw, menu, xmlnsEmpty);
                ms.Position = 0;
                this.Context.Response.ContentType = "text/xml";
                this.Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
                this.Context.Response.Write(GetStringFromStream(ms));
                //xw.Flush();
                //xw.Close();

            }
            catch (Exception e)
            {
                log.Error("Unable get directories: " + e.Message);
                this.Context.Response.Redirect(this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "Error?error=" + System.Web.HttpUtility.UrlEncode(e.Message), false);
                //return menu;
            }
        }

        [WebMethod(Description = "Get list of directories")]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, XmlSerializeString = true)]
        public string[] GetDirectories()
        {
            List<string> directories = new List<string>();
            try
            {
                foreach (DirectoryType dt in Global.directoryConfiguration)
                {
                    directories.Add(dt.name);
                }
                return directories.ToArray();
            }
            catch (Exception e)
            {
                log.Error("Unable get directories: " + e.Message);
                return directories.ToArray() ;
            }
        }

        [WebMethod(Description = "Get search form from a particular directory for CiscoIPPhone")]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, XmlSerializeString = true)]
        public void SearchForCiscoIPPhone(string directory)
        {
            CicsoIPPhoneInputType input = new CicsoIPPhoneInputType();
            try
            {
                input.Prompt = "Entrer un critère de recherche";
                input.Title = "Recherche répertoire";

                input.InputItem = new CiscoIPPhoneInputItemType[3];
                
                input.InputItem[1] = new CiscoIPPhoneInputItemType();
                input.InputItem[1].DefaultValue = " ";
                input.InputItem[1].DisplayName = "Prénom";
                input.InputItem[1].InputFlags = "A";
                input.InputItem[1].QueryStringParam = "givenName";

                input.InputItem[0] = new CiscoIPPhoneInputItemType();
                input.InputItem[0].DefaultValue = " ";
                input.InputItem[0].DisplayName = "Nom";
                input.InputItem[0].InputFlags = "A";
                input.InputItem[0].QueryStringParam = "sn";

                input.InputItem[2] = new CiscoIPPhoneInputItemType();
                input.InputItem[2].DefaultValue = " ";
                input.InputItem[2].DisplayName = "Numéro de tél";
                input.InputItem[2].InputFlags = "T";
                input.InputItem[2].QueryStringParam = "telephonenumber";

                input.URL = this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "GetResultsForCiscoIPPhone?directory=" + System.Web.HttpUtility.UrlEncode(directory) + "&pos=0";
                //return input;
                CicsoIPPhoneInputTypeSerializer xml = new CicsoIPPhoneInputTypeSerializer();
                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings(); ;
                settings.Encoding = System.Text.Encoding.UTF8;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(ms, settings);
                System.Xml.Serialization.XmlSerializerNamespaces xmlnsEmpty = new System.Xml.Serialization.XmlSerializerNamespaces();
                xmlnsEmpty.Add("", "");
                xml.Serialize(xw, input, xmlnsEmpty);
                ms.Position = 0;
                this.Context.Response.ContentType = "text/xml";
                this.Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
                this.Context.Response.Write(GetStringFromStream(ms));
                //ms.Flush();
                //ms.Close();
            }
            catch (Exception e)
            {
                log.Error("Unable to build Cisco Ipphone Input Type: " + e.Message);
                this.Context.Response.Redirect(this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "Error?error=" + System.Web.HttpUtility.UrlEncode(e.Message), false);
                //return input;
            }
        }

        [WebMethod(Description = "Get results form from a particular directory for CiscoIPPhone")]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, XmlSerializeString = true)]
        public void GetResultsForCiscoIPPhone(string directory, string givenName, string sn, string telephonenumber, string pos)
        {
            string gn = "givenName";
            string name = "sn";
            string tel = "telephonenumber";
            string filter = "";
            CiscoIPPhoneDirectoryType dir = new CiscoIPPhoneDirectoryType();
            List<CiscoIPPhoneDirectoryEntryType> entry = new List<CiscoIPPhoneDirectoryEntryType>();
            try
            {
                dir.Title = "Recherche répertoire";
                foreach (DirectoryType dt in Global.directoryConfiguration)
                {
                    if (dt.name == directory)
                    {
                        if (dt.Item is SqlDatasourceType)
                        {
                            SqlDatasourceType sdt = dt.Item as SqlDatasourceType;
                            if (sdt.ipphonefilter != null)
                            {
                                gn = sdt.ipphonefilter.firstnamemap;
                                name = sdt.ipphonefilter.lastnamemap;
                                tel = sdt.ipphonefilter.telephonenumbermap;
                            }
                        }
                        else if (dt.Item is LdapDatasourceType)
                        {
                            LdapDatasourceType ldt = dt.Item as LdapDatasourceType;
                            if (ldt.ipphonefilter != null)
                            {
                                gn = ldt.ipphonefilter.firstnamemap;
                                name = ldt.ipphonefilter.lastnamemap;
                                tel = ldt.ipphonefilter.telephonenumbermap;
                            }
                        }
                        else if (dt.Item is CiscoDatasourceType)
                        {
                            CiscoDatasourceType cdt = dt.Item as CiscoDatasourceType;
                            if (cdt.ipphonefilter != null)
                            {
                                gn = cdt.ipphonefilter.firstnamemap;
                                name = cdt.ipphonefilter.lastnamemap;
                                tel = cdt.ipphonefilter.telephonenumbermap;
                            }
                        }
                        filter = gn + " LIKE '" + givenName.Trim() + "*' AND " + name + " LIKE '" + sn.Trim() + "*' AND " + tel + " LIKE '" + telephonenumber.Trim() + "*'";
                    }
                    
                }

                DataTable results = null;
                int identityCol = 0;
                int telephoneCol = 0;
                foreach (DirectoryType dt in Global.directoryConfiguration)
                {
                    if (dt.name == directory)
                    {

                        FieldFormatter[] ffs = null;
                        if (dt.Item is SqlDatasourceType)
                        {
                            ffs = ((SqlDatasourceType)dt.Item).fieldFormatters;
                        }
                        else if (dt.Item is LdapDatasourceType)
                        {
                            ffs = ((LdapDatasourceType)dt.Item).fieldFormatters;
                        }
                        else if (dt.Item is CiscoDatasourceType)
                        {
                            ffs = ((CiscoDatasourceType)dt.Item).fieldFormatters;
                        }
                        int cpt = 0;
                        foreach (FieldFormatter ff in ffs)
                        {
                            if (ff.fieldType == FieldType.Identity)
                            {
                                identityCol = cpt;
                            }
                            if (ff.fieldType == FieldType.Telephone)
                            {
                                telephoneCol = cpt;
                            }
                            
                            cpt++;
                        }
                        
                        break;
                    }
                }
                if (HttpRuntime.Cache.Get(directory + "_" + filter) != null)
                {
                    results = (DataTable)HttpRuntime.Cache.Get(directory + "_" + filter);
                }
                else
                {
                    if (HttpRuntime.Cache.Get(directory) != null)
                    {
                        DataSet fromCache = (DataSet)HttpRuntime.Cache.Get(directory);
                        DataView dv = null;
                        try
                        {
                            if (fromCache != null)
                            {
                                dv = fromCache.Tables[0].AsDataView();
                                dv.RowFilter = filter;
                                DataTable calcTable = dv.ToTable("CalcTable");
                                foreach (DirectoryType dt in Global.directoryConfiguration)
                                {
                                    if (dt.name == directory)
                                    {
                                        
                                        FieldFormatter[] ffs = null;
                                        List<string> cols = new List<string>();
                                        if (dt.Item is SqlDatasourceType)
                                        {
                                            ffs = ((SqlDatasourceType)dt.Item).fieldFormatters;
                                        }
                                        else if (dt.Item is LdapDatasourceType)
                                        {
                                            ffs = ((LdapDatasourceType)dt.Item).fieldFormatters;
                                        }
                                        else if (dt.Item is CiscoDatasourceType)
                                        {
                                            ffs = ((CiscoDatasourceType)dt.Item).fieldFormatters;
                                        }
                                        
                                        foreach (FieldFormatter ff in ffs)
                                        {
                                            
                                            cols.Add(ff.fieldName);
                                            if (!calcTable.Columns.Contains(ff.fieldName))
                                            {
                                                DataColumn dc = new DataColumn();
                                                dc.DataType = typeof(string);
                                                dc.ColumnName = ff.fieldName;
                                                dc.Expression = ff.value;
                                                calcTable.Columns.Add(dc);
                                            }
                                            
                                        }
                                        DataView sortedView = calcTable.AsDataView();
                                        if (cols.Count > 0)
                                        {
                                            sortedView.Sort = cols[identityCol];
                                        }
                                        results = sortedView.ToTable("Results", false, cols.ToArray());
                                        dv = results.AsDataView();
                                        dv.RowFilter = results.Columns[telephoneCol].ColumnName + " <> ''";
                                        results = dv.ToTable();
                                        HttpRuntime.Cache.Insert(directory + "_" + filter, results, null, DateTime.Now.AddMinutes(Double.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("DMDRefreshTimer"))), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                log.Debug("Cache is null, no data to retreive...");
                            }
                        }
                        catch (Exception e)
                        {
                            log.Error("Error while searching: " + e.Message);
                        }
                    }
                }
                if (results.Rows.Count > 0)
                {
                    if ((Int32.Parse(pos) + 32) >= results.Rows.Count)
                    {
                        dir.Prompt = "Enreg. " + (Int32.Parse(pos) + 1).ToString() + " à " + results.Rows.Count.ToString() + " sur " + results.Rows.Count.ToString();
                        dir.SoftKey = new CiscoIPPhoneSoftKeyType[4];

                        dir.SoftKey[0] = new CiscoIPPhoneSoftKeyType();
                        dir.SoftKey[0].Name = "Compos.";
                        dir.SoftKey[0].URL = "SoftKey:Dial";
                        dir.SoftKey[0].URLDown = "";
                        dir.SoftKey[0].Postion = 1;
                        dir.SoftKey[0].PostionSpecified = true;

                        dir.SoftKey[1] = new CiscoIPPhoneSoftKeyType();
                        dir.SoftKey[1].Name = "EditNum.";
                        dir.SoftKey[1].URL = "SoftKey:EditDial";
                        dir.SoftKey[1].URLDown = "";
                        dir.SoftKey[1].Postion = 2;
                        dir.SoftKey[1].PostionSpecified = true;


                        dir.SoftKey[2] = new CiscoIPPhoneSoftKeyType();
                        dir.SoftKey[2].Name = "Quitter";
                        dir.SoftKey[2].URL = "SoftKey:Exit";
                        dir.SoftKey[2].URLDown = "";
                        dir.SoftKey[2].Postion = 3;
                        dir.SoftKey[2].PostionSpecified = true;

                        dir.SoftKey[3] = new CiscoIPPhoneSoftKeyType();
                        dir.SoftKey[3].Name = "Recher.";
                        dir.SoftKey[3].URL = this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "SearchForCiscoIPPhone?directory=" + System.Web.HttpUtility.UrlEncode(directory);
                        dir.SoftKey[3].URLDown = "";
                        dir.SoftKey[3].Postion = 4;
                        dir.SoftKey[3].PostionSpecified = true;
                    }
                    else
                    {
                        dir.Prompt = "Enreg. " + (Int32.Parse(pos) + 1).ToString() + " à " + (Int32.Parse(pos) + 32).ToString() + " sur " + results.Rows.Count.ToString();
                        this.Context.Response.AddHeader("Refresh", ";url=" + this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "GetResultsForCiscoIPPhone?directory=" + System.Web.HttpUtility.UrlEncode(directory) + "&givenName=" +System.Web.HttpUtility.UrlEncode(givenName)+ "&sn=" +System.Web.HttpUtility.UrlEncode(sn)+ "&telephonenumber=" +System.Web.HttpUtility.UrlEncode(telephonenumber)+ "&pos=" + System.Web.HttpUtility.UrlEncode((Int32.Parse(pos) + 32).ToString()));
                        dir.SoftKey = new CiscoIPPhoneSoftKeyType[5];

                        dir.SoftKey[0] = new CiscoIPPhoneSoftKeyType();
                        dir.SoftKey[0].Name = "Compos.";
                        dir.SoftKey[0].URL = "SoftKey:Dial";
                        dir.SoftKey[0].URLDown = "";
                        dir.SoftKey[0].Postion = 1;
                        dir.SoftKey[0].PostionSpecified = true;

                        dir.SoftKey[1] = new CiscoIPPhoneSoftKeyType();
                        dir.SoftKey[1].Name = "EditNum.";
                        dir.SoftKey[1].URL = "SoftKey:EditDial";
                        dir.SoftKey[1].URLDown = "";
                        dir.SoftKey[1].Postion = 2;
                        dir.SoftKey[1].PostionSpecified = true;

                        dir.SoftKey[2] = new CiscoIPPhoneSoftKeyType();
                        dir.SoftKey[2].Name = "Quitter";
                        dir.SoftKey[2].URL = "SoftKey:Exit";
                        dir.SoftKey[2].URLDown = "";
                        dir.SoftKey[2].Postion = 3;
                        dir.SoftKey[2].PostionSpecified = true;

                        dir.SoftKey[3] = new CiscoIPPhoneSoftKeyType();
                        dir.SoftKey[3].Name = "Suivant";
                        dir.SoftKey[3].URL = "SoftKey:Update";
                        dir.SoftKey[3].URLDown = "";
                        dir.SoftKey[3].Postion = 4;
                        dir.SoftKey[3].PostionSpecified = true;

                        dir.SoftKey[4] = new CiscoIPPhoneSoftKeyType();
                        dir.SoftKey[4].Name = "Recher.";
                        dir.SoftKey[4].URL = this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "SearchForCiscoIPPhone?directory=" + System.Web.HttpUtility.UrlEncode(directory);
                        dir.SoftKey[4].URLDown = "";
                        dir.SoftKey[4].Postion = 5;
                        dir.SoftKey[4].PostionSpecified = true;
                    }
                    for (int cptRow = Int32.Parse(pos); cptRow <= Int32.Parse(pos) + 31; cptRow++)
                    {
                        if (cptRow < results.Rows.Count)
                        {
                            CiscoIPPhoneDirectoryEntryType dirEntry = new CiscoIPPhoneDirectoryEntryType();
                            dirEntry.Name = (string)results.Rows[cptRow][identityCol];
                            dirEntry.Telephone = (string)results.Rows[cptRow][telephoneCol];
                            entry.Add(dirEntry);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    dir.Prompt = "Pas d'enregistrement trouvé";
                    dir.SoftKey = new CiscoIPPhoneSoftKeyType[2];

                    dir.SoftKey[0] = new CiscoIPPhoneSoftKeyType();
                    dir.SoftKey[0].Name = "Quitter";
                    dir.SoftKey[0].URL = "SoftKey:Exit";
                    dir.SoftKey[0].URLDown = "";
                    dir.SoftKey[0].Postion = 3;
                    dir.SoftKey[0].PostionSpecified = true;

                    dir.SoftKey[1] = new CiscoIPPhoneSoftKeyType();
                    dir.SoftKey[1].Name = "Recher.";
                    dir.SoftKey[1].URL = this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "SearchForCiscoIPPhone?directory=" + System.Web.HttpUtility.UrlEncode(directory);
                    dir.SoftKey[1].URLDown = "";
                    dir.SoftKey[1].Postion = 1;
                    dir.SoftKey[1].PostionSpecified = true;
                }
                
                dir.DirectoryEntry = entry.ToArray();
                
                //return dir;
                CiscoIPPhoneDirectoryTypeSerializer xml = new CiscoIPPhoneDirectoryTypeSerializer();
                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings(); ;
                settings.Encoding = System.Text.Encoding.UTF8;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(ms, settings);
                System.Xml.Serialization.XmlSerializerNamespaces xmlnsEmpty = new System.Xml.Serialization.XmlSerializerNamespaces();
                xmlnsEmpty.Add("", "");
                xml.Serialize(xw, dir, xmlnsEmpty);
                ms.Position = 0;
                this.Context.Response.ContentType = "text/xml";
                this.Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
                this.Context.Response.Write(GetStringFromStream(ms));
            }
            catch (Exception e)
            {
                log.Error("Unable to build Cisco Ipphone Directory Type: " + e.Message);
                this.Context.Response.Redirect(this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "Error?error=" + System.Web.HttpUtility.UrlEncode(e.Message), false);
                //return dir;
            }
        }

        [WebMethod(Description = "Get error form for CiscoIPPhone")]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, XmlSerializeString = true)]
        public void Error(string error)
        {
            CiscoIPPhoneTextType er = new CiscoIPPhoneTextType();
            
            er.Prompt = "Erreur";
            er.Title = "La requête a échouée";
            er.Text = error;
            er.SoftKey = new CiscoIPPhoneSoftKeyType[1];
            er.SoftKey[0] = new CiscoIPPhoneSoftKeyType();
            er.SoftKey[0].Name = "Quitter";
            er.SoftKey[0].Postion = 3;
            er.SoftKey[0].PostionSpecified = true;
            er.SoftKey[0].URL = "SoftKey:Exit";
            er.SoftKey[0].URLDown = "";
            //return er;
            CiscoIPPhoneTextTypeSerializer xml = new CiscoIPPhoneTextTypeSerializer();
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings(); ;
            settings.Encoding = System.Text.Encoding.UTF8;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(ms, settings);
            System.Xml.Serialization.XmlSerializerNamespaces xmlnsEmpty = new System.Xml.Serialization.XmlSerializerNamespaces();
            xmlnsEmpty.Add("", "");
            xml.Serialize(xw, er, xmlnsEmpty);
            ms.Position = 0;
            this.Context.Response.ContentType = "text/xml";
            this.Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Context.Response.Write(GetStringFromStream(ms));
        }

        public string GetStringFromStream(System.IO.Stream s)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(s))
            {
                return reader.ReadToEnd();
            }
        }
    }

    [Serializable]
    public class LookupResponse
    {
        public string dirNum;
        public string name;
        public string[] callids;
    }
}
