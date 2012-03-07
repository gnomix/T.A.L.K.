﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :2.0.50727.4959
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Ce code source a été automatiquement généré par wsdl, Version=2.0.50727.3038.
// 
namespace Wybecom.TalkPortal.CTI.JTAPI.Proxy {
    using System.Xml.Serialization;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Diagnostics;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="JTAPISnapshotServicePortBinding", Namespace="http://ws.jtapi.talk.wybecom.org/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineStatus))]
    public partial class JTAPISnapshotServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetSnapshotOperationCompleted;
        
        /// <remarks/>
        public JTAPISnapshotServiceService() {
            string urlSetting = System.Web.Configuration.WebConfigurationManager.AppSettings["JTAPISnapshotService"];
            if ((urlSetting != null)) {
                this.Url = urlSetting;
            }
            else {
                this.Url = "http://localhost:8080/JTAPIConnector/JTAPISnapshotService";
            }
        }
        
        /// <remarks/>
        public event GetSnapshotCompletedEventHandler GetSnapshotCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://ws.jtapi.talk.wybecom.org/", ResponseNamespace="http://ws.jtapi.talk.wybecom.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public LineControl[] GetSnapshot() {
            object[] results = this.Invoke("GetSnapshot", new object[0]);
            return ((LineControl[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetSnapshot(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetSnapshot", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public LineControl[] EndGetSnapshot(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((LineControl[])(results[0]));
        }
        
        /// <remarks/>
        public void GetSnapshotAsync() {
            this.GetSnapshotAsync(null);
        }
        
        /// <remarks/>
        public void GetSnapshotAsync(object userState) {
            if ((this.GetSnapshotOperationCompleted == null)) {
                this.GetSnapshotOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetSnapshotOperationCompleted);
            }
            this.InvokeAsync("GetSnapshot", new object[0], this.GetSnapshotOperationCompleted, userState);
        }
        
        private void OnGetSnapshotOperationCompleted(object arg) {
            if ((this.GetSnapshotCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetSnapshotCompleted(this, new GetSnapshotCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://wybecom.org/talkportal/cti/stateserver")]
    public partial class LineControl : LineStatus {
        
        private LineControlConnection[] lineControlConnectionField;
        
        /// <remarks/>
        public LineControlConnection[] lineControlConnection {
            get {
                return this.lineControlConnectionField;
            }
            set {
                this.lineControlConnectionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://wybecom.org/talkportal/cti/stateserver")]
    public partial class LineControlConnection {
        
        private TerminalState terminalStateField;
        
        private ConnectionState stateField;
        
        private ConnectionState remoteStateField;
        
        private string contactField;
        
        private string callidField;
        
        /// <remarks/>
        public TerminalState terminalState {
            get {
                return this.terminalStateField;
            }
            set {
                this.terminalStateField = value;
            }
        }
        
        /// <remarks/>
        public ConnectionState state {
            get {
                return this.stateField;
            }
            set {
                this.stateField = value;
            }
        }
        
        /// <remarks/>
        public ConnectionState remoteState {
            get {
                return this.remoteStateField;
            }
            set {
                this.remoteStateField = value;
            }
        }
        
        /// <remarks/>
        public string contact {
            get {
                return this.contactField;
            }
            set {
                this.contactField = value;
            }
        }
        
        /// <remarks/>
        public string callid {
            get {
                return this.callidField;
            }
            set {
                this.callidField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://wybecom.org/talkportal/cti/stateserver")]
    public enum TerminalState {
        
        /// <remarks/>
        idle,
        
        /// <remarks/>
        ringing,
        
        /// <remarks/>
        talking,
        
        /// <remarks/>
        held,
        
        /// <remarks/>
        bridged,
        
        /// <remarks/>
        inuse,
        
        /// <remarks/>
        dropped,
        
        /// <remarks/>
        unknown,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://wybecom.org/talkportal/cti/stateserver")]
    public enum ConnectionState {
        
        /// <remarks/>
        unknown,
        
        /// <remarks/>
        idle,
        
        /// <remarks/>
        failed,
        
        /// <remarks/>
        disconnected,
        
        /// <remarks/>
        established,
        
        /// <remarks/>
        alerting,
        
        /// <remarks/>
        offered,
        
        /// <remarks/>
        queued,
        
        /// <remarks/>
        network_reached,
        
        /// <remarks/>
        network_alerting,
        
        /// <remarks/>
        initiated,
        
        /// <remarks/>
        dialing,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineControl))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://wybecom.org/talkportal/cti/stateserver")]
    public partial class LineStatus {
        
        private string directoryNumberField;
        
        private string forwardField;
        
        private bool doNotDisturbField;
        
        private bool mwiOnField;
        
        private Status statusField;
        
        private string monitoredField;
        
        /// <remarks/>
        public string directoryNumber {
            get {
                return this.directoryNumberField;
            }
            set {
                this.directoryNumberField = value;
            }
        }
        
        /// <remarks/>
        public string forward {
            get {
                return this.forwardField;
            }
            set {
                this.forwardField = value;
            }
        }
        
        /// <remarks/>
        public bool doNotDisturb {
            get {
                return this.doNotDisturbField;
            }
            set {
                this.doNotDisturbField = value;
            }
        }
        
        /// <remarks/>
        public bool mwiOn {
            get {
                return this.mwiOnField;
            }
            set {
                this.mwiOnField = value;
            }
        }
        
        /// <remarks/>
        public Status status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public string monitored {
            get {
                return this.monitoredField;
            }
            set {
                this.monitoredField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://wybecom.org/talkportal/cti/stateserver")]
    public enum Status {
        
        /// <remarks/>
        available,
        
        /// <remarks/>
        donotdisturb,
        
        /// <remarks/>
        forwarded,
        
        /// <remarks/>
        busy,
        
        /// <remarks/>
        hidden,
        
        /// <remarks/>
        dialing,
        
        /// <remarks/>
        ringing,
        
        /// <remarks/>
        inactive,
        
        /// <remarks/>
        unknown,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetSnapshotCompletedEventHandler(object sender, GetSnapshotCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetSnapshotCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetSnapshotCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public LineControl[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((LineControl[])(this.results[0]));
            }
        }
    }
}
