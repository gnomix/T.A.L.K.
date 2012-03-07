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
    public class QueueStatistics
    {
        private ushort loggedinagents;
        private ushort insessionagents;
        private ushort availableagents;
        private ushort unavailableagents;
        private ushort inworkagents;
        private ushort selectedagents;
        private uint callsinpriorityqueue1;
        private uint callsinpriorityqueue2;
        private uint callsinpriorityqueue3;
        private uint callsinpriorityqueue4;
        private uint callsinpriorityqueue5;
        private uint callsinpriorityqueue6;
        private uint callsinpriorityqueue7;
        private uint callsinpriorityqueue8;
        private uint callsinpriorityqueue9;
        private uint callsinpriorityqueue10;
        private uint starttime;
        private uint endtime;
        private uint totalcalls;
        private uint handledcalls;
        private uint oldestcallinqueue;
        private uint callsabandonned;
        private uint averagetalkduration;
        private uint averagewaitduration;
        private uint longesttalkduration;
        private uint longestwaitduration;
        private uint csqid;

        public QueueStatistics()
        {
        }

        public QueueStatistics(uint cId,
                    ushort loggedinagts, ushort insessionagts,
                    ushort availableagts, ushort unavailableagets,
                    ushort inworkagets, ushort selectedagets,
                    uint callsinpriorityq1, uint callsinpriorityq2,
                    uint callsinpriorityq3, uint callsinpriorityq4,
                    uint callsinpriorityq5, uint callsinpriorityq6,
                    uint callsinpriorityq7, uint callsinpriorityq8,
                    uint callsinpriorityq9, uint callsinpriorityq10,
                    uint starttm, uint endtm, uint totalc, uint handledc,
                    uint oldestcallinq, uint callsabd, uint averagetalkdur,
                    uint averagewaitdur, uint longesttalkdur,
                    uint longestwaitdur)
        {
            loggedinagents = loggedinagts;
            insessionagents = insessionagts;
            availableagents = availableagts;
            unavailableagents = unavailableagets;
            inworkagents = inworkagets;
            selectedagents = selectedagets;
            callsinpriorityqueue1 = callsinpriorityq1;
            callsinpriorityqueue2 = callsinpriorityq2;
            callsinpriorityqueue3 = callsinpriorityq3;
            callsinpriorityqueue4 = callsinpriorityq4;
            callsinpriorityqueue5 = callsinpriorityq5;
            callsinpriorityqueue6 = callsinpriorityq6;
            callsinpriorityqueue7 = callsinpriorityq7;
            callsinpriorityqueue8 = callsinpriorityq8;
            callsinpriorityqueue9 = callsinpriorityq9;
            callsinpriorityqueue10 = callsinpriorityq10;
            starttime = starttm;
            endtime = endtm;
            totalcalls = totalc;
            handledcalls = handledc;
            oldestcallinqueue = oldestcallinq;
            callsabandonned = callsabd;
            averagetalkduration = averagetalkdur;
            averagewaitduration = averagewaitdur;
            longesttalkduration = longesttalkdur;
            longestwaitduration = longestwaitdur;
            csqid = cId;
        }

        public ushort LoggedInAgents
        {
            get
            {
                return loggedinagents;
            }
            set
            {
                loggedinagents = value;
            }
        }

        public ushort InSessionAgents
        {
            get
            {
                return insessionagents;
            }
            set
            {
                insessionagents = value;
            }
        }

        public ushort AvailableAgents
        {
            get
            {
                return availableagents;
            }
            set
            {
                availableagents = value;
            }
        }

        public ushort UnavailableAgents
        {
            get
            {
                return unavailableagents;
            }
            set
            {
                unavailableagents = value;
            }
        }

        public ushort InWorkAgents
        {
            get
            {
                return inworkagents;
            }
            set
            {
                inworkagents = value;
            }
        }

        public ushort SelectedAgents
        {
            get
            {
                return selectedagents;
            }
            set
            {
                selectedagents = value;
            }
        }

        public uint CallsInPriority1
        {
            get
            {
                return callsinpriorityqueue1;
            }
            set
            {
                callsinpriorityqueue1 = value;
            }
        }

        public uint CallsInPriority2
        {
            get
            {
                return callsinpriorityqueue2;
            }
            set
            {
                callsinpriorityqueue2 = value;
            }
        }

        public uint CallsInPriority3
        {
            get
            {
                return callsinpriorityqueue3;
            }
            set
            {
                callsinpriorityqueue3 = value;
            }
        }

        public uint CallsInPriority4
        {
            get
            {
                return callsinpriorityqueue4;
            }
            set
            {
                callsinpriorityqueue4 = value;
            }
        }

        public uint CallsInPriority5
        {
            get
            {
                return callsinpriorityqueue5;
            }
            set
            {
                callsinpriorityqueue5 = value;
            }
        }

        public uint CallsInPriority6
        {
            get
            {
                return callsinpriorityqueue6;
            }
            set
            {
                callsinpriorityqueue6 = value;
            }
        }

        public uint CallsInPriority7
        {
            get
            {
                return callsinpriorityqueue7;
            }
            set
            {
                callsinpriorityqueue7 = value;
            }
        }

        public uint CallsInPriority8
        {
            get
            {
                return callsinpriorityqueue8;
            }
            set
            {
                callsinpriorityqueue8 = value;
            }
        }

        public uint CallsInPriority9
        {
            get
            {
                return callsinpriorityqueue9;
            }
            set
            {
                callsinpriorityqueue9 = value;
            }
        }

        public uint CallsInPriority10
        {
            get
            {
                return callsinpriorityqueue10;
            }
            set
            {
                callsinpriorityqueue10 = value;
            }
        }

        public uint StartTime
        {
            get
            {
                return starttime;
            }
            set
            {
                starttime = value;
            }
        }

        public uint EndTime
        {
            get
            {
                return endtime;
            }
            set
            {
                endtime = value;
            }
        }

        public uint TotalCalls
        {
            get
            {
                return totalcalls;
            }
            set
            {
                totalcalls = value;
            }
        }

        public uint HandledCalls
        {
            get
            {
                return handledcalls;
            }
            set
            {
                handledcalls = value;
            }
        }

        public uint OldestCallInQueue
        {
            get
            {
                return oldestcallinqueue;
            }
            set
            {
                oldestcallinqueue = value;
            }
        }

        public uint CallsAbandonned
        {
            get
            {
                return callsabandonned;
            }
            set
            {
                callsabandonned = value;
            }
        }

        public uint AverageTalkDuration
        {
            get
            {
                return averagetalkduration;
            }
            set
            {
                averagetalkduration = value;
            }
        }

        public uint AverageWaitDuration
        {
            get
            {
                return averagewaitduration;
            }
            set
            {
                averagewaitduration = value;
            }
        }

        public uint LongestTalkDuration
        {
            get
            {
                return longesttalkduration;
            }
            set
            {
                longesttalkduration = value;
            }
        }

        public uint LongestWaitDuration
        {
            get
            {
                return longestwaitduration;
            }
            set
            {
                longestwaitduration = value;
            }
        }

        public uint CSQID
        {
            get
            {
                return csqid;
            }
            set
            {
                csqid = value;
            }
        }
    }
}
