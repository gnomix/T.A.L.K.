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
using System.Web;
using System.Xml.Serialization;
using log4net;

namespace Wybecom.TalkPortal.CTI
{
    /// <summary>
    /// Presence and status information
    /// </summary>
    [Serializable()]
    public class LineControl : LineStatus
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private string _forward = "";
        //private bool _doNotDisturb = false;
        //private bool _mwiOn = false;
        private static string sort = "startTime";
        private LineControlConnection[] _lineControlConnections;
        private Queue<Call> placedCalls = new Queue<Call>(100);
        private Queue<Call> missedCalls = new Queue<Call>(100);
        private Queue<Call> receivedCalls = new Queue<Call>(100);

        public LineControl()
        {
        }

        //public string forward
        //{
        //    get { return _forward; }
        //    set { _forward = value; }
        //}

        //public bool doNotDisturb
        //{
        //    get { return _doNotDisturb; }
        //    set { _doNotDisturb = value; }
        //}

        //public bool mwiOn
        //{
        //    get { return _mwiOn; }
        //    set { _mwiOn = value; }
        //}

        /// <summary>
        /// Line status
        /// <seealso cref="Status"/>
        /// </summary>
        public override Status status
        {
            get
            {
                try
                {
                    Status _calcStatus = base.status;
                    if (_lineControlConnections != null)
                    {
                        if (_lineControlConnections.Length > 0)
                        {
                            foreach (LineControlConnection lcc in lineControlConnection)
                            {
                                switch (lcc.state)
                                {
                                    case ConnectionState.alerting:
                                        _calcStatus = Status.ringing;
                                        break;
                                    case ConnectionState.dialing:
                                        _calcStatus = Status.dialing;
                                        break;
                                    case ConnectionState.offered:
                                        _calcStatus = Status.ringing;
                                        break;
                                    default:
                                        _calcStatus = Status.busy;
                                        break;
                                }
                                if (_calcStatus == Status.ringing || _calcStatus == Status.dialing)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    return _calcStatus;
                }
                catch (Exception e)
                {
                    log.Error("Unable to get status: " + e.Message);
                    return Status.unknown;
                }
            }
            set
            {
                base.status = value;
            }
        }

        /// <summary>
        /// Active connections
        /// <seealso cref="LineControlConnection"/>
        /// </summary>
        public LineControlConnection[] lineControlConnection
        {
            get { return _lineControlConnections; }
            set { _lineControlConnections = value; }
        }

        /// <summary>
        /// Adds a call to a call log
        /// </summary>
        /// <param name="call">
        /// Description de l'appel
        /// <seealso cref="Call"/>
        /// </param>
        public void AddCall(Call call)
        {
            switch (call.type)
            {
                case CallType.missed:
                    AddMissedCall(call);
                    break;
                case CallType.placed:
                    AddPlacedCall(call);
                    break;
                case CallType.received:
                    AddReceivedCall(call);
                    break;
            }
        }

        /// <summary>
        /// Gets call logs
        /// </summary>
        /// <param name="type">
        /// Call logs type
        /// <seealso cref="CallType"/>
        /// </param>
        /// <param name="sortppt">
        /// Field use to sorting
        /// </param>
        /// <returns>
        /// Call logs
        /// </returns>
        public Call[] GetCalls(CallType type, string sortppt)
        {
            Call[] calls = null;
            sort = sortppt;
            switch (type)
            {
                case CallType.missed:
                    calls= missedCalls.ToArray();
                    break;
                case CallType.placed:
                    calls= placedCalls.ToArray();
                    break;
                case CallType.received:
                    calls= receivedCalls.ToArray();
                    break;
                default:
                    calls = missedCalls.ToArray();
                    break;
            }
            Array.Sort<Call>(calls, new Comparison<Call>(CallSort));
            return calls;
        }

        private void AddMissedCall(Call call)
        {
            try
            {
                if (isExists(call, CallType.missed))
                {
                    if (missedCalls.Count == 100)
                    {
                        missedCalls.Dequeue();
                    }
                    missedCalls.Enqueue(call);
                }
            }
            catch (Exception e)
            {
                log.Error("Error while adding missed call: " + e.Message);
            }
        }

        private void AddPlacedCall(Call call)
        {
            try
            {
                if (isExists(call, CallType.placed))
                {
                    if (placedCalls.Count == 100)
                    {
                        placedCalls.Dequeue();
                    }
                    placedCalls.Enqueue(call);
                }
            }
            catch (Exception e)
            {
                log.Error("Error while adding placed call: " + e.Message);
            }
        }

        private void AddReceivedCall(Call call)
        {
            try
            {
                if (isExists(call, CallType.received))
                {
                    if (receivedCalls.Count == 100)
                    {
                        receivedCalls.Dequeue();
                    }
                    receivedCalls.Enqueue(call);
                }
            }
            catch (Exception e)
            {
                log.Error("Error while adding received call: " + e.Message);
            }
        }

        private bool isExists(Call call, CallType type)
        {
            bool isnew = true;
            switch (type)
            {
                case CallType.missed:
                    foreach (Call c in missedCalls)
                    {
                        if (c.callId == call.callId)
                        {
                            isnew = false;
                            break;
                        }
                    }
                    break;
                case CallType.placed:
                    foreach (Call c in placedCalls)
                    {
                        if (c.callId == call.callId)
                        {
                            isnew = false;
                            break;
                        }
                    }
                    break;
                case CallType.received:
                    foreach (Call c in receivedCalls)
                    {
                        if (c.callId == call.callId)
                        {
                            isnew = false;
                            break;
                        }
                    }
                    break;
            }
            return isnew;
        }

        public override bool Equals(object obj)
        {
            bool b = base.Equals(obj);
            try
            {
                if (b)
                {
                    if (obj != null)
                    {
                        if (obj is LineControl)
                        {
                            LineControl lc = obj as LineControl;
                            if (lc.directoryNumber == this.directoryNumber)
                            {
                                if (lc.lineControlConnection != null)
                                {
                                    foreach (LineControlConnection lcc in lc.lineControlConnection)
                                    {
                                        //find same connection
                                        if (this.lineControlConnection != null)
                                        {
                                            foreach (LineControlConnection lcci in this.lineControlConnection)
                                            {
                                                if (lcci.callid == lcc.callid)
                                                {
                                                    if (!lcci.Equals(lcc))
                                                    {
                                                        b = false;
                                                        break;
                                                    }
                                                }
                                                else
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
                                        if (!b)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.lineControlConnection != null)
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
                            throw new Exception("Can't evaluate this object " + this.directoryNumber + ", " + obj.ToString());
                        }
                    }
                }
                return b;
            }
            catch (Exception e)
            {
                log.Error("Unable to compare linecontrol: " + e.Message);
                return b;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string toString = "";
            try
            {
                toString += this.directoryNumber + " ";
                toString += "status: " + this.status.ToString() + " ";
                toString += "do not disturb: " + this.doNotDisturb.ToString() + " ";
                toString += "mwi: " + this.mwiOn.ToString() + " ";
                toString += "forward: " + this.forward + " ";
                toString += "monitored: " + this.monitored + " ";
                if (this.lineControlConnection != null)
                {
                    toString += " linecontrolconnections: ";
                    foreach (LineControlConnection lcc in this.lineControlConnection)
                    {
                        toString += lcc.ToString();
                    }
                }
                return toString;
            }
            catch (Exception e)
            {
                log.Error("Unable to getString: " + e.Message);
                return toString;
            }
        }

        private int CallSort(Call c1, Call c2)
        {
            int result = 0;
            try
            {
                switch (sort)
                {
                    case "startTime":
                        result = -DateTime.Compare(c1.startTime, c2.startTime);
                        break;
                    case "caller":
                        result = String.Compare(c1.caller, c2.caller);
                        break;
                    case "callee":
                        result = String.Compare(c1.callee, c2.callee);
                        break;
                    default:
                        result = -DateTime.Compare(c1.startTime, c2.startTime);
                        break;
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            return result;
        }
    }

    /// <summary>
    /// An active connection
    /// </summary>
    [Serializable()]
    public class LineControlConnection
    {
        private ConnectionState _state;
        private ConnectionState _remoteState;
        private TerminalState _terminalState;
        private string _contact;
        private string _callid;

        public LineControlConnection()
        {
        }

        public TerminalState terminalState
        {
            get { return _terminalState; }
            set { _terminalState = value; }
        }

        public ConnectionState state
        {
            get { return _state; }
            set { _state = value; }
        }

        public ConnectionState remoteState
        {
            get { return _remoteState; }
            set { _remoteState = value; }
        }

        public string contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        public string callid
        {
            get { return _callid; }
            set { _callid = value; }
        }

        public override bool Equals(object obj)
        {
            bool b = false;
            if (obj is LineControlConnection)
            {
                LineControlConnection lcc = obj as LineControlConnection;
                if (lcc.callid == this.callid && this.contact == this.contact && lcc.remoteState == this.remoteState && lcc.state == this.state && lcc.terminalState == this.terminalState)
                {
                    b = true;
                }
            }
            else
            {
                throw new Exception("Can't evaluate this object " + this.callid + ", " + obj.ToString());
            }
            return b;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string toString = "[";
            toString += "callid: " + this.callid + " ";
            toString += "contact: " + this.contact + " ";
            toString += "state: " + this.state.ToString() + " ";
            toString += "remote state: " + this.remoteState.ToString() + " ";
            toString += "terminal state: " + this.terminalState.ToString() + " ";
            toString += "] ";
            return toString;
        }
     }

    /// <summary>
    /// Connection states
    /// </summary>
    public enum ConnectionState
    {
        unknown,
        idle,
        failed,
        disconnected,
        established,
        alerting,
        offered,
        queued,
        network_reached,
        network_alerting,
        initiated,
        dialing
    }

    /// <summary>
    /// Terminal states
    /// </summary>
    public enum TerminalState
    {
        idle,
        ringing,
        talking,
        held,
        bridged,
        inuse,
        dropped,
        unknown
    }

    /// <summary>
    /// Call description
    /// </summary>
    [Serializable()]
    public class Call
    {
        private string _caller;
        private string _callee;
        private string _callId;
        private CallType _type;
        private DateTime _startTime;
        private DateTime _endTime;

        /// <summary>
        /// Caller
        /// </summary>
        public string caller
        {
            get { return _caller; }
            set { _caller = value; }
        }

        /// <summary>
        /// Callee
        /// </summary>
        public string callee
        {
            get { return _callee; }
            set { _callee = value; }
        }

        /// <summary>
        /// Call id
        /// </summary>
        public string callId
        {
            get { return _callId; }
            set { _callId = value; }
        }

        /// <summary>
        /// Call type
        /// <seealso cref="CallType"/>
        /// </summary>
        public CallType type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Call date
        /// </summary>
        public DateTime startTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        /// <summary>
        /// Call end date
        /// </summary>
        public DateTime endTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public Call()
        {
        }

        public override string ToString()
        {
            string toString = "";
            toString += "caller: " + this.caller + " ";
            toString += "callee: " + this.callee + " ";
            toString += "callid: " + this.callId + " ";
            toString += "type: " + this.type + " ";
            toString += "startDateTime: " + this.startTime.ToString() + " ";
            toString += "endDateTime: " + this.endTime.ToString();
            return toString;
        }
    }

    /// <summary>
    /// Call type
    /// </summary>
    public enum CallType
    {
        /// <summary>
        /// missed call
        /// </summary>
        missed,
        /// <summary>
        /// placed call
        /// </summary>
        placed,
        /// <summary>
        /// received call
        /// </summary>
        received
    }
}
