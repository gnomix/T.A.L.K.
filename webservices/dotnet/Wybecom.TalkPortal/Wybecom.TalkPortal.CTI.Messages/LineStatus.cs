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

namespace Wybecom.TalkPortal.CTI
{
    /// <summary>
    /// Status about a line
    /// </summary>
    [Serializable()]
    public class LineStatus
    {
        private string _directoryNumber;
        private Status _status = Status.unknown;
        private string _forward = "";
        private bool _doNotDisturb = false;
        private bool _mwiOn = false;
        private string _monitored = "";

        public LineStatus()
        {
            this._status = Status.unknown;
        }

        public LineStatus(string dn)
        {
            this._status = Status.unknown;
            this.directoryNumber = dn;
        }

        public LineStatus(string dn, Status state)
        {
            this.directoryNumber = dn;
            this.status = state;
        }

        /// <summary>
        /// The line extension
        /// </summary>
        public string directoryNumber
        {
            get { return _directoryNumber; }
            set { _directoryNumber = value; }
        }

        /// <summary>
        /// The forward destination
        /// </summary>
        public string forward
        {
            get {
                if (_forward == null)
                {
                    _forward = "";
                }
                return _forward; 
            }
            set { _forward = value; }
        }

        /// <summary>
        /// DND status
        /// </summary>
        public bool doNotDisturb
        {
            get { return _doNotDisturb; }
            set { _doNotDisturb = value; }
        }

        /// <summary>
        /// MWI status
        /// </summary>
        public bool mwiOn
        {
            get { return _mwiOn; }
            set { _mwiOn = value; }
        }

        /// <summary>
        /// Line status
        /// <seealso cref="Wybecom.TalkPortal.CTI.Status"/>
        /// </summary>
        public virtual Status status
        {
            get { return this._status; }
            set { _status = value; }
        }

        public Status GetStatus()
        {
            return this._status;
        }

        /// <summary>
        /// Monitored status
        /// </summary>
        public string monitored
        {
            get { return _monitored; }
            set { _monitored = value; }
        }

        public override bool Equals(object obj)
        {
            bool b = false;
            try
            {
                if (obj != null)
                {
                    if (obj is LineStatus)
                    {
                        LineStatus lc = obj as LineStatus;
                        if (lc.directoryNumber == this.directoryNumber && lc.status == this.status && lc.doNotDisturb == this.doNotDisturb && lc.forward == this.forward && lc.mwiOn == this.mwiOn && lc.monitored == this.monitored)
                        {
                            b = true;
                        }
                    }
                    else
                    {
                        throw new Exception("Can't evaluate this object " + this._directoryNumber + ", " + obj.ToString());
                    }
                }
                return b;
            }
            catch
            {
                
                return b;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// Status
    /// </summary>
    public enum Status
    {
        available,
        donotdisturb,
        forwarded,
        busy,
        hidden,
        dialing,
        ringing,
        inactive,
        unknown
    }
}
