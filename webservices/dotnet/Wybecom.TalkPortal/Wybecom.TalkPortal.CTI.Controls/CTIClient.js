/// <reference name="MicrosoftAjax.js"/>
/*
This file is part of TALK (Wybecom).

TALK is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License, version 2 as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

TALK is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with TALK.  If not, see <http://www.gnu.org/licenses/gpl-2.0.html>.
 
TALK is based upon:hangup
- Sun JTAPI http://java.sun.com/products/jtapi/
- JulMar TAPI http://julmar.com/
- Asterisk.Net http://sourceforge.net/projects/asterisk-dotnet/
 
*/

Type.registerNamespace("Wybecom.TalkPortal.CTI.Controls");

Wybecom.TalkPortal.CTI.Controls.CTIClient = function(element) {
    Wybecom.TalkPortal.CTI.Controls.CTIClient.initializeBase(this, [element]);

    this._displayInput = null;
    this._displayCopyright = null;
    this._displayPhoneControl = null;
    this._displayStatus = null;
    this._monitoredLine = null;
    this._copyright = null;
    this._ctiService = null;
    this._stateService = null;
    this._callLogsService = null;
    this._dmdService = null;
    this._input = null;
    this._tbinput = null;
    this._phoneControl = null;
    this._agentControl = null;
    this._state = null;
    this._statecontent = null;
    this._callhandler = null;
    this._transferhandler = null;
    this._consulttransferhandler = null;
    this._monitorhandler = null;
    this._holdhandler = null;
    this._dndhandler = null;
    this._voicemailhandler = null;
    this._agentloghandler = null;
    this._agentstatehandler = null;
    this._codifhandler = null;
    this._monitoringRequest = null;
    this._waitingMonitoringRequest = null;
    this._presenceRequest = null;
    this._waitingPresenceRequest = null;
    this._ctiRequest = null;
    this._lineControl = null;
    this._retryInterval = 10000;
    this._mevoPilot = null;
    this._enableTransfer = false;
    this._enableConsultTransfer = true;
    this._enableHold = true;
    this._enableDnd = true;
    this._enableMevo = true;
    this._enableDirectory = true;
    this._enableCallLogs = true;
    this._executorArray = null;
    this._user = null;
    this._password = null;
    this._mode = null;
    this._token = null;
    this._showAlert = false;
    this._showCallResultAlert = false;
    this._showCallResultRememberAlert = false;
    this._speedDialService = null;
    this._speedDials = null;
    this._currentAction = "Unknown";
    this._transferButton = null;
    this._currentTransferDestination = "";
    this._currentTransferFrom = "";
    this._enableTransferLookup = false;
    this._enablePopupTransfer = false;
    this._enableMonitor = false;
    this._enableAgent = false;
    this._enableCodif = false;
    this._codifMode = null;
    this._callToCodif = null;
    this._currentCall = null;
}

