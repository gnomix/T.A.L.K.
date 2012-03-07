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
using System.ComponentModel;
using Wybecom.TalkPortal.Providers;

using System.IO;


namespace Wybecom.TalkPortal.CTI.Controls
{
    /// <summary>
    /// AJAX CTI Control
    /// </summary>
    /// <example>
    /// Monitoring 1000 extension in trusting mode
    /// <code>
    /// <script type="text/javascript" language="javascript">
    ///    function OnCallReceived(sender,eventArgs) {
    ///        alert(eventArgs.get_caller());
    ///    }
    ///    function OnCallEnded(sender, eventArgs) {
    ///        lcs = eventArgs.get_lineControlConnection();
    ///        for (lcCompteur = 0; lcCompteur < lcs.length; lcCompteur = lcCompteur + 1) {
    ///            var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
    ///            var lcc = lcs[lcCompteur];
    ///            alert(lcc.callid + ': ' + lcc.contact);
    ///        }
    ///    }
    ///    function OnDirectoryClicked(sender, eventArgs) {
    ///    }
    ///    function OnExceptionHandler(sender, eventArgs) {
    ///        alert("Stack Trace: " + eventArgs.get_stackTrace() + "/r/n" +
    ///      "Error: " + eventArgs.get_message() + "/r/n" +
    ///      "Status Code: " + eventArgs.get_statusCode() + "/r/n" +
    ///      "Exception Type: " + eventArgs.get_exceptionType() + "/r/n" +
    ///      "Timed Out: " + eventArgs.get_timedOut());
    ///    }
    /// </script>
    /// <cc1:CTIClient ID="CTIClient1" runat="server" display="true" displayCopyright="false" 
    /// displayInput="false" displayPhoneControl="true" displayStatus="true" monitoredLine="1000" 
    /// OnCallReceived="OnCallReceived" 
    /// OnCallEnded="OnCallEnded" 
    /// DirectoryClicked="OnDirectoryClicked"  
    /// MevoPilot="965999" enableDnd="false" 
    /// callLogsService="http://localhost/ProxyWebServices/CallLogsServer.asmx" 
    /// ctiService="http://localhost/ProxyWebServices/CTIServer.asmx" 
    /// stateService="http://localhost/ProxyWebServices/LineControlServer.asmx" Mode="sso" 
    /// ExceptionHandler="OnExceptionHandler"/>
    /// </code>
    /// </example>
    [ToolboxData("<{0}:CTIClient runat=server></{0}:CTIClient>")]
    public class CTIClient : ScriptControl
    {
        public CTIClient()
        {
            
        }

        #region
        //private members
        private bool _display = false;
        private bool _displayInput = false;
        private bool _displayCopyright = false;
        private bool _displayPhoneControl = false;
        private bool _displayStatus = false;
        private string _monitoredLine;
        private string _onCallInitialized;
        private string _onCallReceived;
        private string _onCallEnded;
        private string _onNewCallToCodif;
        private string _directoryClicked;
        private string _exceptionHandler;
        private string _mevoPilot;
        private bool _enableTransfer = false;
        private bool _enableConsultTransfer = true;
        private bool _enableHold = true;
        private bool _enableDnd = true;
        private bool _enableMevo = true;
        private bool _enableDirectory = true;
        private bool _enableCallLogs = true;
        private bool _showAlert = false;
        private ScriptManager sm;

        private static readonly byte[] encryptionKeyBytes =
            new byte[] { 0x36, 0x37, 0x46, 0x31, 0x42, 0x41, 0x39, 0x43 };
        #endregion

