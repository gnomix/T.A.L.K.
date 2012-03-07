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
 * Implementation of ReloadLineResult
 */
package org.wybecom.talk.jtapi.ws.rest;

import javax.xml.bind.annotation.XmlRootElement;

/**
 * 
 * @author Yohann BARRE
 */
@XmlRootElement
public class ReloadLineResult {
    private String line;
    private String result;

    public String getLine(){
        return this.line;
    }

    public void setLine(String line){
        this.line = line;
    }

    public String getResult(){
        return this.result;
    }

    public void setResult(String result){
        this.result = result;
    }
}