Wybecom.TalkPortal.CTI.Controls.CTIClient.prototype = {
    initialize: function() {
        Wybecom.TalkPortal.CTI.Controls.CTIClient.callBaseMethod(this, 'initialize');

        // Ajouter ici une initialisation personnalisée
        this._state = $get(this.get_id() + "_state");
        this._statecontent = $get(this.get_id() + "_statecontent");
        this._setControlElement();
        this._input = $get(this.get_id() + "_destination");
        this._phoneControl = $get(this.get_id() + "_phonecontrol");
        this._tbinput = $get(this.get_id() + "_tbdestination");
        this._callhandler_onclick$delegate = Function.createDelegate(this, this._callhandler_onclick);
        this._transferhandler_onclick$delegate = Function.createDelegate(this, this._transferhandler_onclick);
        this._consulttransferhandler_onclick$delegate = Function.createDelegate(this, this._consulttransferhandler_onclick);
        this._holdhandler_onclick$delegate = Function.createDelegate(this, this._holdhandler_onclick);
        this._dndhandler_onclick$delegate = Function.createDelegate(this, this._dndhandler_onclick);
        this._voicemailhandler_onclick$delegate = Function.createDelegate(this, this._voicemailhandler_onclick);
        this._directoryhandler_onclick$delegate = Function.createDelegate(this, this._directoryhandler_onclick);
        this._monitorhandler_onclick$delegate = Function.createDelegate(this, this._monitorhandler_onclick);
        this._codifhandler_onclick$delegate = Function.createDelegate(this, this._codifhandler_onclick);
        this._input_onenter$delegate = Function.createDelegate(this, this._input_onenter);
        if (this._enableConsultTransfer && this._enablePopupTransfer) {
            this._initPopupTransfer();
        }
        if (this._enableAgent) {
            this._agentControl = $get(this.get_id() + "_agentcontrol");
            if (this._agentControl != null) {
                this._agentloghandler = $get(this.get_id() + "_agentlog");
                this._agentstatehandler = $get(this.get_id() + "_agentstate");
                this._agentloghandler_onclick$delegate = Function.createDelegate(this, this._agentloghandler_onclick);
                this._agentstatehandler_onclick$delegate = Function.createDelegate(this, this._agentstatehandler_onclick);
            }
        }
        if (this._input != null) {
            $addHandlers(this._input, { keypress: this._input_onenter$delegate });
        }
        this._addControlHandlers();

        Sys.Net.WebRequestManager.add_invokingRequest(Function.createDelegate(this, this._onInvokingRequest));
    },
    dispose: function() {
        //Ajouter ici des actions dispose personnalisées
        Wybecom.TalkPortal.CTI.Controls.CTIClient.callBaseMethod(this, 'dispose');
        this._removeControlHandlers();
        this._lineControl = null;
        this._monitoredLine = null;
        this._stopMonitoring();
    },

    _initPopupTransfer: function() {
        this._transferButton = $get('popupTransferButton');
        if (this._transferButton != null) {
            this._transferButton_onclick$delegate = Function.createDelegate(this, this._transferButton_onclick);
            $addHandlers(this._transferButton, { click: this._transferButton_onclick$delegate });
        }
    },

    _removeControlHandlers: function() {
        if (this._callhandler != null) {
            $removeHandler(this._callhandler, 'click', this._callhandler_onclick$delegate);
        }
        if (this._transferhandler != null) {
            $removeHandler(this._transferhandler, 'click', this._transferhandler_onclick$delegate);
        }
        if (this._consulttransferhandler != null) {
            $removeHandler(this._consulttransferhandler, 'click', this._consulttransferhandler_onclick$delegate);
        }
        if (this._holdhandler != null) {
            $removeHandler(this._holdhandler, 'click', this._holdhandler_onclick$delegate);
        }
        if (this._dndhandler != null) {
            $removeHandler(this._dndhandler, 'click', this._dndhandler_onclick$delegate);
        }
        if (this._voicemailhandler != null) {
            $removeHandler(this._voicemailhandler, 'click', this._voicemailhandler_onclick$delegate);
        }
        if (this._directoryhandler != null) {
            $removeHandler(this._directoryhandler, 'click', this._directoryhandler_onclick$delegate);
        }
        if (this._monitorhandler != null) {
            $removeHandler(this._monitorhandler, 'click', this._monitorhandler_onclick$delegate);
        }
        if (this._agentloghandler != null) {
            $removeHandler(this._agentloghandler, 'click', this._agentloghandler_onclick$delegate);
        }
        if (this._agentstatehandler != null) {
            $removeHandler(this._agentstatehandler, 'click', this._agentstatehandler_onclick$delegate);
        }
        if (this._codifhandler != null) {
            $removeHandler(this._codifhandler, 'click', this._codifhandler_onclick$delegate);
        }
    },

    _addControlHandlers: function() {
        if (this._callhandler != null) {
            $addHandlers(this._callhandler, { click: this._callhandler_onclick$delegate });
        }
        if (this._transferhandler != null) {
            $addHandlers(this._transferhandler, { click: this._transferhandler_onclick$delegate });
        }
        if (this._consulttransferhandler != null) {
            $addHandlers(this._consulttransferhandler, { click: this._consulttransferhandler_onclick$delegate });
        }
        if (this._holdhandler != null) {
            $addHandlers(this._holdhandler, { click: this._holdhandler_onclick$delegate });
        }
        if (this._dndhandler != null) {
            $addHandlers(this._dndhandler, { click: this._dndhandler_onclick$delegate });
        }
        if (this._voicemailhandler != null) {
            $addHandlers(this._voicemailhandler, { click: this._voicemailhandler_onclick$delegate });
        }
        if (this._directoryhandler != null) {
            $addHandlers(this._directoryhandler, { click: this._directoryhandler_onclick$delegate });
        }
        if (this._monitorhandler != null) {
            $addHandlers(this._monitorhandler, { click: this._monitorhandler_onclick$delegate });
        }
        if (this._agentloghandler != null) {
            $addHandlers(this._agentloghandler, { click: this._agentloghandler_onclick$delegate });
        }
        if (this._agentstatehandler != null) {
            $addHandlers(this._agentstatehandler, { click: this._agentstatehandler_onclick$delegate });
        }
        if (this._codifhandler != null) {
            $addHandlers(this._codifhandler, { click: this._codifhandler_onclick$delegate });
        }
    },

    _onInvokingRequest: function(sender, networkRequestEventArgs) {
    },

    _setControlElement: function() {
        this._callhandler = $get(this.get_id() + "_handlecall");
        this._transferhandler = $get(this.get_id() + "_transfercall");
        this._consulttransferhandler = $get(this.get_id() + "_consulttransfercall");
        this._holdhandler = $get(this.get_id() + "_hold");
        this._dndhandler = $get(this.get_id() + "_dnd");
        this._voicemailhandler = $get(this.get_id() + "_voicemail");
        this._directoryhandler = $get(this.get_id() + "_directory");
        this._monitorhandler = $get(this.get_id() + "_monitor");
        this._codifhandler = $get("popupCodifButton");
    },

    _input_onenter: function(evt) {
        if (window.event) { e = window.event || evt; }
        var key = e.keyCode || e.which;
        if (key == 13) {
            switch (this._currentAction) {
                case "Unknown":

                    break;
                case "Call":
                    this._Call('');
                    break;
                case "Forward":
                    this._Forward('');
                    break;
                case "Transfer":
                    this._Transfer('', '');
                    break;
                case "ConsultTransfer":
                    this._ConsultTransfer('', '');
                    break;
                case "Monitor":
                    this._Monitor('');
                    break;
            }
        }
    },

    _transferButton_onclick: function() {
        if (this._ctiClient != null) {
            this._ConsultTransfer('', this._currentTransferDestination);
            try {
                disablePopup();
            }
            catch (disablePopupException) {
            }
        }
    },

    _callhandler_onclick: function() {
        //Call or Answer or HangOff
        if (this._lineControl != null) {
            if (this._lineControl.lineControlConnection != null && this._lineControl.lineControlConnection.length > 0) {
                for (lcCompteur = 0; lcCompteur < this._lineControl.lineControlConnection.length; lcCompteur = lcCompteur + 1) {
                    var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
                    var lcc = this._lineControl.lineControlConnection[lcCompteur];
                    switch (lcc.state) {
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.alerting:
                            this._UnHook(lcc.callid);
                            lcCompteur = this._lineControl.lineControlConnection.length;
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.offered:
                            this._UnHook(lcc.callid);
                            lcCompteur = this._lineControl.lineControlConnection.length;
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.established:
                            this._HangUp(lcc.callid);
                            lcCompteur = this._lineControl.lineControlConnection.length;
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.failed:
                            this._HangUp(lcc.callid);
                            lcCompteur = this._lineControl.lineControlConnection.length;
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.dialing:
                            this._HangUp(lcc.callid);
                            lcCompteur = this._lineControl.lineControlConnection.length;
                            break;
                    }
                }
            }
            else {
                this._Call("");
            }
        }
    },

    _transferhandler_onclick: function() {
        if (this._lineControl != null) {
            if (this._lineControl.lineControlConnection != null && this._lineControl.lineControlConnection.length > 0) {
                for (lcCompteur = 0; lcCompteur < this._lineControl.lineControlConnection.length; lcCompteur = lcCompteur + 1) {
                    var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
                    var lcc = this._lineControl.lineControlConnection[lcCompteur];
                    switch (lcc.state) {
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.established:
                            this._Transfer(lcc.callid, '');
                            lcCompteur = this._lineControl.lineControlConnection.length;
                            break;
                    }
                }
            }
            else {
                this._Forward('');
            }
        }

    },

    _consulttransferhandler_onclick: function() {
        if (this._lineControl != null) {
            if (this._lineControl.lineControlConnection != null && this._lineControl.lineControlConnection.length > 0) {
                for (lcCompteur = 0; lcCompteur < this._lineControl.lineControlConnection.length; lcCompteur = lcCompteur + 1) {
                    var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
                    var lcc = this._lineControl.lineControlConnection[lcCompteur];
                    switch (lcc.state) {
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.established:
                            this._ConsultTransfer(lcc.callid, '');
                            lcCompteur = this._lineControl.lineControlConnection.length;
                            break;
                    }
                }
            }
            else {
                this._Forward('');
            }
        }
    },

    _holdhandler_onclick: function() {
        if (this._lineControl != null) {
            if (this._lineControl.lineControlConnection != null && this._lineControl.lineControlConnection.length > 0) {
                for (lcCompteur = 0; lcCompteur < this._lineControl.lineControlConnection.length; lcCompteur = lcCompteur + 1) {
                    var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
                    var lcc = this._lineControl.lineControlConnection[lcCompteur];
                    switch (lcc.terminalState) {
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.TerminalState.talking:
                            this._Hold(lcc.callid);
                            lcCompteur = this._lineControl.lineControlConnection.length;
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.TerminalState.held:
                            this._UnHold(lcc.callid);
                            lcCompteur = this._lineControl.lineControlConnection.length;
                            break;
                    }
                }
            }
        }
    },

    _dndhandler_onclick: function() {
        this._DoNotDisturb();
    },

    _voicemailhandler_onclick: function() {
        this._CallMevo();
    },

    _directoryhandler_onclick: function() {
        this._directoryClickedCallBack();
    },

    _monitorhandler_onclick: function() {
        this._Monitor("");
    },

    _agentloghandler_onclick: function() {
        this._Login();
    },

    _codifhandler_onclick: function() {
        this._CodifCall();
    },

    _agentstatehandler_onclick: function() {
    },

    get_retryInterval: function() {
        return this._retryInterval;
    },

    set_retryInterval: function(value) {
        if (this._retryInterval !== value) {
            this._retryInterval = value;
            this.raisePropertyChanged('retryInterval');
        }
    },

    get_displayInput: function() {
        return this._displayInput;
    },

    set_displayInput: function(value) {
        if (this._displayInput !== value) {
            this._displayInput = value;
            if (this._input != null) {
                this._showInput(value);
            }
            else {
                this._input = $get(this.get_id() + '_destination');
                this._showInput(value);
            }
            this.raisePropertyChanged('displayInput');
        }
    },

    _showInput: function(value) {
        if (value) {
            this._input.style.display = 'block';
            if (this._tbinput != null) {
                try {
                    this._tbinput.focus();
                }
                catch (err) {
                }
            }
            else {
                this._tbinput = $get(this.get_id() + '_tbdestination');
                try {
                    this._tbinput.focus();
                }
                catch (err) {
                }
            }
        }
        else {
            this._input.style.display = 'none';
        }
    },

    get_showAlert: function() {
        return this._showAlert;
    },

    set_showAlert: function(value) {
        if (this._showAlert !== value) {
            this._showAlert = value;
            this.raisePropertyChanged('showAlert');
        }
    },

    get_showCallResultAlert: function() {
        return this._showCallResultAlert;
    },

    set_showCallResultAlert: function(value) {
        if (this._showCallResultAlert !== value) {
            this._showCallResultAlert = value;
            this.raisePropertyChanged('showCallResultAlert');
        }
    },

    get_showCallResultRememberAlert: function() {
        return this._showCallResultRememberAlert;
    },

    set_showCallResultRememberAlert: function(value) {
        if (this._showCallResultRememberAlert !== value) {
            this._showCallResultRememberAlert = value;
            this.raisePropertyChanged('showCallResultRememberAlert');
        }
    },

    get_displayCopyright: function() {
        return this._displayCopyright;
    },

    set_displayCopyright: function(value) {
        if (this._displayCopyright !== value) {
            this._displayCopyright = value;
            this.raisePropertyChanged('displayCopyright');
        }
    },

    get_displayPhoneControl: function() {
        return this._displayPhoneControl;
    },

    set_displayPhoneControl: function(value) {
        if (this._displayPhoneControl !== value) {
            this._displayPhoneControl = value;
            this.raisePropertyChanged('displayPhoneControl');
        }
    },

    get_displayStatus: function() {
        return this._displayStatus;
    },

    set_displayStatus: function(value) {
        if (this._displayStatus !== value) {
            this._displayStatus = value;
            this.raisePropertyChanged('displayStatus');
        }
    },

    get_monitoredLine: function() {
        return this._monitoredLine;
    },

    set_monitoredLine: function(value) {
        if (this._monitoredLine !== value) {
            this._monitoredLine = value;
            this.raisePropertyChanged('monitoredLine');
        }
    },

    get_ctiService: function() {
        return this._ctiService;
    },

    set_ctiService: function(value) {
        if (this._ctiService !== value) {
            this._ctiService = value;
            this.raisePropertyChanged('ctiService');
        }
    },

    get_dmdService: function() {
        return this._dmdService;
    },

    set_dmdService: function(value) {
        if (this._dmdService !== value) {
            this._dmdService = value;
            this.raisePropertyChanged('dmdService');
        }
    },

    get_stateService: function() {
        return this._stateService;
    },

    set_stateService: function(value) {
        if (this._stateService !== value) {
            this._stateService = value;
            this.raisePropertyChanged('stateService');
            if (this.get_isUpdating() && this._monitoredLine != null) {
                this._restartMonitoring();
            }
        }
    },

    get_callLogsService: function() {
        return this._callLogsService;
    },

    set_callLogsService: function(value) {
        if (this._callLogsService !== value) {
            this._callLogsService = value;
            this.raisePropertyChanged('callLogsService');
        }
    },

    get_speedDialService: function() {
        return this._speedDialService;
    },

    set_speedDialService: function(value) {
        if (this._speedDialService !== value) {
            this._speedDialService = value;
            this._GetSpeedDial();
            this.raisePropertyChanged('speedDialService');
        }
    },

    get_lineControl: function() {
        return this._lineControl;
    },

    set_lineControl: function(value) {
        this._lineControl = value;
        var lcs = value.lineControlConnection;
        var heldLC = null;
        var activeLC = null;
        if (lcs != null && lcs.length > 1) {
            for (lcCompteur = 0; lcCompteur < lcs.length; lcCompteur = lcCompteur + 1) {
                if (lcs[lcCompteur].terminalState == Wybecom.TalkPortal.CTI.Proxy.LCS.TerminalState.held) {
                    heldLC = lcs[lcCompteur];
                }
                if (lcs[lcCompteur].terminalState == Wybecom.TalkPortal.CTI.Proxy.LCS.TerminalState.talking) {
                    activeLC = lcs[lcCompteur];
                }

            }
        }
        if (activeLC != null && heldLC != null) {
            if (activeLC.contact == this._currentTransferDestination && heldLC.contact == this._currentTransferFrom && this._enablePopupTransfer) {
                this._loadTransferPopup(this._currentTransferFrom, this._currentTransferDestination);
                if (this._enableTransferLookup) {
                    this._transferLookup();
                }
            }
        }
        else {
            try {
                disablePopup();
            }
            catch (disablePopupException) {
            }
        }
        this.raisePropertyChanged('lineControl');
        var lccea = new Wybecom.TalkPortal.CTI.Controls.lineControlChangedEventArgs(null);
        lccea.set_lineControl(value);
        this.raiselineControlChanged(lccea);
        if (value.status == Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown) {
            if (this._waitingMonitoringRequest == null) {
                this._waitingMonitoringRequest = window.setInterval(Function.createDelegate(this, this._startMonitoring), this._retryInterval);
            }
        }
        else {
            if (!this.get_isUpdating()) {
                this._restartMonitoring();
            }
        }
    },

    set_waitingPresenceRequest: function(value) {
        this._waitingPresenceRequest = value;
    },

    get_mevoPilot: function() {
        return this._mevoPilot;
    },

    set_mevoPilot: function(value) {
        if (this._mevoPilot !== value) {
            this._mevoPilot = value;
            this.raisePropertyChanged('mevoPilot');
        }
    },

    get_enableTransfer: function() {
        return this._enableTransfer;
    },

    set_enableTransfer: function(value) {
        if (this._enableTransfer !== value) {
            this._enableTransfer = value;
            this.raisePropertyChanged('enableTransfer');
        }
    },

    get_enableConsultTransfer: function() {
        return this._enableConsultTransfer;
    },

    set_enableConsultTransfer: function(value) {
        if (this._enableConsultTransfer !== value) {
            this._enableConsultTransfer = value;
            this.raisePropertyChanged('enableConsultTransfer');
        }
    },

    get_enableHold: function() {
        return this._enableHold;
    },

    set_enableHold: function(value) {
        if (this._enableHold !== value) {
            this._enableHold = value;
            this.raisePropertyChanged('enableHold');
        }
    },

    get_enableDnd: function() {
        return this._enableDnd;
    },

    set_enableDnd: function(value) {
        if (this._enableDnd !== value) {
            this._enableDnd = value;
            this.raisePropertyChanged('enableDnd');
        }
    },

    get_enableMevo: function() {
        return this._enableMevo;
    },

    set_enableMevo: function(value) {
        if (this._enableMevo !== value) {
            this._enableMevo = value;
            this.raisePropertyChanged('enableMevo');
        }
    },

    get_enableDirectory: function() {
        return this._enableDirectory;
    },

    set_enableDirectory: function(value) {
        if (this._enableDirectory !== value) {
            this._enableDirectory = value;
            this.raisePropertyChanged('enableDirectory');
        }
    },

    get_mode: function() {
        return this._mode;
    },

    set_mode: function(value) {
        if (this._mode !== value) {
            this._mode = value;
            this.raisePropertyChanged('mode');
            this._Authenticate();
        }
    },

    get_codifMode: function() {
        return this._codifMode;
    },

    set_codifMode: function(value) {
        if (this._codifMode !== value) {
            this._codifMode = value;
            this.raisePropertyChanged('codifMode');
        }
    },

    get_callToCodif: function() {
        return this._callToCodif;
    },

    set_callToCodif: function(value) {
        if (this._callToCodif !== value) {
            this._callToCodif = value;
            this.raisePropertyChanged('codifMode');
        }
        //raise new call to codif event
        this._newCallToCodifCallBack(value);
    },

    get_user: function() {
        return this._user;
    },

    set_user: function(value) {
        if (this._user !== value) {
            this._user = value;
            this.raisePropertyChanged('user');
            this._Authenticate();
        }
    },

    get_password: function() {
        return this._password;
    },

    set_password: function(value) {
        if (this._password !== value) {
            this._password = value;
            this.raisePropertyChanged('password');
            this._Authenticate();
        }
    },

    get_token: function() {
        return this._token;
    },

    set_token: function(value) {
        var wasnull = false;
        if (this._token == null) {
            wasnull = true;
        }
        if (this._token !== value) {
            this._token = value;
            this.raisePropertyChanged('token');
            if (wasnull) {
                this._restartMonitoring();
            }
        }
    },

    get_enableTransferLookup: function() {
        return this._enableTransferLookup;
    },

    set_enableTransferLookup: function(value) {
        this._enableTransferLookup = value;
        this.raisePropertyChanged("enableTransferLookup");
    },

    get_enablePopupTransfer: function() {
        return this._enablePopupTransfer;
    },

    set_enablePopupTransfer: function(value) {
        this._enablePopupTransfer = value;
        if (value) {
            this._initPopupTransfer();
        }
        this.raisePropertyChanged("enablePopupTransfer");
    },

    get_enableMonitor: function() {
        return this._enableMonitor;
    },

    set_enableMonitor: function(value) {
        this._enableMonitor = value;
        this.raisePropertyChanged("enableMonitor");
    },

    get_enableCodif: function() {
        return this._enableCodif;
    },

    set_enableCodif: function(value) {
        this._enableCodif = value;
        this.raisePropertyChanged("enableCodif");
    },

    get_enableAgent: function() {
        return this._enableAgent;
    },

    set_enableAgent: function(value) {
        this._enableAgent = value;
        if (value) {
            this._agentControl = $get(this.get_id() + "_agentcontrol");
            if (this._agentControl != null) {
                this._agentloghandler = $get(this.get_id() + "_agentlog");
                this._agentstatehandler = $get(this.get_id() + "_agentstate");
                this._agentloghandler_onclick$delegate = Function.createDelegate(this, this._agentloghandler_onclick);
                this._agentstatehandler_onclick$delegate = Function.createDelegate(this, this._agentstatehandler_onclick);
            }
        }

        this.raisePropertyChanged("enableAgent");
    },

    get_enableCallLogs: function() {
        return this._enableCallLogs;
    },

    _Authenticate: function() {
        var params = null;
        if (this._monitoredLine != null && this._monitoredLine != "" && this._ctiService != null && this._ctiService != "") {
            switch (this._mode) {
                case Wybecom.TalkPortal.CTI.Controls.AuthenticationMode.sso:
                    if (this._user != null && this._user != "") {
                        params = { dn: this._monitoredLine, user: this._user, password: '' };
                    }
                    break;
                case Wybecom.TalkPortal.CTI.Controls.AuthenticationMode.manual:
                    if (this._user != null && this._user != "" && this._password != null && this._password != "") {
                        params = { dn: this._monitoredLine, user: this._user, password: this._password };
                    }
                    break;
                default:
                    params = null;
                    break;
            }
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._ctiService, "Authenticate", false, params, Function.createDelegate(this, this._onAuthenticateSuccess), Function.createDelegate(this, this._onAuthenticateFailure), null, 30000);
        }
    },

    get_callPhoneControl: function() {
        var call = "<td class=\"button\" id=\"" + this.get_id() + "_handlecall\" >";

        if (this._lineControl != null) {
            switch (this._lineControl.status) {
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.available:
                    call += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-place\" title=\"Appeler\"></span>Appeler";
                    break;
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.busy:
                    call += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-end-place\" title=\"Raccrocher\"></span>Raccrocher";
                    break;
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.dialing:
                    call += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-end-place\" title=\"Raccrocher\"></span>Raccrocher";
                    break;
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.ringing:
                    call += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-answer\" title=\"D&eacute;crocher\"></span>D&eacute;crocher";
                    break;
                default:
                    call += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-place\" title=\"Appeler\"></span>Appeler";
                    break;
            }
        }
        else {
            call += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-place\" title=\"Appeler\"></span>Appeler";
        }
        call += "</a></td>";
        return call;
    },

    get_transferPhoneControl: function() {
        var transfer = "<td class=\"button\" id=\"" + this.get_id() + "_transfercall\" ";
        if (!this.get_enableTransfer()) {
            transfer += "style=\"display:none;\">";
        }
        else {
            transfer += ">";
        }
        if (this._lineControl != null) {
            switch (this._lineControl.status) {
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.available:
                    if (this._lineControl.forward != null && this._lineControl.forward != "") {
                        transfer += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-transfer\" title=\"Annuler renvoi\"></span>Annuler renvoi";
                    }
                    else {
                        transfer += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-transfer\" title=\"Renvoyer\"></span>Renvoyer";
                    }
                    break;
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.ringing:
                    transfer += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-transfer\" title=\"Renvoi imm&eacute;diat\"></span>Renvoi imm&eacute;diat";
                    break;
                default:
                    transfer += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-transfer\" title=\"Transf&eacute;rer\"></span>Transf&eacute;rer";
                    break;
            }
        }
        else {
            transfer += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-transfer\" title=\"Renvoi imm&eacute;diat\"></span>Renvoi";
        }
        transfer += "</a></td>";
        return transfer;
    },

    get_consulttransferPhoneControl: function() {
        var transfer = "<td class=\"button\" id=\"" + this.get_id() + "_consulttransfercall\" ";
        if (!this.get_enableConsultTransfer()) {
            transfer += "style=\"display:none;\">";
        }
        else {
            transfer += ">";
        }
        if (this._lineControl != null) {
            switch (this._lineControl.status) {
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.available:
                    if (!this.get_enableTransfer()) {
                        if (this._lineControl.forward != null && this._lineControl.forward != "") {
                            transfer += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-transfer\" title=\"Annuler renvoi\"></span>Annuler renvoi";
                        }
                        else {
                            transfer += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-transfer\" title=\"Renvoyer\"></span>Renvoyer";
                        }
                    } else {
                        transfer += "<a href=\"javascript:function() {return false;}\">";
                    }
                    break;
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.ringing:
                    if (!this.get_enableTransfer()) {
                        transfer += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-transfer\" title=\"Renvoi imm&eacute;diat\"></span>Renvoi imm&eacute;diat";
                    } else {
                        transfer += "<a href=\"javascript:function() {return false;}\">";
                    }
                    break;
                default:
                    transfer += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-transfer\" title=\"Transf&eacute;rer avec consultation\"></span>Transf&eacute;rer avec consultation";
                    break;
            }
        }
        else {
            if (!this.get_enableTransfer()) {
                transfer += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-transfer\" title=\"Renvoi imm&eacute;diat\"></span>Renvoi";
            } else {
                transfer += "<a href=\"javascript:function() {return false;}\">";
            }
        }
        transfer += "</a></td>";
        return transfer;
    },

    get_holdPhoneControl: function() {
        var hold = "<td class=\"button\" id=\"" + this.get_id() + "_hold\" ";
        if (!this.get_enableHold()) {
            hold += "style=\"display:none;\"";
        }

        if (this._lineControl != null) {
            switch (this._lineControl.status) {
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.available:
                    if (this.get_enableHold) {
                        hold += "style=\"display:none;\">";
                    }
                    break;
                default:
                    hold += ">";
                    break;
            }
            if (this._lineControl.lineControlConnection != null && this._lineControl.lineControlConnection.length > 0) {
                var isHeld = false;
                for (nbLc = 0; nbLc < this._lineControl.lineControlConnection.length; nbLc++) {
                    if (this._lineControl.lineControlConnection[nbLc].terminalState == Wybecom.TalkPortal.CTI.Proxy.LCS.TerminalState.held) {
                        isHeld = true;
                        break;
                    }
                }
                if (isHeld) {
                    hold += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-unhold\" title=\"Reprendre\"></span>Reprendre";
                }
                else {
                    hold += "<a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-hold\" title=\"Attente\"></span>Attente";
                }
            }

        }
        else {
            hold += "><a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-hold\" title=\"Attente\"></span>Attente";
        }
        hold += "</a></td>";
        return hold;
    },

    get_dndPhoneControl: function() {
        var dnd = "<td class=\"button\" id=\"" + this.get_id() + "_dnd\" ";
        if (!this.get_enableDnd()) {
            dnd += "style=\"display:none;\"";
        }

        if (this._lineControl != null) {
            if (this._lineControl.doNotDisturb) {
                dnd += "><a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-disable-dnd\" title=\"Disponible\"></span>Disponible";
            }
            else {
                dnd += "><a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-dnd\" title=\"Ne pas d&eacute;ranger\"></span>Ne pas d&eacute;ranger";
            }
        }
        else {
            dnd += "><a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-dnd\" title=\"Ne pas d&eacute;ranger\"></span>Ne pas d&eacute;ranger";
        }
        dnd += "</a></td>";
        return dnd;
    },

    get_mevoPhoneControl: function() {
        var mevo = "<td class=\"button\" id=\"" + this.get_id() + "_voicemail\" ";
        if (!this.get_enableMevo()) {
            mevo += "style=\"display:none;\"";
        }

        if (this._lineControl != null) {
            if (this._lineControl.mwiOn) {
                mevo += "><a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-new-mail\" title=\"Nouveaux messages\"></span>Nouveaux messages";
            }
            else {
                mevo += "><a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-mail\" title=\"Messagerie vocale\"></span>Messagerie vocale";
            }
        }
        else {
            mevo += "><a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-mail\" title=\"Messagerie vocale\"></span>Messagerie vocale";
        }
        mevo += "</a></td>";
        return mevo;
    },

    get_directoryPhoneControl: function() {
        var directory = "<td class=\"button\" id=\"" + this.get_id() + "_directory\" ";
        if (!this.get_enableDirectory()) {
            directory += "style=\"display:none;\"";
        }

        directory += "><a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-directory\" title=\"Annuaire\"></span>Annuaire";
        directory += "</a></td>";
        return directory;
    },

    get_callLogsPhoneControl: function() {
        var callLogs = "<td class=\"button\" id=\"" + this.get_id() + "_calllogs\" ";
        if (!this.get_enableCallLogs()) {
            callLogs += "style=\"display:none;\"";
        }

        callLogs += "><a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-calllogs\" title=\"Journaux d'appels\"></span>Journaux d'appels";
        callLogs += "</a></td>";
        return callLogs;
    },

    get_monitorPhoneControl: function() {
        var monitor = "<td class=\"button\" id=\"" + this.get_id() + "_monitor\" ";
        if (!this.get_enableMonitor()) {
            monitor += "style=\"display:none;\"";
        }

        monitor += "><a class=\"toolbar\" href=\"javascript:function() {return false;}\"><span class=\"icon-monitor\" title=\"Ecoute discr&egrave;te\"></span>Ecoute discr&egrave;te";
        monitor += "</a></td>";
        return monitor;
    },

    set_enableCallLogs: function(value) {
        if (this._enableCallLogs !== value) {
            this._enableCallLogs = value;
            this.raisePropertyChanged('enableCallLogs');
        }
    },

    _Call: function(destination) {
        var params = null;
        if ((destination == null || destination == "") && this._token != null) {
            if (this.get_displayInput()) {
                if (this._tbinput.value != "") {
                    params = { caller: this._monitoredLine, callee: this._tbinput.value, token: this._token };
                }
            }
            else {
                this.set_displayInput(true);
                this._currentAction = "Call";
            }
        }
        else if (this._token != null) {
            params = { caller: this._monitoredLine, callee: destination, token: this._token };

        }
        if (params != null) {

            Sys.Net.WebServiceProxy.invoke(this._ctiService, "Call", false, params, Function.createDelegate(this, this._onCallSuccess), Function.createDelegate(this, this._onCallFailed), null, 5000);
        }
    },

    _Forward: function(destination) {
        var params = null;
        if ((destination == null || destination == "") && this._token != null) {
            if (this._lineControl.forward == "") {
                if (this.get_displayInput()) {
                    if (this._tbinput.value != "" && this._token != null) {
                        params = { caller: this._monitoredLine, destination: this._tbinput.value, token: this._token };
                    }
                }
                else {
                    this.set_displayInput(true);
                    this._currentAction = "Forward";
                }
            }
            else if (this._token != null) {
                params = { caller: this._monitoredLine, destination: destination, token: this._token };
            }
        }
        else if (this._token != null) {
            params = { caller: this._monitoredLine, destination: destination, token: this._token };

        }

        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._ctiService, "Forward", false, params, Function.createDelegate(this, this._onForwardSuccess), Function.createDelegate(this, this._onForwardFailed), null, 5000);
        }
    },

    _DoNotDisturb: function() {
        var params = null;
        if (this._token != null) {
            params = { caller: this._monitoredLine, token: this._token };
            Sys.Net.WebServiceProxy.invoke(this._ctiService, "DoNotDisturb", false, params, Function.createDelegate(this, this._onDoNotDisturbSuccess), Function.createDelegate(this, this._onDoNotDisturbFailed), null, 5000);
        }
    },

    _UnHook: function(callid) {
        //answering current call;
        var params = null;
        if ((callid != null && callid != "") && this._token != null) {
            params = { callee: this._monitoredLine, callid: callid, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._ctiService, "UnHook", false, params, Function.createDelegate(this, this._onUnHookSuccess), Function.createDelegate(this, this._onUnHookFailed), null, 5000);
        }
    },

    _Divert: function(callid) {
        var params = null;
        if ((callid != null && callid != "") && this._token != null) {
            params = { callid: callid, caller: this._monitoredLine, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._ctiService, "Divert", false, params, Function.createDelegate(this, this._onDivertSuccess), Function.createDelegate(this, this._onDivertFailed), null, 5000);
        }
    },

    _CallMevo: function() {
        this._Call(this._mevoPilot);
    },

    _HangUp: function(callid) {
        var params = null;
        if ((callid != null && callid != "") && this._token != null) {
            params = { caller: this._monitoredLine, callid: callid, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._ctiService, "HangUp", false, params, Function.createDelegate(this, this._onHangUpSuccess), Function.createDelegate(this, this._onHangUpFailed), null, 5000);
        }
    },

    _Transfer: function(callid, destination) {
        var params = null;
        if (destination == null || destination == "") {
            if (this.get_displayInput()) {
                if (this._tbinput.value != "") {
                    destination = this._tbinput.value;
                }
            }
            else {
                this.set_displayInput(true);
                this._currentAction = "Transfer";
            }
        }
        if (callid == null || callid == "") {
            if (this._lineControl != null) {
                if (this._lineControl.lineControlConnection != null && this._lineControl.lineControlConnection.length > 0) {
                    for (lcCompteur = 0; lcCompteur < this._lineControl.lineControlConnection.length; lcCompteur = lcCompteur + 1) {
                        var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
                        var lcc = this._lineControl.lineControlConnection[lcCompteur];
                        switch (lcc.state) {
                            case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.established:
                                callid = lcc.callid
                                lcCompteur = this._lineControl.lineControlConnection.length;
                                break;
                        }
                    }
                }
            }
        }
        if (callid != null && callid != "" && destination != null && destination != "" && this._token != null) {
            params = { callid: callid, caller: this._monitoredLine, destination: destination, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._ctiService, "Transfer", false, params, Function.createDelegate(this, this._onTransferSuccess), Function.createDelegate(this, this._onTransferFailed), null, 5000);
        }
    },



    _Hold: function(callid) {
        var params = null;
        if (callid != null && callid != "" && this._token != null) {
            params = { callid: callid, caller: this._monitoredLine, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._ctiService, "Hold", false, params, Function.createDelegate(this, this._onHoldSuccess), Function.createDelegate(this, this._onHoldFailed), null, 5000);
        }
    },

    _UnHold: function(callid) {
        var params = null;
        if (callid != null && callid != "" && this._token != null) {
            params = { callid: callid, caller: this._monitoredLine, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._ctiService, "UnHold", false, params, Function.createDelegate(this, this._onUnHoldSuccess), Function.createDelegate(this, this._onUnHoldFailed), null, 5000);
        }
    },

    _ConsultTransfer: function(callid, destination) {
        var params = null;

        if (destination == null || destination == "") {
            if (this.get_displayInput()) {
                if (this._tbinput.value != "") {
                    destination = this._tbinput.value;
                }
            }
            else {
                this.set_displayInput(true);
                this._currentAction = "ConsultTransfer";
            }
        }

        if (callid == null || callid == "") {
            if (this._lineControl != null) {
                if (this._lineControl.lineControlConnection != null && this._lineControl.lineControlConnection.length == 1) {
                    var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
                    var lcc = this._lineControl.lineControlConnection[0];
                    if (lcc.state == Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.established) {
                        callid = lcc.callid;
                    }
                    if (callid != null && callid != "" && destination != null && destination != "" && this._token != null) {
                        params = { callid: callid, caller: this._monitoredLine, destination: destination, token: this._token };
                    }
                    if (params != null) {
                        Sys.Net.WebServiceProxy.invoke(this._ctiService, "ConsultTransfer", false, params, Function.createDelegate(this, this._onConsultTransferSuccess), Function.createDelegate(this, this._onConsultTransferFailed), null, 5000);
                        this._currentTransferDestination = destination;
                        this._currentTransferFrom = lcc.contact;
                    }
                }
                else if (this._lineControl.lineControlConnection != null && this._lineControl.lineControlConnection.length == 2) {
                    if (this._token != null) {
                        params = { callid: null, caller: this._monitoredLine, destination: null, token: this._token };
                    }
                    if (params != null) {
                        Sys.Net.WebServiceProxy.invoke(this._ctiService, "Transfer", false, params, Function.createDelegate(this, this._onTransferSuccess), Function.createDelegate(this, this._onTransferFailed), null, 5000);
                    }
                }
            }
        }
        else {
            if (this._token != null) {
                params = { callid: callid, caller: this._monitoredLine, destination: destination, token: this._token };
            }
            if (params != null) {
                Sys.Net.WebServiceProxy.invoke(this._ctiService, "ConsultTransfer", false, params, Function.createDelegate(this, this._onTransferSuccess), Function.createDelegate(this, this._onTransferFailed), null, 5000);
            }
        }
    },

    _Monitor: function(destination) {
        var params = null;
        if ((destination == null || destination == "") && this._token != null) {
            if (this.get_displayInput()) {
                if (this._tbinput.value != "") {
                    params = { monitorer: this._monitoredLine, monitored: this._tbinput.value, token: this._token };
                }
            }
            else {
                this.set_displayInput(true);
                this._currentAction = "Monitor";
            }
        }
        else if (this._token != null) {
            params = { monitorer: this._monitoredLine, monitored: destination, token: this._token };

        }
        if (params != null) {

            Sys.Net.WebServiceProxy.invoke(this._ctiService, "Monitor", false, params, Function.createDelegate(this, this._onMonitorSuccess), Function.createDelegate(this, this._onMonitorFailed), null, 5000);
        }
    },

    _Login: function() {
        var params = null;

        if (this._token != null) {
            params = { agentid: this._user, pwd: "", extension: this._monitoredLine, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._ctiService, "Login", false, params, Function.createDelegate(this, this._onLoginSuccess), Function.createDelegate(this, this._onLoginFailed), null, 5000);
        }
    },

    _CodifCall: function() {
        var params = null;
        var cid = $get("popupCodifInfos");
        var codifvalue = $get("selectCodif");
        if (cid != null && cid.innerHTML != "" && codifvalue != null) {
            params = { callid: cid.innerHTML, extension: this._monitoredLine, codif: codifvalue.options[codifvalue.selectedIndex].value };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._callLogsService, "CodifCall", false, params, Function.createDelegate(this, this._onCodifCallSuccess), Function.createDelegate(this, this._onCodifCallFailed), params, 5000);
        }
    },

    _transferLookup: function() {
        var params = { dirNum: this._currentTransferFrom };
        Sys.Net.WebServiceProxy.invoke(this._dmdService, "Lookup", false, params, Function.createDelegate(this, this._onTransferLookupSuccess), Function.createDelegate(this, this._onTransferLookupFailed), "from", 10000);
        params = { dirNum: this._currentTransferDestination };
        Sys.Net.WebServiceProxy.invoke(this._dmdService, "Lookup", false, params, Function.createDelegate(this, this._onTransferLookupSuccess), Function.createDelegate(this, this._onTransferLookupFailed), "destination", 10000);

    },

    _monitorLookup: function(monitorer) {
        var params = { dirNum: monitorer };
        Sys.Net.WebServiceProxy.invoke(this._dmdService, "Lookup", false, params, Function.createDelegate(this, this._onMonitorLookupSuccess), Function.createDelegate(this, this._onMonitorLookupFailed), null, 10000);
    },

    _lookup: function(extension) {
        var params = { dirNum: extension };
        Sys.Net.WebServiceProxy.invoke(this._dmdService, "Lookup", false, params, Function.createDelegate(this, this._onLookupSuccess), Function.createDelegate(this, this._onLookupFailed), extension, 10000);
    },

    _onTransferLookupSuccess: function(result, context) {
        var elem = null;
        var defaultText = "";
        switch (context) {
            case "from":
                elem = $get('popupTransferFrom');
                defaultText = this._currentTransferFrom;
                break;
            case "destination":
                elem = $get('popupTransferTo');
                defaultText = this._currentTransferDestination;
                break;
        }
        if (result != "Inconnu") {
            elem.innerHTML = result;
        }
        else {
            elem.innerHTML = defaultText;
        }
    },

    _onTransferLookupFailed: function(err, response, context) {
        if (err.get_statusCode() >= 0) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
              "Error: " + err.get_message() + "/r/n" +
              "Status Code: " + err.get_statusCode() + "/r/n" +
              "Exception Type: " + err.get_exceptionType() + "/r/n" +
              "Timed Out: " + err.get_timedOut());
            }
        }
    },

    _onMonitorLookupSuccess: function(result) {
        var monitordiv = $get('monitor_identity');
        if (monitordiv != null) {
            monitordiv.innerHTML = result;
        }
    },

    _onMonitorLookupFailed: function(err, response) {
        if (err.get_statusCode() >= 0) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
              "Error: " + err.get_message() + "/r/n" +
              "Status Code: " + err.get_statusCode() + "/r/n" +
              "Exception Type: " + err.get_exceptionType() + "/r/n" +
              "Timed Out: " + err.get_timedOut());
            }
        }
    },

    _onLookupSuccess: function(result, context) {
        var elem = $get('contact_identity');
        if (result != "Inconnu") {
            elem.innerHTML = result + ' (' + context + ')';
        }
    },

    _onLookupFailed: function(err, response, context) {
        if (err.get_statusCode() >= 0) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
              "Error: " + err.get_message() + "/r/n" +
              "Status Code: " + err.get_statusCode() + "/r/n" +
              "Exception Type: " + err.get_exceptionType() + "/r/n" +
              "Timed Out: " + err.get_timedOut());
            }
        }
    },

    _loadTransferPopup: function(from, to) {
        centerPopup();
        loadPopup(from, to);

    },

    _loadCodifPopup: function(call) {

        centerCodifPopup();

        var callinfo = $get("popupCodifInfos");
        if (callinfo != null) {
            callinfo.innerHTML = call.callId;
            loadCodifPopup();

            this._stopMonitoring();
            if (this.get_showCallResultAlert() && this._monitoringRequest == null) {
                alert("Pensez à codifier cet appel avant de continuer");
            }
            this._getCodifs();
        }
    },

    _getCodifs: function() {
        Sys.Net.WebServiceProxy.invoke(this._callLogsService, "GetCodification", false, null, Function.createDelegate(this, this._onGetCodifSuccess), Function.createDelegate(this, this._onGetCodifFailed), null, 5000);
    },

    _getCall: function(cid) {
        var params = null;
        if (cid != null && cid != "") {
            params = { callid: cid, extension: this._monitoredLine };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._callLogsService, "GetCall", false, params, Function.createDelegate(this, this._onGetCallSuccess), Function.createDelegate(this, this._onGetCallFailed), null, 5000);
        }
    },

    _startMonitoring: function() {
        if (this._monitoredLine != null && this._token != null) {
            if (this.get_stateService() != null) {
                if (this._lineControl == null) {
                    this._lineControl = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControl();
                    this._lineControl.directoryNumber = this._monitoredLine;
                    this._lineControl.status = Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown;
                    this._lineControl.forward = "";
                    this._lineControl.doNotDisturb = false;
                    this._lineControl.mwiOn = false;
                    this._lineControl.monitored = "";
                    this._lineControl.lineControlConnection = null;
                }

                var params = { "lc": this._lineControl, token: this._token };
                this._monitoringRequest = Sys.Net.WebServiceProxy.invoke(this._stateService, "GetLineControl", false, params, Function.createDelegate(this, this._onGetLineControlComplete), Function.createDelegate(this, this._onGetLineControlFailed), null, 20000);
            }
        }

    },

    _stopMonitoring: function() {
        if (this._monitoringRequest != null) {
            this._monitoringRequest.get_executor().abort();
            this._monitoringRequest = null;
        }

        if (this._waitingMonitoringRequest != null) {
            window.clearInterval(this._waitingMonitoringRequest);
            this._waitingMonitoringRequest = null;
        }
    },

    _restartMonitoring: function() {
        this._stopMonitoring();
        this._startMonitoring();
    },

    _onGetLineControlComplete: function(result, context) {
        var lc = this.get_lineControl();
        var frombusy = false;
        if (lc.status == Wybecom.TalkPortal.CTI.Proxy.LCS.Status.busy || lc.status == Wybecom.TalkPortal.CTI.Proxy.LCS.Status.ringing) {
            frombusy = true;
        }
        if (lc.forward != result.linecontrol.forward) {
            var eventargs = new Wybecom.TalkPortal.CTI.Controls.lineForwardChangedEventArgs(null);
            eventargs.set_forward(result.linecontrol.forward);
            this.raiseLineForwardChanged(eventargs);
        }
        this.set_lineControl(result.linecontrol);

        this.set_token(result.token);
        this._changeStateControl();
        this._setControl();
        switch (result.linecontrol.status) {
            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.available:
                if (frombusy) {
                    var callEndArgs = new Wybecom.TalkPortal.CTI.Controls.callEndedEventArgs(null);
                    callEndArgs.set_lineControlConnection(lc.lineControlConnection);
                    callEndArgs.set_callid(lc.lineControlConnection[0].callid);
                    this._callEndedCallBack(callEndArgs);
                }
                break;
            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.donotdisturb:
                break;
            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.forwarded:
                break;
            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.busy:
                break;
            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.hidden:
                break;
            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.dialing:
                break;
            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.ringing:
                var eventArgs = new Wybecom.TalkPortal.CTI.Controls.callReceivedEventArgs(null, null);

                if (result.linecontrol.lineControlConnection != null) {
                    for (lcCompteur = 0; lcCompteur < result.linecontrol.lineControlConnection.length; lcCompteur = lcCompteur + 1) {
                        var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
                        var lcc = result.linecontrol.lineControlConnection[lcCompteur];
                        if (lcc.state == Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.alerting) {
                            eventArgs.set_callid(lcc.callid);
                            eventArgs.set_caller(lcc.contact);
                        }
                    }
                }
                this._callReceivedCallBack(eventArgs);
                break;
            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown:
                break;
            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.inactive:
                if (this._showAlert) {
                    alert("Inactive line");
                }
                break;
        }

        if (result.linecontrol.lineControlConnection != null && lc.lineControlConnection != null) {
            if (result.linecontrol.lineControlConnection.length < lc.lineControlConnection.length) {
                //parcourir les deux tableaux et trouver l'appel terminé...
                var callid = "";
                for (lcCompteur = 0; lcCompteur < lc.lineControlConnection.length; lcCompteur = lcCompteur + 1) {
                    var finded = false;
                    for (lcCpt = 0; lcCpt < result.linecontrol.lineControlConnection.length; lcCpt = lcCpt + 1) {
                        if (result.linecontrol.lineControlConnection[lcCpt].callid == lc.lineControlConnection[lcCompteur].callid) {
                            finded = true;
                        }
                    }
                    if (!finded) {
                        callid = lc.lineControlConnection[lcCompteur].callid;
                    }
                }
                var callEndArgs = new Wybecom.TalkPortal.CTI.Controls.callEndedEventArgs(null);
                //all linecontrolconnections even to get informations about precedent call (with callid)
                callEndArgs.set_lineControlConnection(lc.lineControlConnection);
                callEndArgs.set_callid(callid);
                this._callEndedCallBack(callEndArgs);
            }
        }
    },

    _onGetLineControlFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
            this._stopMonitoring();

        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't monitor this line!");
            }
        }
        if (err.get_timedOut()) {
            this._startMonitoring();
        }
    },

    _changeStateControl: function() {
        if (this._statecontent != null) {
            this._statecontent.innerHTML = this._getStateContent();
        }
    },

    _getStateContent: function() {
        var content = "";
        if (this._lineControl != null && this._statecontent != null) {
            if (this._lineControl.forward != "") {
                content += "Renvoyé vers " + this._lineControl.forward + "<br/>";
            }
            if (this._lineControl.doNotDisturb) {
                content += "Ne pas déranger<br/>";
            }
            if (this._lineControl.mwiOn) {
                content += "Vous avez un message vocal<br/>";
            }
            if (this._lineControl.monitored != "") {
                content += "<div id=\"monitordiv\"><font color='#ff0000'>Ecouté par <span id=\"monitor_identity\">" + this._lineControl.monitored + "</span></font></div><br/>";
                this._monitorLookup(this._lineControl.monitored);
            }
            switch (this._lineControl.status) {
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.available:
                    content += "Disponible";
                    break;
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.busy:
                    if (this._lineControl.lineControlConnection != null) {
                        for (lcCompteur = 0; lcCompteur < this._lineControl.lineControlConnection.length; lcCompteur = lcCompteur + 1) {
                            var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
                            var lcc = this._lineControl.lineControlConnection[lcCompteur];
                            switch (lcc.state) {
                                case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.established:
                                    switch (lcc.remoteState) {
                                        case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.established:
                                            content += "En ligne avec <span id=\"contact_identity\">" + lcc.contact + "</span>";
                                            this._lookup(lcc.contact);
                                            break;
                                        case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.alerting:
                                            content += "Numérotation en cours vers <span id=\"contact_identity\">" + lcc.contact + "</span>";
                                            this._lookup(lcc.contact);
                                            break;
                                    }
                                    break;
                                case Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.initiated:
                                    content += "Préparation pour appel ";
                                    break;
                            }

                        }
                    }
                    break;
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.dialing:
                    if (this._lineControl.lineControlConnection != null) {
                        for (lcCompteur = 0; lcCompteur < this._lineControl.lineControlConnection.length; lcCompteur = lcCompteur + 1) {
                            var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
                            var lcc = this._lineControl.lineControlConnection[lcCompteur];
                            if (lcc.state == Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.dialing) {
                                content += "Numérotation en cours vers <span id=\"contact_identity\">" + lcc.contact + "</span>";
                                this._lookup(lcc.contact);
                            }
                        }
                    }
                    break;
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.ringing:
                    if (this._lineControl.lineControlConnection != null) {
                        for (lcCompteur = 0; lcCompteur < this._lineControl.lineControlConnection.length; lcCompteur = lcCompteur + 1) {
                            var lcc = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineControlConnection();
                            var lcc = this._lineControl.lineControlConnection[lcCompteur];
                            if (lcc.state == Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.alerting || lcc.state == Wybecom.TalkPortal.CTI.Proxy.LCS.ConnectionState.offered) {
                                content += "Appel de <span id=\"contact_identity\">" + lcc.contact + "</span>";
                                this._lookup(lcc.contact);
                            }
                        }
                    }
                    break;
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown:
                    content += "<font color='#ff0000'>Etat de la ligne inconnu</font>";
                    break;
                case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.inactive:
                    content += "Déconnecté";
                    break;
            }
            return content
        }
    },

    _setControl: function() {
        if (this._phoneControl != null) {

        }
        else {
            this._phoneControl = $get(this.get_id() + "_phonecontrol");
        }
        this._removeControlHandlers();
        var phone = "<table class=\"toolbar\"><tr>";
        phone += this.get_callPhoneControl();
        phone += this.get_transferPhoneControl();
        phone += this.get_consulttransferPhoneControl();
        phone += this.get_holdPhoneControl();
        phone += this.get_dndPhoneControl();
        phone += this.get_mevoPhoneControl();
        phone += this.get_directoryPhoneControl();
        phone += this.get_callLogsPhoneControl();
        phone += this.get_monitorPhoneControl();
        phone += "</tr></table>";
        this._phoneControl.innerHTML = phone;
        this._setControlElement();
        this._addControlHandlers();
    },

    _startPresence: function(linesStatus, context, OnSuccess, OnFailed) {
        this._stopPresence();
        this._presenceRequest = Sys.Net.WebServiceProxy.invoke(this._stateService, "GetLinesStatus", false, linesStatus, OnSuccess, OnFailed, context, 20000);
    },

    _stopPresence: function() {
        if (this._presenceRequest != null) {
            this._presenceRequest.get_executor().abort();
        }
        if (this._waitingPresenceRequest != null) {
            window.clearInterval(this._waitingPresenceRequest);
            this._waitingPresenceRequest = null;
        }
    },



    _onCallSuccess: function(result, context) {
        //trace
        if (this._tbinput != null && this._tbinput.value != "") {
            this._tbinput.value = "";
        }
        this.set_displayInput(false);
        this.set_token(result.token);
        if (result.callid != '') {
            var callinit = new Wybecom.TalkPortal.CTI.Controls.callInitializedEventArgs(null);
            callinit.set_callid(result.callid);
            this.raiseCallInitialized(callinit);
        }
    },

    _onCallFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't call with this line!");
            }
        }
        this.set_displayInput(false);
    },

    _onForwardSuccess: function(result, context) {
        //trace
        if (this._tbinput != null && this._tbinput.value != "") {
            this._tbinput.value = "";
        }
        this.set_displayInput(false);
        this.set_token(result.token);
    },

    _onForwardFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't forward with this line!");
            }
        }
        this.set_displayInput(false);
    },

    _onUnHookSuccess: function(result, context) {
        //trace
        this.set_token(result.token);
    },

    _onUnHookFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't unhook with this line!");
            }
        }
    },

    _onHangUpSuccess: function(result, context) {
        //trace
        this.set_token(result.token);
    },

    _onHangUpFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't hangup with this line!");
            }
        }
    },

    _onDoNotDisturbSuccess: function(result, context) {
        //trace
        this.set_token(result.token);
    },

    _onDoNotDisturbFailed: function(err, response, context) {
        this._exceptioneHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't set donotdisturb with this line!");
            }
        }
    },

    _onHoldSuccess: function(result, context) {
        //trace
        this.set_token(result.token);
    },

    _onHoldFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't hold with this line!");
            }
        }
    },

    _onUnHoldSuccess: function(result, context) {
        //trace
        this.set_token(result.token);
    },

    _onUnHoldFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't unhold with this line!");
            }
        }
    },

    _onDivertSuccess: function(result, context) {
        //trace
        this.set_token(result.token);
    },

    _onDivertFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't divert with this line!");
            }
        }
    },

    _onTransferSuccess: function(result, context) {
        //trace

        this.set_token(result.token);
    },

    _onTransferFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't transfer with this line!");
            }
        }
        this.set_displayInput(false);
    },

    _onConsultTransferSuccess: function(result, context) {
        //trace

        this.set_token(result.token);
    },

    _onConsultTransferFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't transfer with this line!");
            }
        }
        this.set_displayInput(false);
    },

    _onMonitorSuccess: function(result, context) {
        //trace
        if (this._tbinput != null && this._tbinput.value != "") {
            this._tbinput.value = "";
        }
        this.set_displayInput(false);
        this.set_token(result.token);
    },

    _onMonitorFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't call with this line!");
            }
        }
        this.set_displayInput(false);
    },

    _onLoginSuccess: function(result, context) {
        //trace
        this.set_token(result.token);
    },

    _onLoginFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1 && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException' && err.get_exceptionType() != 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationExpiredException') {
            if (this.get_showAlert()) {
                alert("Authentication failed: token has expired!");
            }
        }
        else if (err.get_exceptionType() == 'Wybecom.TalkPortal.Providers.AuthenticationMismatchException') {
            if (this.get_showAlert()) {
                alert("Authentication mismatch: you can't call with this line!");
            }
        }
    },

    _onCodifCallSuccess: function(result, context) {
        if (this.get_showCallResultRememberAlert() && result > 0) {
            alert("Vous n'avez pas codifier vos derniers appels, " + result + " appel(s) ne seront pas codifiés");
        }
        disableCodifPopup();
        this._startMonitoring();
    },

    _onCodifCallFailed: function(err, response, context) {
        this._startMonitoring();
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
        var codiferror = $get("popupCodifError");
        if (codiferror != null) {
            codiferror.innerHTML = "La codification de l'appel a echouee, contacter votre administrateur";
        }
    },

    _onGetCallSuccess: function(result, context) {
        var codiferror = $get("popupCodifError");
        if (codiferror != null) {
            codiferror.innerHTML = "";
        }
        this.set_callToCodif(result);
    },

    _onGetCallFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
    },

    _onGetCodifSuccess: function(result, context) {
        var codiflist = $get("popupCodifList");
        var opts = "<select id='selectCodif'>";
        for (nbresult = 0; nbresult <= result.length - 1; nbresult = nbresult + 1) {
            opts += "<option value='" + result[nbresult] + "'>" + result[nbresult] + "</option>";
        }
        codiflist.innerHTML = opts + "</select>";
    },

    _onGetCodifFailed: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
    },

    _onAuthenticateSuccess: function(result, context) {
        //trace
        this._token = result;
        this._restartMonitoring();
        this._GetSpeedDial();
    },

    _onAuthenticateFailure: function(err, response, context) {
        this._exceptionHandlerCallBack(err);
        if (err.get_statusCode() != -1) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
          "Error: " + err.get_message() + "/r/n" +
          "Status Code: " + err.get_statusCode() + "/r/n" +
          "Exception Type: " + err.get_exceptionType() + "/r/n" +
          "Timed Out: " + err.get_timedOut());
            }
        }
    },

    _GetSpeedDial: function() {
        var params = null;
        if (this._speedDialService != null && this._token != null && this._monitoredLine != null && this._monitoredLine != "") {
            params = { extension: this._monitoredLine, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._speedDialService, "GetSpeedDials", false, params, Function.createDelegate(this, this._onGetSpeedDialSuccess), Function.createDelegate(this, this._onGetSpeedDialFailure), null, 10000);
        }
    },

    _onGetSpeedDialSuccess: function(result, context) {
        this._speedDials = result;
    },

    _onGetSpeedDialFailure: function(err, response, context) {
        if (err.get_statusCode() != -1) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
              "Error: " + err.get_message() + "/r/n" +
              "Status Code: " + err.get_statusCode() + "/r/n" +
              "Exception Type: " + err.get_exceptionType() + "/r/n" +
              "Timed Out: " + err.get_timedOut());
            }
        }
    },

    _AddSpeedDial: function(speeddial) {
        var params = null;
        if (this._speedDialService != null && this._token != null && this._monitoredLine != null && this._monitoredLine != "") {
            params = { extension: this._monitoredLine, speeddial: speeddial, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._speedDialService, "AddSpeedDial", false, params, Function.createDelegate(this, this._onAddSpeedDialSuccess), Function.createDelegate(this, this._onAddSpeedDialFailure), null, 10000);
        }
    },

    _onAddSpeedDialSuccess: function(result, context) {
        this._speedDials.push(context);
    },

    _onAddSpeedDialFailure: function(err, response, context) {
        if (err.get_statusCode() != -1) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
                  "Error: " + err.get_message() + "/r/n" +
                  "Status Code: " + err.get_statusCode() + "/r/n" +
                  "Exception Type: " + err.get_exceptionType() + "/r/n" +
                  "Timed Out: " + err.get_timedOut());
            }
        }
    },

    _RemoveSpeedDial: function(speeddial) {
        var params = null;
        if (this._speedDialService != null && this._token != null && this._monitoredLine != null && this._monitoredLine != "") {
            params = { extension: this._monitoredLine, speeddial: speeddial, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._speedDialService, "RemoveSpeedDial", false, params, Function.createDelegate(this, this._onRemoveSpeedDialSuccess), Function.createDelegate(this, this._onRemoveSpeedDialFailure), null, 10000);
        }
    },

    _onRemoveSpeedDialSuccess: function(result, context) {
    },

    _onRemoveSpeedDialFailure: function(err, response, context) {
        if (err.get_statusCode() != -1) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
                  "Error: " + err.get_message() + "/r/n" +
                  "Status Code: " + err.get_statusCode() + "/r/n" +
                  "Exception Type: " + err.get_exceptionType() + "/r/n" +
                  "Timed Out: " + err.get_timedOut());
            }
        }
    },

    _EditSpeedDial: function(exspeeddial, newspeeddial) {
        var params = null;
        if (this._speedDialService != null && this._token != null && this._monitoredLine != null && this._monitoredLine != "") {
            params = { extension: this._monitoredLine, newspeeddial: newspeeddial, exspeeddial: exspeeddial, token: this._token };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._speedDialService, "EditSpeedDial", false, params, Function.createDelegate(this, this._onEditSpeedDialSuccess), Function.createDelegate(this, this._onEditSpeedDialFailure), null, 10000);
        }
    },

    _onEditSpeedDialSuccess: function(result, context) {
    },

    _onEditSpeedDialFailure: function(err, response, context) {
        if (err.get_statusCode() != -1) {
            if (this.get_showAlert()) {
                alert("Stack Trace: " + err.get_stackTrace() + "/r/n" +
                  "Error: " + err.get_message() + "/r/n" +
                  "Status Code: " + err.get_statusCode() + "/r/n" +
                  "Exception Type: " + err.get_exceptionType() + "/r/n" +
                  "Timed Out: " + err.get_timedOut());
            }
        }
    },


    //events
    get_events: function() {
        if (!this._events) { this._events = new Sys.EventHandlerList(); }
        return this._events;
    },

    add_callInitialized: function(handler) {
        this.get_events().addHandler("callInitialized", handler);
    },

    remove_callInitialized: function(handler) {
        this.get_events().removeHandler("callInitialized", handler);
    },

    raiseCallInitialized: function(eventArgs) {
        var handler = this.get_events().getHandler("callInitialized");
        if (handler) {
            handler(this, eventArgs);
        }
    },

    add_callReceived: function(handler) {
        this.get_events().addHandler("callReceived", handler);
    },

    remove_callReceived: function(handler) {
        this.get_events().removeHandler("callReceived", handler);
    },

    _callReceivedCallBack: function(eventArgs) {
        var handler = this.get_events().getHandler("callReceived");
        if (handler) {
            handler(this, eventArgs);
        }
    },

    add_directoryClicked: function(handler) {
        this.get_events().addHandler("directoryClicked", handler);
    },

    remove_directoryClicked: function(handler) {
        this.get_events().removeHandler("directoryClicked", handler);
    },

    _directoryClickedCallBack: function() {
        var handler = this.get_events().getHandler("directoryClicked");
        if (handler) {
            handler(this, Sys.EventArgs.Empty);
        }
    },

    add_callEnded: function(handler) {
        this.get_events().addHandler("callEnded", handler);
    },

    remove_callEnded: function(handler) {
        this.get_events().removeHandler("callEnded", handler);
    },

    _callEndedCallBack: function(eventArgs) {
        var handler = this.get_events().getHandler("callEnded");
        if (handler) {
            handler(this, eventArgs);
        }
        if (this._currentCall != eventArgs.get_callid()) {
            this._currentCall = eventArgs.get_callid();
            this._getCall(eventArgs.get_callid());
        }
    },

    add_newCallToCodif: function(handler) {
        this.get_events().addHandler("newCallToCodif", handler);
    },

    remove_newCallToCodif: function(handler) {
        this.get_events().removeHandler("newCallToCodif", handler);
    },

    _newCallToCodifCallBack: function(eventArgs) {
        var handler = this.get_events().getHandler("newCallToCodif");
        if (handler) {
            handler(this, eventArgs);
        }
        if (this._enableCodif) {
            try {
                if (eventArgs != null && eventArgs.type != null) {
                    switch (this._codifMode) {
                        case Wybecom.TalkPortal.CTI.Controls.CodifMode.allcalls:
                            this._loadCodifPopup(eventArgs);
                            break;
                        case Wybecom.TalkPortal.CTI.Controls.CodifMode.placedcalls:
                            if (eventArgs.type == Wybecom.TalkPortal.CTI.Proxy.CLS.CallType.placed) {
                                this._loadCodifPopup(eventArgs);
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Controls.CodifMode.receivedcalls:
                            if (eventArgs.type == Wybecom.TalkPortal.CTI.Proxy.CLS.CallType.received) {
                                this._loadCodifPopup(eventArgs);
                            }
                            break;
                    }
                }
            } catch (exp) {
            }
        }
    },

    add_exceptionHandler: function(handler) {
        this.get_events().addHandler("exceptionHandler", handler);
    },

    remove_exceptionHandler: function(handler) {
        this.get_events().removeHandler("exceptionHandler", handler);
    },

    _exceptionHandlerCallBack: function(eventArgs) {
        var handler = this.get_events().getHandler("exceptionHandler");
        if (handler) {
            handler(this, eventArgs);
        }
    },

    add_lineControlChanged: function(handler) {
        /// <summary>
        /// Add an event handler for the lineControlChanged event
        /// </summary>
        /// <param name="handler" type="Function" mayBeNull="false">
        /// Event handler
        /// </param>
        /// <returns />
        this.get_events().addHandler('lineControlChanged', handler);
    },
    remove_lineControlChanged: function(handler) {
        /// <summary>
        /// Remove an event handler from the lineControlChanged event
        /// </summary>
        /// <param name="handler" type="Function" mayBeNull="false">
        /// Event handler
        /// </param>
        /// <returns />
        this.get_events().removeHandler('lineControlChanged', handler);
    },
    raiselineControlChanged: function(eventArgs) {
        /// <summary>
        /// Raise the lineControlChanged event
        /// </summary>
        /// <param name="eventArgs" type="Sys.CancelEventArgs" mayBeNull="false">
        /// Event arguments for the lineControlChanged event
        /// </param>
        /// <returns />

        var handler = this.get_events().getHandler('lineControlChanged');
        if (handler) {
            handler(this, eventArgs);
        }
    },

    add_lineForwardChanged: function(handler) {
        /// <summary>
        /// Add an event handler for the lineForwardChanged event
        /// </summary>
        /// <param name="handler" type="Function" mayBeNull="false">
        /// Event handler
        /// </param>
        /// <returns />
        this.get_events().addHandler('lineForwardChanged', handler);
    },
    remove_lineForwardChanged: function(handler) {
        /// <summary>
        /// Remove an event handler from the lineForwardChanged event
        /// </summary>
        /// <param name="handler" type="Function" mayBeNull="false">
        /// Event handler
        /// </param>
        /// <returns />
        this.get_events().removeHandler('lineForwardChanged', handler);
    },
    raiseLineForwardChanged: function(eventArgs) {
        /// <summary>
        /// Raise the lineForwardChanged event
        /// </summary>
        /// <param name="eventArgs" type="Sys.CancelEventArgs" mayBeNull="false">
        /// Event arguments for the lineControlChanged event
        /// </param>
        /// <returns />

        var handler = this.get_events().getHandler('lineForwardChanged');
        if (handler) {
            handler(this, eventArgs);
        }
    }
}

