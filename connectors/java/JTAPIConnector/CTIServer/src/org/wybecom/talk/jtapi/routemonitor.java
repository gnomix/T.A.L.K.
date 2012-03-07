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
 * Implementation of routemonitor
 */

package org.wybecom.talk.jtapi;

import java.util.*;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.telephony.*;
import javax.telephony.callcenter.*;
import javax.telephony.callcenter.events.*;
import javax.telephony.events.*;
import org.wybecom.talk.team.config.*;
/**
 * Implementation of routemonitor
 * @author Yohann BARRE
 */
public class routemonitor implements AddressObserver, RouteCallback, TerminalObserver, ACDAddressObserver, CallObserver{

    private trace logger;
    private RouteAddress monitoredAddress;
    private ACDAddress monitoredAcdAddress;
    private Terminal monitoredTerminal;
    private Xmlteam team;
    private Vector ctimonitors = new Vector();


    /**
     * Initialize a routemonitor
     * @param address The JTAPI RouteAddress
     * @param log The logger
     * @param xmlteam The team settings
     * @param aaddress The JTAPI ACDAddress
     */
    public routemonitor(RouteAddress address, trace log, Xmlteam xmlteam, ACDAddress aaddress) {
        this.logger = log;
        this.team = xmlteam;
        this.monitoredAddress = address;
        this.monitoredAcdAddress = aaddress;
    }

    /**
     * \brief Starts the routemonitor
     */
    public void start() {
        try {
            this.log("Starting RouteAdress: " + this.monitoredAddress.ALL_ROUTE_ADDRESS + ", which will act as " + this.team.getName() + " team.");
            if (monitoredAddress != null) {
                try {
                        Terminal[] ts = this.monitoredAddress.getTerminals();
                        this.log(ts.length + " terminals associated with " + this.monitoredAddress.getName());
                        if (ts != null) {
                            this.monitoredTerminal = ts[0];

                            this.log("Application will monitoring " + this.monitoredAddress.getName() + " with this terminal " + this.monitoredTerminal.getName());

                            ts = null;

                            /*for (ACDManagerAddress ama : this.getACDManagerAddress()){
                                ctimonitor cm = new ctimonitor(ama, this.getLogger());
                                ctimonitors.add(cm);
                                cm.start();
                            }*/
                        }
                    }
                    catch (Exception terminalException) {
                        this.log("Unable to retrieve terminal for " + this.monitoredAddress.getName() + " " + terminalException.toString());
                    }
                this.addObservers();
            }
        }
        catch (Exception e) {
            this.log("exception while starting this routemonitor: " + e.getMessage());
        }
    }

