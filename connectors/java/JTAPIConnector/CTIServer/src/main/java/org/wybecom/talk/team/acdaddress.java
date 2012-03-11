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
public class acdaddress  extends callcenteraddress  implements ACDAddress {

    private ACDManagerAddress[] managerAddresses;
    private Vector agents = new Vector();

    public acdaddress(Address a, ACDManagerAddress[] addresses){
        super(a);
        managerAddresses = addresses;
    }

    public Agent[] getLoggedOnAgents() throws MethodNotSupportedException {
        Vector v = new Vector();
        for (Object a : agents){
            if (((Agent)a).getState() != Agent.LOG_OUT && ((Agent)a).getState() != Agent.UNKNOWN) {
                v.add(a);
            }
        }
        return (Agent[])v.toArray(new Agent[0]);
    }

    public int getNumberQueued() throws MethodNotSupportedException {
        return -1;
    }

    public Call getOldestCallQueued() throws MethodNotSupportedException {
        for (ACDManagerAddress ama : managerAddresses){
            
        }
        return null;
    }

    public int getRelativeQueueLoad() throws MethodNotSupportedException {
        return 0;
    }

    public int getQueueWaitTime() throws MethodNotSupportedException {
        return -1;
    }

    public ACDManagerAddress[] getACDManagerAddress() throws MethodNotSupportedException {
        return managerAddresses;
    }

    public void fireACDAddrEv(ACDAddrEv ev){
        boolean isnew = true;
        for (Object o : agents){
            if (((agent)o).getAgentID().equalsIgnoreCase(ev.getAgent().getAgentID())){
                isnew = false;
                break;
            }
        }
        if (isnew){
            System.out.println("Adding agent " + ev.getAgent().toString() + " to " + this.toString());
            agents.add(ev.getAgent());
        }
        ACDAddrEv[] events = new ACDAddrEv[1];
        events[0] = ev;
        for (AddressObserver ao : this.getObservers()){
            if (ao instanceof ACDAddressObserver){
                ao.addressChangedEvent(events);
            }
        }
    }

    @Override public String toString(){
        return "ACDAddress: " + this.getAddress().toString();
    }
}
