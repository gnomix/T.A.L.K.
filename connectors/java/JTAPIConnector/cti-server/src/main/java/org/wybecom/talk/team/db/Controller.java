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

import java.util.*;
import javax.persistence.*;
import javax.persistence.Query;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class Controller {

    private EntityManagerFactory emf;
    private EntityManager em;

    public Controller(){
        emf = Persistence.createEntityManagerFactory("TalkDBPU");
        em = emf.createEntityManager();
    }

    public List GetTeams(){
        Query q = em.createQuery("Select t from Team as t");
        return q.getResultList();
    }

    public List GetACDManagerAddressGroups(){
        Query q = em.createQuery("select amag from ACDManagerAddressGroup as amag");
        return q.getResultList();
    }

}
