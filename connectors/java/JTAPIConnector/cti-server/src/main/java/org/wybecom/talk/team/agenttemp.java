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

import javax.telephony.*;
import java.util.*;
import javax.swing.event.EventListenerList;
import org.wybecom.talk.jtapi.*;
import javax.telephony.events.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class agenttemp extends monitor{

    static final int AGENT_STATE_LOGOUT = 0;
    static final int AGENT_STATE_NOTREADY = 1;
    static final int AGENT_STATE_READY = 2;
    static final int AGENT_STATE_RESERVED = 3;
    static final int AGENT_STATE_TALKING = 4;
    static final int AGENT_STATE_WORK = 5;
    private int agentState;
    private Boolean logged;
    private final EventListenerList listeners = new EventListenerList();

    public agenttemp(Vector lineStateClient, Address address, trace log) {
        super(lineStateClient, address, log);
    }

    public int getAgentState(){
        return agentState;
    }
    
    public Boolean isLoggedIn(){
        return logged;
    }
    
    public void setAgentState(int newState){
        int oldState = agentState;
        agentState = newState;
        fireAgentStateChanged(oldState, agentState);
    }
    
    public void Login(){
        logged = true;
        agentState = AGENT_STATE_NOTREADY;
    }

    public void Logoff(){
        logged = false;
        agentState = AGENT_STATE_LOGOUT;
    }

    public void addAgentStateListener(agentStateListener listener){
        listeners.add(agentStateListener.class, listener);
    }

    public void removeAgentStateListener(agentStateListener listener){
        listeners.remove(agentStateListener.class, listener);
    }

    public agentStateListener[] getAgentStateListener(){
        return listeners.getListeners(agentStateListener.class);
    }

    protected void fireAgentStateChanged(int previousState, int newState){
        for (agentStateListener asl : getAgentStateListener()){
            asl.agentStateChanged(previousState, newState);
        }
    }

    @Override public void setLineControl(String Ev){

    }

    @Override public void addressChangedEvent ( AddrEv [] events ) {
        super.addressChangedEvent(events);
    }

    @Override public void callChangedEvent ( CallEv [] events ) {
        super.callChangedEvent(events);
    }

    @Override public void terminalChangedEvent ( TermEv [] events ) {
        this.log(String.valueOf(events.length) + " terminal changed events received.");
        for ( int i=0; i<events.length; i++ ) {
                switch ( events[i].getID () ) {
                    case TermObservationEndedEv.ID:
                        this.log("Terminal observation ended from " + this.getTerminalName() + ", disconnecting agent...");
                        Logoff();
                        break;
                }
            }
    }
    public interface agentStateListener extends EventListener {
        void agentStateChanged(int previousState, int newState);
    }


}
