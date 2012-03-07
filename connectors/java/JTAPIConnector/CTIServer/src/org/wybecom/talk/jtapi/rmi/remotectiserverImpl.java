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


package org.wybecom.talk.jtapi.rmi;


import java.rmi.RemoteException;
import org.wybecom.talk.jtapi.ctiserver;
/**
 * @author Yohann BARRE
 * @deprecated RMI is no longer advised as the JTAPI servlet hosts the ctiserver
 * 
 */
public class remotectiserverImpl implements remotectiserver {
    
    private ctiserver server;
    
    public remotectiserverImpl(ctiserver cs) {
        server = cs;
    }
    
    public remotectiserverImpl() {
    
    }

    public Boolean Forward(String caller, String destination) throws RemoteException {
        return server.Forward(caller, destination);
    }

    public String Call(String caller, String callee) throws RemoteException {
        return server.Call(caller, callee);
    }

    public Boolean UnHook(String callee, String callid) throws RemoteException {
        return server.UnHook(callee, callid);
    }

    public Boolean Divert(String callid, String caller) throws RemoteException {
        return server.Divert(callid, caller);
    }

    public Boolean DoNotDisturb(String caller) throws RemoteException {
        return server.DoNotDisturb(caller);
    }

    public Boolean Hold(String callid, String callee) throws RemoteException {
        return server.Hold(callid, callee);
    }

    public Boolean UnHold(String callid, String callee) throws RemoteException {
        return server.UnHold(callid, callee);
    }

    public Boolean Transfer(String callid, String callee, String destination) throws RemoteException {
        return server.Transfer(callid, callee, destination);
    }
    
    public Boolean Transfer(String caller) throws RemoteException {
        return server.Transfer(caller);
    }
    
    public Boolean ConsultTransfer(String callid, String callee, String destination) throws RemoteException {
        return server.ConsultTransfer(callid, callee, destination);
    }

    public Boolean HangUp(String caller, String callid) throws RemoteException {
        return server.HangUp(caller, callid);
    }

    public Boolean Monitor(String monitorer, String monitored) throws RemoteException {
        return server.Monitor(monitorer, monitored);
    }

}