    /**
     * \brief Stops the routemonitor
     */
    public void stop() {
        try {
            this.removeObservers();
            if (monitoredAddress != null) {
                try {
                    this.finalize();
                } catch (Throwable ex) {
                    Logger.getLogger(monitor.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        }
        catch (Exception e){
            this.log("exception while stoping this monitor" + e.getMessage());
        }
    }

    public void log(String string) {
            this.logger.println(string);
        }

    public trace getLogger(){
            return this.logger;
        }

    /**
     * Accessor
     * @return The JTAPI RouteAddress name
     */
    public String getAddressName() {
        return this.monitoredAddress.getName();
    }

    /**
     * Accessor
     * @return The JTAPI terminal associated with the JTAPI RouteAddress
     */
    public Terminal getTerminal() {
        return this.monitoredTerminal;
    }

    /**
     * Accessor
     * @return The JTAPI ACDAddress
     */
    public ACDAddress getACDAddress(){
        return this.monitoredAcdAddress;
    }

    /**
     * \brief Adds observers from the JTAPI RouteAddress and Terminal
     */
    public void addObservers() {
        try {
            if (monitoredTerminal != null) {
                if (monitoredTerminal.getObservers() == null) {
                    this.log("Adding observer from Terminal: " + this.monitoredTerminal.getName());
                    monitoredTerminal.addObserver(this);
                }
            }
            this.monitoredAddress.addObserver(this);
            this.monitoredAddress.addCallObserver(this);
            this.monitoredAddress.registerRouteCallback(this);
            this.monitoredAcdAddress.addObserver(this);
            this.log(this.team.getName() + " team is operational");
        }
        catch (Exception e){
            this.log("exception while adding observers from " + this.monitoredAddress.getName() + " " + e.getMessage());
        }
    }

    /**
     * \brief Removes the observers
     */
    public void removeObservers() {
        try {
            if (monitoredTerminal != null) {
                if (monitoredTerminal.getObservers() != null) {
                    this.log("Removing observer from Terminal: " + this.monitoredTerminal.getName());
                    monitoredTerminal.removeObserver(this);
                }
            }
            this.monitoredAddress.removeObserver(this);
            this.monitoredAddress.removeCallObserver(this);
            this.monitoredAcdAddress.removeObserver(this);
            this.monitoredAddress.cancelRouteCallback(this);
        }
        catch (Exception e){
            this.log("exception while removing observers from " + this.monitoredAddress.getName() + " " + e.getMessage());
        }
    }

    /**
         * Implementation of addressChangedEvent inherited from AddressObserver
         * @param events
         */
        public void addressChangedEvent ( AddrEv [] events ) {
            for (int i=0; i<events.length; i++ ) {
                String eventName = events[i].toString();
                
                    switch (events[i].getID()){
                        case AddrObservationEndedEv.ID:
                            break;
                        case ACDAddrLoggedOnEv.ID:

                            break;
                        default:
                            break;
                    }
                //}
            }
        }

        /**
         * Implementation of terminalChangedEvent inherited from TerminalObserver
         * @param events
         */
        public void terminalChangedEvent ( TermEv [] events ) {
            for ( int i=0; i<events.length; i++ ) {
                switch ( events[i].getID () ) {
                    case TermObservationEndedEv.ID:
                        break;
                }
            }
        }

        /**
         * Implementation of callChangedEvent inherited from CallObserver
         * @param events
         */
        public void callChangedEvent(CallEv[] events) {
            for ( int i=0; i<events.length; i++ ) {
                switch ( events[i].getID () ) {
                    default:
                        break;
                }
            }
        }

        /**
         * Implementation of reRouteEvent inherited from RouteCallBack
         * @param event
         */
        public void reRouteEvent( ReRouteEvent event) {
        }

        /**
         * Implementation of routeCallbackEndedEvent inherited from RouteCallBack
         * @param event
         */
        public void routeCallbackEndedEvent(RouteCallbackEndedEvent event) {
        }

        /**
         * Implementation of routeEndEvent inherited from RouteCallBack
         * @param event
         */
        public void routeEndEvent(RouteEndEvent event) {
        }

        /**
         * Implementation of routeEvent inherited from RouteCallBack
         * @param event
         */
        public void routeEvent(RouteEvent event) {
            String[] destinations = this.getDestinations();
            if (destinations != null && destinations.length > 0){
            try {
                String dest = "Selected destinations: ";
                for (String s : destinations){
                    dest += s + " ";
                }
                this.log(dest);
                RouteThread rt = new RouteThread(event, destinations);
                rt.start();
            }
            catch (Exception selectRouteException) {
                this.log("Unable to selectRoute: " + selectRouteException.toString());
            }
            }
            else {
                this.log("No destinations available");
                try {
                    this.log("Disconnecting connection: " + event.getCurrentRouteAddress().getConnections()[0].toString());
                    event.getCurrentRouteAddress().getConnections()[0].disconnect();
                }
                catch (Exception cancelException){
                    this.log("Unable to stop call back: "+ cancelException.toString());
                }
            }
        }

        /**
         * Implementation of routeUsedEvent inherited from RouteCallBack
         * @param event
         */
        public void routeUsedEvent(RouteUsedEvent event) {
            /*try {
                this.logger.println("End route: " + event.getRouteUsed().getName());
                event.getRouteSession().endRoute(RouteSession.ERROR_RESOURCE_BUSY);
            }
            catch (Exception e) {
                this.logger.println("Unable to endRoute: " + e.getMessage());
            }*/
        }

        public String getRouteEvCause(int cause){
            String s = "";
            switch (cause){
                case RouteSession.CAUSE_INVALID_DESTINATION:
                    s = "Invalid destination";
                    break;
                case RouteSession.CAUSE_NO_ERROR:
                    s = "No error";
                    break;
                case RouteSession.CAUSE_PARAMETER_NOT_SUPPORTED:
                    s = "Parameter not supported";
                    break;
                case RouteSession.CAUSE_ROUTING_TIMER_EXPIRED:
                    s = "Routing timer expired";
                    break;
                case RouteSession.CAUSE_STATE_INCOMPATIBLE:
                    s = "State incompatible";
                    break;
                case RouteSession.CAUSE_UNSPECIFIED_ERROR:
                    s = "Unspecified error";
                    break;
                case RouteSession.ERROR_RESOURCE_BUSY:
                    s = "Ressource busy";
                    break;
                case RouteSession.ERROR_RESOURCE_OUT_OF_SERVICE:
                    s = "Ressource out of service";
                    break;
                case RouteSession.ERROR_UNKNOWN:
                    s = "Unknown";
                    break;
            }
            return s;
        }

        public RouteSession[] getActiveRouteSession(){
            RouteSession[] sessions = null;
            try{
                sessions = this.monitoredAddress.getActiveRouteSessions();
            }
            catch (Exception e){
                this.log("Unable to get active route sessions: " + e.toString());
            }
            return sessions;
        }

        public String[] getDestinations() {
            Vector v = new Vector();
            try {
                /*this.log(String.valueOf(this.monitoredAcdAddress.getLoggedOnAgents().length) + " agents logged");
                for (Agent a : this.monitoredAcdAddress.getLoggedOnAgents()) {
                    if (a.getState() == Agent.READY || a.getState() == Agent.WORK_READY){
                        v.add(a.getAgentAddress().getName());
                    }
                }*/
                
                for (ACDManagerAddress ama : this.getACDManagerAddress()){
                    if (ama.getConnections() == null){
                        v.add(ama.getName());
                    }
                }
            }
            catch (Exception e){
                this.log("Unable to get destinations from " + this.getAddressName() + ": " + e.toString());
            }
            return (String[])v.toArray(new String[0]);
        }

        /**
         * Retreives an array of ACDManagerAddress
         * @return an array of ACDManagerAddress
         * @throws MethodNotSupportedException
         */
        public ACDManagerAddress[] getACDManagerAddress() throws MethodNotSupportedException {
            return monitoredAcdAddress.getACDManagerAddress();
        }

        class RouteThread extends Thread {
            RouteEvent event;
            String[] rSelected;

            RouteThread(RouteEvent ev, String[] selected){
                event = ev;
                rSelected = selected;
            }

            public void run(){
                try{
                    event.getRouteSession().selectRoute(rSelected);
                }
                catch (Exception e){
                    
                }
            }
        }
}
