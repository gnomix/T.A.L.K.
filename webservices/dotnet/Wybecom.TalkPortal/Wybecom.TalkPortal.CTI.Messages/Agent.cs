 ///
 ///  WYBECOM T.A.L.K. -- Telephony Application Library Kit
 ///  Copyright (C) 2010 WYBECOM
 ///
 ///  Yohann BARRE <y.barre@wybecom.com>
 ///
 ///  This program is free software: you can redistribute it and/or modify
 ///  it under the terms of the GNU General Public License as published by
 ///  the Free Software Foundation, either version 3 of the License, or
 ///  (at your option) any later version.
 ///
 ///  This program is distributed in the hope that it will be useful,
 ///  but WITHOUT ANY WARRANTY; without even the implied warranty of
 ///  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 ///  GNU General Public License for more details.
 ///
 ///  You should have received a copy of the GNU General Public License
 ///  along with this program.  If not, see <http:///www.gnu.org/licenses/>.
 ///
 ///  T.A.L.K. is based upon:
 ///  - Sun JTAPI http:///java.sun.com/products/jtapi/
 ///  - JulMar TAPI http:///julmar.com/
 ///  - Asterisk.Net http:///sourceforge.net/projects/asterisk-dotnet/
 ///
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wybecom.TalkPortal.CTI.ACD
{
    [Serializable()]
    public class Agent
    {
        private AgentType _agenttype;
        private string _loginid;
        private string _lastname;
        private string _firstname;
        private string _extension;
        private string _description;
        private CSQ[] _csqs;

        public Agent()
        {
        }

        public override string ToString()
        {
            string a = "";
            foreach (CSQ c in _csqs)
            {
                a += " " + c.ToString();
            }
            return "Agent " + loginID + ", CSQs: " + a;
        }

        public Agent(AgentType agtType, string login, string lastname, string firstname, string extension, string description, CSQ[] csq)
        {
            _agenttype = agtType;
            _loginid = login;
            _lastname = lastname;
            _firstname = firstname;
            _extension = extension;
            _description = description;
            _csqs = csq;
        }

        public AgentType agentType
        {
            get
            {
                return _agenttype;
            }
            set
            {
                _agenttype = value;
            }
        }

        public string loginID
        {
            get
            {
                return _loginid;
            }
            set
            {
                _loginid= value;
            }
        }

        public string lastName
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
            }
        }

        public string firstName
        {
            get
            {
                return _firstname;
            }
            set
            {
                _firstname = value;
            }
        }

        public string Extension
        {
            get
            {
                return _extension;
            }
            set
            {
                _extension = value;
            }
        }

        public string Description
        {

            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public CSQ[] CSQs
        {
            get
            {
                return _csqs;
            }
            set
            {
                _csqs = value;
            }
        }

    }

    [Serializable()]
    public enum AgentType : ushort
    {
        Agent = 1,
        Supervisor = 2
    }
}
