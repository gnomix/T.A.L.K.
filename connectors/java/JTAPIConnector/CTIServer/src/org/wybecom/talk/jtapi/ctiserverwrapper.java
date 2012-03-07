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

package org.wybecom.talk.jtapi;


import org.tanukisoftware.wrapper.*;
import java.rmi.RemoteException;
import java.rmi.AlreadyBoundException;
import java.rmi.registry.Registry;
import java.rmi.registry.LocateRegistry;
import java.rmi.server.UnicastRemoteObject;
import org.wybecom.talk.jtapi.rmi.remotectiserver;
import org.wybecom.talk.jtapi.rmi.remotectiserverImpl;
import org.wybecom.talk.jtapi.rmi.remoteteamserverImpl;
import org.wybecom.talk.jtapi.rmi.remoteteamserver;
import org.wybecom.talk.team.ciscoteamserver;
import org.wybecom.talk.team.teamserver;
/**
 * @deprecated It is more efficient to runs a ctiserver inside the JTAPI servlet or as a standalone application
 * 
 */
public class ctiserverwrapper implements WrapperListener{

    private ctiserver server;
    private teamserver tserver;
    
    
    public ctiserverwrapper() {
    
    }
    
    public Integer start(String[] arg0) {
        if (arg0.length < 1) { 
            server = new ciscoctiserver();
            server.start();
        }
        else {
            switch (CTIServerType.valueOf(arg0[0])){
                case cisco:
                    if (arg0.length == 2) {
                        server = new ciscoctiserver(arg0[1]);
                        server.println("Starting cisco cti server filtering partition: " + arg0[1]);
                        server.start();
                    }
                    else {
                        server = new ciscoctiserver();
                        server.println("Starting cisco cti server...");
                        server.start();
                    }
                    break;
                case team:
                    tserver = new teamserver();
                    System.out.println("Starting team server");
                    tserver.start();
                    break;
                case ciscoteam:
                    tserver = new ciscoteamserver();
                    System.out.println("Starting ciscoteam server");
                    tserver.start();
                    break;
                case defaultimplementation:
                    server = new ctiserver();
                    server.println("Starting default cti server...");
                    server.start();
                    break;
            }
        }
        if (server != null){
       remotectiserverImpl ctiserver = new remotectiserverImpl(server);
       //bind remote object's stub in the registry
       try
       {
            server.println("Binding stub...");
            remotectiserver stub = (remotectiserver)UnicastRemoteObject.exportObject(ctiserver);
            server.println("Stub successfully created...");
            Registry registry = LocateRegistry.getRegistry();
            server.println("Try to binding...");
            registry.bind("TalkCTIServer", stub);
            server.println("Bounded, registry OK");
       }
       catch (RemoteException re)
       {
           server.println("Unable to export stub or binding stub: " + re.toString());
       }
       catch (AlreadyBoundException abe)
       {
           server.println("Stub is already bounded: " + abe.toString());
       }
        }
        if (tserver != null){
       
       //bind remote object's stub in the registry
       try
       {
            remoteteamserverImpl ctiserver = new remoteteamserverImpl(tserver);
            System.out.println("Binding team stub...");
            remoteteamserver stub = (remoteteamserver)UnicastRemoteObject.exportObject(ctiserver);
            System.out.println("Team stub successfully created...");
            Registry registry = LocateRegistry.getRegistry();
            System.out.println("Try to binding...");
            registry.bind("TalkTeamServer", stub);
            System.out.println("Bounded, registry OK");
       }
       catch (RemoteException re)
       {
           System.out.println("Unable to export team stub or binding stub: " + re.toString());
       }
       catch (AlreadyBoundException abe)
       {
           System.out.println("Team Stub is already bounded: " + abe.toString());
       }
        }
       return null;
    }

    
    public int stop(int arg0) {
        server.stop();
        return 0;
    }

    
    public void controlEvent(int arg0) {
        if ( ( arg0 == WrapperManager.WRAPPER_CTRL_LOGOFF_EVENT )
            && ( WrapperManager.isLaunchedAsService() || WrapperManager.isIgnoreUserLogoffs() ) )
        {
            // Ignore
        }
        else
        {
            WrapperManager.stop( 0 );
            // Will not get here.
        }
    }

    
    public static void main(String[] args){
        WrapperManager.start(new ctiserverwrapper(), args);
    }
    
    
    public enum CTIServerType {
        /**
         *
         */
        defaultimplementation,
        /**
         *
         */
        team,
        /**
         *
         */
        ciscoteam,
        /**
         *
         */
        cisco;
    }
}




