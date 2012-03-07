using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wybecom.TalkPortal.Connectors.TAPI.Client;
using log4net;

namespace Wybecom.TalkPortal.Providers
{
    public class TAPISnapshotProvider : SnapshotProvider
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _applicationName;
        private TAPISnapshotServer _snapshotServer;

        public TAPISnapshotProvider()
        {
            _snapshotServer = new TAPISnapshotServer();
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
            LineControl[] jlcs = _snapshotServer.GetSnapshot();
            
            if (jlcs != null)
            {
                log.Debug(jlcs.Length + " lines retreived from snapshot");
                foreach (LineControl lc in jlcs)
                {
                    Wybecom.TalkPortal.CTI.LineControl ctilc = new Wybecom.TalkPortal.CTI.LineControl();
                    ctilc.directoryNumber = lc.directoryNumberField;
                    ctilc.doNotDisturb = lc.doNotDisturbField;
                    ctilc.forward = lc.forwardField;
                    ctilc.lineControlConnection = GetLineControlConnections(lc.lineControlConnectionField);
                    ctilc.monitored = lc.monitoredField;
                    ctilc.mwiOn = lc.mwiOnField;
                    ctilc.status = (Wybecom.TalkPortal.CTI.Status)Translate(typeof(Status), lc.statusField, typeof(Wybecom.TalkPortal.CTI.Status));
                    lcs.Add(ctilc);
                }
            }
            log.Debug("Snapshot contains " + lcs.Count + " lines");
            return lcs.ToArray();
        }

        private object Translate(Type sourcetype, object totranslate, Type translatedtype)
        {
            return Enum.Parse(translatedtype, Enum.GetName(sourcetype, totranslate), true);
        }

        private Wybecom.TalkPortal.CTI.LineControlConnection[] GetLineControlConnections(LineControlConnection[] lc)
        {
            List<Wybecom.TalkPortal.CTI.LineControlConnection> lccs = null;
            if (lc != null)
            {
                lccs = new List<Wybecom.TalkPortal.CTI.LineControlConnection>();
                foreach (LineControlConnection lcc in lc)
                {
                    if (lcc != null)
                    {
                        Wybecom.TalkPortal.CTI.LineControlConnection ctilcc = new Wybecom.TalkPortal.CTI.LineControlConnection();
                        ctilcc.callid = lcc.callidField;
                        ctilcc.contact = lcc.contactField;
                        ctilcc.remoteState = (Wybecom.TalkPortal.CTI.ConnectionState)Translate(typeof(ConnectionState), lcc.remoteStateField, typeof(Wybecom.TalkPortal.CTI.ConnectionState));
                        ctilcc.state = (Wybecom.TalkPortal.CTI.ConnectionState)Translate(typeof(ConnectionState), lcc.stateField, typeof(Wybecom.TalkPortal.CTI.ConnectionState));
                        ctilcc.terminalState = (Wybecom.TalkPortal.CTI.TerminalState)Translate(typeof(TerminalState), lcc.terminalStateField, typeof(Wybecom.TalkPortal.CTI.TerminalState));
                        lccs.Add(ctilcc);
                    }
                }
            }
            if (lccs != null)
            {
                return lccs.ToArray();
            }
            else
            {
                return null;
            }

        }
    }
}
