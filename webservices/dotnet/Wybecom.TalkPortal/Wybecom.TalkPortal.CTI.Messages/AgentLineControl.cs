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
    public class AgentLineControl : LineControl
    {
        private string _agentid;
        private CallCenterCall _ccc;
        private AgentState _agentstate;

        public string agentid
        {
            get
            {
                return _agentid;
            }
            set
            {
                _agentid = value;
            }
        }

        public CallCenterCall callcentercall
        {
            get
            {
                return _ccc;
            }
            set
            {
                _ccc = value;
            }
        }

        public AgentState agentstate
        {
            get
            {
                return _agentstate;
            }
            set
            {
                _agentstate = value;
            }
        }

        public override string ToString()
        {
            return agentid + ", agentstate: " + agentstate + ", callcentercall: " + callcentercall.ToString() + ", " + base.ToString();
        }

        public override bool Equals(object obj)
        {
            bool b = base.Equals(obj);
            if (b)
            {
                if (obj != null && obj is AgentLineControl)
                {
                    AgentLineControl alc = obj as AgentLineControl;
                    if (!(alc.agentid == this.agentid && alc.agentstate == this.agentstate && alc.callcentercall.Equals(this.callcentercall)))
                    {
                        b = false;
                    }
                }
            }
            return b;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [Serializable()]
    public class CallCenterCall
    {
        private string _caller;
        private string _applicationData;
        private string _lastredirectnumber;
        private string[] _callvariables;

        public string caller
        {
            get
            {
                return _caller;
            }
            set
            {
                _caller = value;
            }
        }

        public string applicationData
        {
            get
            {
                return _applicationData;
            }
            set
            {
                _applicationData = value;
            }
        }

        public string lastredirectnumber
        {
            get
            {
                return _lastredirectnumber;
            }
            set
            {
                _lastredirectnumber = value;
            }
        }

        public string[] callvariables
        {
            get
            {
                return _callvariables;
            }
            set
            {
                _callvariables = value;
            }
        }

        public override string ToString()
        {
            string call = "caller: " + caller + ", lastredirectnumber: " + lastredirectnumber + ", applicationData: " + applicationData;
            if (callvariables != null)
            {
                foreach (string s in callvariables)
                {
                    call += " callvariable: " + s;
                }
            }
            return call;
        }

        public override bool Equals(object obj)
        {
            bool b = true;
            if (obj != null && obj is CallCenterCall)
            {
                CallCenterCall ccc = obj as CallCenterCall;
                if (ccc.applicationData == this.applicationData && ccc.caller == this.caller)
                {
                    if (ccc.callvariables != null && this.callvariables != null)
                    {
                        if (ccc.callvariables.Length == this.callvariables.Length)
                        {
                            for (int i = 0; i < ccc.callvariables.Length; i++)
                            {
                                if (ccc.callvariables[i] != this.callvariables[i])
                                {
                                    b = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            b = false;
                        }
                    }
                }
                else
                {
                    b = false;
                }
            }
            else
            {
                b = false;
            }
            return b;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public enum AgentState
    {
        BUSY,
        LOG_IN,
        LOG_OUT,
        NOT_READY,
        READY,
        UNKNOWN,
        WORK_NOT_READY,
        WORK_READY
    }
}