Wybecom.TalkPortal.CTI.Controls.CTIClient.descriptor = {
    properties: [{ name: 'displayInput', type: String },
                    { name: 'displayCopyright', type: String },
                    { name: 'displayPhoneControl', type: String },
                    { name: 'displayStatus', type: String },
                    { name: 'monitoredLine', type: String },
                    { name: 'ctiService', type: String },
                    { name: 'stateService', type: String },
                    { name: 'callLogsService', type: String },
                    { name: 'mevoPilot', type: String },
                    { name: 'enableTransfer', type: String },
                    { name: 'enableHold', type: String },
                    { name: 'enableDnd', type: String },
                    { name: 'enableMevo', type: String },
                    { name: 'enableDirectory', type: String },
                    { name: 'enableCallLogs', type: String }, ],
    events: [{ name: 'callInitialized' }, { name: 'callReceived' }, { name: 'callEnded' }, { name: 'newCallToCodif' }, { name: 'directoryClicked' }, { name: 'exceptionHandler'}]
}


Wybecom.TalkPortal.CTI.Controls.CTIClient.registerClass('Wybecom.TalkPortal.CTI.Controls.CTIClient', Sys.UI.Control);

Wybecom.TalkPortal.CTI.Controls.callInitializedEventArgs = function(callid) {
    Wybecom.TalkPortal.CTI.Controls.callInitializedEventArgs.initializeBase(this);
    this._callid = callid;
}

