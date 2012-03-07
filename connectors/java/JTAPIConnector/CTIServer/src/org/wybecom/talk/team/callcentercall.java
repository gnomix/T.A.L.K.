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
import javax.telephony.capabilities.*;
import javax.telephony.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class callcentercall implements CallCenterCall {

    private Call call;
    private Object applicationData;

    public callcentercall(Call c){
        call = c;
    }

    public Connection[] connectPredictive(Terminal originatorTerminal, Address originatorAddress, String destination, int connectionState, int maxRings, int treatment, int endpointType) throws ResourceUnavailableException, PrivilegeViolationException, InvalidPartyException, InvalidArgumentException, InvalidStateException, MethodNotSupportedException {
        throw new MethodNotSupportedException();
    }

    public void setApplicationData(Object data) throws ResourceUnavailableException, InvalidArgumentException, InvalidStateException, MethodNotSupportedException {
        applicationData = data;
    }

    public Object getApplicationData() throws MethodNotSupportedException {
        return applicationData;
    }

    public CallCenterTrunk[] getTrunks() throws MethodNotSupportedException {
        return null;
    }

    public Connection[] getConnections() {
        return null;
    }

    public Provider getProvider() {
        return call.getProvider();
    }

    public int getState() {
        return call.getState();
    }

    public Connection[] connect(Terminal origterm, Address origaddr, String dialedDigits) throws ResourceUnavailableException, PrivilegeViolationException, InvalidPartyException, InvalidArgumentException, InvalidStateException, MethodNotSupportedException {
        return call.connect(origterm, origaddr, dialedDigits);
    }

    public void addObserver(CallObserver observer) throws ResourceUnavailableException, MethodNotSupportedException {
        call.addObserver(observer);
    }

    public CallObserver[] getObservers() {
        return call.getObservers();
    }

    public void removeObserver(CallObserver observer) {
        call.removeObserver(observer);
    }

    public CallCapabilities getCapabilities(Terminal terminal, Address address) throws InvalidArgumentException {
        return call.getCapabilities(terminal, address);
    }

    public CallCapabilities getCallCapabilities(Terminal term, Address addr) throws InvalidArgumentException, PlatformException {
        return call.getCallCapabilities(term, addr);
    }

}
