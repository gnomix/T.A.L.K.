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
 * Implementation of ciscomonitor
 */
package org.wybecom.talk.jtapi;

import java.util.*;
import javax.telephony.*;
import javax.telephony.events.*;
import com.cisco.jtapi.extensions.*;
import org.wybecom.talkportal.cti.stateserver.*;

/**
 * @author Yohann BARRE
 * \brief Implementation of ciscomonitor @see monitor
 */
public class ciscomonitor extends monitor implements CiscoTerminalObserver {

    private String partition;

    /**
     * Initialize the monitor
     * @param lineStateClient The state server client
     * @param address The JTAPI Address
     * @param log The logger
     */
    public ciscomonitor(Vector lineStateClient, Address address, trace log) {
        super(lineStateClient, address, log);
        this.partition = ((CiscoAddress)address).getPartition();
    }

    /**
     * Initialize the monitor
     * @param address The JTAPI Address
     * @param log The logger
     */
    public ciscomonitor(Address address, trace log){
        super(address, log);
        this.partition = ((CiscoAddress)address).getPartition();
    }

    /**
     * Gets the line partition
     * @return The monitored line partition
     */
    public String getPartition() {
        return this.partition;
    }

    /**
     * Sets the line partition
     * @param p The name of the partition
     */
    public void setPartition(String p) {
        this.partition = p;
    }
    
    @Override public void addressChangedEvent( AddrEv [] events ) {
        super.addressChangedEvent(events);
        for (int i=0; i<events.length; i++ ) {
            switch (events[i].getID()){
                case CiscoAddrInServiceEv.ID:
                    this.setAddressInService(true);
                    this.log("Cisco address in service received for "+ this.getDirNum());
                    this.setMonitoredLineStatus(Status.AVAILABLE);
                    this.setLineControl("CiscoAddrInServiceEv");
                    break;
                case CiscoAddrOutOfServiceEv.ID:
                    this.setAddressInService(false);
                    this.log("Cisco address out of service received for "+ this.getDirNum());
                    this.setMonitoredLineStatus(Status.INACTIVE);
                    this.setLineControl("CiscoAddrOutOfServiceEv");
                    break;
                case CiscoAddrRemovedEv.ID:
                    this.log("Cisco address removed received for "+ this.getDirNum());
                    this.setMonitoredLineStatus(Status.INACTIVE);
                    this.setLineControl("CiscoAddrRemovedEv");
                    this.removeAddressObservers();
                    break;
            }
        }
        
    }
    
    @Override public void callChangedEvent( CallEv [] events ){
        super.callChangedEvent(events);
        for (int i=0; i<events.length; i++ ) {
            switch (events[i].getID()){
                case CiscoCallChangedEv.ID:
                    this.log("Cisco call changed event received for "+ this.getDirNum());
                    this.printConnections();
                    this.setLineControl("CiscoCallChangedEv");
                    break;
                case CiscoCallSecurityStatusChangedEv.ID:
                    this.log("Cisco call security status changed event received for "+ this.getDirNum());
                    this.printConnections();
                    this.setLineControl("CiscoCallSecurityStatusChangedEv");
                    break;
                case CiscoConferenceChainAddedEv.ID:
                    this.log("Cisco conference chain added event received for "+ this.getDirNum());
                    this.printConnections();
                    this.setLineControl("CiscoConferenceChainAddedEv");
                    break;
                case CiscoConferenceChainRemovedEv.ID:
                    this.log("Cisco conference chain removed event received for "+ this.getDirNum());
                    this.printConnections();
                    this.setLineControl("CiscoConferenceChainRemovedEv");
                    break;
                case CiscoConferenceEndEv.ID:
                    this.log("Cisco conference end event received for "+ this.getDirNum());
                    this.printConnections();
                    this.setLineControl("CiscoConferenceEndEv");
                    break;
                case CiscoConferenceStartEv.ID:
                    this.log("Cisco conference start event received for "+ this.getDirNum());
                    this.printConnections();
                    this.setLineControl("CiscoConferenceStartEv");
                    break;
                case CiscoConsultCallActiveEv.ID:
                    this.log("Cisco consult call event received for "+ this.getDirNum());
                    this.printConnections();
                    this.setLineControl("CiscoConsultCallActiveEv");
                    break;
                case CiscoToneChangedEv.ID:
                    this.log("Cisco tone changed event received for "+ this.getDirNum());
                    this.printConnections();
                    this.setLineControl("CiscoToneChangedEv");
                    break;
                case CiscoTransferEndEv.ID:
                    this.log("Cisco transfer end event received for "+ this.getDirNum());
                    this.printConnections();
                    this.setLineControl("CiscoTransferEndEv");
                    break;
                case CiscoTransferStartEv.ID:
                    this.log("Cisco transfer start event received for "+ this.getDirNum());
                    this.printConnections();
                    this.setLineControl("CiscoTransferStartEv");
                    break;
                case CiscoTermConnMonitoringStartEv.ID:
                        this.log("Monitoring starts for " + ((CiscoTermConnMonitoringStartEv)events[i]).getTerminalConnection().getConnection().getAddress().getName());
                        break;
                    case CiscoTermConnMonitoringEndEv.ID:
                        this.log("Monitoring ends for " + ((CiscoTermConnMonitoringEndEv)events[i]).getTerminalConnection().getConnection().getAddress().getName());
                        this.monitored = "";
                        this.setLineControl("CiscoTermConnMonitoringEndEv");
                        break;
                    case CiscoTermConnMonitorInitiatorInfoEv.ID:
                        this.log("Monitoring initiated by: " + ((CiscoTermConnMonitorInitiatorInfoEv)events[i]).getCiscoMonitorInitiatorInfo().getAddress().getName());
                        this.monitored = ((CiscoTermConnMonitorInitiatorInfoEv)events[i]).getCiscoMonitorInitiatorInfo().getAddress().getName();
                        this.setLineControl("CiscoTermConnMonitorInitiatorInfoEv");
                        break;
                    case CiscoTermConnMonitorTargetInfoEv.ID:
                        this.log("Monitoring target info: " + ((CiscoTermConnMonitorTargetInfoEv)events[i]).getCiscoMonitorTargetInfo().getAddress().getName());
                        break;
            }
        }
    }
    
