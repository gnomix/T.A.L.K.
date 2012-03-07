using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wybecom.TalkPortal.CTI.JTAPI.Proxy;
using log4net;

namespace Wybecom.TalkPortal.Providers
{
    public class TalkSnapshotProvider : SnapshotProvider
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _applicationName;
        private JTAPISnapshotServiceService _jtapisnapshot;

        public TalkSnapshotProvider()
        {
            _jtapisnapshot = new JTAPISnapshotServiceService();
        }

        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        public override Wybecom.TalkPortal.CTI.LineControl[] GetSnapshot()
        {
            List<Wybecom.TalkPortal.CTI.LineControl> lcs = new List<Wybecom.TalkPortal.CTI.LineControl>();
            LineControl[] jlcs = _jtapisnapshot.GetSnapshot();
            if (jlcs != null)
            {
                log.Debug(jlcs.Length + " lines retreived from snapshot");
                foreach (LineControl lc in jlcs)
                {
                    Wybecom.TalkPortal.CTI.LineControl ctilc = new Wybecom.TalkPortal.CTI.LineControl();
                    ctilc.directoryNumber = lc.directoryNumber;
                    ctilc.doNotDisturb = lc.doNotDisturb;
                    ctilc.forward = lc.forward;
                    ctilc.lineControlConnection = GetLineControlConnections(lc.lineControlConnection);
                    if (lc.monitored != null)
                    {
                        ctilc.monitored = lc.monitored;
                    }
                    else
                    {
                        ctilc.monitored = "";
                    }
                    ctilc.mwiOn = lc.mwiOn;
                    ctilc.status = (Wybecom.TalkPortal.CTI.Status)Translate(typeof(Status), lc.status, typeof(Wybecom.TalkPortal.CTI.Status));
                    lcs.Add(ctilc);
                }
            }
            return lcs.ToArray();
        }

        private object Translate(Type sourcetype, object totranslate, Type translatedtype)
        {
            return Enum.Parse(translatedtype, Enum.GetName(sourcetype,totranslate), true);
        }

        private Wybecom.TalkPortal.CTI.LineControlConnection[] GetLineControlConnections(LineControlConnection[] lc)
        {
            List<Wybecom.TalkPortal.CTI.LineControlConnection> lccs = new List<Wybecom.TalkPortal.CTI.LineControlConnection>();
            if (lc != null)
            {
                foreach (LineControlConnection lcc in lc)
                {
                    Wybecom.TalkPortal.CTI.LineControlConnection ctilcc = new Wybecom.TalkPortal.CTI.LineControlConnection();
                    ctilcc.callid = lcc.callid;
                    ctilcc.contact = lcc.contact;
                    ctilcc.remoteState = (Wybecom.TalkPortal.CTI.ConnectionState)Translate(typeof(ConnectionState),lcc.remoteState,typeof(Wybecom.TalkPortal.CTI.ConnectionState));
                    ctilcc.state = (Wybecom.TalkPortal.CTI.ConnectionState)Translate(typeof(ConnectionState), lcc.state, typeof(Wybecom.TalkPortal.CTI.ConnectionState));
                    ctilcc.terminalState = (Wybecom.TalkPortal.CTI.TerminalState)Translate(typeof(TerminalState), lcc.terminalState, typeof(Wybecom.TalkPortal.CTI.TerminalState));
                    lccs.Add(ctilcc);
                }
            }
            return lccs.ToArray();
            
        }
    }
}
