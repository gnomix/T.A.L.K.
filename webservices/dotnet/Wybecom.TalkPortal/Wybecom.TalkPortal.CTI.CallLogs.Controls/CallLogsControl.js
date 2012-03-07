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
 
TALK is based upon:
- Sun JTAPI http://java.sun.com/products/jtapi/
- JulMar TAPI http://julmar.com/
- Asterisk.Net http://sourceforge.net/projects/asterisk-dotnet/
 
*/
Type.registerNamespace("Wybecom.TalkPortal.CTI.CallLogs.Controls");

Wybecom.TalkPortal.CTI.CallLogs.Controls.CallLogsControl = function(element) {
    Wybecom.TalkPortal.CTI.CallLogs.Controls.CallLogsControl.initializeBase(this, [element]);
    this._ctiClient = null;
    this._callLogsService = null;
    this._directoryService = null;
    this._missedcalltab = null;
    this._missedcalltabcontent = null;
    this._missedcallrefresh = null;
    this._placedcalltab = null;
    this._placedcalltabcontent = null;
    this._placedcallrefresh = null;
    this._receivedcalltab = null;
    this._receivedcalltabcontent = null;
    this._receivedcallrefresh = null;
    this._presenceenabled = false;
    this._lookupenabled = false;
    this._dirNumLength = 4;
    this.selectedtab = null;
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
    this._currentPage = 0;
    this._missedPresenceArray = null;
    this._placedPresenceArray = null;
    this._receivedPresenceArray = null;
    this._emptyCallLogsText = 'Aucune entrée disponible';
    this._showAlert = false;
}

