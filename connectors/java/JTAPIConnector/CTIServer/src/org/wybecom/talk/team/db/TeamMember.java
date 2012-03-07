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
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.ManyToMany;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;

/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
@Entity
@Table(name = "TeamMember")
@NamedQueries({@NamedQuery(name = "TeamMember.findAll", query = "SELECT t FROM TeamMember t"), @NamedQuery(name = "TeamMember.findByTeammembername", query = "SELECT t FROM TeamMember t WHERE t.teammembername = :teammembername"), @NamedQuery(name = "TeamMember.findByTeammemberextension", query = "SELECT t FROM TeamMember t WHERE t.teammemberextension = :teammemberextension"), @NamedQuery(name = "TeamMember.findByIssupervisor", query = "SELECT t FROM TeamMember t WHERE t.issupervisor = :issupervisor"), @NamedQuery(name = "TeamMember.findByIslogged", query = "SELECT t FROM TeamMember t WHERE t.islogged = :islogged"), @NamedQuery(name = "TeamMember.findByTeammemberid", query = "SELECT t FROM TeamMember t WHERE t.teammemberid = :teammemberid")})
public class TeamMember implements Serializable {
    private static final long serialVersionUID = 1L;
    @Basic(optional = false)
    @Column(name = "teammembername")
    private String teammembername;
    @Basic(optional = false)
    @Column(name = "teammemberextension")
    private String teammemberextension;
    @Basic(optional = false)
    @Column(name = "issupervisor")
    private boolean issupervisor;
    @Column(name = "islogged")
    private Boolean islogged;
    @Id
    @Basic(optional = false)
    @Column(name = "teammemberid")
    private Integer teammemberid;
    @ManyToMany(mappedBy = "teamMemberCollection")
    private Collection<Team> teamCollection;

    public TeamMember() {
    }

    public TeamMember(Integer teammemberid) {
        this.teammemberid = teammemberid;
    }

    public TeamMember(Integer teammemberid, String teammembername, String teammemberextension, boolean issupervisor) {
        this.teammemberid = teammemberid;
        this.teammembername = teammembername;
        this.teammemberextension = teammemberextension;
        this.issupervisor = issupervisor;
    }

    public String getTeammembername() {
        return teammembername;
    }

    public void setTeammembername(String teammembername) {
        this.teammembername = teammembername;
    }

    public String getTeammemberextension() {
        return teammemberextension;
    }

    public void setTeammemberextension(String teammemberextension) {
        this.teammemberextension = teammemberextension;
    }

    public boolean getIssupervisor() {
        return issupervisor;
    }

    public void setIssupervisor(boolean issupervisor) {
        this.issupervisor = issupervisor;
    }

    public Boolean getIslogged() {
        return islogged;
    }

    public void setIslogged(Boolean islogged) {
        this.islogged = islogged;
    }

    public Integer getTeammemberid() {
        return teammemberid;
    }

    public void setTeammemberid(Integer teammemberid) {
        this.teammemberid = teammemberid;
    }

    public Collection<Team> getTeamCollection() {
        return teamCollection;
    }

    public void setTeamCollection(Collection<Team> teamCollection) {
        this.teamCollection = teamCollection;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (teammemberid != null ? teammemberid.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof TeamMember)) {
            return false;
        }
        TeamMember other = (TeamMember) object;
        if ((this.teammemberid == null && other.teammemberid != null) || (this.teammemberid != null && !this.teammemberid.equals(other.teammemberid))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "org.wybecom.talk.team.db.TeamMember[teammemberid=" + teammemberid + "]";
    }

}