        #region
        //public members
        /// <summary>
        /// Makes the control visible or not
        /// </summary>
        public bool display
        {
            get { return _display; }
            set { _display = value; }
        }
        /// <summary>
        /// Makes the text field visible or not
        /// </summary>
        public bool displayInput
        {
            get { return _displayInput; }
            set { _displayInput = value; }
        }
        /// <summary>
        /// Makes the copyright visible or not
        /// </summary>
        public bool displayCopyright
        {
            get { return this._displayCopyright; }
            set { this._displayCopyright = value; }
        }
        /// <summary>
        /// Makes the button zone visible or not
        /// </summary>
        public bool displayPhoneControl
        {
            get { return this._displayPhoneControl; }
            set { this._displayPhoneControl = value; }
        }
        /// <summary>
        /// Makes the status zone visible or not
        /// </summary>
        public bool displayStatus
        {
            get { return this._displayStatus; }
            set { this._displayStatus = value; }
        }
        /// <summary>
        /// The line to monitor
        /// </summary>
        public string monitoredLine
        {
            get {return this._monitoredLine;}
            set { this._monitoredLine = value; }
        }
        /// <summary>
        /// CTIServer URL (ProxyWebServices)
        /// </summary>
        public string ctiService
        {
            get
            {
                String s = (String)ViewState["ctiService"];
                return ((s == null) ? "http://" + System.Net.Dns.GetHostName() + "/ProxyWebServices/CTIServer.asmx" : s);
            }
            set
            {
                ViewState["ctiService"] = value;
            }
        }

        /// <summary>
        /// DMD URL (DMDWS)
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
        /// User name
        /// </summary>
        public string User
        {
            get
            {
                String user = "";
                switch (Mode)
                {
                    case AuthenticationMode.trusted:
                        user = HttpContext.Current.User.Identity.Name;
                        break;
                    case AuthenticationMode.manual:
                        user = (String)ViewState["User"];
                        break;
                    case AuthenticationMode.sso:
                        user = HttpContext.Current.User.Identity.Name;
                        break;
                }
                return user;
            }
            set
            {
                ViewState["User"] = value;
            }
        }
        /// <summary>
        /// User password
        /// </summary>
        public string Password
        {
            get
            {
                String password = "";
                switch (Mode)
                {
                    case AuthenticationMode.trusted:
                        password = "";
                        break;
                    case AuthenticationMode.manual:
                        password = (String)ViewState["Password"];
                        break;
                    case AuthenticationMode.sso:
                        password = "";
                        break;
                }
                return password;
            }
            set
            {
                ViewState["Password"] = value;
            }
        }
        /// <summary>
        /// Authentication mode
        /// <seealso cref="Wybecom.TalkPortal.CTI.Controls.AuthenticationMode"/>
        /// </summary>
        public AuthenticationMode Mode
        {
            get
            {
                AuthenticationMode mode = AuthenticationMode.trusted;
                if (ViewState["Mode"] != null)
                {
                    mode = (AuthenticationMode)ViewState["Mode"];
                }
                return mode;
            }
            set
            {
                ViewState["Mode"] = value;
            }
        }
        /// <summary>
        /// Codification mode
        /// <seealso cref="Wybecom.TalkPortal.CTI.Controls.CodifMode"/>
        /// </summary>
        public CodifMode CodifMode
        {
            get
            {
                CodifMode mode = CodifMode.allcalls;
                if (ViewState["CodifMode"] != null)
                {
                    mode = (CodifMode)ViewState["CodifMode"];
                }
                return mode;
            }
            set
            {
                ViewState["CodifMode"] = value;
            }
        }
        /// <summary>
        /// LineControlServer URL (ProxyWebServices)
        /// </summary>
        public string stateService
        {
            get
            {
                String s = (String)ViewState["stateService"];
                return ((s == null) ? "http://" + System.Net.Dns.GetHostName() + "/ProxyWebServices/LineControlServer.asmx" : s);
            }
            set
            {
                ViewState["stateService"] = value;
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
                return ((s == null) ? "http://" + System.Net.Dns.GetHostName() + "/ProxyWebServices/CallLogsServer.asmx" : s);
            }
            set
            {
                ViewState["callLogsService"] = value;
            }
        }

        /// <summary>
        /// SpeedDialServer URL (ProxyWebServices)
        /// </summary>
        public string speedDialService
        {
            get
            {
                String s = (String)ViewState["speedDialService"];
                return ((s == null) ? "http://" + System.Net.Dns.GetHostName() + "/ProxyWebServices/SpeedDialServer.asmx" : s);
            }
            set
            {
                ViewState["speedDialService"] = value;
            }
        }

