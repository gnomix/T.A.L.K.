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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wybecom.TalkPortal.CTI.Controls;
using System.Web.Configuration;

namespace Wybecom.TalkPortal
{
    public partial class CTIControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //CTIClient
                ddlAuthenticationMode.DataSource = Enum.GetNames(typeof(Wybecom.TalkPortal.CTI.Controls.AuthenticationMode));
                ddlAuthenticationMode.DataBind();
                ddlCodifMode.DataSource = Enum.GetNames(typeof(Wybecom.TalkPortal.CTI.Controls.CodifMode));
                ddlCodifMode.DataBind();
                cbDisplay.Checked = CTIClient1.display;
                cbDisplayCopyright.Checked = CTIClient1.displayCopyright;
                cbDisplayInput.Checked = CTIClient1.displayInput;
                cbDisplayPhoneControl.Checked = CTIClient1.displayPhoneControl;
                cbDisplayStatus.Checked = CTIClient1.displayStatus;
                tbExtension.Text = CTIClient1.monitoredLine;
                ddlAuthenticationMode.SelectedIndex = ddlAuthenticationMode.Items.IndexOf(ddlAuthenticationMode.Items.FindByValue(Enum.GetName(typeof(Wybecom.TalkPortal.CTI.Controls.AuthenticationMode),CTIClient1.Mode)));
                tbUser.Text = CTIClient1.User;
                tbPassword.Text = CTIClient1.Password;
                tbExtension.Text = CTIClient1.monitoredLine;
                tbCTIService.Text = CTIClient1.ctiService;
                tbStateService.Text = CTIClient1.stateService;
                tbDMD.Text = CTIClient1.dmdService;
                tbCTICallLogsService.Text = CTIClient1.callLogsService;
                cbVM.Checked = CTIClient1.enableMevo;
                tbVM.Text = CTIClient1.MevoPilot;
                cbCodif.Checked = CTIClient1.enableCodif;
                ddlCodifMode.SelectedIndex = ddlCodifMode.Items.IndexOf(ddlCodifMode.Items.FindByValue(Enum.GetName(typeof(Wybecom.TalkPortal.CTI.Controls.CodifMode), CTIClient1.CodifMode)));
                cbTransfer.Checked = CTIClient1.enableTransfer;
                cbConsultTransfer.Checked = CTIClient1.enableConsultTransfer;
                cbHold.Checked = CTIClient1.enableHold;
                cbDND.Checked = CTIClient1.enableDnd;
                cbDirectory.Checked = CTIClient1.enableDirectory;
                cbCallLogs.Checked = CTIClient1.enableCallLogs;
                cbAgent.Checked = CTIClient1.enableAgent;
                //DMDControl
                tbcticlient.Text = DMDClient1.CTIClientID;
                tbDirectoryName.Text = DMDClient1.DirectoryName;
                tbDdmd.Text = DMDClient1.dmdService;
                cbdconsulttransfer.Checked = DMDClient1.enableConsultTransfer;
                cbdgsmconsulttransfer.Checked = DMDClient1.enableGSMConsultTransfer;
                cbdgsmtransfer.Checked = DMDClient1.enableGSMTransfer;
                cbdTransfer.Checked = DMDClient1.enableTransfer;
                cbdtransferlookup.Checked = DMDClient1.EnableTransferLookup;
                
                foreach (Wybecom.TalkPortal.DMD.Controls.FilterControl fc in DMDClient1.FilterGroup )
                {
                    lbFilters.Items.Add(new ListItem(fc.Label, fc.Filter));
                }
                cbPage.Checked = DMDClient1.PageEnabled;
                tbrheader.Text =DMDClient1.ResultsHeader;
                tbsheader.Text = DMDClient1.SearchHeader;
                cbDMDDisplay.Checked =DMDClient1.Show;
                cbheader.Checked = DMDClient1.ShowHeader;
                cbSort.Checked = DMDClient1.SortEnabled;
                tbtarget.Text =DMDClient1.TargetControlID;