Wybecom.TalkPortal.CTI.Controls.callInitializedEventArgs.prototype = {
    
    get_callid: function() {
        return this._callid;
    },
    set_callid: function(value) {
        this._callid = value;
    }
}

Wybecom.TalkPortal.CTI.Controls.callInitializedEventArgs.descriptor = {
    properties: [{ name: 'callid', type: String}]
}

Wybecom.TalkPortal.CTI.Controls.callInitializedEventArgs.registerClass('Wybecom.TalkPortal.CTI.Controls.callInitializedEventArgs', Sys.EventArgs);

Wybecom.TalkPortal.CTI.Controls.callReceivedEventArgs = function(caller, callid) {
    Wybecom.TalkPortal.CTI.Controls.callReceivedEventArgs.initializeBase(this);
    this._caller = caller;
    this._callid = callid;
}

Wybecom.TalkPortal.CTI.Controls.callReceivedEventArgs.prototype = {
    get_caller: function() {
        return this._caller;
    },
    set_caller: function(value) {
        this._caller = value;
    },
    get_callid: function() {
        return this._callid;
    },
    set_callid: function(value) {
        this._callid = value;
    }
}

Wybecom.TalkPortal.CTI.Controls.callReceivedEventArgs.descriptor = {
    properties: [{ name: 'caller', type: String },
                    { name: 'callid', type: String}]
}

