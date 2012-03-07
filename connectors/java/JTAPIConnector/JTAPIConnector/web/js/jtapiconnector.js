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

function reloadStateServers(){
    $.ajax({
            type: "GET",
            contentType: "application/json",
            datatype: "json",
            data: null,
            error: function(){
                alert("Error while reloading State server clients!");
            },
            success: function(data){
                location.reload(true);
            },
            url: "resources/ReloadStateServer"
        });
}

function reloadProvider(prov){
    $.ajax({
            type: "GET",
            contentType: "application/json",
            datatype: "json",
            data: {provider: prov},
            error: function(){
                alert("Error while reloading JTAPI provider "+prov+"!");
            },
            success: function(data){
                location.reload(true);
            },
            url: "resources/ReloadJTAPIProvider"
        });
}

function reloadLine(address){
    $.ajax({
            type: "GET",
            contentType: "application/json",
            datatype: "json",
            data: {line: address},
            error: function(){
                alert("Error while reloading Address " +address+ "!");
            },
            success: function(data){
                location.reload(true);
            },
            url: "resources/ReloadLine"
        });
}
