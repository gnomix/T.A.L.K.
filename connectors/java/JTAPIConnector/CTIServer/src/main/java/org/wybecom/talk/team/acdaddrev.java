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

import javax.telephony.callcenter.events.*;
import javax.telephony.callcenter.*;
import javax.telephony.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class acdaddrev extends addrev implements ACDAddrEv {

    private int callCenterCause;
    private Agent agent;
    private AgentTerminal term;
    private Address agentAddress;
    private int state;

    public acdaddrev(int Cause, int MetaCode, boolean isNew, int eventId, Address sourceAddress, int callCause, Agent ag, AgentTerminal t, Address agAddress, int State){
        super(Cause, MetaCode, isNew, eventId, sourceAddress);
        callCenterCause = callCause;
        agent = ag;
        term = t;
        agentAddress = agAddress;
        state = State;
    }

    public int getCallCenterCause(){
        return callCenterCause;
    }


    public Agent getAgent(){
        return agent;
    }

    public AgentTerminal getAgentTerminal(){
        return term;
    }

    public int getState(){
        return state;
    }

    public Address getAgentAddress(){
        return agentAddress;
    }

    public CallCenterTrunk[] getTrunks(){
        return null;
    }

}