                //CallLogs
                tbclcss.Text = CallLogsControl1.CssClass;
                tbclcti.Text = CallLogsControl1.CTIClientID;
                tbclservice.Text = CallLogsControl1.callLogsService;
                tbcldmd.Text = CallLogsControl1.directoryService;
                cbpresence.Checked = CallLogsControl1.presenceEnabled;
                cbcllookup.Checked = CallLogsControl1.lookupEnabled;
                tbdnlength.Text = CallLogsControl1.dirNumLength.ToString();
                tbmissed.Text = CallLogsControl1.missedTabText;
                tbplaced.Text = CallLogsControl1.placedTabText;
                tbreceived.Text = CallLogsControl1.receivedTabText;
                tbempty.Text = CallLogsControl1.emptyCallLogsText;
            }
        }

        protected void BtnApply_Click(object sender, EventArgs e)
        {
            saveCTI();
            saveCallLogs();
            saveDMD();
        }

        protected void BtnApplyDMD_Click(object sender, EventArgs e)
        {
            saveDMD();
            saveCallLogs();
            saveCTI();
        }

        protected void btnAddFilter_Click(object sender, EventArgs e)
        {
            if (tbFilterName.Text != "" && tbfiltervalue.Text != "")
            {
                lbFilters.Items.Add(new ListItem(tbFilterName.Text, tbfiltervalue.Text));
                tbFilterName.Text = "";
                tbfiltervalue.Text = "";
            }
        }

        protected void btnDeleteFilter_Click(object sender, EventArgs e)
        {
            lbFilters.Items.Remove(lbFilters.SelectedItem);
        }

        protected void lbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilterName.Text = lbFilters.SelectedItem.Text;
            tbfiltervalue.Text = lbFilters.SelectedItem.Value;
        }

        protected void btncl_Click(object sender, EventArgs e)
        {
            saveCallLogs();
            saveDMD();
            saveCTI();
        }

        private void saveCallLogs()
        {
            CallLogsControl1.CssClass = tbclcss.Text;
            if (tbclcti.Text != null)
            {
                CallLogsControl1.CTIClientID = tbclcti.Text;
            }
            CallLogsControl1.callLogsService = tbclservice.Text;
            CallLogsControl1.directoryService = tbcldmd.Text;
            CallLogsControl1.presenceEnabled = cbpresence.Checked;
            CallLogsControl1.lookupEnabled = cbcllookup.Checked;
            CallLogsControl1.dirNumLength = Int32.Parse(tbdnlength.Text);
            CallLogsControl1.missedTabText = tbmissed.Text;
            CallLogsControl1.placedTabText = tbplaced.Text;
            CallLogsControl1.receivedTabText = tbreceived.Text;
            CallLogsControl1.emptyCallLogsText = tbempty.Text;
        }

        private void saveDMD()
        {
            if (tbcticlient.Text != "")
            {
                DMDClient1.CTIClientID = tbcticlient.Text;
            }
            DMDClient1.DirectoryName = tbDirectoryName.Text;
            DMDClient1.dmdService = tbDdmd.Text;
            DMDClient1.enableConsultTransfer = cbdconsulttransfer.Checked;
            DMDClient1.enableGSMConsultTransfer = cbdgsmconsulttransfer.Checked;
            DMDClient1.enableGSMTransfer = cbdgsmtransfer.Checked;
            DMDClient1.enableTransfer = cbdTransfer.Checked;
            DMDClient1.EnableTransferLookup = cbdtransferlookup.Checked;
            DMDClient1.FilterGroup.Clear();
            foreach (ListItem li in lbFilters.Items)
            {
                Wybecom.TalkPortal.DMD.Controls.FilterControl fc = new Wybecom.TalkPortal.DMD.Controls.FilterControl(li.Text, li.Value);
                fc.ID = fc.Label + "_" + fc.Filter + "_FilterControl";
                DMDClient1.FilterGroup.Add(fc);
            }
            DMDClient1.PageEnabled = cbPage.Checked;
            DMDClient1.ResultsHeader = tbrheader.Text;
            DMDClient1.SearchHeader = tbsheader.Text;
            DMDClient1.Show = cbDMDDisplay.Checked;
            DMDClient1.ShowHeader = cbheader.Checked;
            DMDClient1.SortEnabled = cbSort.Checked;
            if (tbtarget.Text != "")
            {
                DMDClient1.TargetControlID = tbtarget.Text;
            }
        }

        private void saveCTI()
        {
            CTIClient1.display = cbDisplay.Checked;
            CTIClient1.displayCopyright = cbDisplayCopyright.Checked;
            CTIClient1.displayInput = cbDisplayInput.Checked;
            CTIClient1.displayPhoneControl = cbDisplayPhoneControl.Checked;
            CTIClient1.displayStatus = cbDisplayStatus.Checked;
            CTIClient1.monitoredLine = tbExtension.Text;
            CTIClient1.Mode = (Wybecom.TalkPortal.CTI.Controls.AuthenticationMode)Enum.Parse(typeof(Wybecom.TalkPortal.CTI.Controls.AuthenticationMode),ddlAuthenticationMode.SelectedValue);
            CTIClient1.CodifMode = (Wybecom.TalkPortal.CTI.Controls.CodifMode)Enum.Parse(typeof(Wybecom.TalkPortal.CTI.Controls.CodifMode), ddlCodifMode.SelectedValue);
            switch (CTIClient1.Mode)
            {
                case Wybecom.TalkPortal.CTI.Controls.AuthenticationMode.manual:
                    CTIClient1.User = tbUser.Text;
                    CTIClient1.Password = tbPassword.Text;
                    break;
            }
            CTIClient1.ctiService = tbCTIService.Text;
            CTIClient1.stateService = tbStateService.Text;
            CTIClient1.dmdService = tbDMD.Text;
            CTIClient1.callLogsService = tbCTICallLogsService.Text;
            CTIClient1.enableMevo = cbVM.Checked;
            CTIClient1.enableCodif = cbCodif.Checked;
            CTIClient1.MevoPilot = tbVM.Text;
            CTIClient1.enableTransfer = cbTransfer.Checked;
            CTIClient1.enableConsultTransfer = cbConsultTransfer.Checked;
            CTIClient1.enableHold = cbHold.Checked;
            CTIClient1.enableDnd = cbDND.Checked;
            CTIClient1.enableDirectory = cbDirectory.Checked;
            CTIClient1.enableCallLogs = cbCallLogs.Checked;
            CTIClient1.enableAgent = cbAgent.Checked;
        }
    }
}