        /// <summary>
        /// Call initiliazed handler
        /// </summary>
        /// <example>
        /// <code>
        /// <script language="javascript" type="text/javascript">
        /// function OnCallInitialized(sender, eventArgs) {
        ///    alert(eventArgs.get_callee());
        /// }
        /// </script>
        /// <cc1:CTIClient ID="CTIClient1" runat="server" display="true" 
        /// displayCopyright="false" displayInput="false" displayPhoneControl="true" 
        /// displayStatus="true" monitoredLine="1000" 
        /// OnCallReceived="OnCallReceived" OnCallInitialized="OnCallInitialized"
        /// MevoPilot="965999" enableDnd="false" Mode="sso"/<
        /// </code>
        /// </example>
        public string OnCallInitialized
        {
            get { return this._onCallInitialized; }
            set { this._onCallInitialized = value; }
        }

        /// <summary>
        /// Call received handler
        /// </summary>
        /// <example>
        /// <code>
        /// <script language="javascript" type="text/javascript">
        /// function OnCallReceived(sender, eventArgs) {
        ///    alert(eventArgs.get_caller());
        /// }
        /// </script>
        /// <cc1:CTIClient ID="CTIClient1" runat="server" display="true" 
        /// displayCopyright="false" displayInput="false" displayPhoneControl="true" 
        /// displayStatus="true" monitoredLine="1000" 
        /// OnCallReceived="OnCallReceived" 
        /// MevoPilot="965999" enableDnd="false" Mode="sso"/<
        /// </code>
        /// </example>
        public string OnCallReceived
        {
            get { return this._onCallReceived; }
            set { this._onCallReceived = value; }
        }
        /// <summary>
        /// End call handler
        /// </summary>
        /// <example>
        /// <code>
        /// <script language="javascript" type="text/javascript">
        /// function OnCallEnded(sender, eventArgs) {
        ///    lcs = eventArgs.get_lineControlConnection();
        ///    for (lcCompteur = 0; lcCompteur < lcs.length; lcCompteur = lcCompteur + 1) {
        ///        var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
        ///        var lcc = lcs[lcCompteur];
        ///        alert(lcc.callid + ': ' + lcc.contact);
        ///    }
        /// }
        /// </script>
        /// <cc1:CTIClient ID="CTIClient1" runat="server" display="true" displayCopyright="false" 
        /// displayInput="false" displayPhoneControl="true" displayStatus="true" monitoredLine="1000" 
        /// OnCallEnded="OnCallEnded"  MevoPilot="965999" enableDnd="false" Mode="sso"/>
        /// </code>
        /// </example>
        public string OnCallEnded
        {
            get { return this._onCallEnded; }
            set { this._onCallEnded = value; }
        }

