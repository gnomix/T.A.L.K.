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
 * Implementation of ctimonitor
 */

package org.wybecom.talk.jtapi;

import javax.telephony.*;
import javax.telephony.events.*;
/**
 * Implementation of ctimonitor
 * @author Yohann BARRE
 */
public class ctimonitor extends monitor{

    /**
     * Initializethe ctimonitor
     * @param a The JTAPI Address
     * @param log The logger
     */
    public ctimonitor(Address a, trace log){
        super(a,log);
    }

    @Override public void terminalChangedEvent ( TermEv [] events ){

    }

    @Override public void addressChangedEvent ( AddrEv [] events ){

    }

    @Override public void callChangedEvent ( CallEv [] events ){
        
    }

    @Override public void start(){
        try {
            this.log("Starting cti monitor: " + this.getTerminalName() + ":" + this.getDirNum());
            this.addObservers();
        }
        catch (Exception e) {
            this.log("exception while starting this cti monitor: " + e.toString());
        }
    }
}
