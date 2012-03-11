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
import javax.telephony.*;
import javax.telephony.capabilities.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class callcenteraddress implements CallCenterAddress {

    private Address address;

    public callcenteraddress(Address a){
        address = a;
    }

    public Address getAddress(){
        return address;
    }

    public void addCallObserver(CallObserver observer, boolean remain) throws ResourceUnavailableException, PrivilegeViolationException, MethodNotSupportedException {
        address.addCallObserver(observer);
    }

    public String getName() {
        return address.getName();
    }

    public Provider getProvider() {
        return address.getProvider();
    }

    public Terminal[] getTerminals() {
        return address.getTerminals();
    }

    public Connection[] getConnections() {
        return address.getConnections();
    }

    public void addObserver(AddressObserver observer) throws ResourceUnavailableException, MethodNotSupportedException {
        address.addObserver(observer);
    }

    public AddressObserver[] getObservers() {
        return address.getObservers();
    }

    public void removeObserver(AddressObserver observer) {
        address.removeObserver(observer);
    }

    public void addCallObserver(CallObserver observer) throws ResourceUnavailableException, MethodNotSupportedException {
        address.addCallObserver(observer);
    }

    public CallObserver[] getCallObservers() {
        return address.getCallObservers();
    }

    public void removeCallObserver(CallObserver observer) {
        address.removeCallObserver(observer);
    }

    public AddressCapabilities getCapabilities() {
        return address.getCapabilities();
    }

    public AddressCapabilities getAddressCapabilities(Terminal terminal) throws InvalidArgumentException, PlatformException {
        return address.getAddressCapabilities(terminal);
    }
}
