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
 * Implementation of ciscoroutemonitor
 */

package org.wybecom.talk.jtapi;

import java.util.*;
import javax.telephony.callcenter.*;
import javax.telephony.callcenter.events.*;
import com.cisco.jtapi.extensions.*;
import org.wybecom.talk.jmf.*;
import org.wybecom.talk.team.config.*;
/**
 * Implementation of ciscoroutemonitor (a monitored RouteAddress)
 * @author Yohann BARRE
 */
public class ciscoroutemonitor extends routemonitor implements CiscoTerminalObserver {

    Vector ctimonitors;
    
    /**
     * Initialize a cisco route monitor
     * @param address The JTAPI Route Address
     * @param log The logger
     * @param xmlteam The team associated
     * @param aaddress The JTAPi ACDAddress
     */
    public ciscoroutemonitor(RouteAddress address, trace log, Xmlteam xmlteam, ACDAddress aaddress) {
        super(address, log, xmlteam, aaddress);
        ctimonitors = new Vector();
    }

    @Override public void start() {
        try {
            this.log("Starting RouteAdress: " + this.getAddressName());
            super.start();
            if (this.getTerminal() instanceof CiscoRouteTerminal) {
                if (!((CiscoRouteTerminal)this.getTerminal()).isRegisteredByThisApp()) {
                    this.log("Trying to register " + this.getTerminal().getName() + " Route Point for routing calls");
                    ((CiscoRouteTerminal)this.getTerminal()).register(null, CiscoRouteTerminal.NO_MEDIA_REGISTRATION);
                    this.log("Adding observer to " + this.getTerminal().getName() + " Route Point");
                    ((CiscoRouteTerminal)this.getTerminal()).addObserver(this);

                    //register ctiports
                    for (ACDManagerAddress ama : this.getACDManagerAddress()){
                        if (ama.getTerminals()[0] instanceof CiscoMediaTerminal){
                            rtpmanager manager = new rtpmanager();
                            if (!((CiscoMediaTerminal)ama.getTerminals()[0]).isRegisteredByThisApp()){
                                ciscoctimonitor ccm = new ciscoctimonitor(ama , this.getLogger(), manager);
                                ctimonitors.add(ccm);
                                ccm.start();
                                this.log("Registering " + ama.getName() + " on port "+ String.valueOf(manager.getPort()));
                                CiscoMediaCapability[] cmcs = new CiscoMediaCapability[1];
                                cmcs[0] = new CiscoG711MediaCapability();
                                ((CiscoMediaTerminal)ama.getTerminals()[0]).register(java.net.InetAddress.getLocalHost(), manager.getPort(),cmcs);
                            }
                            else {
                                this.log("ACDManagerAddress " + ama.getName() + " is already registered by this application");
                            }
                        }
                        else {
                            this.log("ACDManagerAddress " + ama.getName() + " is not associated with a CiscoMediaTerminal, it will not be monitored");
                        }
                    }
                }
                else {
                    this.log(this.getTerminal().getName() + " Route Point is already registered with this application");
                }
            }
        }
        catch (Exception e) {
            this.log("exception while starting this ciscoroutemonitor: " + e.getMessage());
        }
    }

    @Override public void stop() {
        try {
            super.stop();
            ((CiscoRouteTerminal)this.getTerminal()).unregister();
        }
        catch (Exception e){
        }
    }

    /**
     * Implementation of JTAPI reRouteEvent
     * @param event
     */
    @Override public void reRouteEvent( ReRouteEvent event) {
        }

    /**
     * Implementation of JTAPI routeCalbackEndedEvent
     * @param event
     */
    @Override public void routeCallbackEndedEvent(RouteCallbackEndedEvent event) {
        }

    /**
     * Implementation of JTAPI routeEndEvent
     * @param event
     */
    @Override public void routeEndEvent(RouteEndEvent event) {
            this.log("Receive route end event: " + event.toString());
        }

    /**
     * Implementation of JTAPI routeEvent
     * @param event
     */
    @Override public void routeEvent(RouteEvent event) {
            this.log("Receive route event: " + event.toString());

            
            String[] destinations = this.getDestinations();
            
            try {
                String dest = "Selected destinations: ";
                for (String s : destinations){
                    dest += s + " ";
                }
                this.log(dest);
                CiscoRouteThread rt = new CiscoRouteThread(event, destinations);
                rt.start();
            }
            catch (Exception selectRouteException) {
                this.log("Unable to selectRoute: " + selectRouteException.toString());
            }
          
            
            /*String[] destinations = new String[2];
            this.log("Original requested route: "+event.getCurrentRouteAddress().toString());
            destinations[0] = event.getCurrentRouteAddress().getName();
            destinations[1] = "8400";

            int[] calledOptions = new int[2];
            calledOptions[0] = CiscoRouteSession.RESET_ORIGINALCALLED;
            calledOptions[1] = CiscoRouteSession.DONOT_RESET_ORIGINALCALLED;
            try {
                ((CiscoRouteSession)event.getRouteSession()).selectRoute(destinations, CiscoRouteSession.ROUTEADDRESS_SEARCH_SPACE , null, null, calledOptions, null, null);
            }
            catch (Exception selectRouteException) {
                this.log("Unable to selectRoute: " + selectRouteException.getMessage());
            }*/
        }

    /**
     *
     * @param event
     */
    @Override public void routeUsedEvent(RouteUsedEvent event) {
            this.log("Receive route used event: " + event.toString());
        }

    class CiscoRouteThread extends Thread {
            RouteEvent event;
            String[] rSelected;

            CiscoRouteThread(RouteEvent ev, String[] selected){
                event = ev;
                rSelected = selected;
            }

            public void run(){
                try{
                    
                    ((CiscoRouteSession)event.getRouteSession()).selectRoute(rSelected, CiscoRouteSession.ROUTEADDRESS_SEARCH_SPACE , null, null, null, null, null);
                }
                catch (Exception e){

                }
            }
        }
        
}
