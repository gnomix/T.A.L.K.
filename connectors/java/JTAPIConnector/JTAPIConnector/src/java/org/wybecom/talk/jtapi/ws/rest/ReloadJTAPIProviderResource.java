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
 * Implementation of ReloadJTAPIProvider REST WS
 * \package
 * The JTAPI Servlet REST API
 */
package org.wybecom.talk.jtapi.ws.rest;

import java.util.logging.Level;
import java.util.logging.Logger;
import javax.servlet.ServletContext;
import javax.ws.rs.core.Context;
import javax.ws.rs.Path;
import javax.ws.rs.GET;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import org.wybecom.talk.jtapi.ctiserver;

/**
 * REST Web Service
 *
 * @author Yohann BARRE
 */

@Path("ReloadJTAPIProvider")
public class ReloadJTAPIProviderResource {
    @Context
    private ServletContext servletContext;

    /** Creates a new instance of ReloadJTAPIProviderResource */
    public ReloadJTAPIProviderResource() {
    }

    /**
     * Reloads a JTAPI Provider
     * @return ReloadProviderResult The result of the operation
     */
    @GET
    @Produces("application/json")
    public ReloadProviderResult getJson(@QueryParam("provider") String provider) {
        ReloadProviderResult result = new ReloadProviderResult();
        try {
            result.setProvider(provider);
            ctiserver server = (ctiserver) servletContext.getAttribute("JTAPIServer");
            server.reloadProvider(provider);
            result.setResult("OK");
            return result;
        } catch (InterruptedException ex) {
            Logger.getLogger(ReloadJTAPIProviderResource.class.getName()).log(Level.SEVERE, null, ex);
            result.setResult(ex.toString());
            return result;
        }
    }

}
