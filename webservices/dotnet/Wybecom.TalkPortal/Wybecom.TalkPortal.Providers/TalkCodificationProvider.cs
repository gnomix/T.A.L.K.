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
using log4net;
using System.Configuration.Provider;
using Wybecom.TalkPortal.CTI.Proxy.CLS;
using Wybecom.TalkPortal.CTI;

namespace Wybecom.TalkPortal.Providers
{
    public class TalkCodificationProvider : CodificationProvider
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _applicationName;
        private string _connectionStringName;
        private CodifEntities _codif;

        public TalkCodificationProvider()
        {
            _codif = new CodifEntities("name=" + connectionStringName);
        }

        public string connectionStringName
        {
            get
            {
                if (String.IsNullOrEmpty(_connectionStringName))
                {
                    return "CodifEntities";
                }
                else
                {
                    return _connectionStringName;
                }
            }
            set
            {
                _connectionStringName = value;
            }
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

        public override void AddCall(Wybecom.TalkPortal.CTI.Call call, string extension)
        {
            Calls ccall = new Calls();
            ccall.callee = call.callee;
            ccall.caller = call.caller;
            ccall.id = call.callId;
            ccall.calltype = call.type.ToString();
            ccall.startdatetime = call.startTime;
            ccall.extension = extension;
            ccall.enddatetime = call.endTime;
            _codif.AddToCalls(ccall);
            _codif.SaveChanges();

        }

        public override Wybecom.TalkPortal.CTI.Proxy.CLS.Call GetCall(string callid, string extension)
        {
            Wybecom.TalkPortal.CTI.Proxy.CLS.Call c = null;
            try
            {
                Calls ccall = _codif.Calls.First(call => call.id == callid && call.extension == extension);
                c = new Wybecom.TalkPortal.CTI.Proxy.CLS.Call();
                c.callee = ccall.callee;
                c.caller = ccall.caller;
                c.callId = ccall.id;
                c.startTime = ccall.startdatetime;
                c.endTime = ccall.enddatetime;
                c.type = (Wybecom.TalkPortal.CTI.Proxy.CLS.CallType)Enum.Parse(typeof(Wybecom.TalkPortal.CTI.Proxy.CLS.CallType), ccall.calltype);
            }
            catch (Exception e)
            {
                log.Debug("Error while get call " + callid + ", " + extension + ": " + e.ToString());
            }
            return c;
        }

        public override int CodifCall(string callid, string extension, string codif)
        {
            int result = 0;
            Calls ccall = null;
            try {
                ccall = _codif.Calls.First(call => call.id == callid && call.extension == extension);
                Codif cdf = _codif.Codif.First(cod => cod.codif1 == codif);
                ccall.Codif = cdf;
            
                _codif.SaveChanges();
            }
            catch (Exception codifException)
            {
                log.Error("Error while write call result for " + extension + ", " + callid + ": " + codifException.ToString()); 
            }
            if (ccall != null)
            {
                log.Debug("Trying to retreive last calls...");
                try
                {
                    result = _codif.Calls.Count(call => call.extension == extension && call.calltype == ccall.calltype && call.startdatetime > ccall.enddatetime);
                    //if (lastcall != null)
                    //{
                      //  log.Debug("First last call is " + lastcall.callid);
                        //result = _codif.Calls.SkipWhile(call => call.extension == extension && call.startdatetime > ccall.enddatetime && call.calltype == ccall.calltype).Count();
                      //  result = 1;
                    //}
                }
                catch (Exception e)
                {
                    log.Error("Error while check if this call was the last placed call: " + e.ToString());
                }
            }
            return result;

        }

        public override string[] GetCodif(bool active)
        {
            return _codif.Codif.Where(codif => codif.active == active).Select(cod => cod.codif1).ToArray();
        }

        public override void AddCodif(string codif)
        {
            Codif cod = new Codif();
            cod.active = true;
            cod.codif1 = codif;

            _codif.AddToCodif(cod);
            _codif.SaveChanges();
        }

        public override void DeleteCodif(string codif)
        {
            _codif.DeleteObject(_codif.Codif.First(cod => cod.codif1 == codif));
            _codif.SaveChanges();
        }

        public override void EditCodif(string oldcodif, string newcodif)
        {
            _codif.Codif.First(cod => cod.codif1 == oldcodif).codif1 = newcodif;
            _codif.SaveChanges();
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "TalkCodificationProvider";
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Talk Codification Provider");
            }
            base.Initialize(name, config);
            _applicationName = config["applicationName"];

            if (String.IsNullOrEmpty(_applicationName))
                _applicationName = "/";
            config.Remove("applicationName");

            connectionStringName = config["connectionStringName"];

            if (!String.IsNullOrEmpty(config["connectionStringName"]))
            {
                config.Remove("connectionStringName");
            }
            
            if (config.Count > 0)
            {
                string attr = config.Get(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized attribute: " + attr);
            }
        }

       
    }
}
