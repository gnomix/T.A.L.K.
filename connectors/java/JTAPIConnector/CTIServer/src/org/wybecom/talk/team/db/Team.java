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
import javax.persistence.JoinColumn;
import javax.persistence.JoinTable;
import javax.persistence.ManyToMany;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;

/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
@Entity
@Table(name = "Team")
@NamedQueries({@NamedQuery(name = "Team.findAll", query = "SELECT t FROM Team t"), @NamedQuery(name = "Team.findByTeampattern", query = "SELECT t FROM Team t WHERE t.teampattern = :teampattern"), @NamedQuery(name = "Team.findByTeamname", query = "SELECT t FROM Team t WHERE t.teamname = :teamname"), @NamedQuery(name = "Team.findByTeamdefaultdestination", query = "SELECT t FROM Team t WHERE t.teamdefaultdestination = :teamdefaultdestination"), @NamedQuery(name = "Team.findByTeamid", query = "SELECT t FROM Team t WHERE t.teamid = :teamid")})
public class Team implements Serializable {
    private static final long serialVersionUID = 1L;
    @Basic(optional = false)
    @Column(name = "teampattern")
    private String teampattern;
    @Basic(optional = false)
    @Column(name = "teamname")
    private String teamname;
    @Basic(optional = false)
    @Column(name = "teamdefaultdestination")
    private String teamdefaultdestination;
    @Id
    @Basic(optional = false)
    @Column(name = "teamid")
    private Integer teamid;
    @JoinTable(name = "TeamMemberTeam", joinColumns = {@JoinColumn(name = "teamid", referencedColumnName = "teamid")}, inverseJoinColumns = {@JoinColumn(name = "teammemberid", referencedColumnName = "teammemberid")})
    @ManyToMany
    private Collection<TeamMember> teamMemberCollection;
    @JoinColumn(name = "acdmanageraddressgroupid", referencedColumnName = "acdmanageraddressgroupid")
    @ManyToOne(optional = false)
    private ACDManagerAddressGroup acdmanageraddressgroupid;

    public Team() {
    }

    public Team(Integer teamid) {
        this.teamid = teamid;
    }

    public Team(Integer teamid, String teampattern, String teamname, String teamdefaultdestination) {
        this.teamid = teamid;
        this.teampattern = teampattern;
        this.teamname = teamname;
        this.teamdefaultdestination = teamdefaultdestination;
    }

    public String getTeampattern() {
        return teampattern;
    }

    public void setTeampattern(String teampattern) {
        this.teampattern = teampattern;
    }

    public String getTeamname() {
        return teamname;
    }

    public void setTeamname(String teamname) {
        this.teamname = teamname;
    }

    public String getTeamdefaultdestination() {
        return teamdefaultdestination;
    }

    public void setTeamdefaultdestination(String teamdefaultdestination) {
        this.teamdefaultdestination = teamdefaultdestination;
    }

    public Integer getTeamid() {
        return teamid;
    }

    public void setTeamid(Integer teamid) {
        this.teamid = teamid;
    }

    public Collection<TeamMember> getTeamMemberCollection() {
        return teamMemberCollection;
    }

    public void setTeamMemberCollection(Collection<TeamMember> teamMemberCollection) {
        this.teamMemberCollection = teamMemberCollection;
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
        hash += (teamid != null ? teamid.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Team)) {
            return false;
        }
        Team other = (Team) object;
        if ((this.teamid == null && other.teamid != null) || (this.teamid != null && !this.teamid.equals(other.teamid))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "org.wybecom.talk.team.db.Team[teamid=" + teamid + "]";
    }

}
