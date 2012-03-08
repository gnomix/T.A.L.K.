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
namespace Wybecom.TalkPortal.Connectors.TAPI.Client {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="BasicHttpBinding_ITAPISnapshotServer", Namespace="http://tempuri.org/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineStatus))]
    public partial class TAPISnapshotServer : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetSnapshotOperationCompleted;
        
        /// <remarks/>
        public TAPISnapshotServer() {
            string urlSetting = System.Web.Configuration.WebConfigurationManager.AppSettings["TAPISnapshotService"];
            if ((urlSetting != null)) {
                this.Url = urlSetting;
            }
            else {
                this.Url = "http://localhost:8731/TAPISnapshotService";
            }
        }
        
        /// <remarks/>
        public event GetSnapshotCompletedEventHandler GetSnapshotCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://wybecom.org/talkportal/cti/tapisnapshotservice/ITAPISnapshotServer/GetSnap" +
            "shot", RequestNamespace="http://wybecom.org/talkportal/cti/tapisnapshotservice", ResponseNamespace="http://wybecom.org/talkportal/cti/tapisnapshotservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.datacontract.org/2004/07/Wybecom.TalkPortal.CTI.Proxy")]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/Wybecom.TalkPortal.CTI.Proxy")]
    public partial class LineControl : LineStatus {
        
        private LineControlConnection[] lineControlConnectionFieldField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        public LineControlConnection[] lineControlConnectionField {
            get {
                return this.lineControlConnectionFieldField;
            }
            set {
                this.lineControlConnectionFieldField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/Wybecom.TalkPortal.CTI.Proxy")]
    public partial class LineControlConnection {
        
        private string callidFieldField;
        
        private string contactFieldField;
        
        private ConnectionState remoteStateFieldField;
        
        private ConnectionState stateFieldField;
        
        private TerminalState terminalStateFieldField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string callidField {
            get {
                return this.callidFieldField;
            }
            set {
                this.callidFieldField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string contactField {
            get {
                return this.contactFieldField;
            }
            set {
                this.contactFieldField = value;
            }
        }
        
        /// <remarks/>
        public ConnectionState remoteStateField {
            get {
                return this.remoteStateFieldField;
            }
            set {
                this.remoteStateFieldField = value;
            }
        }
        
        /// <remarks/>
        public ConnectionState stateField {
            get {
                return this.stateFieldField;
            }
            set {
                this.stateFieldField = value;
            }
        }
        
        /// <remarks/>
        public TerminalState terminalStateField {
            get {
                return this.terminalStateFieldField;
            }
            set {
                this.terminalStateFieldField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/Wybecom.TalkPortal.CTI.Proxy")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/Wybecom.TalkPortal.CTI.Proxy")]
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
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineControl))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/Wybecom.TalkPortal.CTI.Proxy")]
    public partial class LineStatus {
        
        private string directoryNumberFieldField;
        
        private bool doNotDisturbFieldField;
        
        private string forwardFieldField;
        
        private string monitoredFieldField;
        
        private bool mwiOnFieldField;
        
        private Status statusFieldField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string directoryNumberField {
            get {
                return this.directoryNumberFieldField;
            }
            set {
                this.directoryNumberFieldField = value;
            }
        }
        
        /// <remarks/>
        public bool doNotDisturbField {
            get {
                return this.doNotDisturbFieldField;
            }
            set {
                this.doNotDisturbFieldField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string forwardField {
            get {
                return this.forwardFieldField;
            }
            set {
                this.forwardFieldField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string monitoredField {
            get {
                return this.monitoredFieldField;
            }
            set {
                this.monitoredFieldField = value;
            }
        }
        
        /// <remarks/>
        public bool mwiOnField {
            get {
                return this.mwiOnFieldField;
            }
            set {
                this.mwiOnFieldField = value;
            }
        }
        
        /// <remarks/>
        public Status statusField {
            get {
                return this.statusFieldField;
            }
            set {
                this.statusFieldField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/Wybecom.TalkPortal.CTI.Proxy")]
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