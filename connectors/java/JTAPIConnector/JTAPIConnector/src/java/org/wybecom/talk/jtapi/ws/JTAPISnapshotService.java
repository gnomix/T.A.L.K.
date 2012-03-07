/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package org.wybecom.talk.jtapi.ws;

import java.util.Enumeration;
import java.util.Vector;
import javax.annotation.Resource;
import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.servlet.ServletContext;
import javax.xml.ws.WebServiceContext;
import javax.xml.ws.handler.MessageContext;
import org.wybecom.talk.jtapi.ctiserver;
import org.wybecom.talk.jtapi.monitor;

/**
 *
 * @author Yohann
 */
@WebService()
public class JTAPISnapshotService {

    @Resource
    private WebServiceContext context;
    /**
     * Retreive a snapshot containing the state of all monitored lines
     */
    @WebMethod(operationName = "GetSnapshot")
    public org.wybecom.talkportal.cti.stateserver.LineControl[] GetSnapshot() {
        org.wybecom.talkportal.cti.stateserver.LineControl[] lcs = null;
        ctiserver server = getCTIServer();
        if (server != null){
            Vector monitors = server.getMonitors();
            lcs = new org.wybecom.talkportal.cti.stateserver.LineControl[monitors.size()];
            Enumeration mons = monitors.elements();
            int i = 0;
            while (mons.hasMoreElements()) {
                monitor mon = (monitor)mons.nextElement();
                lcs[i] = mon.getLineControl();
                i++;
            }
        }
        return lcs;
    }

    private ctiserver getCTIServer(){
        ServletContext servletContext = (ServletContext)context.getMessageContext().get(MessageContext.SERVLET_CONTEXT);
        servletContext.getAttribute("JTAPIServer");
        return (ctiserver)servletContext.getAttribute("JTAPIServer");
    }

}