Wybecom.TalkPortal.CTI.CallLogs.Controls.CallLogsControl.prototype = {
    initialize: function() {
        Wybecom.TalkPortal.CTI.CallLogs.Controls.CallLogsControl.callBaseMethod(this, 'initialize');

        // Ajouter ici une initialisation personnalisée
        this._missedcalltab = $get(this.get_id() + "_missedcalltab");
        this._placedcalltab = $get(this.get_id() + "_placedcalltab");
        this._receivedcalltab = $get(this.get_id() + "_receivedcalltab");
        this._missedcalltabcontent = $get(this.get_id() + "_missedcalltabcontent");
        this._placedcalltabcontent = $get(this.get_id() + "_placedcalltabcontent");
        this._receivedcalltabcontent = $get(this.get_id() + "_receivedcalltabcontent");
        this._setRefreshElement();
        this._missedcallrefresh_onclick$delegate = Function.createDelegate(this, this._missedcallrefresh_onclick);
        this._placedcallrefresh_onclick$delegate = Function.createDelegate(this, this._placedcallrefresh_onclick);
        this._receivedcallrefresh_onclick$delegate = Function.createDelegate(this, this._receivedcallrefresh_onclick);
        this._missedcalltab_onclick$delegate = Function.createDelegate(this, this._missedcalltab_onclick);
        this._placedcalltab_onclick$delegate = Function.createDelegate(this, this._placedcalltab_onclick);
        this._receivedcalltab_onclick$delegate = Function.createDelegate(this, this._receivedcalltab_onclick);
        this._tab_onover$delegate = Function.createDelegate(this, this._tab_onmouseover);
        this._tab_onout$delegate = Function.createDelegate(this, this._tab_onmouseout);
        this._addControlHandlers();
        this._selectedtab = "missed";
        this._refresh("missed");
        this._refresh("placed");
        this._refresh("received");
        if (this._availableImageUrl != null) {
            this._availableImage.src = this._availableImageUrl;
        }
        if (this._busyImageUrl != null) {
            this._busyImage.src = this._busyImageUrl;
        }
        if (this._logoutImageUrl != null) {
            this._logoutImage.src = this._logoutImageUrl;
        }
        if (this._privateImageUrl != null) {
            this._privateImage.src = this._privateImageUrl;
        }
        if (this._unknownImageUrl != null) {
            this._unknownImage.src = this._unknownImageUrl;
        }
    },
    dispose: function() {
        //Ajouter ici des actions dispose personnalisées
        Wybecom.TalkPortal.CTI.CallLogs.Controls.CallLogsControl.callBaseMethod(this, 'dispose');
        this._removeControlHandlers();
    },

    _setRefreshElement: function() {
        this._missedcallrefresh = $get(this.get_id() + "_missedcallrefresh");
        this._placedcallrefresh = $get(this.get_id() + "_placedcallrefresh");
        this._receivedcallrefresh = $get(this.get_id() + "_receivedcallrefresh");
    },

    _removeControlHandlers: function() {
        $removeHandler(this._missedcalltab, 'click', this._missedcalltab_onclick$delegate);
        $removeHandler(this._placedcalltab, 'click', this._placedcalltab_onclick$delegate);
        $removeHandler(this._receivedcalltab, 'click', this._receivedcalltab_onclick$delegate);
        $removeHandler(this._missedcalltab, 'mouseover', this._tab_onover$delegate);
        $removeHandler(this._placedcalltab, 'mouseover', this._tab_onover$delegate);
        $removeHandler(this._receivedcalltab, 'mouseover', this._tab_onover$delegate);
        $removeHandler(this._missedcalltab, 'mouseout', this._tab_onout$delegate);
        $removeHandler(this._placedcalltab, 'mouseout', this._tab_onout$delegate);
        $removeHandler(this._receivedcalltab, 'mouseout', this._tab_onout$delegate);
        $removeHandler(this._missedcallrefresh, 'click', this._missedcallrefresh_onclick$delegate);
        $removeHandler(this._placedcallrefresh, 'click', this._placedcallrefresh_onclick$delegate);
        $removeHandler(this._receivedcallrefresh, 'click', this._receivedcallrefresh_onclick$delegate);
    },

    _addControlHandlers: function() {
        $addHandlers(this._missedcalltab, { click: this._missedcalltab_onclick$delegate });
        $addHandlers(this._placedcalltab, { click: this._placedcalltab_onclick$delegate });
        $addHandlers(this._receivedcalltab, { click: this._receivedcalltab_onclick$delegate });
        $addHandlers(this._missedcalltab, { mouseover: this._tab_onover$delegate });
        $addHandlers(this._placedcalltab, { mouseover: this._tab_onover$delegate });
        $addHandlers(this._receivedcalltab, { mouseover: this._tab_onover$delegate });
        $addHandlers(this._missedcalltab, { mouseout: this._tab_onout$delegate });
        $addHandlers(this._placedcalltab, { mouseout: this._tab_onout$delegate });
        $addHandlers(this._receivedcalltab, { mouseout: this._tab_onout$delegate });
        $addHandlers(this._missedcallrefresh, { click: this._missedcallrefresh_onclick$delegate });
        $addHandlers(this._placedcallrefresh, { click: this._placedcallrefresh_onclick$delegate });
        $addHandlers(this._receivedcallrefresh, { click: this._receivedcallrefresh_onclick$delegate });
    },

    _missedcallrefresh_onclick: function() {
        this._refresh("missed");
    },

    _placedcallrefresh_onclick: function() {
        this._refresh("placed");
    },

    _receivedcallrefresh_onclick: function() {
        this._refresh("received");
    },

    _missedcalltab_onclick: function() {
        this._highLightTab("missed");
    },

    _placedcalltab_onclick: function() {
        this._highLightTab("placed");
    },

    _receivedcalltab_onclick: function() {
        this._highLightTab("received");
    },

    _tab_onmouseover: function(sender, args) {
        if (sender.target.getAttribute('id').indexOf(this._selectedtab) < 0) {
            sender.target.className = 'tab_over';
        }
    },

    _tab_onmouseout: function(sender, args) {
        if (sender.target.getAttribute('id').indexOf(this._selectedtab) < 0) {
            sender.target.className = 'tab_unselected';
        }

    },

    _highLightTab: function(calltype) {
        this._currentPage = 0;
        this._selectedtab = calltype;
        switch (calltype) {
            case "missed":
                this._missedcalltab.className = 'tab';
                this._missedcalltabcontent.style.display = 'block';
                this._placedcalltab.className = 'tab_unselected_bottom';
                this._placedcalltabcontent.style.display = 'none';
                this._receivedcalltab.className = 'tab_unselected';
                this._receivedcalltabcontent.style.display = 'none';
                break;
            case "placed":
                this._missedcalltab.className = 'tab_unselected';
                this._missedcalltabcontent.style.display = 'none';
                this._placedcalltab.className = 'tab';
                this._placedcalltabcontent.style.display = 'block';
                this._receivedcalltab.className = 'tab_unselected_bottom';
                this._receivedcalltabcontent.style.display = 'none';
                break;
            case "received":
                this._missedcalltab.className = 'tab_unselected';
                this._missedcalltabcontent.style.display = 'none';
                this._placedcalltab.className = 'tab_unselected';
                this._placedcalltabcontent.style.display = 'none';
                this._receivedcalltab.className = 'tab';
                this._receivedcalltabcontent.style.display = 'block';
                break;
        }
        this._Presence(calltype);
    },

    _refresh: function(calltype, sort) {
        switch (calltype) {
            case "missed":
                this._getmissedcalls(sort);
                break;
            case "placed":
                this._getplacedcalls(sort);
                break;
            case "received":
                this._getreceivedcalls(sort);
                break;
        }
    },

    _getmissedcalls: function(sort) {
        var params = null;
        if (sort == null) {
            sort = "startTime";
        }
        if (this._ctiClient != null) {
            var params = { dn: this._ctiClient.get_monitoredLine(), sort: sort };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._callLogsService, "GetMissedCalls", false, params, Function.createDelegate(this, this._onGetMissedCallsSuccess), Function.createDelegate(this, this._onGetMissedCallsFailure), null, 4000);
        }
    },

    _getplacedcalls: function(sort) {
        var params = null;
        if (sort == null) {
            sort = "startTime";
        }
        if (this._ctiClient != null) {
            var params = { dn: this._ctiClient.get_monitoredLine(), sort: sort };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._callLogsService, "GetPlacedCalls", false, params, Function.createDelegate(this, this._onGetPlacedCallsSuccess), Function.createDelegate(this, this._onGetPlacedCallsFailure), null, 4000);
        }
    },

    _getreceivedcalls: function(sort) {
        var params = null;
        if (sort == null) {
            sort = "startTime";
        }
        if (this._ctiClient != null) {
            var params = { dn: this._ctiClient.get_monitoredLine(), sort: sort };
        }
        if (params != null) {
            Sys.Net.WebServiceProxy.invoke(this._callLogsService, "GetReceivedCalls", false, params, Function.createDelegate(this, this._onGetReceivedCallsSuccess), Function.createDelegate(this, this._onGetReceivedCallsFailure), null, 4000);
        }
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

    get_emptyCallLogsText: function() {
        return this._emptyCallLogsText;
    },

    set_emptyCallLogsText: function(value) {
        this._emptyCallLogsText = value;
        this.raisePropertyChanged("emptyCallLogsText");
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

    _onGetMissedCallsSuccess: function(result, context) {
        this._getResults("missed", result);
    },

    _onGetMissedCallsFailure: function(err, response, context) {
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

    _onGetPlacedCallsSuccess: function(result, context) {
        this._getResults("placed", result);
    },

    _onGetPlacedCallsFailure: function(err, response, context) {
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

    _onGetReceivedCallsSuccess: function(result, context) {
        this._getResults("received", result);
    },

    _onGetReceivedCallsFailure: function(err, response, context) {
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

    _getResults: function(calltype, result) {
        if (result != null && result.length > 0) {
            var pagePresenceArray = new Array();
            var rows = '';
            var rowHeader = '<tr class="rowHeader">';
            var rowFooter = '<tr class="rowFooter">';
            var nbPage = Math.floor(result.length / 10);
            var activePage = 0;
            this._currentPage = 0;
            var dirNum = '';
            var nbPageMod = result.length % 10;
            if (nbPageMod > 0) {
                nbPage++;
            }
            var caller = 'callee';
            switch (calltype) {
                case "missed":
                    this._missedPresenceArray = new Array();
                    caller = 'caller';
                    break;
                case "placed":
                    this._placedPresenceArray = new Array();
                    caller = 'callee';
                    break;
                case "received":
                    this._receivedPresenceArray = new Array();
                    caller = 'caller';
                    break;
            }
            var dirNumContext = new Array();
            for (nbResult = 0; nbResult < result.length; nbResult = nbResult + 1) {
                if (Math.floor(nbResult / 10) > activePage) {
                    activePage++;
                }
                switch (calltype) {
                    case "missed":
                        dirNum = result[nbResult].caller;
                        break;
                    case "placed":
                        dirNum = result[nbResult].callee;
                        break;
                    case "received":
                        dirNum = result[nbResult].caller;
                        break;
                }
                if ((nbResult % 10) == 0 && nbResult != 0) {
                    switch (calltype) {
                        case "missed":
                            this._missedPresenceArray.push(pagePresenceArray);
                            break;
                        case "placed":
                            this._placedPresenceArray.push(pagePresenceArray);
                            break;
                        case "received":
                            this._receivedPresenceArray.push(pagePresenceArray);
                            break;
                    }
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

                if (this._lookupenabled) {
                    var hash = null;
                    for (dirNumCpt = 0; dirNumCpt < dirNumContext.length; dirNumCpt++) {
                        if (dirNumContext[dirNumCpt].num == dirNum) {
                            dirNumContext[dirNumCpt].callids.push(result[nbResult].callId);
                            hash = dirNumContext[dirNumCpt];
                            break;
                        }
                    }
                    if (hash == null) {
                        var callidarray = new Array();
                        callidarray.push(result[nbResult].callId);
                        hash = new Object();
                        hash.num = dirNum;
                        hash.callids = callidarray;
                        dirNumContext.push(hash);
                    }


                    rows += '<td align="center" id="name_' + dirNum + '_' + result[nbResult].callId + '">Inconnu</td>';
                }

                if (this._presenceenabled && this._ctiClient != null) {
                    if (dirNum != null) {
                        if (dirNum.length != null && dirNum.length == this._dirNumLength) {
                            var statushash = null;
                            for (statuscpt = 0; statuscpt < pagePresenceArray.length; statuscpt++) {
                                if (pagePresenceArray[statuscpt].linestatus.directoryNumber == dirNum) {
                                    pagePresenceArray[statuscpt].callids.push(result[nbResult].callId);
                                    statushash = pagePresenceArray[statuscpt];
                                }
                            }
                            if (statushash == null) {
                                var arrayofcallid = new Array();
                                arrayofcallid.push(result[nbResult].callId);
                                statushash = new Object();
                                var lctl = new Wybecom.TalkPortal.CTI.Proxy.LCS.LineStatus();
                                lctl.directoryNumber = dirNum;
                                lctl.status = Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown;
                                statushash.linestatus = lctl;
                                statushash.callids = arrayofcallid;
                                pagePresenceArray.push(statushash);
                            }
                            rows += '<td align="center"><a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Call(\'' + dirNum + '\')">' + dirNum + '</a></td><td align="center"><img name="img_' + dirNum + '_' + result[nbResult].callId + '"/></td>';
                        }
                        else {
                            rows += '<td align="center"><a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Call(\'' + dirNum + '\')">' + dirNum + '</a></td><td></td>';
                        }
                    }
                    else {
                        rows += '<td align="center"></td><td></td>';
                    }
                }
                else {
                    rows += '<td align="center"><a href="javascript:$find(\'' + this._ctiClient.get_id() + '\')._Call(\'' + dirNum + '\')">' + dirNum + '</a></td>';
                }
                rows += '<td align="center">' + result[nbResult].startTime.toLocaleString() + '</td>';
                rows += '</tr>';

            }

            switch (calltype) {
                case "missed":
                    this._missedPresenceArray.push(pagePresenceArray);
                    break;
                case "placed":
                    this._placedPresenceArray.push(pagePresenceArray);
                    break;
                case "received":
                    this._receivedPresenceArray.push(pagePresenceArray);
                    break;
            }
            if (this._presenceenabled && this._lookupenabled) {
                rowFooter += '<td align="center" colspan="4"><table border="0"><tbody><tr>';
                rowHeader += '<th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'' + caller + '\')">Nom</a></th><th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'' + caller + '\')">T&eacute;l&eacute;phone</a></th><th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'' + caller + '\')">Pr&eacute;sence</a></th><th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'startTime\')">Heure</a></th>';
            }
            else if (this._presenceenabled || this._lookupenabled) {
                rowFooter += '<td align="center" colspan="3"><table border="0"><tbody><tr>';
                if (this._presenceenabled) {
                    rowHeader += '<th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'' + caller + '\')">T&eacute;l&eacute;phone</a></th><th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'' + caller + '\')">Pr&eacute;sence</a></th><th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'startTime\')">Heure</a></th>';
                }
                else {
                    rowHeader += '<th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'' + caller + '\')">Nom</a></th><th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'' + caller + '\')">T&eacute;l&eacute;phone</a></th><th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'startTime\')">Heure</a></th>';
                }
            }
            else {
                rowFooter += '<td align="center" colspan="2"><table border="0"><tbody><tr>';
                rowHeader += '<th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'' + caller + '\')">T&eacute;l&eacute;phone</a></th><th align="center"><a href="javascript:$find(\'' + this.get_id() + '\')._refresh(\'' + calltype + '\', \'startTime\')">Heure</a></th>';
            }

            rowFooter += this._setFooter(nbPage, calltype);
            rowFooter += '</tr></tbody></table></td></tr>';
            rowHeader += '</tr>';

            switch (calltype) {
                case "missed":
                    this._missedcalltabcontent.innerHTML = '<div class="refresh" id="' + this.get_id() + '_missedcallrefresh">Rafraîchir</div><table border="0" cellpadding="4" cellspacing="0">' + rowHeader + rows + rowFooter + '</table>';
                    break;
                case "placed":
                    this._placedcalltabcontent.innerHTML = '<div class="refresh" id="' + this.get_id() + '_placedcallrefresh">Rafraîchir</div><table border="0" cellpadding="4" cellspacing="0">' + rowHeader + rows + rowFooter + '</table>';
                    break;
                case "received":
                    this._receivedcalltabcontent.innerHTML = '<div class="refresh" id="' + this.get_id() + '_receivedcallrefresh">Rafraîchir</div><table border="0" cellpadding="4" cellspacing="0">' + rowHeader + rows + rowFooter + '</table>';
                    break;
            }
            if (this._presenceenabled && this._ctiClient != null) {
                this._setPresenceImage(calltype);
                this._Presence(calltype);
            }
            if (this._lookupenabled) {
                this._multiLookup(dirNumContext);
            }
        }
        else {
            switch (calltype) {
                case "missed":
                    this._missedcalltabcontent.innerHTML = '<div class="refresh" id="' + this.get_id() + '_missedcallrefresh">Rafraîchir</div>' + this._emptyCallLogsText;
                    break;
                case "placed":
                    this._placedcalltabcontent.innerHTML = '<div class="refresh" id="' + this.get_id() + '_placedcallrefresh">Rafraîchir</div>' + this._emptyCallLogsText;
                    break;
                case "received":
                    this._receivedcalltabcontent.innerHTML = '<div class="refresh" id="' + this.get_id() + '_receivedcallrefresh">Rafraîchir</div>' + this._emptyCallLogsText;
                    break;
            }
        }
        this._removeControlHandlers();
        this._setRefreshElement();
        this._addControlHandlers();
    },

    _lookup: function(context) {
        if (this._directoryService != null) {
            var instance = this;
            for (var i = 0; i < context.length; i = i + 1) {
                var params = { dirNum: context[i].num };
                var c = context[i];
                Sys.Net.WebServiceProxy.invoke(this._directoryService, "Lookup", false, params,
                            Function.createDelegate(this, this._onLookupSuccess),
                            Function.createDelegate(this, this._onLookupFailed),
                            c, 5000);
            }
        }
    },

    _multiLookup: function(context) {
        if (this._directoryService != null) {
            var lookup = new Array();
            for (var i = 0; i < context.length; i = i + 1) {
                var l = new Wybecom.TalkPortal.LookupResponse();
                l.dirNum = context[i].num;
                l.name = "";
                l.callids = context[i].callids;
                lookup.push(l);
            }

            var params = { "lookup": lookup };
            Sys.Net.WebServiceProxy.invoke(this._directoryService, "MultiLookup", false, params,
                            Function.createDelegate(this, this._onMultiLookupSuccess),
                            Function.createDelegate(this, this._onLookupFailed),
                            null, 5000);
        }
    },

    _setFooter: function(nbPage, calltype) {
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
                    rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ',\'' + calltype + '\')" style="color: rgb(51,51,51)">' + (countPage + 1) + '</a></td>';
                }
            }
            else if (countPage == this._currentPage - 1 && nbPage > 10) {
                rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ',\'' + calltype + '\')" style="color: rgb(51,51,51)">...</a></td>';
            }
            else if (countPage == lastIndex + 1 && nbPage > 10) {
                rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ',\'' + calltype + '\')" style="color: rgb(51,51,51)">...</a></td>';
            }
            else {
                rowFooter += '<td><a href="javascript:$find(\'' + this.get_id() + '\')._changePage(' + countPage + ',\'' + calltype + '\')" style="color: rgb(51,51,51)">' + (countPage + 1) + '</a></td>';
            }
        }
        return rowFooter;
    },

    get_ctiClient: function() {
        return this._ctiClient;
    },

    set_ctiClient: function(value) {
        this._ctiClient = value;
        this.raisePropertyChanged('ctiClient');
    },

    get_callLogsService: function() {
        return this._callLogsService;
    },

    set_callLogsService: function(value) {
        this._callLogsService = value;
        this.raisePropertyChanged('callLogsService');
        this._refresh("missed");
        this._refresh("placed");
        this._refresh("received");
    },

    get_directoryService: function() {
        return this._directoryService;
    },

    set_directoryService: function(value) {
        this._directoryService = value;
        this.raisePropertyChanged('directoryService');
    },

    get_presenceenabled: function() {
        return this._presenceenabled;
    },

    set_presenceenabled: function(value) {
        this._presenceenabled = value;
        this.raisePropertyChanged('presenceenabled');
    },

    get_lookupenabled: function() {
        return this._lookupenabled;
    },

    set_lookupenabled: function(value) {
        this._lookupenabled = value;
        this.raisePropertyChanged('lookupenabled');
    },

    get_dirNumLength: function() {
        return this._dirNumLength;
    },

    set_dirNumLength: function(value) {
        this._dirNumLength = value;
        this.raisePropertyChanged('dirNumLength');
    },

    _Presence: function(calltype) {
        if (calltype == null) {
            calltype = this._selectedtab;
        }
        if (calltype == this._selectedtab) {
            if (this._presenceenabled && this._ctiClient != null) {
                var presenceArray = null;
                switch (calltype) {
                    case "missed":
                        presenceArray = this._missedPresenceArray;
                        break;
                    case "placed":
                        presenceArray = this._placedPresenceArray;
                        break;
                    case "received":
                        presenceArray = this._receivedPresenceArray;
                        break;
                }
                if (presenceArray != null && presenceArray.length > this._currentPage) {
                    var linearray = new Array();
                    for (nbImage = 0; nbImage < presenceArray[this._currentPage].length; nbImage++) {
                        linearray.push(presenceArray[this._currentPage][nbImage].linestatus);
                    }
                    var params = { "lines": linearray };
                    $find(this._ctiClient.get_id())._startPresence(params, this.get_id(), Function.createDelegate(this, this._onPresenceSuccess), Function.createDelegate(this, this._onPresenceFailed));
                }
            }
        }
    },

    _onLookupSuccess: function(result, context) {
        for (var i = 0; i < context.callids.length; i++) {
            var elem = $get('name_' + context.num + '_' + context.callids[i]);
            if (elem != null) {
                elem.innerText = result;
            }
        }
    },

    _onMultiLookupSuccess: function(result) {
        for (var i = 0; i < result.length; i++) {
            for (var j = 0; j < result[i].callids.length; j++) {
                var elem = $get('name_' + result[i].dirNum + '_' + result[i].callids[j]);
                if (elem != null) {
                    elem.innerHTML = result[i].name;
                }
            }
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

    _onPresenceSuccess: function(result, context) {
        var allUnknown = true;
        var presenceArray = null;
        switch (this._selectedtab) {
            case "missed":
                presenceArray = this._missedPresenceArray;
                break;
            case "placed":
                presenceArray = this._placedPresenceArray;
                break;
            case "received":
                presenceArray = this._receivedPresenceArray;
                break;
        }
        if (presenceArray != null && presenceArray.length > this._currentPage) {
            for (nbResult = 0; nbResult < result.length; nbResult++) {
                for (nbPresence = 0; nbPresence < presenceArray[this._currentPage].length; nbPresence++) {
                    if (result[nbResult].directoryNumber == presenceArray[this._currentPage][nbPresence].linestatus.directoryNumber) {
                        presenceArray[this._currentPage][nbPresence].linestatus = result[nbResult];
                        break;
                    }
                }
                if (result[nbResult].status != Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown) {
                    allUnknown = false;
                }
            }


            this._setPresenceImage(this._selectedtab);
            if (!allUnknown) {
                this._Presence(this._selectedtab);
            }
            else {
                this._delayPresence();
            }
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

    _delayPresence: function() {
        $find(this._ctiClient.get_id()).set_waitingPresenceRequest(window.setTimeout(Function.createDelegate(this, this._Presence), 10000));
    },

    _changePage: function(page, calltype) {
        var trs = null;
        switch (calltype) {
            case "missed":
                trs = this._missedcalltabcontent.getElementsByTagName('tr');
                break;
            case "placed":
                trs = this._placedcalltabcontent.getElementsByTagName('tr');
                break;
            case "received":
                trs = this._receivedcalltabcontent.getElementsByTagName('tr');
                break;
        }
        var activePage = 0;
        this._currentPage = page;
        var nbResult = trs.length - 3;
        var nbPage = Math.floor(nbResult / 10);
        var nbPageMod = nbResult % 10;
        if (nbPageMod > 0) {
            nbPage++;
        }
        for (nbTr = 1; nbTr < trs.length - 2; nbTr++) {

            if (Math.floor((nbTr - 1) / 10) > activePage) {
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

        this._setPresenceImage(calltype);
        var row = this._setFooter(nbPage, calltype);
        //        trs[trs.length - 1].parentNode.deleteRow(0);
        //        var tr = document.createElement('tr');
        //        tr.innerHTML = row;
        //        trs[trs.length - 1].parentNode.appendChild(tr);
        row = '<table border="0"><tbody><tr>' + row + '</tr></tbody></table>';
        trs[trs.length - 1].parentNode.parentNode.parentNode.innerHTML = row;
        this._Presence(calltype);
    },

    _setPresenceImage: function(calltype) {
        var presenceArray = null;
        switch (calltype) {
            case "missed":
                presenceArray = this._missedPresenceArray;
                break;
            case "placed":
                presenceArray = this._placedPresenceArray;
                break;
            case "received":
                presenceArray = this._receivedPresenceArray;
                break;
        }
        if (this._presenceenabled && this._ctiClient != null) {
            if (document.images) {
                for (nbImage = 0; nbImage < presenceArray[this._currentPage].length; nbImage++) {
                    for (nbcallid = 0; nbcallid < presenceArray[this._currentPage][nbImage].callids.length; nbcallid++) {
                        switch (presenceArray[this._currentPage][nbImage].linestatus.status) {
                            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.unknown:
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].src = this._unknownImage.src;
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].alt = "Inconnu";
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].title = "Inconnu";
                                break;
                            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.available:
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].src = this._availableImage.src;
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].alt = "Disponible";
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].title = "Disponible";
                                break;
                            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.donotdisturb:
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].src = this._availableImage.src;
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].alt = "Ne pas déranger";
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].title = "Ne pas déranger";
                                break;
                            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.forwarded:
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].src = this._availableImage.src;
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].alt = "En renvoi";
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].title = "En renvoi";
                                break;
                            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.busy:
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].src = this._busyImage.src;
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].alt = "Occupé";
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].title = "Occupé";
                                break;
                            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.hidden:
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].src = this._privateImage.src;
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].alt = "Privé";
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].title = "Privé";
                                break;
                            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.dialing:
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].src = this._busyImage.src;
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].alt = "En numérotation";
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].title = "En numérotation";
                                break;
                            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.ringing:
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].src = this._busyImage.src;
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].alt = "En sonnerie";
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].title = "En sonnerie";
                                break;
                            case Wybecom.TalkPortal.CTI.Proxy.LCS.Status.inactive:
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].src = this._logoutImage.src;
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].alt = "Déconnecté";
                                document["img_" + presenceArray[this._currentPage][nbImage].linestatus.directoryNumber + "_" + presenceArray[this._currentPage][nbImage].callids[nbcallid]].title = "Déconnecté";
                                break;
                        }
                    }
                }
            }
        }
    }
}
Wybecom.TalkPortal.CTI.CallLogs.Controls.CallLogsControl.registerClass('Wybecom.TalkPortal.CTI.CallLogs.Controls.CallLogsControl', Sys.UI.Control);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();