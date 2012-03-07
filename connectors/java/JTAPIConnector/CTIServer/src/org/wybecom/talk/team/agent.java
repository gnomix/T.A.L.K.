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

package org.wybecom.talk.team;

import javax.telephony.callcontrol.CallControlForwarding;
import javax.telephony.callcontrol.CallControlAddress;
import javax.telephony.callcontrol.CallControlConnection;
import javax.telephony.callcontrol.CallControlTerminalConnection;
import javax.telephony.callcenter.*;
import javax.telephony.callcenter.events.*;
import javax.telephony.*;
import javax.telephony.events.*;
import org.wybecom.talk.jtapi.stateclient;
import java.util.*;
import org.wybecom.talkportal.cti.stateserver.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class agent implements Agent, AgentTerminalObserver, AddressObserver, CallObserver {

    private int agentState = Agent.UNKNOWN;
    private String agentID;
    private ACDAddress acdAddress;
    private Address agentAddress;
    private AgentTerminal agentTerminal;
    private Vector stateclients;
    private LineControl monitoredLineControl;
    private Boolean dnd;
    private Boolean mwi;
    private CallControlForwarding[] forward;
    private Connection[] connections;

    public agent(Address agAddress, ACDAddress address, AgentTerminal term, String id, int state, Vector stateclt) throws InvalidArgumentException, InvalidStateException{
        agentID = id;
        acdAddress = address;
        agentAddress = agAddress;
        agentTerminal = term;
        stateclients = stateclt;
        dnd = false;
        mwi = false;
        forward = null;

        this.monitoredLineControl = new LineControl();
        this.monitoredLineControl.setDirectoryNumber(this.getAgentAddress().getName());
        this.monitoredLineControl.setLineControlConnection(new ArrayOfLineControlConnection());
        this.monitoredLineControl.setStatus(Status.UNKNOWN);
        this.monitoredLineControl.setDoNotDisturb(dnd);
        this.monitoredLineControl.setForward(GetForward());
        this.monitoredLineControl.setMwiOn(mwi);
        if (agentAddress instanceof CallControlAddress) {
                    try {
                        dnd = ((CallControlAddress)agentAddress).getDoNotDisturb();
                        System.out.println(agentAddress.getName() + " dnd is " + dnd.toString());
                    }
                    catch (Exception dndException) {
                        /*this.logger.printerror("Unable to retreive do not disturb status from "+this.getDirNum()+": "+ dndException.toString());*/
                    }
                    try {
                        mwi = ((CallControlAddress)agentAddress).getMessageWaiting();
                        System.out.println(agentAddress.getName() + " mwi is " + mwi.toString());
                    }
                    catch (Exception mwiException) {
                        /*this.logger.printerror("Unable to retreive mwi status from "+this.getDirNum()+": "+ mwiException.toString());*/
                    }
                    try {
                        forward = ((CallControlAddress)agentAddress).getForwarding();
                        if (forward != null) {
                            for (CallControlForwarding ccf : forward) {
                                System.out.println("Forwarding instruction for " + agentAddress.getName() + ": " + ccf.toString());
                            }
                        }
                        else {
                            System.out.println("No forwarding instruction for " + agentAddress.getName());
                        }
                    }
                    catch (Exception forwardException) {
                        /*this.logger.printerror("Unable to retreive forward status from "+this.getDirNum()+": "+ forwardException.toString());*/
                    }
                    try {
                        this.connections = this.agentAddress.getConnections();
                        if (connections != null) {
                            for (Connection c : this.connections){
                                System.out.println("Connection for "+agentAddress.getName());
                            }
                        }
                        else {
                            System.out.println("No connections associated with " + agentAddress.getName());
                        }
                    }
                    catch (Exception connectionException) {
                        System.out.println("Unable to retrieve connections for " + agentAddress.getName() + " " + connectionException.toString());
                    }

                }
        this.setAgentLineControl("ACDAddrLoggedOnEv");
        try {
            agentAddress.addCallObserver(this);
            agentAddress.addObserver(this);
            agentTerminal.addObserver(this);
        }
        catch (Exception observerException){
            
        }
        this.setState(state);
    }

    public void setState(int state) throws InvalidArgumentException, InvalidStateException {
        if (acdAddress instanceof acdaddress){
            ((acdaddress)acdAddress).fireACDAddrEv(this.getEvent(state));
        }
        /*if (agentTerminal instanceof agentterminal){
            ((agentterminal)agentTerminal).fireAgentTermEv(this.getTermEvent(state));
        }*/
        agentState = state;
    }

    public int getState() {
        return agentState;
    }

    public String getAgentID() {
        return agentID;
    }

    public ACDAddress getACDAddress() {
        return acdAddress;
    }

    public Address getAgentAddress() {
        return agentAddress;
    }

    public AgentTerminal getAgentTerminal(){
        return agentTerminal;
    }

    private ACDAddrEv getEvent(int state){
        ACDAddrEv ev = null;
        switch (state){
            case Agent.BUSY:
                ev = new acdaddrbusyev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, ACDAddrBusyEv.ID, acdAddress, Ev.CAUSE_NORMAL, this, agentTerminal, agentAddress, state);
                break;
            case Agent.LOG_IN:
                ev = new acdaddrloggedonev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, ACDAddrLoggedOnEv.ID, acdAddress, Ev.CAUSE_NORMAL, this, agentTerminal, agentAddress, state);
                break;
            case Agent.LOG_OUT:
                ev = new acdaddrloggedoffev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, ACDAddrLoggedOffEv.ID, acdAddress, Ev.CAUSE_NORMAL, this, agentTerminal, agentAddress, state);
                break;
            case Agent.NOT_READY:
                ev = new acdaddrnotreadyev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, ACDAddrNotReadyEv.ID, acdAddress, Ev.CAUSE_NORMAL, this, agentTerminal, agentAddress, state);
                break;
            case Agent.READY:
                ev = new acdaddrreadyev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, ACDAddrReadyEv.ID, acdAddress, Ev.CAUSE_NORMAL, this, agentTerminal, agentAddress, state);
                break;
            case Agent.UNKNOWN:
                ev = new acdaddrunknownev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, ACDAddrUnknownEv.ID, acdAddress, Ev.CAUSE_NORMAL, this, agentTerminal, agentAddress, state);
                break;
            case Agent.WORK_NOT_READY:
                ev = new acdaddrworknotreadyev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, ACDAddrWorkNotReadyEv.ID, acdAddress, Ev.CAUSE_NORMAL, this, agentTerminal, agentAddress, state);
                break;
            case Agent.WORK_READY:
                ev = new acdaddrworkreadyev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, ACDAddrWorkReadyEv.ID, acdAddress, Ev.CAUSE_NORMAL, this, agentTerminal, agentAddress, state);
                break;
        }
        return ev;
    }

    private AgentTermEv getTermEvent(int state){
        AgentTermEv ev = null;
        switch (state){
            case Agent.BUSY:
                ev = new agenttermbusyev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, AgentTermBusyEv.ID, agentTerminal, Ev.CAUSE_NORMAL, this, acdAddress, this.agentID, state, agentAddress);
                break;
            case Agent.LOG_IN:
                ev = new agenttermloggedonev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, AgentTermBusyEv.ID, agentTerminal, Ev.CAUSE_NORMAL, this, acdAddress, this.agentID, state, agentAddress);
                break;
            case Agent.LOG_OUT:
                ev = new agenttermloggedoffev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, AgentTermBusyEv.ID, agentTerminal, Ev.CAUSE_NORMAL, this, acdAddress, this.agentID, state, agentAddress);
                break;
            case Agent.NOT_READY:
                ev = new agenttermnotreadyev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, AgentTermBusyEv.ID, agentTerminal, Ev.CAUSE_NORMAL, this, acdAddress, this.agentID, state, agentAddress);
                break;
            case Agent.READY:
                ev = new agenttermreadyev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, AgentTermBusyEv.ID, agentTerminal, Ev.CAUSE_NORMAL, this, acdAddress, this.agentID, state, agentAddress);
                break;
            case Agent.UNKNOWN:
                ev = new agenttermunknownev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, AgentTermBusyEv.ID, agentTerminal, Ev.CAUSE_NORMAL, this, acdAddress, this.agentID, state, agentAddress);
                break;
            case Agent.WORK_NOT_READY:
                ev = new agenttermworknotreadyev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, AgentTermBusyEv.ID, agentTerminal, Ev.CAUSE_NORMAL, this, acdAddress, this.agentID, state, agentAddress);
                break;
            case Agent.WORK_READY:
                ev = new agenttermworkreadyev(Ev.CAUSE_NORMAL , Ev.META_UNKNOWN, true, AgentTermBusyEv.ID, agentTerminal, Ev.CAUSE_NORMAL, this, acdAddress, this.agentID, state, agentAddress);
                break;
        }
        return ev;
    }

    public void terminalChangedEvent(TermEv[] events) {
        for ( int i=0; i<events.length; i++ ) {
        }
    }

    public void callChangedEvent(CallEv[] events){
        for ( int i=0; i<events.length; i++ ) {
            try {
                switch (events[i].getID()){
                    case CallObservationEndedEv.ID:
                        this.setState(Agent.READY);
                        break;
                    case TermConnRingingEv.ID:
                        if (((TermConnRingingEv)events[i]).getTerminalConnection().getTerminal().getName().equalsIgnoreCase(agentTerminal.getName())){
                            this.setState(Agent.BUSY);
                        }
                        break;
                    case TermConnActiveEv.ID:
                        this.setState(Agent.NOT_READY);
                        break;
                }
            }
            catch (Exception e){
                
            }
        }
    }

    public void addressChangedEvent(AddrEv[] events){
        for ( int i=0; i<events.length; i++ ) {
        }
    }

    @Override public String toString(){
        return "Agent " +agentID+ " acting for " + acdAddress.getName() + " with " + agentAddress.getName() + ", " + agentTerminal.getName();
    }

    /***
         * Envoie de l'état de l'agent
         * @param ev Evènement JTAPI source de l'envoi
         */
        public void setAgentLineControl(String ev){
            try {
                Enumeration en = stateclients.elements();
                while (en.hasMoreElements()) {
                    stateclient sc = (stateclient)en.nextElement();
                    sc.setAgentLineControl(this.monitoredLineControl.getDirectoryNumber(), this.getAgentID(), this.getAgentState(this.agentState), null, ev);
                }
            }
            catch (Exception e) {
                /*this.logger.printerror("Unable to set line control from " + this.getDirNum() + ": "+ e.toString());*/
            }
        }

        public LineControl getAgentLineControl() {

            try {
                this.monitoredLineControl.setDoNotDisturb(dnd);
                this.monitoredLineControl.setForward(GetForward());
                this.monitoredLineControl.setMwiOn(mwi);
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
                System.out.println("Unable to construct AgentLineControl from " + this.toString() + ": " + e.toString());
                return this.monitoredLineControl;
            }
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

        /**
         * \brief Représence l'identificateur de l'appel
         * @param call Appel
         * @return Identificateur de l'appel
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
                System.out.println("Getting connection state from " + c.toString());
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
                System.out.println("Unable to retreive connection state from " + c.toString());
                return cs;
            }
        }

        private TerminalState getTerminalState(Connection c) {
            TerminalState ts = TerminalState.UNKNOWN;
            try {
                TerminalConnection[] tcs = c.getTerminalConnections();
                if (tcs != null && tcs.length >= 1) {
                    if (tcs[0] instanceof CallControlTerminalConnection) {
                        System.out.println("Terminal connection is " + ((CallControlTerminalConnection)tcs[0]).getCallControlState());
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

        private AgentState getAgentState(int state){
            AgentState as = AgentState.UNKNOWN;
            switch (state){
                case Agent.BUSY:
                    as = AgentState.BUSY;
                    break;
                case Agent.LOG_IN:
                    as = AgentState.LOG_IN;
                    break;
                case Agent.LOG_OUT:
                    as = AgentState.LOG_OUT;
                    break;
                case Agent.NOT_READY:
                    as = AgentState.NOT_READY;
                    break;
                case Agent.READY:
                    as = AgentState.READY;
                    break;
                case Agent.UNKNOWN:
                    as = AgentState.UNKNOWN;
                    break;
                case Agent.WORK_NOT_READY:
                    as = AgentState.WORK_NOT_READY;
                    break;
                case Agent.WORK_READY:
                    as = AgentState.WORK_READY;
                    break;
            }
            return as;
        }
}
