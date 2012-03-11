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


import org.apache.log4j.Logger;
import org.apache.log4j.BasicConfigurator;

public class traceserver implements trace{
    static Logger logger = Logger.getLogger(traceserver.class);
    boolean	traceEnabled = true;
    StringBuffer buffer = new StringBuffer ();

    public traceserver () {
            BasicConfigurator.configure();
            
	}
    
    public final void printerror(Object o) {
        logger.error(o);
    }

   public final void bufPrint ( String str ) {
		if ( traceEnabled ) {
			buffer.append ( str );
		}

	}
    public final void print ( String str ) {
		if ( traceEnabled ) {
			logger.debug( str );
			flush ();
		}
    }
    public final void print ( char character ) {
		if ( traceEnabled ) {
			logger.debug( character );
			flush ();
		}
    }
    public final void print ( int integer ) {
		if ( traceEnabled ) {
			logger.debug( integer );
			flush ();
		}
    }
    public final void println ( String str ) {
		if ( traceEnabled ) {
			logger.debug( str );
			flush ();
		}
    }
    public final void println ( char character ) {
		if ( traceEnabled ) {
			logger.debug( character );
			flush ();
		}
    }
    public final void println ( int integer ) {
		if ( traceEnabled ) {
			logger.debug( integer );
			flush ();
		}
    }
    public final void setTrace ( boolean traceEnabled ) {
        this.traceEnabled = traceEnabled;
    }
    public final void flush () {
		if ( traceEnabled ) {
			buffer = new StringBuffer ();
		}
	}
    public final void clear () {

        
    }
}
