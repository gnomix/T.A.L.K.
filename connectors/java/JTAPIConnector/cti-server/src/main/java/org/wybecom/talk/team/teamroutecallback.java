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
import java.util.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class teamroutecallback implements RouteCallback{

    ACDAddress acdaddress;

    public teamroutecallback(ACDAddress address){
        acdaddress = address;
    }

    public void routeEvent(RouteEvent event){

			System.out.println("Route Event Received");
			String[] routeSelected = this.getAvailableMedia();
                        try{
                            event.getRouteSession().selectRoute(routeSelected );
                            String dest = "Selected destinations: ";
                            for (String s : routeSelected){
                                dest += s + " ";
                            }
                            System.out.println(dest);
                        } catch (Exception e) {
			System.out.println("Exception " + e.toString());
                        }

}

    public synchronized void reRouteEvent(ReRouteEvent event){

    }

    public synchronized void routeUsedEvent(RouteUsedEvent event){

    }

    public synchronized void routeEndEvent(RouteEndEvent event){

    }

    public synchronized void routeCallbackEndedEvent(RouteCallbackEndedEvent event){

    }

    String[] getAvailableMedia(){
            Vector v = new Vector();
            try {
                for (Agent ag : acdaddress.getLoggedOnAgents()){
                    if (ag.getState() == Agent.READY || ag.getState() == Agent.WORK_READY){
                        v.add(ag.getAgentAddress().getName());
                    }
                }
            }
            catch (Exception e){
                System.out.println("Unable to get destinations from " + acdaddress.getName() + ": " + e.toString());
            }
            return (String[])v.toArray(new String[0]);
    }

    
}