        /// <summary>
        /// New call to codif handler
        /// </summary>
        /// <example>
        /// <code>
        /// <script language="javascript" type="text/javascript">
        /// function OnNewCallToCodif(sender, eventArgs) {
        ///    
        ///     alert(eventArgs.caller + ' ' + eventArgs.callee + ':' + eventArgs.callId);
        ///    
        /// }
        /// </script>
        /// <cc1:CTIClient ID="CTIClient1" runat="server" display="true" displayCopyright="false" 
        /// displayInput="false" displayPhoneControl="true" displayStatus="true" monitoredLine="1000" 
        /// OnCallEnded="OnCallEnded" OnNewCallToCodif="OnNewCallToCodif" MevoPilot="965999" enableDnd="false" Mode="sso"/>
        /// </code>
        /// </example>
        public string OnNewCallToCodif
        {
            get { return this._onNewCallToCodif; }
            set { this._onNewCallToCodif = value; }
        }
        /// <summary>
        /// Directory click handler
        /// </summary>
        /// <example>
        /// <code>
        /// <script language="javascript" type="text/javascript">
        /// function OnDirectoryClicked() {
        ///    //Actions...
        /// }
        /// </script>
        /// <cc1:CTIClient ID="CTIClient1" runat="server" display="true" displayCopyright="false" 
        /// displayInput="false" displayPhoneControl="true" displayStatus="true" monitoredLine="1000" 
        /// DirectoryClicked="OnDirectoryClicked"  MevoPilot="965999" enableDnd="false" Mode="sso"/>
        /// </code>
        /// </example>
        public string DirectoryClicked
        {
            get { return this._directoryClicked; }
            set { this._directoryClicked = value; }
        }
        /// <summary>
        /// Exception handler
        /// </summary>
        /// <example>
        /// <code>
        /// <script language="javascript" type="text/javascript">
        /// function OnExceptionHandler(sender, eventArgs) {
        ///    alert("Stack Trace: " + eventArgs.get_stackTrace() + "/r/n" +
        ///  "Error: " + eventArgs.get_message() + "/r/n" +
        ///  "Status Code: " + eventArgs.get_statusCode() + "/r/n" +
        ///  "Exception Type: " + eventArgs.get_exceptionType() + "/r/n" +
        ///  "Timed Out: " + eventArgs.get_timedOut());
        /// }
        /// </script>
        /// <cc1:CTIClient ID="CTIClient1" runat="server" display="true" 
        /// displayCopyright="false" displayInput="false" displayPhoneControl="true" 
        /// displayStatus="true" monitoredLine="1000" ExceptionHandler="OnExceptionHandler"  
        /// MevoPilot="965999" enableDnd="false" Mode="sso"/>
        /// </code>
        /// </example>
        public string ExceptionHandler
        {
            get { return this._exceptionHandler; }
            set { this._exceptionHandler = value; }
        }
        /// <summary>
        /// Voice mail extension
        /// </summary>
        public string MevoPilot
        {
            get { return this._mevoPilot; }
            set { this._mevoPilot = value; }
        }
        /// <summary>
        /// Enable or disable direct transfer
        /// </summary>
        public bool enableTransfer
        {
            get { return this._enableTransfer; }
            set { this._enableTransfer = value; }
        }
        /// <summary>
        /// Enable or disable consult transfer
        /// </summary>
        public bool enableConsultTransfer
        {
            get { return this._enableConsultTransfer; }
            set { this._enableConsultTransfer = value; }
        }
        /// <summary>
        /// Enable or disable hold feature
        /// </summary>
        public bool enableHold
        {
            get { return this._enableHold; }
            set { this._enableHold = value; }
        }
        /// <summary>
        /// Enable or disable dnd feature
        /// </summary>
        public bool enableDnd
        {
            get { return this._enableDnd; }
            set { this._enableDnd = value; }
        }
        /// <summary>
        /// Enable or disable voicemail access feature
        /// </summary>
        public bool enableMevo
        {
            get { return this._enableMevo; }
            set { this._enableMevo = value; }
        }
        /// <summary>
        /// Enable or disable directory access feature
        /// </summary>
        public bool enableDirectory
        {
            get { return this._enableDirectory; }
            set { this._enableDirectory = value; }
        }
        /// <summary>
        /// Enable or disable call logs access feature
        /// </summary>
        public bool enableCallLogs
        {
            get { return this._enableCallLogs; }
            set { this._enableCallLogs = value; }
        }
        /// <summary>
        /// Enable or disable alerts
        /// </summary>
        public bool showAlert
        {
            get { return this._showAlert; }
            set { this._showAlert = value; }
        }

        /// <summary>
        /// Enable or disable reverse lookup on transfer
        /// </summary>
        public bool enableTransferLookup
        {
            get
            {
                bool s = false;
                if (ViewState["enableTransferLookup"] != null)
                {
                    s = (bool)ViewState["enableTransferLookup"];
                }
                return s;
            }
            set
            {
                ViewState["enableTransferLookup"] = value;
            }
        }

