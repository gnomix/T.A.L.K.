/*
 *  WYBECOM T.A.L.K. -- Telephony Application Library Kit
 *  Copyright (C) 2010 WYBECOM
 *
 *  Yohann BARRE <y.barre@wybecom.com>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  T.A.L.K. is based upon:
 *  - Sun JTAPI http://java.sun.com/products/jtapi/
 *  - JulMar TAPI http://julmar.com/
 *  - Asterisk.Net http://sourceforge.net/projects/asterisk-dotnet/
 */

/**
 * \file
 * Implementation of monitor (a monitored line)
 */
package org.wybecom.talk.jtapi;

/**
 *
 * @author Yohann BARRE
 * 
 * 
 */

import java.util.*;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.telephony.*;
import javax.telephony.callcontrol.events.*;
import javax.telephony.events.*;
import javax.telephony.callcontrol.*;
import javax.xml.datatype.*;
import org.wybecom.talkportal.cti.stateserver.*;
/**
 * 
 * \brief A monitored JTAPI Address
 */
public class monitor implements AddressObserver, CallObserver, CallControlCallObserver, CallControlAddressObserver, TerminalObserver{
    
    private trace logger;
    private Address monitoredAddress;
    private Vector stateClients;
    private Boolean dnd;
    private Boolean mwi;
    public String monitored;
    private Boolean addressInService = true;
    private Boolean terminalInService = true;
    private CallControlForwarding[] forward;
    private int metacode;
    private int cause;
    private int callControlState;
    private Connection[] connections;
    private Terminal monitoredTerminal;
    private LineControl monitoredLineControl;
    private Hashtable calls = new Hashtable();
    
    /**
     * Initialize the monitor
     * @param lineStateClient The state server clients
     * @param address The JTAPI Address
     * @param log The logger
     */
    public monitor(Vector lineStateClient, Address address, trace log) {
        this.logger = log;
        this.monitoredAddress = address;
        this.stateClients = lineStateClient;
        this.dnd = false;
        this.mwi = false;
        this.forward = null;
        this.metacode = Ev.META_UNKNOWN;
        this.cause = Ev.CAUSE_UNKNOWN;
        this.callControlState = CallCtlEv.CAUSE_UNKNOWN;
        
        this.monitoredLineControl = new LineControl();
        this.monitoredLineControl.setDirectoryNumber(this.getDirNum());
        this.monitoredLineControl.setLineControlConnection(new ArrayOfLineControlConnection());
        this.monitoredLineControl.setStatus(Status.UNKNOWN);
        this.monitoredLineControl.setDoNotDisturb(dnd);
        this.monitoredLineControl.setForward(this.GetForward());
        this.monitoredLineControl.setMwiOn(this.mwi);
        //this.monitoredLineControl.setMonitored(this.monitored);
    }

    /**
     * Initialize the monitor
     * @param address The JTAPI Address
     * @param log The logger
     */
    public monitor(Address address, trace log){
        this.monitoredAddress = address;
        this.logger = log;
        this.stateClients = new Vector();
        this.dnd = false;
        this.mwi = false;
        this.forward = null;
        this.metacode = Ev.META_UNKNOWN;
        this.cause = Ev.CAUSE_UNKNOWN;
        this.callControlState = CallCtlEv.CAUSE_UNKNOWN;
        try {
                        Terminal[] ts = this.monitoredAddress.getTerminals();
                        this.logger.println(ts.length + " terminals associated with " + this.getDirNum());
                        if (ts != null) {
                            this.monitoredTerminal = ts[0];

                            this.logger.println("Application will monitoring " + this.getDirNum() + " with this terminal " + this.getTerminalName());

                            ts = null;
                        }
                    }
                    catch (Exception terminalException) {
                        this.logger.printerror("Unable to retrieve terminal for " + this.getDirNum() + " " + terminalException.toString());
                    }
    }
    
    /**
     * Accessor
     * @param newAddress The JTAPI Address
     */
    public void setAddress(Address newAddress) {
        this.monitoredAddress = newAddress;
    }

    /**
     * \brief Starts the monitor
     */
    public void start() {
        try {
            if (monitoredAddress != null) {
                try {
                        Terminal[] ts = this.monitoredAddress.getTerminals();
                        this.logger.println(ts.length + " terminals associated with " + this.getDirNum());
                        if (ts != null) {
                            this.monitoredTerminal = ts[0];

                            this.logger.println("Application will monitoring " + this.getDirNum() + " with this terminal " + this.getTerminalName());
                            
                            ts = null;
                        }
                    }
                    catch (Exception terminalException) {
                        this.logger.printerror("Unable to retrieve terminal for " + this.getDirNum() + " " + terminalException.toString());
                    }
                this.monitoredLineControl.setStatus(Status.AVAILABLE);
                this.addObservers();
                
                if (monitoredAddress instanceof CallControlAddress) {
                    try {
                        dnd = ((CallControlAddress)monitoredAddress).getDoNotDisturb();
                        this.logger.println(this.getDirNum() + " dnd is " + dnd.toString());
                    }
                    catch (Exception dndException) {
                        /*this.logger.printerror("Unable to retreive do not disturb status from "+this.getDirNum()+": "+ dndException.toString());*/
                    }
                    try {
                        mwi = ((CallControlAddress)monitoredAddress).getMessageWaiting();
                        this.logger.println(this.getDirNum() + " mwi is " + mwi.toString());
                    }
                    catch (Exception mwiException) {
                        /*this.logger.printerror("Unable to retreive mwi status from "+this.getDirNum()+": "+ mwiException.toString());*/
                    }
                    try {
                        forward = ((CallControlAddress)monitoredAddress).getForwarding();
                        if (forward != null) {
                            for (CallControlForwarding ccf : forward) {
                                this.logger.println("Forwarding instruction for " + this.getDirNum() + ": " + ccf.toString());
                            }
                        }
                        else {
                            this.logger.println("No forwarding instruction for " + this.getDirNum());
                        }
                    }
                    catch (Exception forwardException) {
                        /*this.logger.printerror("Unable to retreive forward status from "+this.getDirNum()+": "+ forwardException.toString());*/
                    }
                    try {
                        this.connections = this.monitoredAddress.getConnections();
                        if (connections != null) {
                            for (Connection c : this.connections){
                                this.logger.println("Connection for "+this.getDirNum()+" in state " + this.getConnectionStateString(c.getState()));
                            }
                        }
                        else {
                            this.logger.println("No connections associated with " + this.getDirNum());
                        }
                    }
                    catch (Exception connectionException) {
                        this.logger.printerror("Unable to retrieve connections for " + this.getDirNum() + " " + connectionException.toString());
                    }
                    
                }
                this.setLineControl("AddrObservationEndedEv");
            }
        }   
        catch (Exception e) {
            /*this.logger.printerror("exception while starting this monitor: " + e.toString());*/
        }
    }

