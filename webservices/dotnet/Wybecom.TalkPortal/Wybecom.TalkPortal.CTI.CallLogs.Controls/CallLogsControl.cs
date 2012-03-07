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
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Wybecom.TalkPortal.CTI.CallLogs.Controls
{
    /// <summary>
    /// Call Logs AJAX control
    /// </summary>
    public class CallLogsControl : ScriptControl
    {
        public CallLogsControl()
        {
            //
            // TODO : ajoutez ici la logique du constructeur
            //
        }

        #region
        /// <summary>
        /// CSS Stylesheet
        /// </summary>
        public override string CssClass
        {
            get
            {
                String s = (String)ViewState["CssClass"];
                return ((s == null) ? "calllogs" : s);
            }
            set { ViewState["CssClass"] = value; }
        }
        /// <summary>
        /// AJAX CTI Control Serveur ID
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
        /// CallLogsServer URL (ProxyWebServices)
        /// </summary>
        public string callLogsService
        {
            get
            {
                String s = (String)ViewState["callLogsService"];
                return ((s == null) ? "http://"+System.Net.Dns.GetHostName()+"/ProxyWebServices/CallLogsServer.asmx" : s);
            }
            set
            {
                ViewState["callLogsService"] = value;
            }
        }
        /// <summary>
        /// DMD URL (DMDWS)
        /// </summary>
        public string directoryService
        {
            get
            {
                String s = (String)ViewState["directoryService"];
                return ((s == null) ? "http://" + System.Net.Dns.GetHostName() + "/Talk/DMDWS.asmx" : s);
            }
            set
            {
                ViewState["directoryService"] = value;
            }
        }
        /// <summary>
        /// Enable or disable presence
        /// </summary>
        public bool presenceEnabled
        {
            get
            {
                if (ViewState["presenceEnabled"] != null)
                {
                    return (bool)ViewState["presenceEnabled"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                ViewState["presenceEnabled"] = value;
            }
        }
        /// <summary>
        /// Enable or disable reverse lookup
        /// </summary>
        public bool lookupEnabled
        {
            get
            {
                if (ViewState["lookupEnabled"] != null)
                {
                    return (bool)ViewState["lookupEnabled"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                ViewState["lookupEnabled"] = value;
            }
        }
        /// <summary>
        /// The length of the telephone number from which presence is enable
        /// </summary>
        public int dirNumLength
        {
            get
            {
                if (ViewState["dirNumLength"] != null)
                {
                    return (int)ViewState["dirNumLength"];
                }
                else
                {
                    return 4;
                }
            }
            set
            {
                ViewState["dirNumLength"] = value;
            }
        }
        /// <summary>
        /// The header of missed call logs tab
        /// </summary>
        /// <value>Appels manqués</value>
        public string missedTabText
        {
            get
            {
                String s = (String)ViewState["missedTabText"];
                return ((s == null) ? "Appels manqués" : s);
            }
            set
            {
                ViewState["missedTabText"] = value;
            }
        }
        /// <summary>
        /// The header of placed call logs tab
        /// </summary>
        /// <value>Appels passés</value>
        public string placedTabText
        {
            get
            {
                String s = (String)ViewState["placedTabText"];
                return ((s == null) ? "Appels passés" : s);
            }
            set
            {
                ViewState["placedTabText"] = value;
            }
        }
        /// <summary>
        /// The header of received call logs tab
        /// </summary>
        /// <value>Appels reçus</value>
        public string receivedTabText
        {
            get
            {
                String s = (String)ViewState["receivedTabText"];
                return ((s == null) ? "Appels reçus" : s);
            }
            set
            {
                ViewState["receivedTabText"] = value;
            }
        }
        /// <summary>
        /// Text to display if call logs is empty
        /// </summary>
        /// <value>Aucune entrée disponible</value>
        public string emptyCallLogsText
        {
            get
            {
                String s = (String)ViewState["emptyCallLogsText"];
                return ((s == null) ? "Aucune entrée disponible" : s);
            }
            set
            {
                ViewState["emptyCallLogsText"] = value;
            }
        }
        /// <summary>
        /// Available image URL (only if presence is enabled)
        /// </summary>
        public string availableImageUrl
        {
            get
            {
                String s = (String)ViewState["availableImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.CTI.CallLogs.Controls.available.png") : s);
            }
            set
            {
                ViewState["availableImageUrl"] = value;
            }
        }
        /// <summary>
        /// Busy image URL (only if presence is enabled)
        /// </summary>
        public string busyImageUrl
        {
            get
            {
                String s = (String)ViewState["busyImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.CTI.CallLogs.Controls.busy.png") : s);
            }
            set
            {
                ViewState["busyImageUrl"] = value;
            }
        }
        /// <summary>
        /// Logoff image URL (only if presence is enabled)
        /// </summary>
        public string logoutImageUrl
        {
            get
            {
                String s = (String)ViewState["logoutImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.CTI.CallLogs.Controls.logout.png") : s);
            }
            set
            {
                ViewState["logoutImageUrl"] = value;
            }
        }
        /// <summary>
        /// Private image URL (only if presence is enabled)
        /// </summary>
        public string privateImageUrl
        {
            get
            {
                String s = (String)ViewState["privateImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.CTI.CallLogs.Controls.private.png") : s);
            }
            set
            {
                ViewState["privateImageUrl"] = value;
            }
        }
        /// <summary>
        /// Unknown image URL (only if presence is enabled)
        /// </summary>
        public string unknownImageUrl
        {
            get
            {
                String s = (String)ViewState["unknownImageUrl"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wybecom.TalkPortal.CTI.CallLogs.Controls.unknown.png") : s);
            }
            set
            {
                ViewState["unknownImageUrl"] = value;
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
        #endregion
        protected override void Render(HtmlTextWriter writer)
        {
            //<div id="calllogs" class="calllogs">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            //<div id="calllogs_content" class="content">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "content");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_content");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            //<div id="calllogs_left" class="leftmenu">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "leftmenu");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_left");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            //<div id="calllogs_toptab" class="toptab">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toptab");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_toptab");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            //</div>
            writer.RenderEndTag();

            //<div id="calllogs_missedcalltab" class="tab">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tab");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_missedcalltab");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write(this.missedTabText);
            //</div>
            writer.RenderEndTag();

            //<div id="calllogs_placedcalltab" class="tab_unselected">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tab_unselected");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_placedcalltab");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write(this.placedTabText);
            //</div>
            writer.RenderEndTag();

            //<div id="calllogs_receivedcalltab" class="tab_unselected">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tab_unselected");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_receivedcalltab");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write(this.receivedTabText);
            //</div>
            writer.RenderEndTag();

            //<div id="calllogs_bottomtab" class="bottomtab">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "bottomtab");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_bottomtab");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            //</div>
            writer.RenderEndTag();

            //</div>
            writer.RenderEndTag();

            //<div id="calllogs_right" class="rightcontent">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "rightcontent");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_right");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            //<div id="calllogs_missedcalltabcontent" class="tab_content">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tab_content");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_missedcalltabcontent");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "refresh");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_missedcallrefresh");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write("Rafraîchir");
            //</div>
            writer.RenderEndTag();


            //</div>
            writer.RenderEndTag();

            //<div id="calllogs_placedcalltabcontent" class="tab_content_unselected">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tab_content_unselected");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_placedcalltabcontent");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "refresh");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_placedcallrefresh");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write("Rafraîchir");
            //</div>
            writer.RenderEndTag();

            //</div>
            writer.RenderEndTag();

            //<div id="calllogs_receivedcalltabcontent" class="tab_content_unselected">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tab_content_unselected");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_receivedcalltabcontent");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "refresh");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_receivedcallrefresh");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write("Rafraîchir");
            //</div>
            writer.RenderEndTag();

            //</div>
            writer.RenderEndTag();

            //</div>
            writer.RenderEndTag();

            //</div>
            writer.RenderEndTag();
            //</div>
            writer.RenderEndTag();
            base.Render(writer);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ScriptManager.GetCurrent(this.Page).Services.Add(new ServiceReference(this.callLogsService));
            string csslink = "<link href='" +
                this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
                "Wybecom.TalkPortal.CTI.CallLogs.Controls.CallLogsControl.css") +
                "' rel='stylesheet' type='text/css' />";
            LiteralControl include = new LiteralControl(csslink);
            try
            {
                this.Page.Header.Controls.Add(include);
            }
            catch 
            {
            }
        }

        protected override IEnumerable<ScriptDescriptor>
                GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor("Wybecom.TalkPortal.CTI.CallLogs.Controls.CallLogsControl", this.ClientID);
            descriptor.AddProperty("callLogsService", this.callLogsService);
            string componentID = this.ResolveControlID();
            if (componentID != null && componentID != "")
            {
                descriptor.AddComponentProperty("ctiClient", componentID);
            }
            descriptor.AddProperty("presenceenabled", this.presenceEnabled);
            descriptor.AddProperty("lookupenabled", this.lookupEnabled);
            descriptor.AddProperty("dirNumLength", this.dirNumLength);
            descriptor.AddProperty("availableImageUrl", this.availableImageUrl);
            descriptor.AddProperty("busyImageUrl", this.busyImageUrl);
            descriptor.AddProperty("logoutImageUrl", this.logoutImageUrl);
            descriptor.AddProperty("privateImageUrl", this.privateImageUrl);
            descriptor.AddProperty("unknownImageUrl", this.unknownImageUrl);
            descriptor.AddProperty("emptyCallLogsText", this.emptyCallLogsText);
            descriptor.AddProperty("directoryService", this.directoryService);
            yield return descriptor;
        }

        protected string ResolveControlID()
        {
            string clientId = null;
            if (this.CTIClientID == "" || this.CTIClientID == null)
            {
                return clientId;
            }

            // See if the animation had a target
            if (!string.IsNullOrEmpty(this.CTIClientID))
            {
                // Try to find a control with the target's id by walking up the NamingContainer tree
                Control control = null;
                Control container = NamingContainer;
                while ((container != null) && ((control = container.FindControl(this.CTIClientID)) == null))
                {
                    container = container.Parent;
                    if ((control = this.FindChildControl(container, this.CTIClientID)) != null)
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
            }
            return clientId;
        }

        private Control FindChildControl(Control c, string clientId)
        {
            Control finded = null;
            foreach (Control ctrl in c.Controls)
            {
                if ((finded = ctrl.FindControl(clientId)) != null)
                {
                    break;
                }
            }
            return finded;
        }

        // Générez la référence de script
        protected override IEnumerable<ScriptReference>
                GetScriptReferences()
        {
            yield return new ScriptReference("Wybecom.TalkPortal.CTI.CallLogs.Controls.CallLogsControl.js", this.GetType().Assembly.FullName);
        }
    }
}