        /// <summary>
        /// Enable or disable popup when transferring
        /// </summary>
        public bool enablePopupTransfer
        {
            get
            {
                bool s = false;
                if (ViewState["enablePopupTransfer"] != null)
                {
                    s = (bool)ViewState["enablePopupTransfer"];
                }
                return s;
            }
            set
            {
                ViewState["enablePopupTransfer"] = value;
            }
        }

        /// <summary>
        /// Enable or disable monitoring feature
        /// </summary>
        public bool enableMonitor
        {
            get {
                bool s = false;
                if (ViewState["enableMonitor"] != null)
                {
                    s = (bool)ViewState["enableMonitor"];
                }
                return s;
            }
            set { ViewState["enableMonitor"] = value; }
        }

        /// <summary>
        /// Enable or disable agent feature
        /// </summary>
        public bool enableAgent
        {
            get
            {
                bool s = false;
                if (ViewState["enableAgent"] != null)
                {
                    s = (bool)ViewState["enableAgent"];
                }
                return s;
            }
            set { ViewState["enableAgent"] = value; }
        }

        /// <summary>
        /// Enable or disable codif feature
        /// </summary>
        public bool enableCodif
        {
            get
            {
                bool s = false;
                if (ViewState["enableCodif"] != null)
                {
                    s = (bool)ViewState["enableCodif"];
                }
                return s;
            }
            set { ViewState["enableCodif"] = value; }
        }

        /// <summary>
        /// Show Call Result Alert
        /// </summary>
        public bool showCallResultAlert
        {
            get
            {
                bool s = false;
                if (ViewState["showCallResultAlert"] != null)
                {
                    s = (bool)ViewState["showCallResultAlert"];
                }
                return s;
            }
            set { ViewState["showCallResultAlert"] = value; }
        }

        /// <summary>
        /// Show Call Result Remember Alert
        /// </summary>
        public bool showCallResultRememberAlert
        {
            get
            {
                bool s = false;
                if (ViewState["showCallResultRememberAlert"] != null)
                {
                    s = (bool)ViewState["showCallResultRememberAlert"];
                }
                return s;
            }
            set { ViewState["showCallResultRememberAlert"] = value; }
        }

        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string csslink = "";
        
