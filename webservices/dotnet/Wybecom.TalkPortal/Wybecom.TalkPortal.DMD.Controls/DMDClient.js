/// <reference name="MicrosoftAjax.js"/>


Type.registerNamespace("Wybecom.TalkPortal.DMD.Controls");

Wybecom.TalkPortal.DMD.Controls.DMDClient = function(element) {
    Wybecom.TalkPortal.DMD.Controls.DMDClient.initializeBase(this, [element]);
    this._dmdclient = null;
    this._filterGroup = null;
    this._button = null;
    this._dmdService = null;
    this._fieldstype = null;
    this._results = null;
    this._searchRequest = null;
    //    this._callableFields = null;
    //    this._presenceField = null;
    this._sortEnable = true;
    this._pageEnabled = true;
    this._directoryName = null;
    this._presenceArray = null;
    this._currentPage = 0;
    this._availableImageUrl = null;
    this._availableImage = new Image(16, 16);
    this._busyImageUrl = null;
    this._busyImage = new Image(16, 16);
    this._logoutImageUrl = null;
    this._logoutImage = new Image(16, 16);
    this._privateImageUrl = null;
    this._privateImage = new Image(16, 16);
    this._unknownImageUrl = null;
    this._unknownImage = new Image(16, 16);
    this._directTransferImageUrl = null;
    this._directTransferImage = new Image(16, 16);
    this._consultTransferImageUrl = null;
    this._consultTransferImage = new Image(16, 16);
    this._dnForwardImageUrl = null;
    this._dnForwardImage = new Image(16, 16);
    this._mevoForwardImageUrl = null;
    this._mevoForwardImage = new Image(16, 16);
    this._dndImageUrl = null;
    this._speeddialImage = new Image(16, 16);
    this._speeddialImageUrl = null;
    this._monitorImageUrl = null;
    this._monitorImage = new Image(16, 16);
    this._dndImage = new Image(16, 16);
    this._ctiClient = null;
    this._callableFieldArray = null;
    this._target = null;
    this._enableTransfer = false;
    this._enableConsultTransfer = true;
    this._enableGSMTransfer = false;
    this._enableGSMConsultTransfer = false;
    this._lineControlStatus = null;
    this._presenceIsEnabled = false;
    this._speeddials = null;
    this._enableSpeedDials = false;
    this._nbSpeedDials = 0;
    this._transferButton = null;
    this._currentTransferDestination = "";
    this._currentTransferFrom = "";
    this._enableTransferLookup = false;
    this._enableMonitor = false;
    this._showAlert = false;
    this._rowsPerPage = 15;
}

