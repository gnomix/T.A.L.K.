<%-- 
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
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@page import="java.util.Vector" %>
<%@page import="org.wybecom.talk.jtapi.ctiserver" %>
<%@page import="org.wybecom.talk.team.teamserver" %>
<%@page import="org.wybecom.talk.jtapi.ctiprovider" %>
<%@page import="javax.telephony.Provider" %>
<%@page import="org.wybecom.talk.jtapi.stateclient" %>
<%@page import="org.wybecom.talk.jtapi.monitor" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
   "http://www.w3.org/TR/html4/loose.dtd">

<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <style type="text/css">
            body {
                font: 0.85em;
                line-height: 1.5em;
                color: #aaaaaa;
            }
            h1 {
                color: #E66C7D;
                font-weight: bold;
                font-size: 1.8em;
                margin-top: 1.25em;
            }
            h2 {
                color: #F2B5BE;
                font-weight: bold;
                font-size: 1.4em;
                border-bottom: 1px solid #F8DADE;
                margin-top: 1.5em;
                line-height: 4 em;
                padding-bottom:20px;
            }
            hr {
               color: #F2B5BE;
            }
            a img {
                border:none;
            }
            h2 a img {
                top: 10px;
                position:relative;
            }
            .rowHeader
            {
                    background-color:#a3a3a3;
                    font-weight:bold;
                    color:#ffffff;
            }

            .row {
                display:block;
                background-color:#F7F6F3;
                color:#333333;
            }

            .rowAlternate {
                display:block;
                background-color:#FFFFFF;
                color:#284775;
            }
        </style>
        <script type="text/javascript" language="Javascript" src="js/jquery-1.4.4.js"></script>
        <script type="text/javascript" language="Javascript" src="js/talk-jquery-ui/jquery-ui-1.8.9.custom.min.js"></script>
        <script type="text/javascript" language="Javascript" src="js/jtapiconnector.js"></script>
        <link rel="STYLESHEET" type="text/css" href="js/talk-jquery-ui/css/custom-theme/jquery-ui-1.8.9.custom.css" />
        
        <title>Wybecom TALK JTAPI Connector</title>
    </head>
    <body>
        <h1>Wybecom TALK JTAPI Connector</h1>
        <%
            
            try {
                ctiserver server = null;
                teamserver ts = null;
                String mode = "defaultimplementation";
                mode = (String)config.getServletContext().getAttribute("JTAPIConnectorMode");

                out.println("<div class=\"ui-state-highlight ui-corner-all\" style=\"margin-top: 20px; padding: 0pt 0.7em;\" id=\"connectorMode\">");
                out.println("<p>Running in "+mode+" mode</p>");
                out.println("</div><hr/>");
                if (mode.equalsIgnoreCase("cisco")){
                    server = (ctiserver)config.getServletContext().getAttribute("JTAPIServer");
                } else if (mode.equalsIgnoreCase("team")) {
                    ts = (teamserver)config.getServletContext().getAttribute("JTAPITeamServer");
                } else if (mode.equalsIgnoreCase("ciscoteam")) {
                    ts = (teamserver)config.getServletContext().getAttribute("JTAPITeamServer");
                } else if (mode.equalsIgnoreCase("defaultimplementation")){
                    server = (ctiserver)config.getServletContext().getAttribute("JTAPIServer");
                }


                Vector ctiproviders = null;
                if (server != null){
                    ctiproviders = server.getProviders();
                }
                if (ts != null){
                    ctiproviders = null;
                }
                if (ctiproviders != null){
                    out.println("<h2>JTAPI providers</h2>");
                    if (ctiproviders.size() > 0){
                        out.println("<div class=\"ui-state-highlight ui-corner-all\" style=\"margin-top: 20px; padding: 0pt 0.7em;\" id=\"jtapiProviders\"><p>");
                        out.println("<table border=\"0\" cellpadding=\"10\" cellspacing=\"0\" class=\"ui-corner-all\">");
                        out.println("<tr class=\"rowHeader\">");
                        out.println("<th>Provider</th>");
                        out.println("<th>State</th>");
                        out.println("<th>User</th>");
                        out.println("<th>Backup provider</th>");
                        out.println("<th>Monitored terminals</th>");
                        out.println("<th>Monitored lines</th>");
                        out.println("<th>Reload the provider</th>");
                        out.println("</tr>");
                        int pcpt = 0;
                        for(Object prov : ctiproviders) {
                            if (prov instanceof ctiprovider) {
                                if (pcpt % 2 == 0){
                                    out.println("<tr class=\"row\">");
                                } else {
                                    out.println("<tr class=\"rowAlternate\">");
                                }
                                out.println("<td>"+((ctiprovider)prov).getProviderName()+"</td>");
                                String provState = "Unknown";
                                switch (((ctiprovider)prov).getProvider().getState()){
                                    case Provider.IN_SERVICE:
                                        provState = "In service";
                                        break;
                                    case Provider.OUT_OF_SERVICE:
                                        provState = "Out of service";
                                        break;
                                    case Provider.SHUTDOWN:
                                        provState = "Shutdown";
                                        break;
                                }
                                out.println("<td>"+provState+"</td>");
                                out.println("<td>"+((ctiprovider)prov).getCTIUser()+"</td>");
                                out.println("<td>"+((ctiprovider)prov).getBackupProviderName()+"</td>");
                                out.println("<td>"+ String.valueOf(((ctiprovider)prov).getProvider().getTerminals().length) +"</td>");
                                out.println("<td>"+ String.valueOf(((ctiprovider)prov).getProvider().getAddresses().length) +"</td>");
                                out.println("<td><a href=\"javascript:reloadProvider('"+((ctiprovider)prov).getProviderName()+"')\"><img src=\"images/reload.png\" alt=\"Reload\" /></a></td>");
                                out.println("</tr>");
                                pcpt++;
                            }
                        }
                        out.println("</table>");
                    } else {
                        out.println("<div class=\"ui-state-error ui-corner-all\" style=\"margin-top: 20px; padding: 0pt 0.7em;\" id=\"jtapiProviders\"><p>");
                        out.println("There is a mistake with your settings (ctiserver.properties), see logs for more details");
                    }
                    

                    } else {
                        out.println("<div class=\"ui-state-error ui-corner-all\" style=\"margin-top: 20px; padding: 0pt 0.7em;\" id=\"jtapiProviders\"><p>");
                        out.println("There is a mistake with your settings (ctiserver.properties, see logs for more details");
                    }
                out.println("</p><br/></div><hr/>");

                Vector presenceServer = null;
                if (server != null){
                    presenceServer = server.getStateClients();
                }
                if (ts != null){
                    presenceServer = ts.getStateClients();
                }
                if (presenceServer != null){
                    out.println("<h2>Presence servers <a href=\"javascript:reloadStateServers()\"><img src=\"images/reload.png\" alt=\"Reload\" /></a></h2>");
                    if (presenceServer.size() > 0){
                    out.println("<div class=\"ui-state-highlight ui-corner-all\" style=\"margin-top: 20px; padding: 0pt 0.7em;\" id=\"presenceServers\"><p>");
                    out.println("<table border=\"0\" cellpadding=\"5\" cellspacing=\"0\" class=\"ui-corner-all\">");
                    out.println("<tr class=\"rowHeader\">");
                    out.println("<th>Server</th>");
                    out.println("</tr>");
                    int prcpt = 0;
                    for(Object prov : presenceServer) {
                        if (prov instanceof stateclient) {
                            if (prcpt % 2 == 0){
                                out.println("<tr class=\"row\">");
                            } else {
                                out.println("<tr class=\"rowAlternate\">");
                            }
                            out.println("<td>"+((stateclient)prov).getServer()+"</td>");
                            out.println("</tr>");
                            prcpt++;
                        }
                    }
                    out.println("</table>");
                    } else {
                        out.println("<div class=\"ui-state-error ui-corner-all\" style=\"margin-top: 20px; padding: 0pt 0.7em;\" id=\"presenceServers\"><p>");
                        out.println("There is a mistake with your settings (stateserver.properties, see logs for more details");
                    }
                    
                } else {
                        out.println("<div class=\"ui-state-error ui-corner-all\" style=\"margin-top: 20px; padding: 0pt 0.7em;\" id=\"presenceServers\"><p>");
                        out.println("There is a mistake with your settings (stateserver.properties, see logs for more details");
                    }
                out.println("</p><br/></div><hr/>");


                Vector monitoredLines = null;
                if (server != null){
                    monitoredLines = server.getMonitors();
                }
                if (ts != null){
                    monitoredLines = null;
                }
                if (monitoredLines != null){
                    out.println("<h2>Monitored lines</h2>");
                    if (monitoredLines.size() > 0){
                    out.println("<div class=\"ui-state-highlight ui-corner-all\" style=\"margin-top: 20px; padding: 0pt 0.7em;\" id=\"monitoredLines\"><p>");
                    out.println("<table border=\"0\" cellpadding=\"5\" cellspacing=\"0\" class=\"ui-corner-all\">");
                    out.println("<tr class=\"rowHeader\">");
                    out.println("<th>Extension</th>");
                    out.println("<th>In Service</th>");
                    out.println("<th>Terminal</th>");
                    out.println("<th>Provider</th>");
                    out.println("<th>Reload the line</th>");
                    out.println("</tr>");
                    int mcpt = 0;
                    for(Object prov : monitoredLines) {
                        if (prov instanceof monitor) {
                            if (mcpt % 2 == 0){
                                out.println("<tr class=\"row\">");
                            } else {
                                out.println("<tr class=\"rowAlternate\">");
                            }
                            out.println("<td>"+((monitor)prov).getDirNum()+"</td>");
                            out.println("<td>"+String.valueOf(((monitor)prov).addressIsInService())+"</td>");
                            out.println("<td>"+ ((monitor)prov).getTerminalName() +"</td>");
                            out.println("<td>"+ ((monitor)prov).getAddress().getProvider().getName() +"</td>");
                            out.println("<td><a href=\"javascript:reloadLine('"+((monitor)prov).getDirNum()+"')\"><img src=\"images/reload.png\" alt=\"Reload\" /></a></td>");
                            out.println("</tr>");
                        }
                    }
                    out.println("</table>");
                    } else {
                        out.println("<div class=\"ui-state-highlight ui-corner-all\" style=\"margin-top: 20px; padding: 0pt 0.7em;\" id=\"monitoredLines\"><p>");
                        out.println("No line monitored...");
                    }
                    
                } else {
                    out.println("<div class=\"ui-state-error ui-corner-all\" style=\"margin-top: 20px; padding: 0pt 0.7em;\" id=\"monitoredLines\"><p>");
                    out.println("No line monitored...Check logs for more details");
                }
                out.println("</p><br/></div><hr />");
            }
            catch (Exception e){
                out.println("<p>"+e.getMessage()+"</p>");
            }
        %>
        
    </body>
</html>
