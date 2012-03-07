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
 * Implementation of JTAPI Servlet
 * \package
 * All JTAPI Connector servlet related libraries
 */
package org.wybecom.talk.jtapi.connector;

import java.io.IOException;
import java.io.PrintWriter;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.ServletContext;
import org.wybecom.talk.jtapi.*;
import org.wybecom.talk.team.teamserver;
import org.wybecom.talk.team.ciscoteamserver;

/**
 *
 * @author Yohann BARRE
 */
public class JTAPIServlet extends HttpServlet {
   
    private ctiserver server;
    private teamserver tserver;
    private ServletContext context = null;
    
    @Override
    public void init() throws ServletException {
        System.out.println("**************************");
        System.out.println("JTAPIConnector servlet initializing...");
        System.out.println("**************************");

        context = getServletContext();
        String mode = getServletConfig().getInitParameter("mode");
        if (mode.equals(null) && mode.equals("")){
            mode = "cisco";
        }
        context.setAttribute("JTAPIConnectorMode", mode);

        System.out.println("Starting JTAPI server in " + mode + " mode.");
        if (mode.equalsIgnoreCase("cisco")){
            String partitions = getServletConfig().getInitParameter("filteredPartitions");
            if (!partitions.equals("") && !partitions.equals(null)){
                server = new ciscoctiserver(partitions, getServletConfig().getServletContext().getRealPath("/ctiserver.properties") , getServletConfig().getServletContext().getRealPath("/stateserver.properties"));
            } else {
                server = new ciscoctiserver(getServletConfig().getServletContext().getRealPath("/ctiserver.properties"), getServletConfig().getServletContext().getRealPath("/stateserver.properties"));
            }
        } else if (mode.equalsIgnoreCase("defaultimplementation")) {

            server = new ctiserver(getServletConfig().getServletContext().getRealPath("/ctiserver.properties") , getServletConfig().getServletContext().getRealPath("/stateserver.properties"));
        } else if (mode.equalsIgnoreCase("team")){
            tserver = new teamserver();
        } else if (mode.equalsIgnoreCase("ciscoteam")){
            tserver = new ciscoteamserver();
        }

        if (server != null){
            server.start();
            System.out.println("JTAPI server started.");
        }

        if (tserver != null ){
            tserver.start();
            System.out.println("JTAPI Team server started.");
        }
        

        System.out.println("**************************");
        System.out.println("Adding JTAPI Server to context.");
        System.out.println("**************************");
        
        context.setAttribute("JTAPIServer", server);
        System.out.println("JTAPI Server added to context.");
    }

    @Override
    public void destroy() {
        System.out.println("**************************");
        System.out.println("JTAPIConnector servlet destroyed...");
        System.out.println("**************************");
        if (server != null) {
            System.out.println("Stopping JTAPI server");
            server.stop();
            System.out.println("**************************");
            System.out.println("Removing JTAPI server from servlet context");
            System.out.println("**************************");
            context = getServletContext();
            context.removeAttribute("JTAPIServer");
        }
    }

    /**
     * Processes requests for both HTTP <code>GET</code> and <code>POST</code> methods.
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
    throws ServletException, IOException {
        response.setContentType("text/html;charset=UTF-8");
        PrintWriter out = response.getWriter();
        try {
            /* TODO output your page here
            out.println("<html>");
            out.println("<head>");
            out.println("<title>Servlet JTAPIServlet</title>");  
            out.println("</head>");
            out.println("<body>");
            out.println("<h1>Servlet JTAPIServlet at " + request.getContextPath () + "</h1>");
            out.println("</body>");
            out.println("</html>");
            */
        } finally { 
            out.close();
        }
    } 

    // <editor-fold defaultstate="collapsed" desc="HttpServlet methods. Click on the + sign on the left to edit the code.">
    /** 
     * Handles the HTTP <code>GET</code> method.
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
    throws ServletException, IOException {
        processRequest(request, response);
    } 

    /** 
     * Handles the HTTP <code>POST</code> method.
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
    throws ServletException, IOException {
        processRequest(request, response);
    }

    /** 
     * Returns a short description of the servlet.
     * @return a String containing servlet description
     */
    @Override
    public String getServletInfo() {
        return "Short description";
    }// </editor-fold>

}