Wybecom.TalkPortal.DMD.Controls.DMDClient.prototype = {
    initialize: function() {
        Wybecom.TalkPortal.DMD.Controls.DMDClient.callBaseMethod(this, 'initialize');

        // Ajouter ici une initialisation personnalisée
        this._dmdclient = $get(this.get_id());
        this._button = $get(this.get_id() + '_button_search');
        if (this._target != null && this._target != "") {
            this._results = $get(this._target);
        }
        else {
            this._results = $get(this.get_id() + '_results');
        }
        this._transferButton = $get('popupTransferButton');
        this._transferButton_onclick$delegate = Function.createDelegate(this, this._transferButton_onclick);
        this._button_onclick$delegate = Function.createDelegate(this, this._button_onclick);
        this._searchtb_onenter$delegate = Function.createDelegate(this, this._searchtb_onenter);
        this._searchtb_onfocus$delegate = Function.createDelegate(this, this._searchtb_onfocus);
        if (this._button != null) {
            $addHandlers(this._button, { click: this._button_onclick$delegate });
        }
        if (this._transferButton != null) {
            $addHandlers(this._transferButton, { click: this._transferButton_onclick$delegate });
        }
        for (i = 0; i < this._filterGroup.length; i = i + 1) {
            var tb = $get(this._filterGroup[i].get_text());
            if (tb != null) {
                $addHandlers(tb, { keypress: this._searchtb_onenter$delegate });
                $addHandlers(tb, { focus: this._searchtb_onfocus$delegate });
            }
        }
        if (this._availableImageUrl != null) {
            this._availableImage = this._availableImageUrl;
        }
        if (this._busyImageUrl != null) {
            this._busyImage = this._busyImageUrl;
        }
        if (this._logoutImageUrl != null) {
            this._logoutImage = this._logoutImageUrl;
        }
        if (this._privateImageUrl != null) {
            this._privateImage = this._privateImageUrl;
        }
        if (this._unknownImageUrl != null) {
            this._unknownImage = this._unknownImageUrl;
        }
        if (this._directTransferImageUrl != null) {
            this._directTransferImage = this._directTransferImageUrl;
        }
        if (this._consultTransferImageUrl != null) {
            this._consultTransferImage = this._consultTransferImageUrl;
        }
        if (this._dnForwardImageUrl != null) {
            this._dnForwardImage = this._dnForwardImageUrl;
        }
        if (this._mevoForwardImageUrl != null) {
            this._mevoForwardImage = this._mevoForwardImageUrl;
        }
        if (this._dndImageUrl != null) {
            this._dndImage = this._dndImageUrl;
        }
        if (this._speeddialImageUrl != null) {
            this._speeddialImage = this._speeddialImageUrl;
        }
        if (this._monitorImageUrl != null) {
            this._monitorImage = this._monitorImageUrl;
        }
    },
    dispose: function() {
        //Ajouter ici des actions dispose personnalisées
        Wybecom.TalkPortal.DMD.Controls.DMDClient.callBaseMethod(this, 'dispose');
        if (this._button != null) {
            $common.removeHandlers(this._button, { click: this._button_onclick$delegate });
        }
    },


    _button_onclick: function() {
        this._Search();
    },

    _transferButton_onclick: function() {
        if (this._ctiClient != null) {
            this._ctiClient._ConsultTransfer('', this._currentTransferDestination);
            disablePopup();
        }
    },

    _searchtb_onenter: function(evt) {
        if (window.event) { e = window.event || evt; }
        var key = e.keyCode || e.which;
        if (e.keyCode == 13) {
            this._Search();
        }
    },

    _searchtb_onfocus: function(e) {
        if (ace != null) {
            var aceId = e.target.id.substr(0, e.target.id.lastIndexOf('_') + 1);
            aceId += 'ACE';
            aceId += e.target.id.substr(e.target.id.lastIndexOf('_'), e.target.id.length);
            var ace = $find(aceId);

            var contextKey = this._directoryName;
            var lastContext = '';
            for (i = 0; i < this._filterGroup.length; i = i + 1) {
                var tb = $get(this._filterGroup[i].get_text());
                if (this._filterGroup[i].get_text() == e.target.id) {
                    contextKey += "," + this._filterGroup[i].get_filter();
                }
                else if (tb != null && tb.value != null && tb.value != '') {
                    lastContext += "," + this._filterGroup[i].get_filter() + " LIKE '" + tb.value + "*'";
                }
            }

            ace.set_contextKey(contextKey + lastContext);
        }
    },

    _Search: function(sort) {
        var filterRequest = '';
        if (this._filterGroup != null) {
            for (i = 0; i < this._filterGroup.length; i = i + 1) {

                if (filterRequest != '') {
                    filterRequest += ' AND ' + this._filterGroup[i].get_filter() + ' LIKE \'' + $get(this._filterGroup[i].get_text()).value + '*\'';
                }
                else {
                    filterRequest += this._filterGroup[i].get_filter() + ' LIKE \'' + $get(this._filterGroup[i].get_text()).value + '*\'';
                }
            }
        }
        var params = null;

        if (sort != null) {
            params = { directoryName: this._directoryName, filter: filterRequest, sort: sort };
            this._searchRequest = Sys.Net.WebServiceProxy.invoke(this._dmdService, "SortSearch", false, params, Function.createDelegate(this, this._onSearchSuccess), Function.createDelegate(this, this._onSearchFailed), null, 20000);
        }
        else {
            params = { directoryName: this._directoryName, filter: filterRequest };
            this._searchRequest = Sys.Net.WebServiceProxy.invoke(this._dmdService, "Search", false, params, Function.createDelegate(this, this._onSearchSuccess), Function.createDelegate(this, this._onSearchFailed), null, 20000);
        }
    },

    get_filterGroup: function() {
        if (this._filterGroup == null) {
            return this._filterGroup = [];
        }
        return this._filterGroup;
    },
    set_filterGroup: function(value) {
        this._filterGroup = value;

        this.raisePropertyChanged("filterGroup");
    },

    get_dmdService: function() {
        return this._dmdService;
    },
    set_dmdService: function(value) {
        this._dmdService = value;
        this._getMetaData();
        this.raisePropertyChanged("dmdService");
    },

    //    get_callableFields: function() {
    //        return this._callableFields;
    //    },

    //    set_callableFields: function(value) {
    //        this._callableFields = value;
    //        this._callableFieldArray = this._callableFields.split();
    //        this.raisePropertyChanged("callableFields");
    //    },

    //    get_presenceField: function() {
    //        return this._presenceField;
    //    },

    //    set_presenceField: function(value) {
    //        this._presenceField = value;
    //        this.raisePropertyChanged("presenceField");
    //    },

    get_sortEnabled: function() {
        return this._sortEnabled;
    },

    set_sortEnabled: function(value) {
        this._sortEnabled = value;
        this.raisePropertyChanged("sortEnabled");
    },

    get_rowsPerPage: function() {
        return this._rowsPerPage;
    },

    set_rowsPerPage: function(value) {
        this._rowsPerPage = value;
        this.raisePropertyChanged("rowsPerPage");
    },

    get_pageEnabled: function() {
        return this._pageEnabled;
    },

    set_pageEnabled: function(value) {
        this._pageEnabled = value;
        this.raisePropertyChanged("pageEnabled");
    },

    get_directoryName: function() {
        return this._directoryName;
    },

    set_directoryName: function(value) {
        this._directoryName = value;
        this._getMetaData();
        this.raisePropertyChanged("directoryName");
    },

    get_availableImageUrl: function() {
        return this._availableImageUrl;
    },

    set_availableImageUrl: function(value) {
        this._availableImageUrl = value;
        this._availableImage.src = value;
        this.raisePropertyChanged("availableImageUrl");
    },

    get_busyImageUrl: function() {
        return this._busyImageUrl;
    },

    set_busyImageUrl: function(value) {
        this._busyImageUrl = value;
        this._busyImage.src = value;
        this.raisePropertyChanged("busyImageUrl");
    },

    get_logoutImageUrl: function() {
        return this._logoutImageUrl;
    },

    set_logoutImageUrl: function(value) {
        this._logoutImageUrl = value;
        this._logoutImage.src = value;
        this.raisePropertyChanged("logoutImageUrl");
    },

    get_privateImageUrl: function() {
        return this._privateImageUrl;
    },

    set_privateImageUrl: function(value) {
        this._privateImageUrl = value;
        this._privateImage.src = value;
        this.raisePropertyChanged("privateImageUrl");
    },

    get_unknownImageUrl: function() {
        return this._unknownImageUrl;
    },

    set_unknownImageUrl: function(value) {
        this._unknownImageUrl = value;
        this._unknownImage.src = value;
        this.raisePropertyChanged("unknownImageUrl");
    },

    get_directTransferImageUrl: function() {
        return this._directTransferImageUrl;
    },

    set_directTransferImageUrl: function(value) {
        this._directTransferImageUrl = value;
        this._directTransferImage.src = value;
        this.raisePropertyChanged("directTransferImageUrl");
    },

    get_consultTransferImageUrl: function() {
        return this._consultTransferImageUrl;
    },

    set_consultTransferImageUrl: function(value) {
        this._consultTransferImageUrl = value;
        this._consultTransferImage.src = value;
        this.raisePropertyChanged("consultTransferImageUrl");
    },

    get_dnForwardImageUrl: function() {
        return this._dnForwardImageUrl;
    },

    set_dnForwardImageUrl: function(value) {
        this._dnForwardImageUrl = value;
        this._dnForwardImage.src = value;
        this.raisePropertyChanged("dnForwardImageUrl");
    },

    get_mevoForwardImageUrl: function() {
        return this._mevoForwardImageUrl;
    },

    set_mevoForwardImageUrl: function(value) {
        this._mevoForwardImageUrl = value;
        this._mevoForwardImage.src = value;
        this.raisePropertyChanged("mevoForwardImageUrl");
    },

    get_dndImageUrl: function() {
        return this._dndImageUrl;
    },

    set_dndImageUrl: function(value) {
        this._dndImageUrl = value;
        this._dndImage.src = value;
        this.raisePropertyChanged("dndImageUrl");
    },

    get_speeddialImageUrl: function() {
        return this._speeddialImageUrl;
    },

    set_speeddialImageUrl: function(value) {
        this._speeddialImageUrl = value;
        this._speeddialImage.src = value;
        this.raisePropertyChanged("speeddialImageUrl");
    },

    get_monitorImageUrl: function() {
        return this._monitorImageUrl;
    },

    set_monitorImageUrl: function(value) {
        this._monitorImageUrl = value;
        this._monitorImage.src = value;
        this.raisePropertyChanged("monitorImageUrl");
    },

    get_enableTransfer: function() {
        return this._enableTransfer;
    },

    set_enableTransfer: function(value) {
        this._enableTransfer = value;
        this.raisePropertyChanged("enableTransfer");
    },

    get_enableConsultTransfer: function() {
        return this._enableConsultTransfer;
    },

    set_enableConsultTransfer: function(value) {
        this._enableConsultTransfer = value;
        this.raisePropertyChanged("enableConsultTransfer");
    },

    get_enableSpeedDials: function() {
        return this._enableSpeedDials;
    },

    set_enableSpeedDials: function(value) {
        this._enableSpeedDials = value;
        if (value) {
            this._speeddials = new Array();
        }
        this.raisePropertyChanged("enableSpeedDials");
    },

    get_enableTransferLookup: function() {
        return this._enableTransferLookup;
    },

    set_enableTransferLookup: function(value) {
        this._enableTransferLookup = value;
        this.raisePropertyChanged("enableTransferLookup");
    },

    get_enableMonitor: function() {
        return this._enableMonitor;
    },

    set_enableMonitor: function(value) {
        this._enableMonitor = value;
        this.raisePropertyChanged("enableMonitor");
    },

    get_ctiClient: function() {
        return this._ctiClient;
    },

    set_ctiClient: function(value) {
        this._ctiClient = value;
        this.raisePropertyChanged("ctiClient");
        if (value != null) {
            this._ctiClient.add_lineControlChanged(Function.createDelegate(this, this._onlineControlChanged));
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

    get_target: function() {
        return this._target;
    },

    set_target: function(value) {
        this._target = value;
        this.raisePropertyChanged("target");
    },

    _escape: function(header) {
        var reg_escape = new RegExp("(_x0020_)", "g");
        var reg_paropen = new RegExp("(_x0028_)", "g");
        var reg_parclose = new RegExp("(_x0029_)", "g");
        header = header.replace(reg_escape, " ");
        header = header.replace(reg_paropen, "(");
        header = header.replace(reg_parclose, ")");
        return header;
    },

    _getMetaData: function() {
        var params = null;
        if (this._directoryName != null && this._directoryName != "") {
            params = { directoryName: this._directoryName };
        }
        if (params != null && this._dmdService != null) {
            Sys.Net.WebServiceProxy.invoke(this._dmdService, "GetMetaData", false, params, Function.createDelegate(this, this._onGetMetaDataSuccess), Function.createDelegate(this, this._onGetMetaDataFailed), null, 20000);
        }
    },

    _onGetMetaDataSuccess: function(result) {
        this._fieldstype = result;
        var nbDn = 0;
        if (this._fieldstype != null && this._fieldstype.length > 0) {

            for (var nbField = 0; nbField < this._fieldstype.length; nbField++) {
                switch (this._fieldstype[nbField]) {
                    case FieldType.Telephone:
                        this._presenceIsEnabled = true;
                        nbDn++;
                        break;
                    case FieldType.GSM:
                        nbDn++;
                        break;
                }
            }
        }
        this._nbSpeedDials = nbDn;
    },

    _onGetMetaDataFailed: function(err) {
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

    _onSearchSuccess: function(result) {
        this._presenceArray = new Array();
        this._speeddials = new Array();
        var pagePresenceArray = new Array();
        var columnsHeader = new Array();
        var rows = '<tbody>';
        var rowHeader = '<thead><tr class="rowHeader">';
        var rowFooter = '<tfoot><tr class="rowFooter">';
        var item = result.getElementsByTagName('Results');
        var nbPage = Math.floor(item.length / this.get_rowsPerPage());
        var activePage = 0;
        this._currentPage = 0;
        var nbPageMod = item.length % this.get_rowsPerPage();
        if (nbPageMod > 0) {
            nbPage++;
        }
        for (nbResult = 0; nbResult < item.length; nbResult = nbResult + 1) {
            if (Math.floor(nbResult / this.get_rowsPerPage()) > activePage) {
                activePage++;
            }


            if ((nbResult % this.get_rowsPerPage()) == 0 && nbResult != 0) {
                this._presenceArray.push(pagePresenceArray);
                pagePresenceArray = new Array();
            }


            if (activePage == this._currentPage) {
                if ((nbResult % 2) > 0) {
                    rows += '<tr class="displayedPage_alternating">';
                }
                else {
                    rows += '<tr class="displayedPage">';
                }
            }
            else {
                rows += '<tr class="hiddenPage">';
            }
            var sdsToAdd = new Array();
            var currentSd = 0;
            for (currentSd; currentSd < this._nbSpeedDials; currentSd++) {
                var sd = new Object();
                sd.position = 0;
                sdsToAdd.push(sd);
            }
            currentSd = 0;
            for (nbColumns = 0; nbColumns < item[nbResult].childNodes.length; nbColumns = nbColumns + 1) {
                if (nbResult == 0) {
                    columnsHeader.push(item[nbResult].childNodes[nbColumns].tagName);
                }

                if (this._fieldstype != null) {
                    switch (this._fieldstype[nbColumns]) {
                        case FieldType.Identity:
                            if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                                rows += '<td align="center">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</td>';
                            }
                            else {
                                rows += '<td></td>';
                            }
                            if (this._enableSpeedDials) {
                                sdsToAdd[currentSd].displayName = item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue;
                            }
                            break;
                        case FieldType.Mail:
                            if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                                rows += '<td align="center"><a href="mailto:' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</a></td>';
                            }
                            else {
                                rows += '<td></td>';
                            }
                            break;
                        case FieldType.Telephone:

                            if (this._ctiClient != null) {
                                if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                                    if (this._enableSpeedDials) {
                                        sdsToAdd[currentSd].directoryNumber = item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue;
                                    }
                                    //                        if (activePage == this._currentPage) {
                                    //                            var lctl = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineStatus();
                                    //                            lctl.directoryNumber = item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue;
                                    //                            lctl.status = Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown;
                                    //                            pagePresenceArray.push(lctl);
                                    //                        }
                                    var lctl = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineStatus();
                                    lctl.directoryNumber = item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue;
                                    lctl.status = Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown;
                                    lctl.forward = "";
                                    lctl.doNotDisturb = false;
                                    lctl.mwiOn = false;
                                    pagePresenceArray.push(lctl);

                                    rows += '<td align="center">';
                                    if (this._enableSpeedDials) {
                                        rows += '<a href="javascript:$find(\'' + this.get_id() + '\')._addSpeedDial(' + ((this._nbSpeedDials * nbResult) + currentSd) + ')" style="text-decoration:none;border:none;"><img name="img_addSpeedDial' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="margin-right:5px;text-decoration:none;border:none;" alt="Ajouter aux numéros abrégés" title="Ajouter aux numéros abrégés" src="' + this._speeddialImage + '"/></a>';
                                    }
                                    rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Call(\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</a>';
                                    if (this._enableTransfer && item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue != "") {
                                        rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Transfer(\'\',\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')" style="text-decoration:none;border:none;"><img name="img_directTransfer' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;padding-left:5px;text-decoration:none;border:none;" alt="Transférer" title="Transférer" src="' + this._directTransferImage + '"/></a>';
                                    }
                                    if (this._enableConsultTransfer && item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue != "") {
                                        rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._ConsultTransfer(\'\',\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')" style="text-decoration:none;border:none;"><img name="img_consultTransfer' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;padding-left:5px;text-decoration:none;border:none;" alt="Transférer avec consultation" title="Transférer avec consultation" src="' + this._consultTransferImage + '"/></a>';
                                    }
                                    if (this._enableMonitor && item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue != "") {
                                        rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Monitor(\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')" style="text-decoration:none;border:none;"><img name="img_monitor' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;padding-left:5px;text-decoration:none;border:none;" alt="Ecoute discrete" title="Ecoute discrete" src="' + this._monitorImage + '"/></a>';
                                    }
                                    rows += '</td><td align="center"><img name="img_' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '"/><img name="img_dnForward' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;" alt="En renvoi" title="En renvoi" src="' + this._dnForwardImage + '"/><img name="img_mevoForward' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;" alt="En renvoi sur messagerie vocale" title="En renvoi sur messagerie vocale" src="' + this._mevoForwardImage + '"/><img name="img_dnd' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;" alt="Ne pas déranger" title="Ne pas déranger" src="' + this._dndImage + '"/></td>';
                                }
                                else {
                                    rows += '<td></td><td></td>';
                                }
                            }
                            else {
                                if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                                    //                                    sdsToAdd[currentSd].directoryNumber = item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue;
                                    rows += '<td align="center">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</td>';
                                } else {
                                    rows += '<td></td>';
                                }
                            }
                            currentSd++;
                            break;
                        case FieldType.GSM:
                            if (this._ctiClient != null) {
                                if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                                    if (this._enableSpeedDials) {
                                        sdsToAdd[currentSd].directoryNumber = item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue;
                                    }
                                    rows += '<td align="center">';
                                    if (this._enableSpeedDials) {
                                        rows += '<a href="javascript:$find(\'' + this.get_id() + '\')._addSpeedDial(' + ((nbResult * this._nbSpeedDials) + currentSd) + ')"><img name="img_addSpeedDial' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="margin-right:5px;" alt="Ajouter aux numéros abrégés" title="Ajouter aux numéros abrégés" src="' + this._speeddialImage + '"/></a>';
                                    }
                                    rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Call(\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</a>';
                                    if (this._enableGSMTransfer) {
                                        rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Transfer(\'\',\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')"><img name="img_directTransfer' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;" alt="Transférer" title="Transférer" src="' + this._directTransferImage + '"/></a>';
                                    }
                                    if (this._enableGSMConsultTransfer) {
                                        rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._ConsultTransfer(\'\',\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')"><img name="img_consultTransfer' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;" alt="Transférer avec consultation" title="Transférer avec consultation" src="' + this._consultTransferImage + '"/></a>';
                                    }
                                    rows += '</td>';
                                }
                                else {
                                    rows += '<td></td>';
                                }
                            }
                            else {
                                if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                                    //                                    sdsToAdd[currentSd].directoryNumber = item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue;
                                    rows += '<td align="center">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</td>';
                                }
                                else {
                                    rows += '<td></td>';
                                }
                            }
                            currentSd++;
                            break;
                        case FieldType.Other:
                            if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                                rows += '<td align="center">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</td>';
                            }
                            else {
                                rows += '<td></td>';
                            }
                            break;
                    }
                }
                else {
                    if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                        rows += '<td align="center">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</td>';
                    }
                    else {
                        rows += '<td></td>';
                    }
                }
                //                if (this._presenceField != null && this._ctiClient != null && nbColumns == this._presenceField) {
                //                    if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                //                        //                        if (activePage == this._currentPage) {
                //                        //                            var lctl = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineStatus();
                //                        //                            lctl.directoryNumber = item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue;
                //                        //                            lctl.status = Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown;
                //                        //                            pagePresenceArray.push(lctl);
                //                        //                        }
                //                        var lctl = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineStatus();
                //                        lctl.directoryNumber = item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue;
                //                        lctl.status = Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown;
                //                        lctl.forward = "";
                //                        lctl.doNotDisturb = false;
                //                        lctl.mwiOn = false;
                //                        pagePresenceArray.push(lctl);
                //                        rows += '<td align="center"><a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Call(\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</a>';
                //                        if (this._enableTransfer && item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue != "") {
                //                            rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Transfer(\'\',\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')"><img name="img_directTransfer' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;padding-left:5px;" alt="Transférer" title="Transférer" src="' + this._directTransferImage + '"/></a>';
                //                        }
                //                        if (this._enableConsultTransfer && item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue != "") {
                //                            rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._ConsultTransfer(\'\',\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')"><img name="img_consultTransfer' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;padding-left:5px;" alt="Transférer avec consultation" title="Transférer avec consultation" src="' + this._consultTransferImage + '"/></a>';
                //                        }
                //                        rows += '</td><td align="center"><img name="img_' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '"/><img name="img_dnForward' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;" alt="En renvoi" title="En renvoi" src="' + this._dnForwardImage + '"/><img name="img_mevoForward' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;" alt="En renvoi sur messagerie vocale" title="En renvoi sur messagerie vocale" src="' + this._mevoForwardImage + '"/><img name="img_dnd' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;" alt="Ne pas déranger" title="Ne pas déranger" src="' + this._dndImage + '"/></td>';
                //                    }
                //                    else {
                //                        rows += '<td></td><td></td>';
                //                    }
                //                }
                //                else if (this._ctiClient != null && this._isCallableField(nbColumns.toString())) {
                //                    if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                //                        rows += '<td align="center"><a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Call(\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</a>';
                //                        if (this._enableTransfer) {
                //                            rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Transfer(\'\',\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')"><img name="img_directTransfer' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;" alt="Transférer" title="Transférer" src="' + this._directTransferImage + '"/></a>';
                //                        }
                //                        if (this._enableConsultTransfer) {
                //                            rows += '<a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._ConsultTransfer(\'\',\'' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '\')"><img name="img_consultTransfer' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '" style="display:none;" alt="Transférer avec consultation" title="Transférer avec consultation" src="' + this._consultTransferImage + '"/></a>';
                //                        }
                //                        rows += '</td>';
                //                    }
                //                    else {
                //                        rows += '<td></td>';
                //                    }
                //                }
                //                else {
                //                    if (item[nbResult].childNodes[nbColumns].childNodes[0] != null) {
                //                        rows += '<td align="center">' + item[nbResult].childNodes[nbColumns].childNodes[0].nodeValue + '</td>';
                //                    }
                //                    else {
                //                        rows += '<td></td>';
                //                    }
                //                }
            }
            currentSd = 0;
            if (this._enableSpeedDials) {
                for (currentSd; currentSd < this._nbSpeedDials; currentSd++) {
                    this._speeddials.push(sdsToAdd[currentSd]);
                }
            }
            rows += '</tr>';
        }
        this._presenceArray.push(pagePresenceArray);
        if (this._presenceIsEnabled && this._ctiClient != null) {
            rowFooter += '<td colspan="' + (columnsHeader.length + 1) + '"><table border="0"><tbody><tr>';
        }
        else {
            rowFooter += '<td colspan="' + columnsHeader.length + '"><table border="0"><tbody><tr>';
        }
        for (nbColumns = 0; nbColumns < columnsHeader.length; nbColumns = nbColumns + 1) {
            if (this._presenceIsEnabled && this._ctiClient != null && this._fieldstype[nbColumns] == FieldType.Telephone) {
                rowHeader += '<th scope="col" align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._Search(' + nbColumns + ')">' + this._escape(columnsHeader[nbColumns]) + '</a></th><th align="center">Présence</th>';
            }
            else {
                rowHeader += '<th scope="col" align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._Search(' + nbColumns + ')">' + this._escape(columnsHeader[nbColumns]) + '</a></th>';
            }
        }
        //        var lastIndex = this._currentPage + 9;
        //        if (lastIndex > nbPage) {
        //            lastIndex = nbPage;
        //        }
        //        for (var countPage = 0; countPage < nbPage; countPage++) {
        //            if (countPage >= this._currentPage && countPage <= lastIndex) {
        //                if (countPage == this._currentPage) {
        //                    rowFooter += '<td><span>' + (countPage + 1) + '</span></td>';
        //                }
        //                else {
        //                    rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ')" style="color: rgb(51,51,51)">' + (countPage + 1) + '</a></td>';
        //                }
        //            }
        //            else if (countPage == this._currentPage - 1 && nbPage > 9) {
        //                rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ')" style="color: rgb(51,51,51)">...</a></td>';
        //            }
        //            else if (countPage == lastIndex + 1 && nbPage > 9) {
        //                rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ')" style="color: rgb(51,51,51)">...</a></td>';
        //            }
        //        }
        rowFooter += this._setDMDFooter(nbPage);
        rowFooter += '</tr></tbody></table></td></tr></tfoot>';
        rowHeader += '</tr></thead>';
        rows += "</tbody>";
        if (item.length > 0) {
            this._results.innerHTML = '<table border="0" cellpadding="4" cellspacing="0">' + rowHeader + rows + rowFooter + '</table>';
        }
        else {
            this._results.innerHTML = '<div>Aucun collaborateur ne correspond à vos critères</div>';
        }
        //        if (document.images) {
        //            for (nbImage = 0; nbImage < this._presenceArray.length; nbImage++) {
        //                document["img_" + this._presenceArray[nbImage].directoryNumber].src = this._unknownImage;
        //                document["img_" + this._presenceArray[nbImage].directoryNumber].alt = "Inconnu";
        //                document["img_" + this._presenceArray[nbImage].directoryNumber].title = "Inconnu";
        //            }
        //        }
        this._setPresenceImage();
        //start monitoring...
        this._Presence();
    },

    _addSpeedDial: function(sd) {
        if (this._speeddials != null && this._speeddials[sd] != null) {
            alert(this._speeddials[sd].displayName + ' ' + this._speeddials[sd].directoryNumber);
            this._ctiClient._AddSpeedDial(this._speeddials[sd]);
        }
    },

    _changePage: function(page) {
        var trs = this._results.getElementsByTagName('tr');
        var activePage = 0;
        this._currentPage = page;
        var nbResult = trs.length - 3;
        var nbPage = Math.floor(nbResult / this.get_rowsPerPage());
        var nbPageMod = nbResult % this.get_rowsPerPage();
        if (nbPageMod > 0) {
            nbPage++;
        }
        for (nbTr = 1; nbTr < trs.length - 2; nbTr++) {

            if (Math.floor((nbTr - 1) / this.get_rowsPerPage()) > activePage) {
                activePage++;
            }
            if (activePage == this._currentPage) {
                if (((nbTr - 1) % 2) > 0) {
                    //                    rows += '<tr class="displayedPage_alternating">';
                    trs[nbTr].className = 'displayedPage_alternating';
                }
                else {
                    //                    rows += '<tr class="displayedPage">';
                    trs[nbTr].className = 'displayedPage';
                }
            }
            else {
                //                rows += '<tr class="hiddenPage">';
                trs[nbTr].className = 'hiddenPage';
            }
        }

        this._setPresenceImage();
        var row = this._setDMDFooter(nbPage);
        //        trs[trs.length - 1].parentNode.deleteRow(0);
        //        var tr = document.createElement('tr');
        //        tr.innerHTML = row;
        //        trs[trs.length - 1].parentNode.appendChild(tr);
        row = '<table border="0"><tbody><tr>' + row + '</tr></tbody></table>';
        trs[trs.length - 1].parentNode.parentNode.parentNode.innerHTML = row;
        this._Presence();
    },

    _setDMDFooter: function(nbPage) {
        var lastIndex = this._currentPage + 9;
        if (lastIndex > nbPage) {
            lastIndex = nbPage;
        }
        var rowFooter = '';
        for (var countPage = 0; countPage < nbPage; countPage++) {
            if (countPage >= this._currentPage && countPage <= lastIndex) {
                if (countPage == this._currentPage) {
                    rowFooter += '<td><span>' + (countPage + 1) + '</span></td>';
                }
                else {
                    rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ')" style="color: rgb(51,51,51)">' + (countPage + 1) + '</a></td>';
                }
            }
            else if (countPage == this._currentPage - 1 && nbPage > 10) {
                rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ')" style="color: rgb(51,51,51)">...</a></td>';
            }
            else if (countPage == lastIndex + 1 && nbPage > 10) {
                rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ')" style="color: rgb(51,51,51)">...</a></td>';
            }
            else if (nbPage - this._currentPage > 10 && countPage < lastIndex && countPage > this._currentPage) {
                rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ')" style="color: rgb(51,51,51)">' + (countPage + 1) + '</a></td>';
            }
            else if (countPage < this._currentPage && nbPage <= 10) {
                rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ')" style="color: rgb(51,51,51)">' + (countPage + 1) + '</a></td>';
            }
        }
        return rowFooter;
    },

    //    _isCallableField: function(field) {
    //        if (this._callableFieldArray != null) {
    //            for (nbCallableField = 0; nbCallableField < this._callableFieldArray.length; nbCallableField++) {
    //                if (this._callableFieldArray[nbCallableField] == field) {
    //                    return true;
    //                }
    //            }
    //            return false;
    //        }
    //        else {
    //            return false;
    //        }
    //    },

    _onlineControlChanged: function(eventArgs) {
        if (eventArgs.get_lineControl().status != this._lineControlStatus) {
            this._lineControlStatus = eventArgs.get_lineControl().status;
            this._setTransferImages();
            this._setMonitorImages();
        }
        var lcs = eventArgs.get_lineControl().lineControlConnection;
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
            this._currentTransferFrom = heldLC.contact;
            if (document['img_consultTransfer' + activeLC.contact] != null) {
                document['img_consultTransfer' + activeLC.contact].style.display = 'inline';
            }
        }
    },

    _setTransferImages: function() {
        if (document.images) {
            for (nbImage = 0; nbImage < document.images.length; nbImage++) {
                if (document.images[nbImage].name.startsWith('img_directTransfer') || document.images[nbImage].name.startsWith('img_consultTransfer')) {
                    var reg = new RegExp("directTransfer|consultTransfer", "g");
                    var presenceImage = document.images[nbImage].name.replace(reg, "");
                    switch (this._lineControlStatus) {
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.busy:
                            if (document[presenceImage].alt == "Disponible") {
                                document.images[nbImage].style.display = 'inline';
                            }
                            else {
                                document.images[nbImage].style.display = 'none';
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.dialing:
                            if (document[presenceImage].alt == "Disponible") {
                                document.images[nbImage].style.display = 'inline';
                            }
                            else {
                                document.images[nbImage].style.display = 'none';
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.ringing:
                            if (document[presenceImage].alt == "Disponible") {
                                document.images[nbImage].style.display = 'inline';
                            }
                            else {
                                document.images[nbImage].style.display = 'none';
                            }
                            break;
                        default:
                            document.images[nbImage].style.display = 'none';
                            break;
                    }
                }

            }
        }
    },

    _setMonitorImages: function() {
        if (document.images) {
            for (nbImage = 0; nbImage < document.images.length; nbImage++) {
                if (document.images[nbImage].name.startsWith('img_monitor')) {
                    var reg = new RegExp("monitor", "g");
                    var presenceImage = document.images[nbImage].name.replace(reg, "");
                    switch (this._lineControlStatus) {
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.busy:
                            document.images[nbImage].style.display = 'none';
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.dialing:
                            document.images[nbImage].style.display = 'none';
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.ringing:
                            document.images[nbImage].style.display = 'none';
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.available:
                            if (document[presenceImage].alt == "Occupé") {
                                document.images[nbImage].style.display = 'inline';
                            } else {
                                document.images[nbImage].style.display = 'none';
                            }
                            break;
                        default:
                            document.images[nbImage].style.display = 'none';
                            break;
                    }
                }

            }
        }
    },

    _onSearchFailed: function(err) {
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

    _onPresenceSuccess: function(result, context) {
        var allUnknown = true;
        this._presenceArray[this._currentPage] = result;
        this._setPresenceImage();
        for (nbLines = 0; nbLines < result.length; nbLines++) {
            if (result[nbLines].status != Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown) {
                allUnknown = false;
            }
        }
        if (!allUnknown) {
            this._Presence();
        }
        else {
            this._delayPresence();
        }
    },

    _delayPresence: function() {
        $find(this._ctiClient.get_id()).set_waitingPresenceRequest(window.setTimeout(Function.createDelegate(this, this._Presence), 10000));
    },

    _Presence: function() {
        if (this._presenceIsEnabled && this._ctiClient != null) {
            var params = { "lines": this._presenceArray[this._currentPage] };
            $find(this._ctiClient.get_id())._startPresence(params, this.get_id(), Function.createDelegate(this, this._onPresenceSuccess), Function.createDelegate(this, this._onPresenceFailed));
        }
    },

    _onPresenceFailed: function(err, response, context) {
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

    _show: function() {
        if (this._dmdclient != null) {
            this._dmdclient.style.display = 'block';
        }
    },

    _hide: function() {
        if (this._dmdclient != null) {
            this._dmdclient.style.display = 'none';
        }
    },

    _isTransferable: function(destination) {
        if (this._ctiClient != null) {
            var lcs = this._ctiClient.get_lineControl().lineControlConnection;
            if (lcs != null && lcs.length > 1) {
                for (lcCompteur = 0; lcCompteur < lcs.length; lcCompteur = lcCompteur + 1) {
                    if (lcs[lcCompteur].contact != null && lcs[lcCompteur].contact == destination) {
                        //                        this._currentTransferDestination = destination;
                        //                        if (this._enableTransferLookup) {
                        //                            this._transferLookup();
                        //                        }
                        //this._loadTransferPopup('', destination);
                        return true;
                    }

                }
            }
        }
        return false;
    },

    _isMonitorable: function() {
        if (this._ctiClient != null) {
            if (this._ctiClient.get_lineControl().status == Wybecom.TalkPortal.CTI.Proxy.LCS.Status.available) {
                return true;
            }
        }
        return false;
    },

    _transferLookup: function() {
        var params = { dirNum: this._currentTransferFrom };
        Sys.Net.WebServiceProxy.invoke(this._dmdService, "Lookup", false, params, Function.createDelegate(this, this._onTransferLookupSuccess), Function.createDelegate(this, this._onTransferLookupFailed), "from", 10000);
        params = { dirNum: this._currentTransferDestination };
        Sys.Net.WebServiceProxy.invoke(this._dmdService, "Lookup", false, params, Function.createDelegate(this, this._onTransferLookupSuccess), Function.createDelegate(this, this._onTransferLookupFailed), "destination", 10000);

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
            elem.innerText = result;
        }
        else {
            elem.innerText = defaultText;
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

    _loadTransferPopup: function(from, to) {
        centerPopup();
        loadPopup(from, to);

    },

    _setPresenceImage: function() {

        if (this._presenceIsEnabled && this._ctiClient != null) {
            if (document.images) {
                for (nbImage = 0; nbImage < this._presenceArray[this._currentPage].length; nbImage++) {
                    switch (this._presenceArray[this._currentPage][nbImage].status) {
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown:
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].src = this._unknownImage;
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].alt = "Inconnu";
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].title = "Inconnu";
                            if (this._enableTransfer) {
                                document["img_directTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableConsultTransfer) {
                                document["img_consultTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableMonitor) {
                                document["img_monitor" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.available:
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].src = this._availableImage;
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].alt = "Disponible";
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].title = "Disponible";
                            if (this._enableTransfer && this._lineControlStatus == Wybecom.TalkPortal.CTI.Proxy.LCS.Status.busy) {
                                document["img_directTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'inline';
                            }
                            if (this._enableConsultTransfer && this._lineControlStatus == Wybecom.TalkPortal.CTI.Proxy.LCS.Status.busy) {
                                document["img_consultTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'inline';
                            }
                            if (this._enableMonitor) {
                                document["img_monitor" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.donotdisturb:
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].src = this._availableImage;
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].alt = "Ne pas déranger";
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].title = "Ne pas déranger";
                            if (this._enableTransfer) {
                                document["img_directTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableConsultTransfer) {
                                document["img_consultTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableMonitor) {
                                document["img_monitor" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.forwarded:
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].src = this._availableImage;
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].alt = "En renvoi";
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].title = "En renvoi";
                            if (this._enableTransfer) {
                                document["img_directTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableConsultTransfer) {
                                document["img_consultTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableMonitor) {
                                document["img_monitor" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.busy:
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].src = this._busyImage;
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].alt = "Occupé";
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].title = "Occupé";
                            if (this._enableTransfer) {
                                if (this._isTransferable(this._presenceArray[this._currentPage][nbImage].directoryNumber)) {
                                    document["img_directTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'inline';

                                }
                                else {

                                    document["img_directTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                                }
                            }

                            if (this._enableConsultTransfer) {
                                if (this._isTransferable(this._presenceArray[this._currentPage][nbImage].directoryNumber)) {
                                    document["img_consultTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'inline';

                                }
                                else {
                                    document["img_consultTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                                }
                            }
                            if (this._enableMonitor && this._isMonitorable()) {
                                document["img_monitor" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'inline';
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.hidden:
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].src = this._privateImage;
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].alt = "Privé";
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].title = "Privé";
                            if (this._enableTransfer) {
                                document["img_directTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableConsultTransfer) {
                                document["img_consultTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableMonitor) {
                                document["img_monitor" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.dialing:
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].src = this._busyImage;
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].alt = "En numérotation";
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].title = "En numérotation";
                            if (this._enableTransfer) {
                                document["img_directTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableConsultTransfer) {
                                document["img_consultTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableMonitor) {
                                document["img_monitor" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.ringing:
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].src = this._busyImage;
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].alt = "En sonnerie";
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].title = "En sonnerie";
                            if (this._enableTransfer) {
                                document["img_directTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableConsultTransfer) {
                                document["img_consultTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableMonitor) {
                                document["img_monitor" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            break;
                        case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.inactive:
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].src = this._logoutImage;
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].alt = "Déconnecté";
                            document["img_" + this._presenceArray[this._currentPage][nbImage].directoryNumber].title = "Déconnecté";
                            if (this._enableTransfer) {
                                document["img_directTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableConsultTransfer) {
                                document["img_consultTransfer" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            if (this._enableMonitor) {
                                document["img_monitor" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = 'none';
                            }
                            break;
                    }
                    if (this._presenceArray[this._currentPage][nbImage].forward != "") {
                        if (this._ctiClient.get_mevoPilot() != "" && this._presenceArray[this._currentPage][nbImage].forward == this._ctiClient.get_mevoPilot()) {
                            document["img_mevoForward" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = "inline";
                            document["img_dnForward" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = "none";
                        }
                        else {
                            document["img_mevoForward" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = "none";
                            document["img_dnForward" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = "inline";
                        }
                    }
                    else {
                        document["img_mevoForward" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = "none";
                        document["img_dnForward" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = "none";
                    }
                    if (this._presenceArray[this._currentPage][nbImage].doNotDisturb) {
                        document["img_dnd" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = "inline";
                    }
                    else {
                        document["img_dnd" + this._presenceArray[this._currentPage][nbImage].directoryNumber].style.display = "none";
                    }
                }
            }
        }
    }
}
Wybecom.TalkPortal.DMD.Controls.DMDClient.registerClass('Wybecom.TalkPortal.DMD.Controls.DMDClient', Sys.UI.Control);

Wybecom.TalkPortal.DMD.Controls.FilterControl = function() {
    Wybecom.TalkPortal.DMD.Controls.FilterControl.initializeBase(this);
    this._label = null;
    this._filter = null;
    this._text = null;
    this._isOptional = false;
    this._autoCompletePrefixLength = -1;
}

Wybecom.TalkPortal.DMD.Controls.FilterControl.prototype = {
    get_label: function() {
        return this._label;
    },
    set_label: function(value) {
        this._label = value;
    },
    get_filter: function() {
        return this._filter;
    },
    set_filter: function(value) {
        this._filter = value;
    },
    get_text: function() {
        return this._text;
    },
    set_text: function(value) {
        this._text = value;
    },
    get_isOptional: function() {
        return this._isOptional;
    },
    set_isOptional: function(value) {
        this._isOptional = value;
    },
    get_autoCompletePrefixLength: function() {
        return this._autoCompletePrefixLength;
    },
    set_autoCompletePrefixLength: function(value) {
        this._autoCompletePrefixLength = value;
    }
}

Wybecom.TalkPortal.DMD.Controls.FilterControl.registerClass('Wybecom.TalkPortal.DMD.Controls.FilterControl', Sys.Component);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();