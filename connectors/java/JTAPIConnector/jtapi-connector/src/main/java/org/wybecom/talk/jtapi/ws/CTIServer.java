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
 * Implementation of CTIServer Web Service
 * \package
 * The JTAPI servlet Web services
 */
package org.wybecom.talk.jtapi.ws;

import java.util.List;
import javax.jws.WebService;
import javax.servlet.ServletContext;
import javax.xml.ws.WebServiceContext;
import javax.xml.ws.handler.MessageContext;
import javax.annotation.Resource;
import javax.jws.WebMethod;
import javax.jws.WebParam;
import org.wybecom.talk.jtapi.ctiserver;
import org.wybecom.talk.jtapi.monitor;
import org.wybecom.talk.team.teamserver;
import org.wybecom.talkportal.cti.stateserver.ArrayOfLineControlConnection;
import org.wybecom.talkportal.cti.stateserver.LineControlConnection;
/**
 *
 * @author Yohann BARRE
 */
@WebService()
public class CTIServer {

    @Resource
    private WebServiceContext context;
    /**
     * Call an extension from a monitored line
     * @param caller The caller extension
     * @param callee The destination
     * @return The call id
     */
    @WebMethod(operationName = "Call")
    public String Call(@WebParam(name = "caller")
    String caller, @WebParam(name = "callee")
    String callee) {

        try
        {
            ctiserver server = getCTIServer();
            return server.Call(caller, callee);
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Unhook an incoming call from a monitored line
     * @param callee The caller extension
     * @param callid The callid
     * @return The result of the operation
     */
    @WebMethod(operationName = "UnHook")
    public Boolean UnHook(@WebParam(name = "callee")
    String callee, @WebParam(name = "callid")
    String callid) {
        try
        {
            ctiserver server = getCTIServer();
            return server.UnHook(callee, callid);
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Hangup a call from a monitored line
     * @param caller The caller extension
     * @param callid The call id
     * @return The result of the operation
     */
    @WebMethod(operationName = "HangUp")
    public Boolean HangUp(@WebParam(name = "caller")
    String caller, @WebParam(name = "callid")
    String callid) {
        try
        {
            ctiserver server = getCTIServer();
            return server.HangUp(caller, callid);
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Forward a monitored line
     * @param caller The extension to forward
     * @param callid The destination
     * @return The result of the operation
     */
    @WebMethod(operationName = "Forward")
    public Boolean Forward(@WebParam(name = "caller")
    String caller, @WebParam(name = "callid")
    String callid) {
        try
        {
            ctiserver server = getCTIServer();
            return server.Forward(caller, callid);
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Put a call on hold from a monitored line
     * @param callid The call id
     * @param caller The caller
     * @return The result of the operation
     */
    @WebMethod(operationName = "Hold")
    public Boolean Hold(@WebParam(name = "callid")
    String callid, @WebParam(name = "caller")
    String caller) {
        try
        {
            ctiserver server = getCTIServer();
            return server.Hold(callid, caller);
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Retreive a holded call from a monitored line
     * @param callid The call id
     * @param caller The caller extension
     * @return The result of the operation
     */
    @WebMethod(operationName = "UnHold")
    public Boolean UnHold(@WebParam(name = "callid")
    String callid, @WebParam(name = "caller")
    String caller) {
        try
        {
            ctiserver server = getCTIServer();
            return server.UnHold(callid, caller);
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Activate / Deactivate do not disturb feature from a monitored line
     * @param caller The extension
     * @return The result of the operation
     */
    @WebMethod(operationName = "DoNotDisturb")
    public Boolean DoNotDisturb(@WebParam(name = "caller")
    String caller) {
        try
        {
            ctiserver server = getCTIServer();
            return server.DoNotDisturb(caller);
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Transfer a call to a destination from a monitored line
     * @param callid The call id
     * @param caller The caller extension
     * @param destination The destination
     * @return The result of the operation
     */
    @WebMethod(operationName = "Transfer")
    public Boolean Transfer(@WebParam(name = "callid")
    String callid, @WebParam(name = "caller")
    String caller, @WebParam(name = "destination")
    String destination) {
        try
        {
            ctiserver server = getCTIServer();
            if (callid == null && destination == null) {
                return server.Transfer(caller);
            }
            else {
                return server.Transfer(callid, caller, destination);
            }
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Divert a call from a monitored line
     * @param callid The call id
     * @param caller The caller extension
     * @return The result of the operation
     */
    @WebMethod(operationName = "Divert")
    public Boolean Divert(@WebParam(name = "callid")
    String callid, @WebParam(name = "caller")
    String caller) {
        try
        {
            ctiserver server = getCTIServer();
            return server.Divert(callid, caller);
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Do a consult transfer from a monitored line
     * @param callid The caller id
     * @param callee The called party
     * @param destination The destination
     * @return The result of the operation
     */
    @WebMethod(operationName = "ConsultTransfer")
    public Boolean ConsultTransfer(@WebParam(name = "callid")
    String callid, @WebParam(name = "callee")
    String callee, @WebParam(name = "destination")
    String destination) {
        //TODO write your implementation code here:
        try
        {
            ctiserver server = getCTIServer();
            return server.ConsultTransfer(callid, callee, destination);
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Start a monitor session from a monitored line
     * @param monitorer The monitor party
     * @param monitored The monitored party
     * @return The result of the operation
     */
    @WebMethod(operationName = "Monitor")
    public Boolean Monitor(@WebParam(name = "monitorer")
    String monitorer, @WebParam(name = "monitored")
    String monitored) {
        //TODO write your implementation code here:
        try
        {
            ctiserver server = getCTIServer();
            return server.Monitor(monitorer, monitored);
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Do not use
     * @param agentId The agent login
     * @param pwd The agent password
     * @param extension The agent extension
     * @param teamextension The team extension (route point)
     * @return The result of the operation
     */
    @WebMethod(operationName = "Login")
    public Boolean Login(@WebParam(name = "agentId")
    String agentId, @WebParam(name = "pwd")
    String pwd, @WebParam(name = "extension")
    String extension, @WebParam(name="teamextension")
    String teamextension) {
        //TODO write your implementation code here:
        try
        {
            teamserver server = getTeamServer();
            if (teamextension.length() > 0){
                return server.Login(agentId, pwd, extension, teamextension);
            }
            else {
                return server.Login(agentId, pwd, extension);
            }
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }

    /**
     * Do not use
     * @param agentId The agent login
     * @param teamextension The team extension (route point)
     * @return
     */
    @WebMethod(operationName = "Logoff")
    public Boolean Logoff(@WebParam(name = "agentId")
    String agentId, @WebParam(name = "teamextension") String teamextension) {
        //TODO write your implementation code here:
        try
        {
            teamserver server = getTeamServer();
            if (teamextension.length() > 0){
                return server.Logoff(agentId, teamextension);
            }
            else {
                return server.Logoff(agentId);
            }
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return null;
        }
    }


    private ctiserver getCTIServer(){
        ServletContext servletContext = (ServletContext)context.getMessageContext().get(MessageContext.SERVLET_CONTEXT);
        servletContext.getAttribute("JTAPIServer");
        return (ctiserver)servletContext.getAttribute("JTAPIServer");
    }

    private teamserver getTeamServer() {
        ServletContext servletContext = (ServletContext)context.getMessageContext().get(MessageContext.SERVLET_CONTEXT);
        servletContext.getAttribute("JTAPITeamServer");
        return (teamserver)servletContext.getAttribute("JTAPITeamServer");
    }

    /**
     * Sends DTMF
     * @param callee The caller which sends the dtmf string
     * @param dtmf The dtmf string to send
     * @return The result of the operation
     */
    @WebMethod(operationName = "SendDTMF")
    public Boolean SendDTMF(@WebParam(name = "callee")
    String callee, @WebParam(name = "dtmf")
    String dtmf) {
        Boolean result = false;
        try
        {
            ctiserver server = getCTIServer();
            result = server.SendDTMF(callee, dtmf);
        }
        catch (Exception e){
            System.out.println(e.getMessage());
        }
        return result;
    }

    /**
     * Whether or not an extension is available for dialing
     * @param caller The extension on which to perform the operation
     * @return The result of the operation
     */
    @WebMethod(operationName = "canCall")
    public Boolean canCall(@WebParam(name = "caller")
    String caller) {
        Boolean result = true;
        try
        {
            ctiserver server = getCTIServer();
            monitor m = server.getMonitorFromDirNum(caller);
            List<LineControlConnection> ccs  = m.getLineControl().getLineControlConnection().getLineControlConnection();
            if ((ccs != null && !ccs.isEmpty()) || !m.addressIsInService()) {
                result = false;
            }
            return result;
        }
        catch (Exception e)
        {
            System.out.println(e.getMessage());
            return result;
        }
    }
}