    /**
     * \brief Stops the monitor
     */
    public void stop() {
        try {
            this.removeObservers();
            if (monitoredAddress != null) {
                this.monitoredLineControl.setStatus(Status.UNKNOWN);
                this.setLineControl("AddrObservationEndedEv");
                try {
                    this.finalize();
                } catch (Throwable ex) {
                    Logger.getLogger(monitor.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        }
        catch (Exception e){
            /*this.logger.printerror("exception while stoping this monitor" + e.toString());*/
        }
    }

    /**
     * \brief Removes JTAPI Observers from the JTAPI Terminal
     */
    public void removeObservers() {
        try {
            if (monitoredTerminal != null) {
                if (monitoredTerminal.getObservers() != null) {
                    this.logger.println("Removing observer from Terminal: " + this.getTerminalName());
                    monitoredTerminal.removeObserver(this);
                }
            }
            this.removeAddressObservers();
        }
        catch (Exception e){
            /*this.logger.printerror("exception while removing observers from " + this.getDirNum() + " " + e.toString());*/        }
    }

    /**
     * \brief Removes JTAPI Observers from the JTAPI Address
     */
    public void removeAddressObservers() {
        if (monitoredAddress != null) {
                if (monitoredAddress.getCallObservers() != null) {
                    this.logger.println("Removing call observer from Address: " + this.getDirNum());
                    monitoredAddress.removeCallObserver(this);
                }
                if (monitoredAddress.getObservers() != null) {
                    this.logger.println("Removing observer from Address: " + this.getDirNum());
                    monitoredAddress.removeObserver(this);
                }
            }
    }

    /**
     * \brief Adds JTAPI Observers from the JTAPI Address
     */
    public void addAddressObservers() {
        try {
        if (monitoredAddress != null) {
            
            
                /*int i = 0;
                while (!this.terminalInService || i < 21) {
                    Thread.sleep(100);
                    i++;
                }*/
                /*if (this.terminalInService) {*/
                    if (monitoredAddress.getObservers() == null) {
                        this.logger.println("Adding observer from Address: " + this.getDirNum());
                        monitoredAddress.addObserver(this);
                    }
                    else {
                        this.log(this.getDirNum() + " already observed");
                    }
                    if (monitoredAddress.getCallObservers() == null) {
                        this.logger.println("Adding call observer from Address: " + this.getDirNum());
                        monitoredAddress.addCallObserver(this);
                    }
                }
                /*else {
                    this.log("Cannot add observers while terminal is not in service");
                }
            }*/
        
        }
        catch (Exception e) {
            /*this.logger.printerror("exception while adding observers from " + this.getDirNum() + " " + e.toString());*/
        }
    }

    /**
     * \brief Adds JTAPI Observers from the JTAPI Terminal
     */
    public void addObservers() {
        try {
            this.addAddressObservers();
            if (monitoredTerminal != null) {
                if (monitoredTerminal.getObservers() == null) {
                    this.logger.println("Adding observer from Terminal: " + this.getTerminalName());
                    monitoredTerminal.addObserver(this);
                }
            }
           /* this.addAddressObservers();*/
            
        }
        catch (Exception e){
            /*this.logger.printerror("exception while adding observers from " + this.getDirNum() + " " + e.toString());*/
        }
    }

        /**
         * Accessor
         * @return The JTAPI Address extension
         */
        public String getDirNum() {
            if (this.monitoredAddress != null) {
                return this.monitoredAddress.getName();
            }
            else {
                return "";
            }
        }

        /**
         * Accessor
         * @return The JTAPI terminal associated with the JTAPI Address
         */
        public String getTerminalName() {
            if (this.monitoredTerminal != null) {
                return this.monitoredTerminal.getName();
            }
            else {
                return "";
            }
        }

        
        private String getConnectionStateString(int state) {
            String connectionState = "UNKNOWN";
            switch (state) {
                case Connection.ALERTING:
                    connectionState = "ALERTING";
                    break;
                case Connection.CONNECTED:
                    connectionState = "CONNECTED";
                    break;
                case Connection.DISCONNECTED:
                    connectionState = "DISCONNECTED";
                    break;
                case Connection.FAILED:
                    connectionState = "FAILED";
                    break;
                case Connection.IDLE:
                    connectionState = "IDLE";
                    break;
                case Connection.INPROGRESS:
                    connectionState = "INPROGRESS";
                    break;
                case Connection.UNKNOWN:
                    connectionState = "UNKNOWN";
                    break;
            }
            return connectionState;
        }

        private String getMetaCodeToString(int meta) {
            String metaString = "META_UNKNOWN";
            switch (meta) {
                case Ev.META_CALL_ADDITIONAL_PARTY:
                    metaString = "META_CALL_ADDITIONAL_PARTY";
                    break;
                case Ev.META_CALL_ENDING:
                    metaString = "META_CALL_ENDING";
                    break;
                case Ev.META_CALL_MERGING:
                    metaString = "META_CALL_MERGING";
                    break;
                case Ev.META_CALL_PROGRESS:
                    metaString = "META_CALL_PROGRESS";
                    break;
                case Ev.META_CALL_REMOVING_PARTY:
                    metaString = "META_CALL_REMOVING_PARTY";
                    break;
                case Ev.META_CALL_STARTING:
                    metaString = "META_CALL_STARTING";
                    break;
                case Ev.META_CALL_TRANSFERRING:
                    metaString = "META_CALL_TRANSFERRING";
                    break;
                case Ev.META_SNAPSHOT:
                    metaString = "META_SNAPSHOT";
                    break;
                case Ev.META_UNKNOWN:
                    metaString = "META_UNKNOWN";
                    break;
            }
            return metaString;
        }
        
        private String GetCauseToString(int cause){
            String strCause = "CAUSE_UNKNOWN";
            switch (cause) {
                case Ev.CAUSE_CALL_CANCELLED:
                    strCause = "CAUSE_CALL_CANCELLED";
                    break;
                case Ev.CAUSE_DEST_NOT_OBTAINABLE:
                    strCause = "CAUSE_DEST_NOT_OBTAINABLE";
                    break;
                case Ev.CAUSE_INCOMPATIBLE_DESTINATION:
                    strCause = "CAUSE_INCOMPATIBLE_DESTINATION";
                    break;
                case Ev.CAUSE_LOCKOUT:
                    strCause = "CAUSE_LOCKOUT";
                    break;
                case Ev.CAUSE_NETWORK_CONGESTION:
                    strCause = "CAUSE_NETWORK_CONGESTION";
                    break;
                case Ev.CAUSE_NETWORK_NOT_OBTAINABLE:
                    strCause = "CAUSE_NETWORK_NOT_OBTAINABLE";
                    break;
                case Ev.CAUSE_NEW_CALL:
                    strCause = "CAUSE_NEW_CALL";
                    break;
                case Ev.CAUSE_NORMAL:
                    strCause = "CAUSE_NORMAL";
                    break;
                case Ev.CAUSE_RESOURCES_NOT_AVAILABLE:
                    strCause = "CAUSE_RESOURCES_NOT_AVAILABLE";
                    break;
                case Ev.CAUSE_SNAPSHOT:
                    strCause = "CAUSE_SNAPSHOT";
                    break;
                case Ev.CAUSE_UNKNOWN:
                    strCause = "CAUSE_UNKNOWN";
                    break;
            }
            return strCause;
        }
        
        private String GetCallControlCause(int cause) {
            String controlCause = "CAUSE_UNKNOWN";
            switch (cause) {
                case CallCtlEv.CAUSE_ALTERNATE:
                    controlCause = "CAUSE_ALTERNATE";
                    break;
                case CallCtlEv.CAUSE_BUSY:
                    controlCause = "CAUSE_BUSY";
                    break;
                case CallCtlEv.CAUSE_CALL_BACK:
                    controlCause = "CAUSE_CALL_BACK";
                    break;
                case CallCtlEv.CAUSE_CALL_CANCELLED:
                    controlCause = "CAUSE_CALL_CANCELLED";
                    break;
                case CallCtlEv.CAUSE_CALL_NOT_ANSWERED:
                    controlCause = "CAUSE_CALL_NOT_ANSWERED";
                    break;
                case CallCtlEv.CAUSE_CALL_PICKUP:
                    controlCause = "CAUSE_CALL_PICKUP";
                    break;
                case CallCtlEv.CAUSE_CONFERENCE:
                    controlCause = "CAUSE_CONFERENCE";
                    break;
                case CallCtlEv.CAUSE_DEST_NOT_OBTAINABLE:
                    controlCause = "CAUSE_DEST_NOT_OBTAINABLE";
                    break;
                case CallCtlEv.CAUSE_DO_NOT_DISTURB:
                    controlCause = "CAUSE_DO_NOT_DISTURB";
                    break;
                case CallCtlEv.CAUSE_INCOMPATIBLE_DESTINATION:
                    controlCause = "CAUSE_INCOMPATIBLE_DESTINATION";
                    break;
                case CallCtlEv.CAUSE_LOCKOUT:
                    controlCause = "CAUSE_LOCKOUT";
                    break;
                case CallCtlEv.CAUSE_NETWORK_CONGESTION:
                    controlCause = "CAUSE_NETWORK_CONGESTION";
                    break;
                case CallCtlEv.CAUSE_NETWORK_NOT_OBTAINABLE:
                    controlCause = "CAUSE_NETWORK_NOT_OBTAINABLE";
                    break;
                case CallCtlEv.CAUSE_NEW_CALL:
                    controlCause = "CAUSE_NEW_CALL";
                    break;
                case CallCtlEv.CAUSE_NORMAL:
                    controlCause = "CAUSE_NORMAL";
                    break;
                case CallCtlEv.CAUSE_PARK:
                    controlCause = "CAUSE_PARK";
                    break;
                case CallCtlEv.CAUSE_REDIRECTED:
                    controlCause = "CAUSE_REDIRECTED";
                    break;
                case CallCtlEv.CAUSE_REORDER_TONE:
                    controlCause = "CAUSE_REORDER_TONE";
                    break;
                case CallCtlEv.CAUSE_RESOURCES_NOT_AVAILABLE:
                    controlCause = "CAUSE_RESOURCES_NOT_AVAILABLE";
                    break;
                case CallCtlEv.CAUSE_SNAPSHOT:
                    controlCause = "CAUSE_SNAPSHOT";
                    break;
                case CallCtlEv.CAUSE_TRANSFER:
                    controlCause = "CAUSE_TRANSFER";
                    break;
                case CallCtlEv.CAUSE_TRUNKS_BUSY:
                    controlCause = "CAUSE_TRUNKS_BUSY";
                    break;
                case CallCtlEv.CAUSE_UNHOLD:
                    controlCause = "CAUSE_UNHOLD";
                    break;
                case CallCtlEv.CAUSE_UNKNOWN:
                    controlCause = "CAUSE_UNKNOWN";
                    break;
            }
            return controlCause;
        }
        
        private String GetCallControlState(int cause) {
            String controlState = "UNKNOWN";
            switch (cause) {
                case CallControlTerminalConnection.ACTIVE:
                    controlState = "ACTIVE";
                    break;
                case CallControlTerminalConnection.BRIDGED:
                    controlState = "BRIDGED";
                    break;
                case CallControlTerminalConnection.DROPPED:
                    controlState = "DROPPED";
                    break;
                case CallControlTerminalConnection.HELD:
                    controlState = "HELD";
                    break;
                case CallControlTerminalConnection.IDLE:
                    controlState = "IDLE";
                    break;
                case CallControlTerminalConnection.INUSE:
                    controlState = "INUSE";
                    break;
                case CallControlTerminalConnection.PASSIVE:
                    controlState = "PASSIVE";
                    break;
                case CallControlTerminalConnection.RINGING:
                    controlState = "RINGING";
                    break;
                case CallControlTerminalConnection.TALKING:
                    controlState = "TALKING";
                    break;
                case CallControlTerminalConnection.UNKNOWN:
                    controlState = "UNKNOWN";
                    break;
            }
            return controlState;
        }
        
        private String GetForward() {
            String fwd = "";
            if (this.forward != null) {
                for (CallControlForwarding ccf : this.forward) {
                    if (ccf.getFilter() == ccf.ALL_CALLS) {
                        fwd = ccf.getDestinationAddress();
                    }
                }
            }
            return fwd;
        }

        public void printConnections() {
            try {
                if (this.connections != null) {
                    this.logger.println(this.connections.length + " connections from " + this.getDirNum());
                    for (Connection c : this.connections) {
                        this.logger.println(" - Connection "+ c.toString());
                        this.printTerminalConnections(c);
                        Connection[] callconnections = c.getCall().getConnections();
                        if (callconnections != null) {
                            for (Connection callc : callconnections) {
                                if (!callc.equals(c)) {
                                    this.logger.println(" - Third party connection "+ callc.toString());
                                }
                            }
                        }
                    }
                }
                else{
                    this.logger.println("No connections from " + this.getDirNum());
                }
            }
            catch (Exception e){
                /*this.logger.printerror("Unable to print connections from " + this.getDirNum() + ": "+ e.toString());*/
            }
        }
        
        private void printTerminalConnections(Connection c) {
            try {
                TerminalConnection[] tcs = c.getTerminalConnections();
                if (tcs != null && tcs.length >= 1) {
                    this.logger.println(tcs.length + " terminal connections associated with " + c.toString());
                    for (TerminalConnection tc : tcs) {
                        if (tc instanceof CallControlTerminalConnection) {
                            this.logger.println(" - Terminal connection " +((CallControlTerminalConnection)tc).toString());
                        }
                        else {
                            this.logger.println(" - Terminal connection " + tc.toString());
                        }
                    }
                }
            }
            catch (Exception e) {
                this.logger.printerror("Unable to print terminal connections from " + this.getDirNum() + ": "+ e.toString());
            }
        }

        /**
         * Implementation of addressChangedEvent inherited from AddressObserver
         * @param events 
         */
        public void addressChangedEvent ( AddrEv [] events ) {
            this.logger.println(String.valueOf(events.length) + " address changed events received for " + this.getDirNum());
            for (int i=0; i<events.length; i++ ) {
                String eventName = events[i].toString();
                this.logger.println("Event type " + eventName + " from " + this.getDirNum());
                //else {
                    this.cause = events[i].getCause();
                    this.logger.println("Event cause: "+ this.GetCauseToString(cause) + " for "+ this.getDirNum());
                    if (events[i].isNewMetaEvent()) {
                        this.metacode = events[i].getMetaCode();
                        this.logger.println("New meta event: " + this.getMetaCodeToString(this.metacode) + " for "+ this.getDirNum());
                    }
                    switch (events[i].getID()){
                        case AddrObservationEndedEv.ID:
                            this.logger.println("Address observation ended for " + this.getDirNum());
                            /*this.stop();*/
                            break;
                        case CallCtlAddrDoNotDisturbEv.ID:
                            this.logger.println("Do not disturb event received for "+ this.getDirNum());
                            this.dnd = ((CallCtlAddrDoNotDisturbEv)events[i]).getDoNotDisturbState();
                            this.setLineControl("CallCtlAddrDoNotDisturbEv");
                            break;
                        case CallCtlAddrForwardEv.ID:
                            this.logger.println("Forwarding event received for "+ this.getDirNum());
                            this.forward = ((CallCtlAddrForwardEv)events[i]).getForwarding();
                            this.setLineControl("CallCtlAddrForwardEv");
                            break;
                        case CallCtlAddrMessageWaitingEv.ID:
                            this.logger.println("Message waiting event received for "+ this.getDirNum());
                            this.mwi = ((CallCtlAddrMessageWaitingEv)events[i]).getMessageWaitingState();
                            this.setLineControl("CallCtlAddrMessageWaitingEv");
                            break;
                        default:
                            this.logger.println("This event is not implemented");
                            break;
                    }
                //}
            }
        }

        /**
         * Implemenation of callChangedEvent inherited from CallObserver
         * @param events
         */
        public void callChangedEvent ( CallEv [] events ) {
            this.logger.println(String.valueOf(events.length) + " call changed events received for "+ this.getDirNum());
            this.connections = this.monitoredAddress.getConnections();
            org.wybecom.talkportal.cti.stateserver.Call call = null;
            for (int i=0; i<events.length; i++ ) {
                this.logger.println("Event type " + events[i].toString() + " from " + this.getDirNum());
                this.cause = events[i].getCause();
                this.logger.println("Event cause: "+ this.GetCauseToString(cause) + " for "+ this.getDirNum());
                if (events[i].isNewMetaEvent()) {
                    this.metacode = events[i].getMetaCode();
                    this.logger.println("New meta event: " + this.getMetaCodeToString(this.metacode) + " for "+ this.getDirNum());
                }
                if (events[i] instanceof CallCtlEv) {
                    this.callControlState = ((CallCtlEv)events[i]).getCallControlCause();
                    this.logger.println("Call Control cause: "+ this.GetCallControlCause(this.callControlState) + " for "+ this.getDirNum());
                }
                switch (events[i].getID()){
                    case CallActiveEv.ID:
                        //CallEv (getCall())
                        this.logger.println("Call active event received for "+ this.getDirNum());
                        this.printConnections();
                        
                        /*String to = this.getDirNum();
                        String from = "";
                        for (Connection c : this.connections) {
                                if (c instanceof CallControlConnection) {
                                    if (((CallControlConnection)c).getCallControlState() == CallControlConnection.OFFERED) {
                                        Connection[] callconnections = ((CallControlConnection)c).getCall().getConnections();
                                            if (callconnections != null) {
                                                for (Connection callc : callconnections) {
                                                    if (!callc.equals(c)) {
                                                        from = callc.getAddress().getName();
                                                        this.logger.println("Call in progress from " + from + " to " + to);
                                                        if (from.equalsIgnoreCase("2426") && to.equalsIgnoreCase("7126")) {
                                                                try {
                                                                    this.logger.println("Reject call from " + from);

                                                                    ((CallControlConnection)c).reject();
                                                                }
                                                                catch (Exception rejectException) {
                                                                    this.logger.println("Call rejection from " + from + " to " + to + " failed: " + rejectException.toString());
                                                                }
                                                            }
                                                            else if (to.equalsIgnoreCase("7126")) {
                                                                try {
                                                                    this.logger.println("Redirect call from " + from);
                                                                    ((CallControlConnection)c).redirect("2426");
                                                                }
                                                                catch (Exception redirectException) {
                                                                    this.logger.println("Call redirection from " + from + " to " + to + " failed: " + redirectException.toString());
                                                                }

                                                            }
                                                        break;
                                                        }
                                                        
                                                    }
                                                }
                                            }
                                        break;
                                    }
                                }
                            
                            
                               */
                        
                        /*if (((ConnInProgressEv)events[i]).getConnection().getAddress().getName().equals(this.getDirNum())) {
                            to =   ((ConnInProgressEv)events[i]).getConnection().getAddress().getName();
                            from = ((ConnInProgressEv)events[i]).getCall().getConnections()[1].getAddress().getName();
                        }
                        else {
                            to =   ((ConnInProgressEv)events[i]).getConnection().getAddress().getName();
                            from = ((ConnInProgressEv)events[i]).getCall().getConnections()[0].getAddress().getName();
                        }*/



                        this.setLineControl("CallActiveEv");
                        break;
                    case CallInvalidEv.ID:
                        //CallEv (getCall())
                        this.logger.println("Call inactive event received for "+ this.getDirNum());
                        this.printConnections();
                        this.setLineControl("CallInvalidEv");
                        break;
                    case CallObservationEndedEv.ID:
                        //CallEv (getCall())
                        this.logger.println("Call observation ended event received for "+ this.getDirNum());
                        this.printConnections();
                        this.setLineControl("CallObservationEndedEv");
                        break;
                    case CallCtlConnOfferedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection offered for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("CallCtlConnOfferedEv");
                        break;
                    case CallCtlConnQueuedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection queued for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("CallCtlConnQueuedEv");
                        break;
                    case CallCtlConnAlertingEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection alerting for " + this.getDirNum());
                        this.printConnections();
                        
                        call = (org.wybecom.talkportal.cti.stateserver.Call)calls.get(this.getCallId(((CallCtlConnAlertingEv)events[i]).getCall().toString()));
                        if (call == null) {
                            call = new org.wybecom.talkportal.cti.stateserver.Call();
                            call.setStartTime(this.GetDate());
                            call.setCallId(this.getCallId(((CallCtlConnAlertingEv)events[i]).getCall().toString()));
                            call.setCallee(((CallCtlConnAlertingEv)events[i]).getCalledAddress().getName());
                            call.setCaller(((CallCtlConnAlertingEv)events[i]).getCallingAddress().getName());
                            call.setType(CallType.MISSED);
                            calls.put(call.getCallId(), call);
                        }
                        this.setLineControl("CallCtlConnAlertingEv");
                        break;
                    case CallCtlConnInitiatedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection initiated for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("CallCtlConnInitiatedEv");
                        break;
                    case CallCtlConnDialingEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection dialing for " + this.getDirNum());
                        this.printConnections();
                        call = new org.wybecom.talkportal.cti.stateserver.Call();
                        call.setStartTime(this.GetDate());
                        call.setCallId(this.getCallId(((CallCtlConnDialingEv)events[i]).getCall().toString()));
                        call.setType(CallType.PLACED);
                        calls.put(call.getCallId(), call);
                        this.setLineControl("CallCtlConnDialingEv");
                        break;
                    case CallCtlConnNetworkReachedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection network reached for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("CallCtlConnNetworkReachedEv");
                        break;
                    case CallCtlConnNetworkAlertingEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection network alerting for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("CallCtlConnNetworkAlertingEv");
                        break;
                    case CallCtlConnFailedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection failed for " + this.getDirNum());
                        this.printConnections();
                        call = (org.wybecom.talkportal.cti.stateserver.Call)calls.get(this.getCallId(((CallCtlConnFailedEv)events[i]).getCall().toString()));
                        call.setEndTime(this.GetDate());
                        if (call != null) {
                            this.addCall(call);
                            calls.remove(call.getCallId());
                        }
                        this.setLineControl("CallCtlConnFailedEv");
                        break;
                    case CallCtlConnEstablishedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection established for " + this.getDirNum());
                        this.printConnections();
                        call = (org.wybecom.talkportal.cti.stateserver.Call)calls.get(this.getCallId(((CallCtlConnEstablishedEv)events[i]).getCall().toString()));
                        calls.remove(this.getCallId(((CallCtlConnEstablishedEv)events[i]).getCall().toString()));
                        switch (call.getType()) {
                            case PLACED:
                                call.setCallee(((CallCtlConnEstablishedEv)events[i]).getCalledAddress().getName());
                                call.setCaller(((CallCtlConnEstablishedEv)events[i]).getCallingAddress().getName());
                                break;
                            case MISSED:
                                call.setType(CallType.RECEIVED);
                                break;
                        }
                        calls.put(call.getCallId(), call);
                        this.setLineControl("CallCtlConnEstablishedEv");
                        break;
                    case CallCtlConnUnknownEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection unknown for " + this.getDirNum());
                        this.printConnections();
                        call = (org.wybecom.talkportal.cti.stateserver.Call)calls.get(this.getCallId(((CallCtlConnUnknownEv)events[i]).getCall().toString()));
                        call.setEndTime(this.GetDate());
                        if (call != null) {
                            this.addCall(call);
                            calls.remove(call.getCallId());
                        }
                        this.setLineControl("CallCtlConnUnknownEv");
                        break;
                    case CallCtlConnDisconnectedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress()) 
                        this.logger.println("Call Control Connection disconnected for " + this.getDirNum());
                        this.printConnections();
                        call = (org.wybecom.talkportal.cti.stateserver.Call)calls.get(this.getCallId(((CallCtlConnDisconnectedEv)events[i]).getCall().toString()));
                        call.setEndTime(this.GetDate());
                        if (call != null) {
                            this.addCall(call);
                            calls.remove(call.getCallId());
                        }
                        this.setLineControl("CallCtlConnDisconnectedEv");
                        break;
                    case ConnAlertingEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.logger.println("Conn Alerting for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("ConnAlertingEv");
                        break;
                    case ConnConnectedEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.logger.println("Conn Connected for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("ConnConnectedEv");
                        break;
                    case ConnCreatedEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.logger.println("Conn Created for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("ConnCreatedEv");
                        break;
                    case ConnDisconnectedEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.logger.println("Conn Disconnected for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("ConnDisconnectedEv");
                        break;
                    case ConnFailedEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.logger.println("Conn Failed for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("ConnFailedEv");
                        break;
                    case ConnInProgressEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.logger.println("Conn In progress for " + this.getDirNum());
                        this.printConnections();
                        
                        
                        this.setLineControl("ConnInProgressEv");
                        break;
                    case ConnUnknownEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.logger.println("Conn Unknown for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("ConnUnknownEv");
                        break;
                    case CallCtlTermConnHeldEv.ID:
                        this.logger.println("Call Control Terminal Connection Held for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("CallCtlTermConnHeldEv");
                        break;
                    case CallCtlTermConnTalkingEv.ID:
                        this.logger.println("Call Control Terminal Connection Talking for " + this.getDirNum());
                        this.printConnections();
                        this.setLineControl("CallCtlTermConnTalkingEv");
                        break;

                }
            }
        }

        /**
         * Implementation of terminalChangedEvent inherited from TerminalObserver
         * @param events
         */
        public void terminalChangedEvent ( TermEv [] events ) {
            this.logger.println(String.valueOf(events.length) + " terminal changed events received.");
            for ( int i=0; i<events.length; i++ ) {
                switch ( events[i].getID () ) {
                    case TermObservationEndedEv.ID:
                        this.logger.println("Terminal observation ended from " + this.getTerminalName());
                        break;
                }
            }
        }

        /***
         * Sends the presence information to the state servers
         * @param ev The related JTAPI events
         */
        public void setLineControl(String ev){
            try {
                Enumeration en = stateClients.elements();
                while (en.hasMoreElements()) {
                    stateclient sc = (stateclient)en.nextElement();
                    sc.setLineState(this.getLineControl(), ev);
                }
            }
            catch (Exception e) {
                /*this.logger.printerror("Unable to set line control from " + this.getDirNum() + ": "+ e.toString());*/
            }
        }
        
        /**
         * Calculates the presence information
         * @return The presence information
         */
        public LineControl getLineControl() {
            
            try {
                this.monitoredLineControl.setDoNotDisturb(dnd);
                this.monitoredLineControl.setForward(this.GetForward());
                this.monitoredLineControl.setMwiOn(this.mwi);
                this.monitoredLineControl.setMonitored(this.monitored);
                ArrayOfLineControlConnection alcc = this.monitoredLineControl.getLineControlConnection();
                if (alcc != null) {
                    alcc.getLineControlConnection().clear();
                }
                if (this.connections != null) {
                    for (Connection c : this.connections){
                        LineControlConnection lcc = new LineControlConnection();
                        lcc.setCallid(this.getCallIdFromConnection(c));
                        lcc.setState(this.getConnectionState(c));
                        lcc.setTerminalState(this.getTerminalState(c));
                        javax.telephony.Call connectionCall = c.getCall();
                        if (connectionCall != null) {
                            Connection[] callconnections = connectionCall.getConnections();
                            if (callconnections != null) {
                                for (Connection callc : callconnections) {
                                    if (!callc.equals(c)) {
                                        lcc.setRemoteState(this.getConnectionState(callc));
                                        if (callc.getAddress() != null) {
                                            lcc.setContact(callc.getAddress().getName());
                                        }
                                    }
                                }
                            }
                        }
                        alcc.getLineControlConnection().add(lcc);
                    }
                    
                }
                this.monitoredLineControl.setLineControlConnection(alcc);
                return this.monitoredLineControl;
            }
            catch (Exception e) {
                this.logger.printerror("Unable to construct LineControl from " + this.getDirNum() + ": " + e.toString());
                return this.monitoredLineControl;
            }
        }
        
        private void addCall(org.wybecom.talkportal.cti.stateserver.Call c) {
            try {
                Enumeration en = stateClients.elements();
                while (en.hasMoreElements()) {
                    stateclient sc = (stateclient)en.nextElement();
                    sc.addCallLog(this.getDirNum(), c);
                }
            }
            catch (Exception e) {
                this.logger.printerror("Unable to add call from " + this.getDirNum() + ": "+ e.toString());
            }
        }
        
        private XMLGregorianCalendar GetDate()
        {
            try
            {
                GregorianCalendar gc = new GregorianCalendar();
                gc.setTime(Calendar.getInstance().getTime());
                return DatatypeFactory.newInstance().newXMLGregorianCalendar(gc);
            }
            catch (Exception e)
            {
                this.logger.println("Unable to get date: " + e.toString());
                return null;
            }
        }

        /**
         * Retreives a call id from call reference
         * @param call Call reference
         * @return The call id
         */
        public String getCallId(String call)
        {
           return call.split("=")[1].split("-")[0];
        }
        
        private String getCallIdFromConnection(Connection c) {
            return this.getCallId(c.getCall().toString());
        }
        
        private ConnectionState getConnectionState(Connection c) {
            ConnectionState cs = ConnectionState.UNKNOWN;
            try{
                this.logger.println("Getting connection state from " + c.toString());
                switch (((CallControlConnection)c).getCallControlState()){
                        case CallControlConnection.ALERTING:
                            cs = ConnectionState.ALERTING;
                            break;
                        case CallControlConnection.DIALING:
                            cs = ConnectionState.DIALING;
                            break;
                        case CallControlConnection.DISCONNECTED:
                            cs = ConnectionState.DISCONNECTED;
                            break;
                        case CallControlConnection.ESTABLISHED:
                            cs = ConnectionState.ESTABLISHED;
                            break;
                        case CallControlConnection.FAILED:
                            cs = ConnectionState.FAILED;
                            break;
                        case CallControlConnection.IDLE:
                            cs = ConnectionState.IDLE;
                            break;
                        case CallControlConnection.INITIATED:
                            cs = ConnectionState.INITIATED;
                            break;
                        case CallControlConnection.NETWORK_ALERTING:
                            cs = ConnectionState.NETWORK_ALERTING;
                            break;
                        case CallControlConnection.NETWORK_REACHED:
                            cs = ConnectionState.NETWORK_REACHED;
                            break;
                        case CallControlConnection.OFFERED:
                            cs = ConnectionState.OFFERED;
                            break;
                        case CallControlConnection.QUEUED:
                            cs = ConnectionState.QUEUED;
                            break;
                        case CallControlConnection.UNKNOWN:
                            cs = ConnectionState.UNKNOWN;
                            break;
                }
                return cs;
            }
            catch (Exception e) {
                this.logger.println("Unable to retreive connection state from " + c.toString());
                return cs;
            }
        }
        
        private TerminalState getTerminalState(Connection c) {
            TerminalState ts = TerminalState.UNKNOWN;
            try {
                TerminalConnection[] tcs = c.getTerminalConnections();
                if (tcs != null && tcs.length >= 1) {
                    if (tcs[0] instanceof CallControlTerminalConnection) {
                        this.logger.println("Terminal connection is " + ((CallControlTerminalConnection)tcs[0]).getCallControlState());
                        switch (((CallControlTerminalConnection)tcs[0]).getCallControlState()) {
                            case CallControlTerminalConnection.BRIDGED:
                                ts = TerminalState.BRIDGED;
                                break;
                            case CallControlTerminalConnection.DROPPED:
                                ts = TerminalState.DROPPED;
                                break;
                            case CallControlTerminalConnection.HELD:
                                ts = TerminalState.HELD;
                                break;
                            case CallControlTerminalConnection.IDLE:
                                ts = TerminalState.IDLE;
                                break;
                            case CallControlTerminalConnection.ACTIVE:
                                ts = TerminalState.TALKING;
                                break;
                            case CallControlTerminalConnection.INUSE:
                                ts = TerminalState.INUSE;
                                break;
                            case CallControlTerminalConnection.PASSIVE:
                                ts = TerminalState.INUSE;
                                break;
                            case CallControlTerminalConnection.RINGING:
                                ts = TerminalState.RINGING;
                                break;
                            case CallControlTerminalConnection.TALKING:
                                ts = TerminalState.TALKING;
                                break;
                            case CallControlTerminalConnection.UNKNOWN:
                                ts = TerminalState.UNKNOWN;
                                break;
                        }
                    }
                    else {
                        switch (tcs[0].getState()) {
                            case CallControlTerminalConnection.BRIDGED:
                                ts = TerminalState.BRIDGED;
                                break;
                            case CallControlTerminalConnection.DROPPED:
                                ts = TerminalState.DROPPED;
                                break;
                            case CallControlTerminalConnection.HELD:
                                ts = TerminalState.HELD;
                                break;
                            case CallControlTerminalConnection.IDLE:
                                ts = TerminalState.IDLE;
                                break;
                            case CallControlTerminalConnection.ACTIVE:
                                ts = TerminalState.TALKING;
                                break;
                            case CallControlTerminalConnection.INUSE:
                                ts = TerminalState.INUSE;
                                break;
                            case CallControlTerminalConnection.PASSIVE:
                                ts = TerminalState.INUSE;
                                break;
                            case CallControlTerminalConnection.RINGING:
                                ts = TerminalState.RINGING;
                                break;
                            case CallControlTerminalConnection.TALKING:
                                ts = TerminalState.TALKING;
                                break;
                            case CallControlTerminalConnection.UNKNOWN:
                                ts = TerminalState.UNKNOWN;
                                break;
                        }
                    
                    }
                }
                return ts;
            }
            catch (Exception e) {
                return ts;
            }
        }
        
        /**
         * Retreives a terminal connection associated with a call
         * @param callid The call id
         * @return The JTAPI TerminalConnection
         */
        public TerminalConnection getTerminalConnectionFromCallId(String callid) {
            TerminalConnection tc = null;
            Connection c = null;
            c = this.getConnectionFromCallId(callid);
            if (c != null) {
                this.logger.println("Getting terminal connections from " + callid);
                TerminalConnection[] tcs = c.getTerminalConnections();
                this.logger.println(tcs.length + " terminal connections from " + callid);
                if (tcs != null && tcs.length >= 1) {
                    tc = tcs[0];
                }
            }
            return tc;
        }
        
        /**
         * Creates a call
         * @return The JTAPI Call
         * @throws ResourceUnavailableException
         * @throws InvalidStateException
         * @throws PrivilegeViolationException
         * @throws MethodNotSupportedException
         */
        public javax.telephony.Call createCall() throws ResourceUnavailableException, InvalidStateException, PrivilegeViolationException, MethodNotSupportedException {
            return this.monitoredAddress.getProvider().createCall();
        }
        
        /**
         *
         * @return
         */
        public Terminal getTerminal() {
            return this.monitoredTerminal;
        }
        
        /**
         *
         * @return The JTAPI Address
         */
        public Address getAddress() {
            return this.monitoredAddress;
        }
        
        /**
         *
         * @param callid The call id
         * @return The connection associated with the call
         */
        public Connection getConnectionFromCallId(String callid) {
            Connection c = null;
            this.logger.println("Getting connection from callid: " + callid);
            for (Connection ct : this.connections) {
                String currentCallid = this.getCallId(ct.getCall().toString());
                this.logger.println("Comparing " + currentCallid + " with " + callid);
                if (currentCallid.equals(callid)) {
                    c = ct;
                    break;
                }
            }
            return c;
        }
        
        /**
         * Accessor
         * @return The DND status
         */
        public Boolean getDnd() {
            return this.dnd;
        }
        
        /**
         * Accessor
         * @param status The DND status
         */
        public void setDnd(Boolean status) {
            this.dnd = status;
        }
        
        /**
         *
         * @param callid The call id
         * @return The JTAPI call
         */
        public javax.telephony.Call getCallFromCallid(String callid) {
            javax.telephony.Call c = null;
            this.logger.println("Getting call from callid: " + callid);
            for (Connection ct : this.connections) {
                String currentCallid = this.getCallId(ct.getCall().toString());
                this.logger.println("Comparing " + currentCallid + " with " + callid);
                if (currentCallid.equals(callid)) {
                    c = ct.getCall();
                    break;
                }
            }
            return c;
        }
        
        
        
        /**
         *
         * @return The current call
         */
        public javax.telephony.callcontrol.CallControlCall getCurrentCallControlCall() {
            javax.telephony.callcontrol.CallControlCall  ccc = null;
            this.logger.println("Getting current call control call from " + this.getDirNum());
            if (this.connections.length > 0 && this.connections.length < 2) {
                if (this.connections[0].getCall() instanceof CallControlCall) {
                    ccc = (CallControlCall)this.connections[0].getCall();
                }
                else {
                    this.logger.printerror("Current call from "+this.getDirNum()+" is not instance of call control call");
                }
            }
            else {
                this.logger.println("Too many connections from " + this.getDirNum());
            }
            return ccc;
        }
        
        
        public void log(String string) {
            this.logger.println(string);
        }

        /**
         *
         * @param status The line status @see \ref org.wybecom.talkportal.cti.stateserver.Status "Status"
         */
        public void setMonitoredLineStatus(Status status) {
            this.monitoredLineControl.setStatus(status);
        }
        
        /**
         * Retreives the JTAPI Address status
         * @return True if line is open, false otherwise
         */
        public Boolean addressIsInService() {
            return this.addressInService;
        }
        
        /**
         * Sets the JTAPI Address status
         * @param service True or false
         */
        public void setAddressInService(Boolean service) {
            this.addressInService = service;
        }
        
        
        public Boolean terminalIsInService() {
            return this.terminalInService;
        }
        
        
        public void setTerminalInService(Boolean service) {
            this.terminalInService = service;
        }

        
        public void setMonitoredTerminal(Terminal terminal) {
            this.monitoredTerminal = terminal;
        }
        
}