Wybecom.TalkPortal.CTI.Controls.callReceivedEventArgs.registerClass('Wybecom.TalkPortal.CTI.Controls.callReceivedEventArgs', Sys.EventArgs);

Wybecom.TalkPortal.CTI.Controls.callEndedEventArgs = function(lineControlConnection, callid) {
    Wybecom.TalkPortal.CTI.Controls.callEndedEventArgs.initializeBase(this);
    this._lineControlConnection = lineControlConnection;
    this._callid = callid;
}

Wybecom.TalkPortal.CTI.Controls.callEndedEventArgs.prototype = {
    get_lineControlConnection: function() {
        return this._lineControlConnection;
    },
    set_lineControlConnection: function(value) {
        this._lineControlConnection = value;
    },

    get_callid: function() {
        return this._callid;
    },
    set_callid: function(value) {
        this._callid = value;
    }
}

Wybecom.TalkPortal.CTI.Controls.callEndedEventArgs.descriptor = {
properties: [{ name: 'lineControlConnection', type: Object }, { name: 'callid', type: Object}]
}

Wybecom.TalkPortal.CTI.Controls.callEndedEventArgs.registerClass('Wybecom.TalkPortal.CTI.Controls.callEndedEventArgs', Sys.EventArgs);


Wybecom.TalkPortal.CTI.Controls.lineControlChangedEventArgs = function(lineControl) {
    Wybecom.TalkPortal.CTI.Controls.lineControlChangedEventArgs.initializeBase(this);
    this._lineControl = lineControl;
}

