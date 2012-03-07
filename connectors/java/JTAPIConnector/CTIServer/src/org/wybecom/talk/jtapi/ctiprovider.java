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
 * Implementation of ctiprovider
 */

package org.wybecom.talk.jtapi;


import javax.telephony.Provider;
/**
 * \brief A link to a JTAPI provider
 * @author Yohann BARRE
 */
public class ctiprovider {
    
    private Provider provider;
    private int state;
    private Boolean isPrimary = true;
    private String primaryProvider;
    private String backupProvider;
    private String ctiUser;
    private String ctiPassword;
    
    /**
     * Initialize the cti provider
     * @param primary Is primary or backup
     * @param pProvider The primary JTAPi provider
     * @param bProvider The backup JTAPI provider
     * @param user The user login
     * @param password The user password
     */
    public ctiprovider(Boolean primary,
            String pProvider,
            String bProvider,
            String user,
            String password) {
        this.isPrimary = primary;
        this.backupProvider = bProvider;
        this.primaryProvider = pProvider;
        this.ctiUser = user;
        this.ctiPassword = password;
        this.state = Provider.OUT_OF_SERVICE;
    }

    /**
     *
     * @return The cti user
     */
    public String getCTIUser() {
        return ctiUser;
    }

    /**
     *
     * @return
     */
    public String getProviderName() {
        return primaryProvider;
    }

    /**
     *
     * @return The backup JTAPI provider
     */
    public String getBackupProviderName() {
        return backupProvider;
    }
    /**
     * 
     * @return The JTAPI identifier used to open the provider
     */
    public String getProviderString() {
        String providerString = "";
        if (isPrimary){
            providerString = this.primaryProvider + ";login=" + this.ctiUser + ";passwd=" + this.ctiPassword;
        }
        else {
            providerString = this.backupProvider + ";login=" + this.ctiUser + ";passwd=" + this.ctiPassword;
        }
        return providerString;
    }
    /**
     * Accessor
     * @param p The JTAPI Provider
     */
    public void setProvider(Provider p) {
        this.provider = p;
    }
    /**
     * Accessor
     * @return The JTAPI provider state
     */
    public int getProviderState() {
        return this.state;
    }
    /**
     * Accessor
     * @param st The JTAPI provider state
     */
    public void setProviderState(int st) {
        this.state = st;
        if (st == Provider.OUT_OF_SERVICE || st == Provider.SHUTDOWN) {
            this.isPrimary = false;
        }
    }
    /**
     * Accessor
     * @return The JTAPI provider
     */
    public Provider getProvider() {
        return this.provider;
    }
    
}
