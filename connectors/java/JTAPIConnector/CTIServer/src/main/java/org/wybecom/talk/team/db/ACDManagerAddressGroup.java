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

package org.wybecom.talk.team.db;

import java.io.Serializable;
import java.util.Collection;
import javax.persistence.Basic;
import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;

/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
@Entity
@Table(name = "ACDManagerAddressGroup")
@NamedQueries({@NamedQuery(name = "ACDManagerAddressGroup.findAll", query = "SELECT a FROM ACDManagerAddressGroup a"), @NamedQuery(name = "ACDManagerAddressGroup.findByAcdmanageraddressgroupname", query = "SELECT a FROM ACDManagerAddressGroup a WHERE a.acdmanageraddressgroupname = :acdmanageraddressgroupname"), @NamedQuery(name = "ACDManagerAddressGroup.findByAcdmanageraddressgroupid", query = "SELECT a FROM ACDManagerAddressGroup a WHERE a.acdmanageraddressgroupid = :acdmanageraddressgroupid")})
public class ACDManagerAddressGroup implements Serializable {
    private static final long serialVersionUID = 1L;
    @Basic(optional = false)
    @Column(name = "acdmanageraddressgroupname")
    private String acdmanageraddressgroupname;
    @Id
    @Basic(optional = false)
    @Column(name = "acdmanageraddressgroupid")
    private Integer acdmanageraddressgroupid;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "acdmanageraddressgroupid")
    private Collection<Team> teamCollection;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "acdmanageraddressgroupid")
    private Collection<ACDManagerAddress> aCDManagerAddressCollection;

    public ACDManagerAddressGroup() {
    }

    public ACDManagerAddressGroup(Integer acdmanageraddressgroupid) {
        this.acdmanageraddressgroupid = acdmanageraddressgroupid;
    }

    public ACDManagerAddressGroup(Integer acdmanageraddressgroupid, String acdmanageraddressgroupname) {
        this.acdmanageraddressgroupid = acdmanageraddressgroupid;
        this.acdmanageraddressgroupname = acdmanageraddressgroupname;
    }

    public String getAcdmanageraddressgroupname() {
        return acdmanageraddressgroupname;
    }

    public void setAcdmanageraddressgroupname(String acdmanageraddressgroupname) {
        this.acdmanageraddressgroupname = acdmanageraddressgroupname;
    }

    public Integer getAcdmanageraddressgroupid() {
        return acdmanageraddressgroupid;
    }

    public void setAcdmanageraddressgroupid(Integer acdmanageraddressgroupid) {
        this.acdmanageraddressgroupid = acdmanageraddressgroupid;
    }

    public Collection<Team> getTeamCollection() {
        return teamCollection;
    }

    public void setTeamCollection(Collection<Team> teamCollection) {
        this.teamCollection = teamCollection;
    }

    public Collection<ACDManagerAddress> getACDManagerAddressCollection() {
        return aCDManagerAddressCollection;
    }

    public void setACDManagerAddressCollection(Collection<ACDManagerAddress> aCDManagerAddressCollection) {
        this.aCDManagerAddressCollection = aCDManagerAddressCollection;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (acdmanageraddressgroupid != null ? acdmanageraddressgroupid.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof ACDManagerAddressGroup)) {
            return false;
        }
        ACDManagerAddressGroup other = (ACDManagerAddressGroup) object;
        if ((this.acdmanageraddressgroupid == null && other.acdmanageraddressgroupid != null) || (this.acdmanageraddressgroupid != null && !this.acdmanageraddressgroupid.equals(other.acdmanageraddressgroupid))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "org.wybecom.talk.team.db.ACDManagerAddressGroup[acdmanageraddressgroupid=" + acdmanageraddressgroupid + "]";
    }

}