Wybecom.TalkPortal.CTI.Controls.lineControlChangedEventArgs.prototype = {
    get_lineControl: function() {
        return this._lineControl;
    },
    set_lineControl: function(value) {
        this._lineControl = value;
    }
}

Wybecom.TalkPortal.CTI.Controls.lineControlChangedEventArgs.descriptor = {
    properties: [{ name: 'lineControl', type: Object}]
}

Wybecom.TalkPortal.CTI.Controls.lineControlChangedEventArgs.registerClass('Wybecom.TalkPortal.CTI.Controls.lineControlChangedEventArgs', Sys.EventArgs);

Wybecom.TalkPortal.CTI.Controls.lineForwardChangedEventArgs = function(forward) {
    Wybecom.TalkPortal.CTI.Controls.lineForwardChangedEventArgs.initializeBase(this);
    this._forward = forward;
}

Wybecom.TalkPortal.CTI.Controls.lineForwardChangedEventArgs.prototype = {
    get_forward: function() {
        return this._forward;
    },
    set_forward: function(value) {
        this._forward = value;
    }
}

Wybecom.TalkPortal.CTI.Controls.lineForwardChangedEventArgs.descriptor = {
    properties: [{ name: 'forward', type: Object}]
}

Wybecom.TalkPortal.CTI.Controls.lineForwardChangedEventArgs.registerClass('Wybecom.TalkPortal.CTI.Controls.lineForwardChangedEventArgs', Sys.EventArgs);

