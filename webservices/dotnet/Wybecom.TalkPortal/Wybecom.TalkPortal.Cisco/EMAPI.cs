using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using log4net;
using System.Net;


namespace Wybecom.TalkPortal.Cisco
{
    public class EMAPI
    {
        public string emserver;
        public string emuser;
        public string empassword;
        private string url = "/emservice/EMServiceServlet";

        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EMAPI(string server, string user, string password)
        {
            emserver = server;
            emuser = user;
            empassword = password;
        }

        public Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.responseType getPhones(string[] users)
        {
            Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.queryType qt = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.queryType();
            qt.appInfo = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.appInfoType();
            qt.appInfo.appCertificate = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.appCertificateType();
            qt.appInfo.appID = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.appIDType();
            qt.appInfo.appCertificate.Value = empassword;
            qt.appInfo.appID.Value = emuser;
            qt.Item = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.userDevicesQueryType();
            List<Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.userIDType> usersid = new List<Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.userIDType>();
            foreach (string s in users)
            {
                Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.userIDType u = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.userIDType();
                u.Value = s;
                usersid.Add(u);
            }
            ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.userDevicesQueryType)qt.Item).userID = usersid.ToArray();
            return Send(qt);
        }

        public Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.responseType Login(string user, string phone, string profile)
        {
            Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.requestType rt = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.requestType();
            rt.appInfo = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.appInfoType();
            rt.appInfo.appCertificate = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.appCertificateType();
            rt.appInfo.appID = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.appIDType();
            rt.appInfo.appCertificate.Value = empassword;
            rt.appInfo.appID.Value = emuser;
            rt.Item = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.loginType();
            ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.loginType)rt.Item).deviceName = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.deviceNameType();
            ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.loginType)rt.Item).deviceName.Value = phone;
            ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.loginType)rt.Item).userID = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.userIDType();
            ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.loginType)rt.Item).userID.Value = user;
            if (profile != null && profile != "")
            {
                ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.loginType)rt.Item).deviceProfile = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.deviceProfileType();
                ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.loginType)rt.Item).deviceProfile.Value = profile;
            }
            return Send(rt);
        }

        public Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.responseType Logout(string device)
        {
            Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.requestType rt = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.requestType();
            rt.appInfo = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.appInfoType();
            rt.appInfo.appCertificate = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.appCertificateType();
            rt.appInfo.appID = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.appIDType();
            rt.appInfo.appCertificate.Value = empassword;
            rt.appInfo.appID.Value = emuser;
            rt.Item = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.logoutType();
            ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.logoutType)rt.Item).deviceName = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.deviceNameType();
            ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.logoutType)rt.Item).deviceName.Value = device;
            return Send(rt);
        }

        public Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.responseType getUsers(string[] phones)
        {
            Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.queryType qt = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.queryType();
            qt.appInfo = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.appInfoType();
            qt.appInfo.appCertificate = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.appCertificateType();
            qt.appInfo.appID = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.appIDType();
            qt.appInfo.appCertificate.Value = empassword;
            qt.appInfo.appID.Value = emuser;
            qt.Item = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.deviceUserQueryType();
            List<Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.deviceNameType> devices = new List<Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.deviceNameType>();
            foreach (string s in phones)
            {
                Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.deviceNameType d = new Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.deviceNameType();
                d.Value = s;
                devices.Add(d);
            }
            ((Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.deviceUserQueryType)qt.Item).deviceName = devices.ToArray();
            return Send(qt);
        }

        private string Serialize(Type type, object o)
        {
            XmlSerializer xs = new XmlSerializer(o.GetType());
            MemoryStream ms = new MemoryStream();
            xs.Serialize(ms, o);
            ms.Position = 0;
            return GetStringFromStream(ms);
        }

        private string GetStringFromStream(Stream s)
        {
            using (StreamReader sr = new StreamReader(s))
            {
                return sr.ReadToEnd();
            }
        }

        private Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.responseType Send(Wybecom.TalkPortal.Cisco.ExtensionMobility.Query.queryType q)
        {
            log.Debug("Envoi d'une requête: " + Serialize(q.GetType(), q));
            string uri = "http://" + emserver + url + "?" + q.ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.Accept = "text/*";
            StreamWriter sw = new StreamWriter(request.GetRequestStream());
            sw.Write(uri);
            sw.Flush();
            sw.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            XmlSerializer xs = new XmlSerializer(typeof(Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.responseType));
            Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.responseType r = (Wybecom.TalkPortal.Cisco.ExtensionMobility.QueryResponse.responseType)xs.Deserialize(sr);
            return r;
        }

        private Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.responseType Send(Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.requestType q)
        {
            log.Debug("Envoi d'une requête: " + Serialize(q.GetType(), q));
            string uri = "http://" + emserver + url + "?" + q.ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.Accept = "text/*";
            StreamWriter sw = new StreamWriter(request.GetRequestStream());
            sw.Write(uri);
            sw.Flush();
            sw.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            XmlSerializer xs = new XmlSerializer(typeof(Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.responseType));
            Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.responseType r = (Wybecom.TalkPortal.Cisco.ExtensionMobility.Request.responseType)xs.Deserialize(sr);
            return r;
        }
    }
}
