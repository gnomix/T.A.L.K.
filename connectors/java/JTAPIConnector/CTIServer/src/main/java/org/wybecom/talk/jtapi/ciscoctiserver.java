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
 * Implementation of ciscoctiserver
 */
package org.wybecom.talk.jtapi;

import javax.telephony.*;
import javax.telephony.events.*;
import com.cisco.jtapi.extensions.*;
import java.util.*;
import org.wybecom.talkportal.cti.stateserver.*;

/**
 * \brief Implementation of Cisco JTAPI server
 * 
 * @see ctiserver
 *
 */
public class ciscoctiserver extends ctiserver {

    private Vector ctiport;
    private String partitions = "";
    
    /**
     *
     */
    public ciscoctiserver() {
        super();
    }

    /**
     * Initialize the cisco cti server
     * @param ctiConfigPath The path where cti settings are stored
     * @param stateServerConfig The path where state server settings are stored
     */
    public ciscoctiserver(String ctiConfigPath, String stateServerConfig) {
        super(ctiConfigPath, stateServerConfig);
    }

    /**
     * Initialize the cisco cti server
     * @param contexts All the filtered partitions
     */
    public ciscoctiserver(String contexts) {
        super();
        this.partitions = contexts;
    }

    /**
     * Initialize the cisco cti server
     * @param contexts All the filtered partitions
     * @param ctiConfigPath The path where cti settings are stored
     * @param stateServerConfig The path where state server settings are stored
     */
    public ciscoctiserver(String contexts, String ctiConfigPath, String stateServerConfig) {
        super(ctiConfigPath, stateServerConfig);
        this.partitions = contexts;
    }


    @Override public void start() {
        super.start();
        /*this.println("Retreiving cisco CTI Ports");
        Enumeration provs = super.getProviders().elements();
        while (provs.hasMoreElements()) {
            try {
                Terminal[] terms = ((ctiprovider)provs.nextElement()).getProvider().getTerminals();
                for (Terminal t : terms) {
                    if ( t instanceof CiscoMediaTerminal) {

                    }
                }
            }
            catch (Exception resourceException) {
                this.printerror("Unable to start CTI ports: " + resourceException.getMessage());
            }
        }*/
    }
    
    @Override public void providerChangedEvent ( ProvEv [] events ) {
        super.providerChangedEvent(events);
        String addr ="";
        monitor mon = null;
        for (int i=0; i<events.length; i++ ) {
            switch (events[i].getID()) {
                case CiscoTermCreatedEv.ID:
                    //Adding Terminal
                    this.println("Ajout d'un nouveau terminal " + ((CiscoTermCreatedEv)events[i]).getTerminal().getName());
                    Address[] dns = ((CiscoTermCreatedEv)events[i]).getTerminal().getAddresses();
                    for(Address a : dns) {
                        if (this.IsMonitored((CiscoAddress)a)) {
                            ciscomonitor m = new ciscomonitor(super.getStateClients(),a,(trace)this);
                            super.addMonitor(m);
                            m.start();
                        }
                    }
                    break;
                case CiscoTermRemovedEv.ID:
                    this.println("Suppression d'un terminal " + ((CiscoTermRemovedEv)events[i]).getTerminal().getName());
                    Address[] dnstoremove = ((CiscoTermRemovedEv)events[i]).getTerminal().getAddresses();
                    for(Address atoremove : dnstoremove) {
                        monitor mtoremove = super.getMonitorFromDirNum(atoremove.getName());
                        if (mtoremove != null) {
                            mtoremove.stop();
                        }
                    }
                    break;
                case CiscoAddrActivatedEv.ID:
                    break;
                case CiscoAddrActivatedOnTerminalEv.ID:
                    break;
                case CiscoAddrAddedToTerminalEv.ID:
                    break;
                case CiscoAddrCreatedEv.ID:
                    addr = ((CiscoAddrCreatedEv)events[i]).getAddress().getName();
                    this.println("Cisco address created received for "+ addr);
                    if (this.IsMonitored((CiscoAddress)((CiscoAddrCreatedEv)events[i]).getAddress())) {
                        mon = this.getMonitorFromDirNum(addr);
                        if (mon != null) {
                            mon.setAddress(((CiscoAddrCreatedEv)events[i]).getAddress());
                            mon.setMonitoredLineStatus(Status.AVAILABLE);
                            mon.setLineControl("CiscoAddrCreatedEv");

                            if (mon.getTerminal().getName().equals(((CiscoAddrCreatedEv)events[i]).getAddress().getTerminals()[0].getName())){
                                this.println(addr + " was monitored on the same device...");
                                mon.addAddressObservers();
                            }
                            else {
                                this.println(addr + " was not monitored on this device...");
                                mon.setMonitoredTerminal(((CiscoAddrCreatedEv)events[i]).getAddress().getTerminals()[0]);
                                mon.addObservers();
                            }
                        }
                        else {
                            ciscomonitor m = new ciscomonitor(super.getStateClients(),((CiscoAddrCreatedEv)events[i]).getAddress(),(trace)this);
                            super.addMonitor(m);
                            m.start();
                        }
                    }
                    break;
                case CiscoAddrRemovedEv.ID:
                    addr = ((CiscoAddrRemovedEv)events[i]).getAddress().getName();
                    this.println("Cisco address removed received for "+ addr);
                    mon = this.getMonitorFromDirNum(addr);
                    
                    mon.setMonitoredLineStatus(Status.INACTIVE);
                    mon.setLineControl("CiscoAddrRemovedEv");
                    mon.removeAddressObservers();
                    break;
                case CiscoAddrRemovedFromTerminalEv.ID:
                    break;
                case CiscoAddrRestrictedEv.ID:
                    break;
                case CiscoAddrRestrictedOnTerminalEv.ID:
                    break;
                case CiscoProvCallParkEv.ID:
                    break;
                case CiscoRestrictedEv.ID:
                    break;
                case CiscoTermActivatedEv.ID:
                    break;
                case CiscoTermRestrictedEv.ID:
                    break;
            }
        }
    }
    
