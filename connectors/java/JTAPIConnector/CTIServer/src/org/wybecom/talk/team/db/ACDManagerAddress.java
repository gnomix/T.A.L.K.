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
 * \package
 * All team database related classes
 */
package org.wybecom.talk.team.db;

import java.io.Serializable;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;

/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
@Entity
@Table(name = "ACDManagerAddress")
@NamedQueries({@NamedQuery(name = "ACDManagerAddress.findAll", query = "SELECT a FROM ACDManagerAddress a"), @NamedQuery(name = "ACDManagerAddress.findByPattern", query = "SELECT a FROM ACDManagerAddress a WHERE a.pattern = :pattern"), @NamedQuery(name = "ACDManagerAddress.findByAcdmanageraddressid", query = "SELECT a FROM ACDManagerAddress a WHERE a.acdmanageraddressid = :acdmanageraddressid")})
public class ACDManagerAddress implements Serializable {
    private static final long serialVersionUID = 1L;
    @Basic(optional = false)
    @Column(name = "pattern")
    private String pattern;
    @Id
    @Basic(optional = false)
    @Column(name = "acdmanageraddressid")
    private Integer acdmanageraddressid;
    @JoinColumn(name = "acdmanageraddressgroupid", referencedColumnName = "acdmanageraddressgroupid")
    @ManyToOne(optional = false)
    private ACDManagerAddressGroup acdmanageraddressgroupid;

    public ACDManagerAddress() {
    }

    public ACDManagerAddress(Integer acdmanageraddressid) {
        this.acdmanageraddressid = acdmanageraddressid;
    }

    public ACDManagerAddress(Integer acdmanageraddressid, String pattern) {
        this.acdmanageraddressid = acdmanageraddressid;
        this.pattern = pattern;
    }

    public String getPattern() {
        return pattern;
    }

    public void setPattern(String pattern) {
        this.pattern = pattern;
    }

    public Integer getAcdmanageraddressid() {
        return acdmanageraddressid;
    }

    public void setAcdmanageraddressid(Integer acdmanageraddressid) {
        this.acdmanageraddressid = acdmanageraddressid;
    }

    public ACDManagerAddressGroup getAcdmanageraddressgroupid() {
        return acdmanageraddressgroupid;
    }

    public void setAcdmanageraddressgroupid(ACDManagerAddressGroup acdmanageraddressgroupid) {
        this.acdmanageraddressgroupid = acdmanageraddressgroupid;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (acdmanageraddressid != null ? acdmanageraddressid.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof ACDManagerAddress)) {
            return false;
        }
        ACDManagerAddress other = (ACDManagerAddress) object;
        if ((this.acdmanageraddressid == null && other.acdmanageraddressid != null) || (this.acdmanageraddressid != null && !this.acdmanageraddressid.equals(other.acdmanageraddressid))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "org.wybecom.talk.team.db.ACDManagerAddress[acdmanageraddressid=" + acdmanageraddressid + "]";
    }

}
