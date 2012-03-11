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


import java.net.URL;
import java.util.*;
import javax.xml.namespace.QName;
import org.wybecom.talkportal.cti.stateserver.*;
/**
 * Implementation of a state server client
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class stateclient {
    
    private URL stateServer;
    StateServer server = null;
    StateServerSoap soapServer = null;

    public boolean inService = false;

    public Hashtable events = new Hashtable();
    
    /**
     * Constructor
     * @param url The State Server WSDL URL
     */
    public stateclient(String url)
    {
        try
        {
            stateServer = new URL(url);
            server = new StateServer(stateServer, new QName("http://wybecom.org/talkportal/cti/stateserver", "StateServer"));
            soapServer = server.getStateServerSoap();
            this.inService = true;
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
        }
    }
    
    /**
     * Sets presence information
     * @param lc The presence information
     * @param event The JTAPI event associated
     */
    public void setLineState(LineControl lc, String event)
    {
        try
        {   
            if (soapServer != null)
            {
                System.out.println("Receive Line Control from " + event + ", " + events.containsKey(event) + "-" + events.get(event).toString());
                if (events.containsKey(event) && Boolean.parseBoolean(events.get(event).toString())) {
                    boolean b = soapServer.setLineControl(lc);
                    if (!b) {
                        System.out.println("");
                    }
                }
            }
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
        }
    }

    
    /**
     * Sets presence information for an agent
     * @param ag The presence information
     * @param event The JTAPI event associated
     */
    public void setAgentLineControl(String extension, String agentid, AgentState state, CallCenterCall call, String event)
    {
        try
        {
            if (soapServer != null)
            {
                System.out.println("Receive Agent Line Control from " + event + ", " + events.containsKey(event) + "-" + events.get(event).toString());
                if (events.containsKey(event) && Boolean.parseBoolean(events.get(event).toString())) {
                    boolean b = soapServer.setAgentLineControl(extension, agentid, state, call);
                    if (!b) {
                        System.out.println("");
                    }
                }
            }
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
        }
    }

    
    /**
     * Adds a call log
     * @param line The extension associated with the call
     * @param call The call 
     */
    public void addCallLog(String line, Call call)
    {
        try
        {   
            if (soapServer != null)
            {
                soapServer.addCallLogs(line, call);
            }
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
        }
    }

    public String getServer(){
        return server.getWSDLDocumentLocation().toString();
    }


}
