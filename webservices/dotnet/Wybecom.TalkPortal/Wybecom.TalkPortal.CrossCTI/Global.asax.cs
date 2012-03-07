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
using System.Web.Security;
using System.Web.SessionState;
using log4net;

namespace Wybecom.TalkPortal.CrossCTI
{
    /// <summary>
    /// ProxyWebServices
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        
        /// <summary>
        /// Starts ProxyWebServices
        /// </summary>
        /// 
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Debug("Cross CTI Web Services Starting...");
        }

        /// <summary>
        /// Unused
        /// </summary>
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Unused
        /// </summary>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Unused
        /// </summary>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Unused
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Unused
        /// </summary>
        protected void Session_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Unused
        /// </summary>
        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}