    @Override public void initMonitors() {
        try {
            for(Object prov : this.getProviders()) {
                if (prov instanceof ctiprovider) {
                    Address[] ads = ((ctiprovider)prov).getProvider().getAddresses();
                    this.println(String.valueOf(ads.length) + " addresses finded from provider: " + ((ctiprovider)prov).getProviderString());
                    for(Address a : ads) {
                        /*if (a instanceof RouteAddress && this.getXmlTeam(a.getName()) != null) {

                            this.println("Adding RouteAddress " + a.getName());
                            this.addRouteMonitor(new ciscoroutemonitor((RouteAddress)a,(trace)this, this.getXmlTeam(a.getName())));
                        }
                        else {*/
                            if (this.IsMonitored((CiscoAddress)a)) {
                                    this.println("Adding address " + a.getName());
                                    super.addMonitor(new ciscomonitor(this.getStateClients(),a,(trace)this));
                            }
                            else {
                                this.println("This address will not be monitored, partition is not recognized:" + a.getName() + " " + ((CiscoAddress)a).getPartition());
                            }
                        /*}*/
                    }
                }
            }
            this.println("Starting monitors...");
            Enumeration mons = this.getMonitors().elements();
            while (mons.hasMoreElements()) {
                ciscomonitor mon = (ciscomonitor)mons.nextElement();
                mon.start();
            }

            /*
             this.println("Starting routemonitors...");
            Enumeration routemons = this.getRouteMonitors().elements();
            while (routemons.hasMoreElements()) {
                ciscoroutemonitor routemon = (ciscoroutemonitor)routemons.nextElement();
                routemon.start();
            }*/
        }
        catch (Exception e) {
            this.printerror("Unable to initialize monitors: " + e.getMessage());
        }
    }

    private Boolean IsMonitored(CiscoAddress a) {
        Boolean b = false;
        if (this.partitions.length() > 0) {
            if (this.partitions.indexOf(((CiscoAddress)a).getPartition())>= 0) {
                b = true;
            }
        }
        else {
            b = true;
        }
        return b;
    }
    
    @Override public Boolean DoNotDisturb(String caller) {
        Boolean result = false;
        ciscomonitor m = (ciscomonitor)this.getMonitorFromDirNum(caller);
        if (m != null){
            try {
                if (m.getTerminal() instanceof CiscoTerminal) {

                    ((CiscoTerminal)m.getTerminal()).setDNDStatus(!m.getDnd());
                }
            }
            catch (Exception dndException) {
                this.printerror("Failed to dnd from: " + caller + " " + dndException.toString());
            }
        }
        return result;
    }

    @Override public Boolean Monitor(String monitorer, String monitored)
    {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(monitorer);
        Address targetAddress = null;
        TerminalConnection targetTerminalConnection = null;
        if (m != null)
        {
            //get target Address
            try
            {
                this.println("Looking for Address: " + monitored);
                Enumeration provs = super.getProviders().elements();
                while (provs.hasMoreElements()) {
                    targetAddress = ((ctiprovider)provs.nextElement()).getProvider().getAddress(monitored);
                    if (targetAddress != null){
                        break;
                    }
                }
            }
            catch (Exception e){
                this.printerror("Unable to get target Address " + monitored + e.getMessage());
            }
            try {
                if (targetAddress != null){

                    CiscoCall monitorCall = (CiscoCall)m.createCall();
                    Terminal[] ts = targetAddress.getTerminals();
                    this.println("Found " + ts.length + " terminals for " + monitored);
                    for (Terminal t : ts) {
                        TerminalConnection[] tcs = t.getTerminalConnections();
                        this.println("Found " + tcs.length + " terminal connections for " + monitored + " associated with " + t.getName());
                        for (TerminalConnection tc : tcs){

                            this.println("TerminalConnection for " + monitored + tc.toString());
                            if (tc.getState() == tc.ACTIVE){
                                targetTerminalConnection = tc;
                            }
                        }
                    }
                    this.println("Starting monitor: " + m.getTerminal().getName() + ", " + m.getAddress().getName() + ", " + targetTerminalConnection.toString() + ", " + monitored );
                    Connection[] cs = monitorCall.startMonitor(m.getTerminal(), m.getAddress(), targetTerminalConnection, CiscoCall.SILENT_MONITOR, CiscoCall.PLAYTONE_NOLOCAL_OR_REMOTE);
                    if (cs != null){
                        result = true;
                    }
                }
                else {
                    this.println("Target Address for: " + monitored + " is null");
                }
            }
            catch (Exception ex){
                this.printerror("Unable to start session " + monitored + " " + ex.toString());
            }
        }
        return result;
    }
}
