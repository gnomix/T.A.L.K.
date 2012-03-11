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
import javax.telephony.capabilities.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class terminal implements Terminal {

    private Terminal t;

    public terminal(Terminal term){
        t = term;
    }

    public String getName() {
        return t.getName();
    }

    public Provider getProvider() {
        return t.getProvider();
    }

    public Address[] getAddresses() {
        return t.getAddresses();
    }

    public TerminalConnection[] getTerminalConnections() {
        return t.getTerminalConnections();
    }

    public void addObserver(TerminalObserver observer) throws ResourceUnavailableException, MethodNotSupportedException {
        t.addObserver(observer);
    }

    public TerminalObserver[] getObservers() {
        return t.getObservers();
    }


    public void removeObserver(TerminalObserver observer) {
        t.removeObserver(observer);
    }

    public void addCallObserver(CallObserver observer) throws ResourceUnavailableException, MethodNotSupportedException {
        t.addCallObserver(observer);
    }

    public CallObserver[] getCallObservers() {
        return t.getCallObservers();
    }

    public void removeCallObserver(CallObserver observer) {
        t.removeCallObserver(observer);
    }

    public TerminalCapabilities getCapabilities() {
        return t.getCapabilities();
    }

    public TerminalCapabilities getTerminalCapabilities(Terminal terminal, Address address) throws InvalidArgumentException, PlatformException  {
        return t.getTerminalCapabilities(terminal, address);
    }
}
