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

import javax.telephony.callcenter.*;
import javax.telephony.callcenter.events.*;
import javax.telephony.*;
import java.util.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class agentterminal extends terminal implements AgentTerminal{

    private Vector agents = new Vector();
    private Vector stateclients;

    public agentterminal(Terminal term, Vector stateclt){
        super(term);
        stateclients = stateclt;
    }

    public Agent addAgent(Address agentAddress, ACDAddress acdAddress, int initialState, String agentID, String password) throws InvalidArgumentException, InvalidStateException, ResourceUnavailableException {
        boolean isnew = true;
        for(Object o: agents){
            if (((agent)o).getAgentID().equalsIgnoreCase(agentID) && ((agent)o).getACDAddress().getName().equalsIgnoreCase(acdAddress.getName())){
                isnew = false;
            }
        }
        if (isnew){
            agent a = new agent(agentAddress, acdAddress, this, agentID, initialState, stateclients);
            agents.add(a);
            return a;
        }
        else{
            return null;
        }
    }

    public void removeAgent(Agent agent) throws InvalidArgumentException, InvalidStateException {
        agents.remove(agent);
    }

    public Agent[] getAgents() {
        return (Agent[])agents.toArray(new Agent[0]);
    }

    public void setAgents(Agent agents[]) throws MethodNotSupportedException {
        throw new MethodNotSupportedException("Not available");
    }

    public void fireAgentTermEv(AgentTermEv ev){
        AgentTermEv[] events = new AgentTermEv[1];
        events[0] = ev;
        for (TerminalObserver to : this.getObservers()){
            if (to instanceof AgentTerminalObserver){
                to.terminalChangedEvent(events);
            }
        }
    }

}