            sm = ScriptManager.GetCurrent(this.Page);
            sm.Services.Add(new ServiceReference(this.ctiService));
            sm.Services.Add(new ServiceReference(this.stateService));
            sm.Services.Add(new ServiceReference(this.callLogsService));
            
        
            csslink = "<link href='" +
                this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
                "Wybecom.TalkPortal.CTI.Controls.CTIClient.css") +
                "' rel='stylesheet' type='text/css' />";
            csslink += "<link href='" +
                this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
                "Wybecom.TalkPortal.CTI.Controls.popup.css") +
                "' rel='stylesheet' type='text/css' />";
            string scriptlink = "<script type=\"text/javascript\" src=\"" +
                this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
                "Wybecom.TalkPortal.CTI.Controls.jquery-1.2.6.min.js") +
                "\"></script>";
            scriptlink += "<script type=\"text/javascript\" src=\"" +
                this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
                "Wybecom.TalkPortal.CTI.Controls.popup.js") +
                "\"></script>";
            LiteralControl include = new LiteralControl(csslink + scriptlink);
            try
            {
                this.Page.Header.Controls.Add(include);
            }
            catch
            {
                
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "cticontrol");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            if (!this.display)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            
            //<div class="input">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "input");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_destination");
            if (!this.displayInput)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            
            
            //<ul>
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            //<li>Destination:
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.Write("Destination:<input type=\"text\" id=\""+this.ClientID+"_tbdestination\" />");
            //<input type="text" id="_tbdestination" />
            //writer.RenderBeginTag(HtmlTextWriterTag.Input);
            //writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            //writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_tbdestination");
            //writer.RenderEndTag();
            //</li>
            writer.RenderEndTag();
            //</ul>
            writer.RenderEndTag();
            //</div>
            writer.RenderEndTag();
            
            //<div class="phonecontrol">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "phonecontrol");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_phonecontrol");
            if (!this.displayPhoneControl)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            
            //<table class="toolbar">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            
            //    <tr>
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            //        <td class="button" id="_handlecall">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_handlecall");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            
            //        <a href="" class="toolbar">
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            
            //        <span class="icon-place" title="Place call">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-place");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Appeler");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            
            //        </span>
            writer.RenderEndTag();
            //        Appeler
            writer.Write("Appeler");
            //        </a>
            writer.RenderEndTag();
            //        </td>
            writer.RenderEndTag();
            //        <td class="button" id="_transfercall">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_transfercall");
            if (!this.enableTransfer)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            
            //        <a class="toolbar" href="">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            
            //        <span class="icon-transfer" title="Divert">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-transfer");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Renvoi imm&eacute;diat");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            
            //        </span>
            writer.RenderEndTag();
            //        Renvoi
            writer.Write("Renvoi");
            //        </a>
            writer.RenderEndTag();
            //        </td>
            writer.RenderEndTag();

            //        <td class="button" id="_transfercall">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_consulttransfercall");
            if (!this.enableConsultTransfer)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            //        <a class="toolbar" href="">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            //        <span class="icon-transfer" title="Divert">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-transfer");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Transfert avec consultation");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            //        </span>
            writer.RenderEndTag();
            //        Renvoi
            writer.Write("Transfert");
            //        </a>
            writer.RenderEndTag();
            //        </td>
            writer.RenderEndTag();

            //        <td class="button" id="_hold">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_hold");
            if (!this.enableHold)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            
            //        <a class="toolbar" href="javascript:return false;">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            
            //        <span class="icon-hold" title="Hold">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-hold");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Attente");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            
            //        </span>
            writer.RenderEndTag();
            //        Attente
            writer.Write("Attente");
            //        </a>
            writer.RenderEndTag();
            //        </td>
            writer.RenderEndTag();
            //        <td class="button" id="_dnd">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_dnd");
            if (!this.enableDnd)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            
            //        <a class="toolbar" href="">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            
            //        <span class="icon-dnd" title="Do not disturb">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-dnd");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Ne pas d&eacute;ranger");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            
            //        </span>
            writer.RenderEndTag();
            //        Ne pas d&eacute;ranger
            writer.Write("Ne pas d&eacute;ranger");
            //        </a>
            writer.RenderEndTag();
            //        </td>
            writer.RenderEndTag();
            //        <td class="button" id="_voicemail">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_voicemail");
            if (!this.enableMevo)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            
            //        <a class="toolbar" href="">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            
            //        <span class="icon-mail" title="Voice mail">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-mail");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Mevo");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            
            //        </span>
            writer.RenderEndTag();
            //        Mevo
            writer.Write("Mevo");
            //        </a>
            writer.RenderEndTag();
            //        </td>
            writer.RenderEndTag();
            //<td class="button" id="_directory">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_directory");
            if (!this.enableDirectory)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //    <a class="toolbar" href="">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            //    <span class="icon-directory" title="Annuaire">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-directory");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Annuaire");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            //    </span>
            writer.RenderEndTag();
            //    Annuaire
            writer.Write("Annuaire");
            //    </a>
            writer.RenderEndTag();
            //    </td>
            writer.RenderEndTag();
            //<td class="button" id="_calllogs">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_calllogs");
            if (!this.enableCallLogs)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //    <a class="toolbar" href="">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            //    <span class="icon-calllogs" title="Journaux d'appels">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-calllogs");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Journaux d'appels");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            //    </span>
            writer.RenderEndTag();
            //    Annuaire
            writer.Write("Journaux d'appels");
            //    </a>
            writer.RenderEndTag();
            //    </td>
            writer.RenderEndTag();
            //<td class="button" id="_monitor">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_monitor");
            if (!this.enableMonitor)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //    <a class="toolbar" href="">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            //    <span class="icon-monitor" title="Ecoute discrète">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-monitor");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "Ecoute discrète");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            //    </span>
            writer.RenderEndTag();
            //    Ecoute
            writer.Write("Ecoute discrète");
            //    </a>
            writer.RenderEndTag();
            //    </td>
            writer.RenderEndTag();
            //    </tr>
            writer.RenderEndTag();
            //</table>
            writer.RenderEndTag();
            //</div>
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            //<div class="state" id="_state">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "state");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_state");
            if (!this.displayStatus)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            //    <div id="_statecontent" class="statecontent"></div>
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "statecontent");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_statecontent");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            //</div>
            writer.RenderEndTag();
            writer.RenderEndTag();
            //</div>
            writer.RenderEndTag();
            if (enableAgent)
            {
                //<div class="agentcontrol">
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "agentcontrol");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_agentcontrol");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                //<table class="toolbar">
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                //<tr>
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                //<td class="button" id="_agentlog">
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_agentlog");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                //<a href="javascript:function() { return false;}" class="toolbar">
                writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                //<span class="icon-agentlog" title="Connexion">
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-agentlog");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, "Connexion");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                //</span>
                writer.RenderEndTag();
                //Connexion
                writer.Write("Connexion");
                //</a>
                writer.RenderEndTag();
                //</td>
                writer.RenderEndTag();
                //<td class="button" id="_agentstate">
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "button");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_agentstate");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                //<a href="javascript:function() { return false;}" class="toolbar">
                writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:function() {return false;}");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "toolbar");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                //<span class="icon-agentlog" title="Connexion">
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon-agentstateready");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, "Prêt");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                //</span>
                writer.RenderEndTag();
                //Connexion
                writer.Write("Prêt");
                //</a>
                writer.RenderEndTag();
                //</td>
                writer.RenderEndTag();
                //</tr>
                writer.RenderEndTag();
                //</table>
                writer.RenderEndTag();
                //</div>
                writer.RenderEndTag();
            }
            //<div class="copyright">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "copyright");
            if (!this.displayCopyright)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            
            
            //Copyright &#169; 2009 Wybecom.
            writer.Write("Copyright &#169; 2009 Wybecom");
            //</div>
            writer.RenderEndTag();
            //</div>
            writer.RenderEndTag();

            //Codification popup
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupCodif");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);//<div id="popupContact">
            //<a id="popupContactClose">x</a>
            //writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupCodifClose");
            //writer.RenderBeginTag(HtmlTextWriterTag.A);
            //writer.Write("x");
            //writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.H1);
            writer.Write("Résultat d'appel ");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupCodifInfos");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupCodifList");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupCodifButton");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write("Valider");
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupCodifError");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();


            writer.RenderEndTag();


            //<div id="backgroundPopup"></div>
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "backgroundPopupCodif");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();

            //end codification popup

            if (enableConsultTransfer && enablePopupTransfer)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupContact");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);//<div id="popupContact">
                //<a id="popupContactClose">x</a>
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupContactClose");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write("x");
                writer.RenderEndTag();

                writer.RenderBeginTag(HtmlTextWriterTag.H1);
                if (enableTransferLookup)
                {
                    writer.Write("Transfert de ");
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupTransferFrom");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.RenderEndTag();
                    writer.Write("&nbsp;vers&nbsp;");
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupTransferTo");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.RenderEndTag();
                    //writer.RenderEndTag();
                }
                else
                {
                    writer.Write("Transfert en cours...");
                }
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Id, "popupTransferButton");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("Transférer");
                writer.RenderEndTag();

                writer.RenderEndTag();


                //<div id="backgroundPopup"></div>
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "backgroundPopup");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
            }
            base.Render(writer);
        }

        protected override IEnumerable<ScriptDescriptor>
                GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor("Wybecom.TalkPortal.CTI.Controls.CTIClient", this.ClientID);
            descriptor.AddProperty("displayInput", this.displayInput);
            descriptor.AddProperty("displayCopyright", this.displayCopyright);
            descriptor.AddProperty("displayPhoneControl", this.displayPhoneControl);
            descriptor.AddProperty("displayStatus", this.displayStatus);
            descriptor.AddProperty("ctiService", this.ctiService);
            descriptor.AddProperty("stateService", this.stateService);
            descriptor.AddProperty("callLogsService", this.callLogsService);
            descriptor.AddProperty("speedDialService", this.speedDialService);
            if (!String.IsNullOrEmpty(this.OnCallInitialized))
            {
                descriptor.AddEvent("callInitialized", this.OnCallInitialized);
            }
            if (!String.IsNullOrEmpty(this.OnCallReceived))
            {
                descriptor.AddEvent("callReceived", this.OnCallReceived);
            }
            if (!String.IsNullOrEmpty(this.OnCallEnded))
            {
                descriptor.AddEvent("callEnded", this.OnCallEnded);
            }
            if (!String.IsNullOrEmpty(this.OnNewCallToCodif))
            {
                descriptor.AddEvent("newCallToCodif", this.OnNewCallToCodif);
            }
            if (!String.IsNullOrEmpty(this.DirectoryClicked))
            {
                descriptor.AddEvent("directoryClicked", this.DirectoryClicked);
            }
            if (!String.IsNullOrEmpty(this.ExceptionHandler))
            {
                descriptor.AddEvent("exceptionHandler", this.ExceptionHandler);
            }
            descriptor.AddProperty("mevoPilot", this.MevoPilot);
            descriptor.AddProperty("enableTransfer", this.enableTransfer);
            descriptor.AddProperty("enableConsultTransfer", this.enableConsultTransfer);
            descriptor.AddProperty("enableHold", this.enableHold);
            descriptor.AddProperty("enableDnd", this.enableDnd);
            descriptor.AddProperty("enableMevo", this.enableMevo);
            descriptor.AddProperty("enableDirectory", this.enableDirectory);
            descriptor.AddProperty("enableCallLogs", this.enableCallLogs);
            descriptor.AddProperty("enableMonitor", this.enableMonitor);
            descriptor.AddProperty("showAlert", this.showAlert);
            
            descriptor.AddProperty("mode", this.Mode);
            if (this.Mode == AuthenticationMode.trusted)
            {
                descriptor.AddProperty("token", GenerateEncryptedToken());
            }
            if (this.Mode == AuthenticationMode.sso && String.IsNullOrEmpty(this.monitoredLine))
            {
                this.monitoredLine = AuthenticationService.getDN();
            }
            descriptor.AddProperty("codifMode", this.CodifMode);
            descriptor.AddProperty("enableCodif", this.enableCodif);
            descriptor.AddProperty("user", this.User);
            descriptor.AddProperty("password", this.Password);
            descriptor.AddProperty("monitoredLine", this.monitoredLine);
            descriptor.AddProperty("enablePopupTransfer", this.enablePopupTransfer);
            descriptor.AddProperty("enableTransferLookup", this.enableTransferLookup);
            descriptor.AddProperty("dmdService", this.dmdService);
            descriptor.AddProperty("enableAgent", this.enableAgent);
            descriptor.AddProperty("showCallResultAlert", this.showCallResultAlert);
            descriptor.AddProperty("showCallResultRememberAlert", this.showCallResultRememberAlert);
            yield return descriptor;
        }

        // Générez la référence de script
        protected override IEnumerable<ScriptReference>
                GetScriptReferences()
        {
            yield return new ScriptReference("Wybecom.TalkPortal.CTI.Controls.CTIClient.js", this.GetType().Assembly.FullName);
        }

        private string GenerateEncryptedToken()
        {
            return AuthenticationService.Encrypt(this._monitoredLine + " is trusted.");
        }

        
    }
    /// <summary>
    /// Authentication mode
    /// - trusted, no authentication mecanism, user and user password are not applied
    /// - manual, require user and user password
    /// - sso, based on windows authentication model, user and user password are not applied
    /// </summary>
    public enum AuthenticationMode
    {
        trusted,
        manual,
        sso
    }

    public enum CodifMode
    {
        allcalls,
        placedcalls,
        receivedcalls
    }
}