if (typeof (Wybecom.TalkPortal.CTI.Controls.AuthenticationMode) === 'undefined') {
    Wybecom.TalkPortal.CTI.Controls.AuthenticationMode = function() { throw Error.invalidOperation(); }
    Wybecom.TalkPortal.CTI.Controls.AuthenticationMode.prototype = { trusted: 0, manual: 1, sso: 2}
    Wybecom.TalkPortal.CTI.Controls.AuthenticationMode.registerEnum('Wybecom.TalkPortal.CTI.Controls.AuthenticationMode', true);
}

if (typeof (Wybecom.TalkPortal.CTI.Controls.CodifMode) === 'undefined') {
    Wybecom.TalkPortal.CTI.Controls.CodifMode = function() { throw Error.invalidOperation(); }
    Wybecom.TalkPortal.CTI.Controls.CodifMode.prototype = { allcalls: 0, placedcalls: 1, receivedcalls: 2 }
    Wybecom.TalkPortal.CTI.Controls.CodifMode.registerEnum('Wybecom.TalkPortal.CTI.Controls.CodifMode', true);
}


if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

//Wybecom.TalkPortal.CTI.Controls.CTIWSClient = function() {
//    Wybecom.TalkPortal.CTI.Controls.CTIWSClient.initializeBase(this);
//    this._timeout = 0;
//    this._userContext = null;
//    this._succeeded = null;
//    this._failed = null;
//    this._wsUrl = null;
//    this._answerWebMethod = null;
//}

//Wybecom.TalkPortal.CTI.Controls.CTIWSClient.prototype = {
//    Answer: function(callee, succeededCallback, failedCallback, userContext) {
//        return this._invoke(Wybecom.TalkPortal.CTI.Controls.CTIWSClient.get_path(), this._answerWebMethod, false, {}, succeededCallback, failedCallback, userContext);
//    }
//}
//Wybecom.TalkPortal.CTI.Controls.CTIWSClient.registerClass('Wybecom.TalkPortal.CTI.Controls.CTIWSClient', Sys.Net.WebServiceProxy);
//Wybecom.TalkPortal.CTI.Controls.CTIWSClient._staticInstance = new 