    @Override public void terminalChangedEvent ( TermEv [] events ) {
        super.terminalChangedEvent(events);
        for ( int i=0; i<events.length; i++ ) {
            switch ( events[i].getID () ) {
                case CiscoMediaOpenLogicalChannelEv.ID:
                    break;
                case CiscoRTPInputKeyEv.ID:
                    break;
                case CiscoRTPInputStartedEv.ID:
                    break;
                case CiscoRTPInputStoppedEv.ID:
                    break;
                case CiscoRTPOutputKeyEv.ID:
                    break;
                case CiscoRTPOutputStartedEv.ID:
                    break;
                case CiscoRTPOutputStoppedEv.ID:
                    break;
                case CiscoTermButtonPressedEv.ID:
                    break;
                case CiscoTermDataEv.ID:
                    break;
                case CiscoTermDeviceStateActiveEv.ID:
                    break;
                case CiscoTermDeviceStateAlertingEv.ID:
                    break;
                case CiscoTermDeviceStateHeldEv.ID:
                    break;
                case CiscoTermDeviceStateIdleEv.ID:
                    break;
                case CiscoTermDeviceStateWhisperEv.ID:
                    break;
                case CiscoTermDNDStatusChangedEv.ID:
                    this.log("Cisco terminal dnd status changed received from " + this.getTerminalName() + ": " + ((CiscoTermDNDStatusChangedEv)events[i]).getDNDStatus());
                    try {
                        this.setDnd(((CiscoTermDNDStatusChangedEv)events[i]).getDNDStatus());
                        this.setLineControl("CiscoTermDNDStatusChangedEv");
                    }
                    catch (Exception dndstatus) {
                        this.log("Unable to set or send dnd status: " + dndstatus.getMessage());
                    }
                    break;
                case CiscoTermInServiceEv.ID:
                    this.log("Cisco terminal in service received from " + this.getTerminalName());
                    this.setTerminalInService(true);
                    break;
                case CiscoTermOutOfServiceEv.ID:
                    this.log("Cisco terminal out of service received from " + this.getTerminalName());
                    this.setTerminalInService(false);
                    break;
                case CiscoTermRegistrationFailedEv.ID:
                    break;
                case CiscoTermSnapshotCompletedEv.ID:
                    break;
                case CiscoTermSnapshotEv.ID:
                    break;
            }
        }
    }
    
    @Override public void start() {
        this.log("Starting cisco monitor: " + this.getTerminalName() + ":" + this.getDirNum());
        super.start();
        if (this.getTerminal() instanceof CiscoMediaTerminal)        {
            super.stop();
        }
        else {
            if (this.getTerminal() instanceof CiscoTerminal) {
                try {
                    this.log("Set dnd changed filter to true for: " + this.getTerminalName());
                    CiscoTermEvFilter filter = ((CiscoTerminal)this.getTerminal()).getFilter();
                    filter.setDNDChangedEvFilter(true);
                    ((CiscoTerminal)this.getTerminal()).setFilter(filter);
                    this.setDnd(((CiscoTerminal)this.getTerminal()).getDNDStatus());
                    this.setLineControl("CiscoTermDNDStatusChangedEv");
                }
                catch (Exception dndException) {
                    this.log("Unable to retreive dnd status from CiscoTerminal: " + this.getTerminalName());
                }
            }
            else {
                this.log("This terminal instance is not a CiscoTerminal instance: " + this.getTerminalName());
            }
        }
    }
}
