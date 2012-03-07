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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.ComponentModel;
using System.Text;
using AjaxControlToolkit;
using log4net;
using Wybecom.TalkPortal.DMD.Proxy;

namespace Wybecom.TalkPortal.DMD.Controls
{
    /// <summary>
    /// DMD AJAX client
    /// </summary>
    /// 
    [ToolboxData("<{0}:DMDClient runat=server></{0}:DMDClient>")]
    [ParseChildren(true)]
    [PersistChildren(true)]
    public class DMDClient : ScriptControl, INamingContainer
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region
        /// <summary>
        ///  Show or hide header
        /// </summary>
        public bool ShowHeader
        {
            get
            {
                if (ViewState["ShowHeader"] != null)
                {
                    return (bool)ViewState["ShowHeader"];
                }
                else
                {
                    return true;
                }
            }
            set
            {
                ViewState["ShowHeader"] = value;
            }
        }
        /// <summary>
        /// The id of the element from which display the control
        /// </summary>
        public string TargetControlID
        {
            get
            {
                return (string)ViewState["TargetControlID"];
            }
            set
            {
                ViewState["TargetControlID"] = value;
            }
        }
        /// <summary>
        /// CSS stylesheet
        /// </summary>
        public override string CssClass
        {
            get {
                String s = (String)ViewState["CssClass"];
                return ((s == null) ? "dmdclient" : s);
            }
            set { ViewState["CssClass"] = value; }
        }
        /// <summary>
        /// The DMD name
		/// <seealso cref="DirectoryType"/>
        /// </summary>
        public string DirectoryName
        {
            get
            {
                String s = (String)ViewState["DirectoryName"];
                if (s != null && s != "")
                {
                    return s;
                }
                else
                {
                    this.Show = false;
                    return "";
                }
            }
            set
            {
                ViewState["DirectoryName"] = value;
            }
        }
        /// <summary>
        /// The header to display in the search section
        /// </summary>
        public string SearchHeader
        {
            get
            {
                String s = (String)ViewState["SearchHeader"];
                return ((s == null) ? "Search" : s);
            }
            set
            {
                ViewState["SearchHeader"] = value;
            }
        }
        /// <summary>
        /// The header to display in the result section
        /// </summary>
        public string ResultsHeader
        {
            get
            {
                String s = (String)ViewState["ResultsHeader"];
                return ((s == null) ? "Results" : s);
            }
            set
            {
                ViewState["ResultsHeader"] = value;
            }
        }
        /// <summary>
        /// The CTI Control server ID
        /// </summary>
        public string CTIClientID
        {
            get
            {
                String s = (String)ViewState["CTIClientID"];
                return ((s == null) ? "" : s);
            }
            set
            {
                ViewState["CTIClientID"] = value;
            }
        }
        /// <summary>
        /// DMD web service URL (DMDWS)
        /// </summary>
        public string dmdService
        {
            get
            {
                String s = (String)ViewState["dmdService"];
                return ((s == null) ? "http://" + System.Net.Dns.GetHostName() + "/Talk/DMDWS.asmx" : s);
            }
            set
            {
                ViewState["dmdService"] = value;
            }
        }
        /// <summary>
        /// Enable or disable sorting
        /// </summary>
        public bool SortEnabled
        {
            get
            {
                bool s  = true;
                if (ViewState["SortEnabled"] != null)
                {
                    s = (bool)ViewState["SortEnabled"];
                }
                return s;
            }
            set
            {
                ViewState["SortEnabled"] = value;
            }
        }
        /// <summary>
        /// Enable or disable paging
        /// </summary>
        public bool PageEnabled
        {
            get
            {
                bool s = true;
                if (ViewState["PageEnabled"] != null)
                {
                    s = (bool)ViewState["PageEnabled"];
                }
                return s;
            }
            set
            {
                ViewState["PageEnabled"] = value;
            }
        }
        /// <summary>
        /// Available image URL (only when presence is enabled)
        /// </summary>
        public string availableImageUrl
        {
            get
            {
                String s = (String)ViewState["availableImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.available.png") : s);
            }
            set
            {
                ViewState["availableImageUrl"] = value;
            }
        }
        /// <summary>
        /// Busy image URL (only when presence is enabled)
        /// </summary>
        public string busyImageUrl
        {
            get
            {
                String s = (String)ViewState["busyImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.busy.png") : s);
            }
            set
            {
                ViewState["busyImageUrl"] = value;
            }
        }
        /// <summary>
        /// Logged off image URL (only when presence is enabled)
        /// </summary>
        public string logoutImageUrl
        {
            get
            {
                String s = (String)ViewState["logoutImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.logout.png") : s);
            }
            set
            {
                ViewState["logoutImageUrl"] = value;
            }
        }
        /// <summary>
        /// Private image URL (only when presence is enabled)
        /// </summary>
        public string privateImageUrl
        {
            get
            {
                String s = (String)ViewState["privateImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.private.png") : s);
            }
            set
            {
                ViewState["privateImageUrl"] = value;
            }
        }
        /// <summary>
        /// Unknown image URL (only when presence is enabled)
        /// </summary>
        public string unknownImageUrl
        {
            get
            {
                String s = (String)ViewState["unknownImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.unknown.png") : s);
            }
            set
            {
                ViewState["unknownImageUrl"] = value;
            }
        }
        /// <summary>
        /// Direct transfer image URL (only when presence is enabled)
        /// </summary>
        public string directTransferImageUrl
        {
            get
            {
                String s = (String)ViewState["directTransferImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.directtransfer.png") : s);
            }
            set
            {
                ViewState["directTransferImageUrl"] = value;
            }
        }
        /// <summary>
        /// Consult transfer image URL (only when presence is enabled)
        /// </summary>
        public string consultTransferImageUrl
        {
            get
            {
                String s = (String)ViewState["consultTransferImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.consulttransfer.png") : s);
            }
            set
            {
                ViewState["consultTransferImageUrl"] = value;
            }
        }
        /// <summary>
        /// Forwarded image URL (only when presence is enabled)
        /// </summary>
        public string dnForwardImageUrl
        {
            get
            {
                String s = (String)ViewState["dnForwardImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.forwardalldn.png") : s);
            }
            set
            {
                ViewState["dnForwardImageUrl"] = value;
            }
        }
        /// <summary>
        /// Forwarded to voicemail image URL (only when presence is enabled)
        /// </summary>
        public string mevoForwardImageUrl
        {
            get
            {
                String s = (String)ViewState["mevoForwardImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.forwardallmevo.png") : s);
            }
            set
            {
                ViewState["mevoForwardImageUrl"] = value;
            }
        }
        /// <summary>
        /// DND image URL (only when presence is enabled)
        /// </summary>
        public string dndImageUrl
        {
            get
            {
                String s = (String)ViewState["dndImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.donotdisturb.png") : s);
            }
            set
            {
                ViewState["dndImageUrl"] = value;
            }
        }
        /// <summary>
        /// Add to speeddial image URL (only when EnableSpeedDials is true)
        /// </summary>
        public string speeddialImageUrl
        {
            get
            {
                String s = (String)ViewState["speeddialImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.bookmark-new.png") : s);
            }
            set
            {
                ViewState["speeddialImageUrl"] = value;
            }
        }
        /// <summary>
        /// Monitor image URL (only when presence is enabled)
        /// </summary>
        public string monitorImageUrl
        {
            get
            {
                String s = (String)ViewState["monitorImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.DMD.Controls.monitor.png") : s);
            }
            set
            {
                ViewState["monitor"] = value;
            }
        }
        /// <summary>
        /// Enable or disable direct transfer
        /// </summary>
        public bool enableTransfer
        {
            get
            {
                if (ViewState["enableTransfer"] != null)
                {
                    return (bool)ViewState["enableTransfer"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                ViewState["enableTransfer"] = value;
            }
        }
        /// <summary>
        /// Enable or disable consult transfer
        /// </summary>
        public bool enableConsultTransfer
        {
            get
            {
                if (ViewState["enableConsultTransfer"] != null)
                {
                    return (bool)ViewState["enableConsultTransfer"];
                }
                else
                {
                    return true;
                }
            }
            set
            {
                ViewState["enableConsultTransfer"] = value;
            }
        }
        /// <summary>
        /// Enable or disable direct transfer for GSM <seealso cref="FielFormatter"/>
        /// </summary>
        public bool enableGSMTransfer
        {
            get
            {
                if (ViewState["enableGSMTransfer"] != null)
                {
                    return (bool)ViewState["enableGSMTransfer"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                ViewState["enableGSMTransfer"] = value;
            }
        }
        /// <summary>
        /// Enable or disable consult transfer for GSM <seealso cref="FielFormatter"/>
        /// </summary>
        public bool enableGSMConsultTransfer
        {
            get
            {
                if (ViewState["enableGSMConsultTransfer"] != null)
                {
                    return (bool)ViewState["enableGSMConsultTransfer"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                ViewState["enableGSMConsultTransfer"] = value;
            }
        }
        /// <summary>
        /// Enable or disable consult transfer for GSM <seealso cref="FielFormatter"/>
        /// </summary>
        public bool enableMonitor
        {
            get
            {
                if (ViewState["enableMonitor"] != null)
                {
                    return (bool)ViewState["enableMonitor"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                ViewState["enableMonitor"] = value;
            }
        }
        /// <summary>
        /// Show or hide the control
        /// </summary>
        public bool Show
        {
            get
            {
                bool s = true;
                if (ViewState["Show"] != null)
                {
                    s = (bool)ViewState["Show"];
                }
                return s;
            }
            set
            {
                ViewState["Show"] = value;
            }
        }
        /// <summary>
        /// AEnable or disable speeddials
        /// </summary>
        public bool EnableSpeedDials
        {
            get
            {
                bool s = false;
                if (ViewState["EnableSpeedDials"] != null)
                {
                    s = (bool)ViewState["EnableSpeedDials"];
                }
                return s;
            }
            set
            {
                ViewState["EnableSpeedDials"] = value;
            }
        }

        /// <summary>
        /// Enable or disable reverse lookup
        /// </summary>
        public bool EnableTransferLookup
        {
            get
            {
                bool s = false;
                if (ViewState["EnableTransferLookup"] != null)
                {
                    s = (bool)ViewState["EnableTransferLookup"];
                }
                return s;
            }
            set
            {
                ViewState["EnableTransferLookup"] = value;
            }
        }

        /// <summary>
        /// Enable or disable alerts
        /// </summary>
        public bool showAlert
        {
            get
            {
                bool s = false;
                if (ViewState["showAlert"] != null)
                {
                    s = (bool)ViewState["showAlert"];
                }
                return s;
            }
            set { ViewState["showAlert"] = value; }
        }

        /// <summary>
        /// Number or row per page
        /// </summary>
        public int rowsPerPage
        {
            get
            {
                int s = 15;
                if (ViewState["RowsPerPage"] != null)
                {
                    s = (int)ViewState["RowsPerPage"];
                }
                return s;
            }
            set
            {
                ViewState["RowsPerPage"] = value;
            }
        }

        public string[] Directories
        {
            get
            {
                string[] dir = null;
                if (String.IsNullOrEmpty(this.dmdService))
                {
                    DMDWebService dmdws = new DMDWebService(this.dmdService);
                    dir = dmdws.GetDirectories();
                }
                return dir;
            }
        }


        private FilterControlCollection Filters = new FilterControlCollection();

        /// <summary>
        /// An array of filters
        /// </summary>
        /// <seealso cref="FilterControl"/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public FilterControlCollection FilterGroup
        {
            get { return Filters; }
        }

        

        #endregion

        public DMDClient()
        {
            //
            // TODO : ajoutez ici la logique du constructeur
            //
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            if (!this.Show)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass + "_searchpanel");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (this.ShowHeader)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass + "_searchheader");
            
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(this.SearchHeader);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass + "_searchcontent");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            
            foreach (FilterControl fc in FilterGroup)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Align, "right");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(fc.Label);
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Align, "left");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                fc.RenderControl(writer);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
            writer.RenderEndTag();
            
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "Chercher");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_button_search");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            string resultcomponent = this.ResolveControlID(this.TargetControlID);
            if (resultcomponent == null || resultcomponent == "")
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass + "_resultspanel");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass + "_resultsheader");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(this.ResultsHeader);
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_results");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
            //base.Render(writer);
            if (!this.DesignMode)
            {

                ScriptManager.GetCurrent(Page).RegisterScriptDescriptors(this);
                foreach (Control c in Controls)
                {
                    if (c is AutoCompleteExtender)
                    {
                        ((AutoCompleteExtender)c).RenderControl(writer) ;
                    }
                }
            }
            
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            
            foreach (FilterControl fc in FilterGroup)
            {
                this.Controls.Add(fc);
                if (fc.AutoCompletePrefixLength > 0)
                {
                    AutoCompleteExtender ace = new AutoCompleteExtender();
                    ace.ID = "ACE_" + fc.ID;
                    ace.Enabled = true;
                    ace.TargetControlID = fc.ID;
                    ace.ServiceMethod = "AutoCompleteFilter";
                    ace.ServicePath = this.dmdService;
                    ace.MinimumPrefixLength = fc.AutoCompletePrefixLength;
                    ace.CompletionInterval = 750;
                    ace.ContextKey = this.DirectoryName + "," + fc.Filter;
                    ace.EnableCaching = false;
                    ace.CompletionSetCount = 100;
                    ace.CompletionListCssClass = fc.CompletionListCssClass;
                    ace.CompletionListItemCssClass = fc.CompletionListItemCssClass;
                    ace.CompletionListHighlightedItemCssClass = fc.CompletionHighlightedListItemCssClass;
                    this.Controls.Add(ace);
                }
            }
            
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string csslink = "<link href='" +
                this.Page.ClientScript.GetWebResourceUrl(this.GetType(), 
                "Wybecom.TalkPortal.DMD.Controls.DMDClient.css") + 
                "' rel='stylesheet' type='text/css' />";
            csslink += "<link href='" +
                this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
                "Wybecom.TalkPortal.DMD.Controls.popup.css") +
                "' rel='stylesheet' type='text/css' />";
            string scriptlink = "<script type=\"text/javascript\" src=\"" +
                this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
                "Wybecom.TalkPortal.DMD.Controls.jquery-1.2.6.min.js") +
                "\"></script>";
            scriptlink += "<script type=\"text/javascript\" src=\"" +
                this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
                "Wybecom.TalkPortal.DMD.Controls.popup.js") +
                "\"></script>";
            LiteralControl include = new LiteralControl(csslink + scriptlink);
            try
            {
                this.Page.Header.Controls.Add(include);
            }
            catch (Exception headerException)
            {
                log.Error("Unable to add css and jquery: " + headerException.Message);
            }
            ScriptManager.GetCurrent(this.Page).Services.Add(new ServiceReference(this.dmdService));
        }

        protected override IEnumerable<ScriptDescriptor>
                GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor("Wybecom.TalkPortal.DMD.Controls.DMDClient", this.ClientID);
            descriptor.AddScriptProperty("filterGroup", FilterGroup.ToJSConstructor());
            string componentID = this.ResolveControlID(this.CTIClientID);
            if (componentID != null && componentID != "")
            {
                descriptor.AddComponentProperty("ctiClient", componentID);
            }
            
            descriptor.AddProperty("target", this.ResolveControlID(this.TargetControlID));
            descriptor.AddProperty("dmdService", this.dmdService);
            //descriptor.AddProperty("callableFields", this.callableFields);
            //descriptor.AddProperty("presenceField", this.presenceField);
            descriptor.AddProperty("sortEnabled", this.SortEnabled);
            descriptor.AddProperty("pageEnabled", this.PageEnabled);
            descriptor.AddProperty("directoryName", this.DirectoryName);
            descriptor.AddProperty("availableImageUrl", this.availableImageUrl);
            descriptor.AddProperty("busyImageUrl", this.busyImageUrl);
            descriptor.AddProperty("logoutImageUrl", this.logoutImageUrl);
            descriptor.AddProperty("privateImageUrl", this.privateImageUrl);
            descriptor.AddProperty("unknownImageUrl", this.unknownImageUrl);
            descriptor.AddProperty("directTransferImageUrl", this.directTransferImageUrl);
            descriptor.AddProperty("consultTransferImageUrl", this.consultTransferImageUrl);
            descriptor.AddProperty("dnForwardImageUrl", this.dnForwardImageUrl);
            descriptor.AddProperty("mevoForwardImageUrl", this.mevoForwardImageUrl);
            descriptor.AddProperty("dndImageUrl", this.dndImageUrl);
            descriptor.AddProperty("enableTransfer", this.enableTransfer);
            descriptor.AddProperty("enableConsultTransfer", this.enableConsultTransfer);
            descriptor.AddProperty("speeddialImageUrl", this.speeddialImageUrl);
            descriptor.AddProperty("monitorImageUrl", this.monitorImageUrl);
            descriptor.AddProperty("enableSpeedDials", this.EnableSpeedDials);
            descriptor.AddProperty("enableTransferLookup", this.EnableTransferLookup);
            descriptor.AddProperty("enableMonitor", this.enableMonitor);
            descriptor.AddProperty("rowsPerPage", this.rowsPerPage);
            yield return descriptor;
        }

        protected string ResolveControlID(string id)
        {
            log.Debug("Search for control: " + id);
            string clientId = null;
            if (id == "" || id == null)
            {
                return clientId;
            }

            // See if the animation had a target
            if (!string.IsNullOrEmpty(id))
            {
                // Try to find a control with the target's id by walking up the NamingContainer tree
                Control control = null;
                Control container = NamingContainer;
                while ((container != null) && ((control = container.FindControl(id)) == null))
                {
                    log.Debug("Search in parent control hierarchy from " + container.ID);
                    container = container.Parent;
                    if ((control = this.FindChildControl(container, id)) != null)
                    {
                        break;
                    }
                }

                // If we found a control
                if (control != null)
                {
                    // Map the server ID to the client ID
                    clientId = control.ClientID;
                }
                else
                {
                    log.Debug("No control finded for " + id);
                }
            }
            return clientId;
        }

        private Control FindChildControl(Control c, string clientId)
        {
            Control finded = null;
            if (c != null)
            {
                foreach (Control ctrl in c.Controls)
                {
                    if ((finded = ctrl.FindControl(clientId)) != null)
                    {
                        break;
                    }
                }
            }
            else
            {
                log.Debug("No parent from this control");
            }
            return finded;
        }

        // Générez la référence de script
        protected override IEnumerable<ScriptReference>
                GetScriptReferences()
        {
            yield return new ScriptReference("Wybecom.TalkPortal.DMD.Controls.DMDClient.js", this.GetType().Assembly.FullName);
        }
    }
    /// <summary>
    /// Collection of FilterControl
    /// </summary>
    /// <seealso cref="FilterControl"/>
    public class FilterControlCollection : CollectionBase
    {
        public FilterControlCollection() { }

        public FilterControl this[int index]
        {
            get { return (FilterControl)this.List[index]; }
            set { this.List[index] = value; }
        }

        public void Add(FilterControl fc)
        {
            this.List.Add(fc);
        }

        public void Insert(int index, FilterControl fc)
        {
            this.List.Insert(index, fc);
        }

        public void Remove(FilterControl fc)
        {
            this.List.Remove(fc);
        }

        public bool Contains(FilterControl fc)
        {
            return this.List.Contains(fc);
        }

        public int IndexOf(FilterControl fc)
        {
            return this.List.IndexOf(fc);
        }

        public void CopyTo(FilterControl[] array, int index)
        {
            this.List.CopyTo(array, index);
        }

        internal string ToJSConstructor()
        {
            StringBuilder output = new StringBuilder();

            output.Append("[");

            for (int i = 0; i < InnerList.Count; i++)
            {
                if (i > 0)
                {
                    output.Append(", ");
                }

                output.Append(((FilterControl)InnerList[i]).CreateScript);
            }

            output.Append("]");

            return output.ToString();
        }
    }

    /// <summary>
    /// Search filter
    /// </summary>
    [ToolboxData("<{0}:FilterControl runat=server></{0}:FilterControl>")]
    public class FilterControl : TextBox
    {
        public FilterControl()
        {
            this.EnableViewState = true;
        }

        public FilterControl(string lbl, string flt)
        {
            Label = lbl;
            Filter = flt;
        }

        /// <summary>
        /// Header field
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("Filter")]
        public string Label
        {
            get
            {
                String s = (String)ViewState["Label"];
                return ((s == null) ? "Filter" : s);
            }

            set
            {
                ViewState["Label"] = value;
            }
        }

        /// <summary>
        /// Filter name
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        public string Filter
        {
            get
            {
                String s = (String)ViewState["Filter"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Filter"] = value;
            }
        }

        /// <summary>
        /// Completion css stylesheet
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("completionListElement")]
        public string CompletionListCssClass
        {
            get
            {
                String s = (String)ViewState["CompletionListCssClass"];
                return ((s == null) ? "completionListElement" : s);
            }

            set
            {
                ViewState["CompletionListCssClass"] = value;
            }
        }

        /// <summary>
        /// Completion list item stylesheet
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("completionListItem")]
        public string CompletionListItemCssClass
        {
            get
            {
                String s = (String)ViewState["CompletionListItemCssClass"];
                return ((s == null) ? "completionListItem" : s);
            }

            set
            {
                ViewState["CompletionListItemCssClass"] = value;
            }
        }

        /// <summary>
        /// Highlighted list item stylesheet
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("completionHighlightedListItem")]
        public string CompletionHighlightedListItemCssClass
        {
            get
            {
                String s = (String)ViewState["CompletionHighlightedListItemCssClass"];
                return ((s == null) ? "completionHighlightedListItem" : s);
            }

            set
            {
                ViewState["CompletionHighlightedListItemCssClass"] = value;
            }
        }


        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool IsOptional
        {
            get
            {
                bool s = false;
                if (ViewState["IsOptional"] != null)
                {
                    s = (bool)ViewState["IsOptional"];
                }
                return s;
            }

            set
            {
                ViewState["IsOptional"] = value;
            }
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(-1)]
        public int AutoCompletePrefixLength
        {
            get
            {
                int s = -1;
                if (ViewState["AutoCompletePrefixLength"] != null)
                {
                    s = (int)ViewState["AutoCompletePrefixLength"];
                }
                return s;
            }

            set
            {
                ViewState["AutoCompletePrefixLength"] = value;
            }
        }


        private FilterControlScriptDescriptor CreateDescriptor()
        {
            return new FilterControlScriptDescriptor(this.GetType().FullName);
        }

        protected FilterControlScriptDescriptor BuildDescriptor()
        {
            FilterControlScriptDescriptor desc = CreateDescriptor();
            desc.AddProperty("label", Label);
            desc.AddProperty("filter", Filter);
            desc.AddProperty("text", ClientID);
            desc.AddProperty("isOptional", IsOptional);
            desc.AddProperty("autoCompletePrefixLength", AutoCompletePrefixLength);
            return desc;
        }

        internal string CreateScript
        {
            get
            {
                string script = BuildDescriptor().CreateScript;
                if (script.EndsWith(";"))
                {
                    script = script.Substring(0, script.Length - 1);
                }
                return script;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            
            base.Render(writer);
            
        }
        
    }
}