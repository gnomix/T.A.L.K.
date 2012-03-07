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
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Wybecom.TalkPortal.DMD;

namespace Wybecom.TalkPortal
{
    public partial class DMDEdit : System.Web.UI.Page
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["dir"] == null)
            {
                log.Error("Unexpected url...transfer to home page.");
                Server.Transfer("DMDMgt.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    DirectoryType dir = Global.GetDirectory(Request.QueryString["dir"]);
                    if (dir != null)
                    {
                        InitDirectory(dir);
                    }
                    else
                    {
                        log.Error("The specified directory doesn't exist.");
                    }
                }
            }
        }

        private void InitDirectory(DirectoryType dir)
        {
            tbDirectoryName.Text = dir.name;
            if (dir.Item is SqlDatasourceType)
            {
                rblDirectoryType.Items.FindByValue("SQL").Selected = true;
                SqlDatasourceType sdt = (SqlDatasourceType)dir.Item;
                tbSqlDsn.Text = sdt.dsn;
                tbSqlUid.Text = sdt.uid;
                tbSqlPwd.Text = sdt.pwd;
                tbSqlCommand.Text = sdt.command;
                tbSqlFilter.Text = sdt.sqlFilter;
                foreach (FieldFormatter ffs in sdt.fieldFormatters)
                {
                    lbSQLFieldFormatters.Items.Add(new ListItem(ffs.fieldName, ffs.value + "#" + ffs.fieldType.ToString()));
                }
                if (sdt.ipphonefilter != null)
                {
                    tbSQLFirstNameFilterMap.Text = sdt.ipphonefilter.firstnamemap;
                    tbSQLLastNameFilterMap.Text = sdt.ipphonefilter.lastnamemap;
                    tbSQLTelephoneNumberFilterMap.Text = sdt.ipphonefilter.telephonenumbermap;
                }
                ShowSQL();
            }
            else if (dir.Item is LdapDatasourceType)
            {
                rblDirectoryType.Items.FindByValue("LDAP").Selected = true;
                LdapDatasourceType ldt = (LdapDatasourceType)dir.Item;
                tbLdapServer.Text = ldt.server;
                ddlLdapAuthentication.Items.FindByText(ldt.authenticationType).Selected = true;
                tbLdapUser.Text = ldt.user;
                tbLdapUserPassword.Text = ldt.userPassword;
                tbLdapTargetOu.Text = ldt.targetOU;
                tbLdapFilter.Text = ldt.ldapFilter;
                tbLdapPageSize.Text = ldt.pageSize.ToString();
                tbLdapNbPages.Text = ldt.nbPages.ToString();
                foreach (string attribut in ldt.ldapAttributes)
                {
                    lbLdapAttribute.Items.Add(new ListItem(attribut, attribut));
                }
                foreach (FieldFormatter ffl in ldt.fieldFormatters)
                {
                    lbLdapFieldFormatters.Items.Add(new ListItem(ffl.fieldName, ffl.value + "#" + ffl.fieldType.ToString()));
                }
                if (ldt.ipphonefilter != null)
                {
                    tbLdapFirstNameFilterMap.Text = ldt.ipphonefilter.firstnamemap;
                    tbLdapLastNameFilterMap.Text = ldt.ipphonefilter.lastnamemap;
                    tbLdapTelephoneNumberFilterMap.Text = ldt.ipphonefilter.telephonenumbermap;
                }
                ShowLdap();
            }
            else if (dir.Item is CiscoDatasourceType)
            {
                rblDirectoryType.Items.FindByValue("CISCO").Selected = true;
                CiscoDatasourceType ldt = (CiscoDatasourceType)dir.Item;
                tbCiscoServer.Text = ldt.server;
                tbAXLUser.Text = ldt.axluser;
                tbAXLUserPassword.Text = ldt.axluserpwd;
                foreach (FieldFormatter ffc in ldt.fieldFormatters)
                {
                    lbCiscoFieldFormatters.Items.Add(new ListItem(ffc.fieldName, ffc.value + "#" + ffc.fieldType.ToString()));
                }
                if (ldt.ipphonefilter != null)
                {
                    tbFirstNameFilterMap.Text = ldt.ipphonefilter.firstnamemap;
                    tbLastNameFilterMap.Text = ldt.ipphonefilter.lastnamemap;
                    tbTelephoneNumberFilterMap.Text = ldt.ipphonefilter.telephonenumbermap;
                }
                ShowCisco();
            }
            else
            {
                log.Error("Datasource type unknown...cannot load " + dir.name);
            }
        }

        protected void rblDirectoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblDirectoryType.SelectedValue == "SQL")
            {
                ShowSQL();
            }
            else if (rblDirectoryType.SelectedValue == "LDAP")
            {
                ShowLdap();
            }
            else if (rblDirectoryType.SelectedValue == "CISCO")
            {
                ShowCisco();
            }
        }

        private void ShowSQL()
        {
            pnlLdapSettings.Visible = false;
            pnlCiscoSettings.Visible = false;
            pnlSqlSettings.Visible = true;
        }

        private void ShowLdap()
        {
            pnlLdapSettings.Visible = true;
            pnlSqlSettings.Visible = false;
            pnlCiscoSettings.Visible = false;
        }

        private void ShowCisco()
        {
            pnlLdapSettings.Visible = false;
            pnlSqlSettings.Visible = false;
            pnlCiscoSettings.Visible = true;
        }

        protected void btnTestSql_Click(object sender, EventArgs e)
        {
            TestSQL();
            ModalPopupExtender1.Show();
        }

        protected void btnTestCisco_Click(object sender, EventArgs e)
        {
            TestCisco();
            ModalPopupExtender1.Show();
        }

        protected void tbAddSQLFieldFormatter_Click(object sender, EventArgs e)
        {
            AddSqlFieldFormatter();
            EmptySqlFieldFormatter();
        }

        
        private void AddSqlFieldFormatter()
        {
            ListItem li = lbSQLFieldFormatters.Items.FindByText(tbFieldFormatterFieldName.Text);
            if (li == null && tbFieldFormatterFieldName.Text != "" && tbFieldFormatterValue.Text != "")
            {
                lbSQLFieldFormatters.Items.Add(new ListItem(tbFieldFormatterFieldName.Text, tbFieldFormatterValue.Text + "#" + ddlSQLFieldType.SelectedValue));
            }
            else
            {
                li.Value = tbFieldFormatterValue.Text;
                li.Value += "#" + ddlSQLFieldType.SelectedValue;
            }
        }

        private void AddCiscoFieldFormatter()
        {
            ListItem li = lbCiscoFieldFormatters.Items.FindByText(tbCiscoFieldFormatterFieldName.Text);
            if (li == null && tbCiscoFieldFormatterFieldName.Text != "" && tbCiscoFieldFormatterValue.Text != "")
            {
                lbCiscoFieldFormatters.Items.Add(new ListItem(tbCiscoFieldFormatterFieldName.Text, tbCiscoFieldFormatterValue.Text + "#" + ddlCiscoFieldType.SelectedValue));
            }
            else
            {
                li.Value = tbCiscoFieldFormatterValue.Text;
                li.Value += "#" + ddlCiscoFieldType.SelectedValue;
            }
        }

        private void EmptySqlFieldFormatter()
        {
            tbFieldFormatterValue.Text = "";
            tbFieldFormatterFieldName.Text = "";
        }

        private void EmptyCiscoFieldFormatter()
        {
            tbCiscoFieldFormatterFieldName.Text = "";
            tbCiscoFieldFormatterValue.Text = "";
        }

        protected void btnDeleteSQLFieldFormatter_Click(object sender, EventArgs e)
        {
            DeleteSqlFieldFormatter();
            EmptySqlFieldFormatter();
        }

        protected void btnDeleteCiscoFieldFormatter_Click(object sender, EventArgs e)
        {
            DeleteCiscoFieldFormatter();
            EmptyCiscoFieldFormatter();
        }

        private void DeleteSqlFieldFormatter()
        {
            lbSQLFieldFormatters.Items.Remove(lbSQLFieldFormatters.SelectedItem);
        }

        private void DeleteCiscoFieldFormatter()
        {
            lbCiscoFieldFormatters.Items.Remove(lbCiscoFieldFormatters.SelectedItem);
        }

        protected void lbSQLFieldFormatters_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFieldFormatterFieldName.Text = lbSQLFieldFormatters.SelectedItem.Text;
            string[] values = lbSQLFieldFormatters.SelectedItem.Value.Split('#');
            tbFieldFormatterValue.Text = values[0];
            ddlSQLFieldType.SelectedIndex = ddlSQLFieldType.Items.IndexOf(ddlSQLFieldType.Items.FindByValue(values[1]));
        }

        protected void lbCiscoFieldFormatters_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCiscoFieldFormatterFieldName.Text = lbCiscoFieldFormatters.SelectedItem.Text;
            string[] values = lbCiscoFieldFormatters.SelectedItem.Value.Split('#');
            tbCiscoFieldFormatterValue.Text = values[0];
            ddlCiscoFieldType.SelectedIndex = ddlCiscoFieldType.Items.IndexOf(ddlCiscoFieldType.Items.FindByValue(values[1]));
        }

        private void AddLdapAttribute()
        {
            ListItem li = lbLdapAttribute.Items.FindByText(tbLdapAttribute.Text);
            if (li == null)
            {
                lbLdapAttribute.Items.Add(new ListItem(tbLdapAttribute.Text, tbLdapAttribute.Text));
            }
        }

        private void EmptyLdapAttribute()
        {
            tbLdapAttribute.Text = "";
        }

        protected void btnAddLdapAttribute_Click(object sender, EventArgs e)
        {
            AddLdapAttribute();
            EmptyLdapAttribute();
        }

        protected void btnDeleteLdapAttribute_Click(object sender, EventArgs e)
        {
            DeleteLdapAttribute();
        }

        private void DeleteLdapAttribute()
        {
            lbLdapAttribute.Items.Remove(lbLdapAttribute.SelectedItem);
        }

        private void TestLdap()
        {
            DirectoryType dir = InitLdapDirectory();
            try
            {
                DataSet ds = LDAP.Search(dir);
                lblResult.Text = ds.Tables[0].Rows.Count.ToString() + " results retreived";
            }
            catch (Exception e)
            {
                lblResult.Text = e.Message + " - " + e.InnerException.Message;
            }
        }

        private void TestSQL()
        {
            DirectoryType dir = InitSqlDirectory();
            try
            {
                DataSet ds = SQL.Search(dir);
                lblResult.Text = ds.Tables[0].Rows.Count.ToString() + " results retreived";
            }
            catch (Exception e)
            {
                lblResult.Text = e.Message + " - " + e.InnerException.Message;
            }
        }

        private void TestCisco()
        {
            DirectoryType dir = InitCiscoDirectory();
            try
            {

                DataSet ds = CiscoDataSource.Search(dir);
                lblResult.Text = ds.Tables[0].Rows.Count.ToString() + " results retreived";
            }
            catch (Exception e)
            {
                lblResult.Text = e.Message + " - " + e.InnerException.Message;
            }
        }

        protected void btnTestLdap_Click(object sender, EventArgs e)
        {
            TestLdap();
            ModalPopupExtender1.Show();
        }

        protected void btnAddLdapFieldFormatter_Click(object sender, EventArgs e)
        {
            AddLdapFieldFormatter();
            EmptyLdapFieldFormatter();
        }

        protected void btnAddCiscoFieldFormatter_Click(object sender, EventArgs e)
        {
            AddCiscoFieldFormatter();
            EmptyCiscoFieldFormatter();
        }

        private void AddLdapFieldFormatter()
        {
            ListItem li = lbLdapFieldFormatters.Items.FindByText(tbLdapFieldFormatterFieldName.Text);
            if (li == null && tbLdapFieldFormatterFieldName.Text != "" && tbLdapFieldFormatterValue.Text != "")
            {
                lbLdapFieldFormatters.Items.Add(new ListItem(tbLdapFieldFormatterFieldName.Text, tbLdapFieldFormatterValue.Text + "#" + ddlLdapFieldType.SelectedValue));
            }
            else
            {
                li.Value = tbLdapFieldFormatterValue.Text;
                li.Value += "#" + ddlLdapFieldType.SelectedValue;
            }
        }

        private void EmptyLdapFieldFormatter()
        {
            tbLdapFieldFormatterFieldName.Text = "";
            tbLdapFieldFormatterValue.Text = "";
        }

        protected void btnDeleteLdapFieldFormatter_Click(object sender, EventArgs e)
        {
            DeleteLdapFieldFormatter();
            EmptyLdapFieldFormatter();
        }

        private void DeleteLdapFieldFormatter()
        {
            lbLdapFieldFormatters.Items.Remove(lbLdapFieldFormatters.SelectedItem);
        }

        protected void lbLdapFieldFormatters_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbLdapFieldFormatterFieldName.Text = lbLdapFieldFormatters.SelectedItem.Text;
            string[] values = lbLdapFieldFormatters.SelectedItem.Value.Split('#');
            tbLdapFieldFormatterValue.Text = values[0];
            ddlLdapFieldType.SelectedIndex = ddlLdapFieldType.Items.IndexOf(ddlLdapFieldType.Items.FindByValue(values[1]));
        }

        protected void lbtnApply_Click(object sender, EventArgs e)
        {
            UpdateDirectory();
            Server.Transfer("DMDMgt.aspx");
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            UpdateDirectory();
            Server.Transfer("DMDMgt.aspx");
        }

        private void UpdateDirectory()
        {
            DirectoryType dir = null;
            List<FieldFormatter> ffs = new List<FieldFormatter>();
            if (rblDirectoryType.SelectedValue == "LDAP")
            {
                dir = InitLdapDirectory();
                foreach (ListItem lil in lbLdapFieldFormatters.Items)
                {
                    FieldFormatter ffl = new FieldFormatter();
                    ffl.fieldName = lil.Text;
                    string[] values = lil.Value.Split('#');
                    ffl.value = values[0];
                    ffl.fieldType = GetFieldType(values[1]);
                    ffs.Add(ffl);
                }
                ((LdapDatasourceType)dir.Item).fieldFormatters = ffs.ToArray();
                ((LdapDatasourceType)dir.Item).ipphonefilter = new CiscoIPPhoneFilterType();
                ((LdapDatasourceType)dir.Item).ipphonefilter.firstnamemap = tbLdapFirstNameFilterMap.Text;
                ((LdapDatasourceType)dir.Item).ipphonefilter.lastnamemap = tbLdapLastNameFilterMap.Text;
                ((LdapDatasourceType)dir.Item).ipphonefilter.telephonenumbermap = tbLdapTelephoneNumberFilterMap.Text;
            }
            else if (rblDirectoryType.SelectedValue == "SQL")
            {
                dir = InitSqlDirectory();
                foreach (ListItem lis in lbSQLFieldFormatters.Items)
                {
                    FieldFormatter ffsql = new FieldFormatter();
                    ffsql.fieldName = lis.Text;
                    string[] values = lis.Value.Split('#');
                    ffsql.value = values[0];
                    ffsql.fieldType = GetFieldType(values[1]);
                    ffs.Add(ffsql);
                }
                ((SqlDatasourceType)dir.Item).fieldFormatters = ffs.ToArray();
                ((SqlDatasourceType)dir.Item).ipphonefilter = new CiscoIPPhoneFilterType();
                ((SqlDatasourceType)dir.Item).ipphonefilter.firstnamemap = tbSQLFirstNameFilterMap.Text;
                ((SqlDatasourceType)dir.Item).ipphonefilter.lastnamemap = tbSQLLastNameFilterMap.Text;
                ((SqlDatasourceType)dir.Item).ipphonefilter.telephonenumbermap = tbSQLTelephoneNumberFilterMap.Text;
            }
            else if (rblDirectoryType.SelectedValue == "CISCO")
            {
                dir = InitCiscoDirectory();
                foreach (ListItem lis in lbCiscoFieldFormatters.Items)
                {
                    FieldFormatter ffcisco = new FieldFormatter();
                    ffcisco.fieldName = lis.Text;
                    string[] values = lis.Value.Split('#');
                    ffcisco.value = values[0];
                    ffcisco.fieldType = GetFieldType(values[1]);
                    ffs.Add(ffcisco);
                }
                ((CiscoDatasourceType)dir.Item).fieldFormatters = ffs.ToArray();
                ((CiscoDatasourceType)dir.Item).ipphonefilter = new CiscoIPPhoneFilterType();
                ((CiscoDatasourceType)dir.Item).ipphonefilter.firstnamemap = tbFirstNameFilterMap.Text;
                ((CiscoDatasourceType)dir.Item).ipphonefilter.lastnamemap = tbLastNameFilterMap.Text;
                ((CiscoDatasourceType)dir.Item).ipphonefilter.telephonenumbermap = tbTelephoneNumberFilterMap.Text;
            }
            Global.UpdateDirectory(dir);
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("DMDMgt.aspx");
        }

        private DirectoryType InitSqlDirectory()
        {
            DirectoryType dir = new DirectoryType();
            dir.name = tbDirectoryName.Text;
            SqlDatasourceType sdt = new SqlDatasourceType();
            sdt.dsn = tbSqlDsn.Text;
            sdt.uid = tbSqlUid.Text;
            sdt.pwd = tbSqlPwd.Text;
            sdt.command = tbSqlCommand.Text;
            sdt.sqlFilter = tbSqlFilter.Text;
            dir.Item = sdt;
            return dir;
        }

        private DirectoryType InitLdapDirectory()
        {
            DirectoryType dir = new DirectoryType();
            dir.name = tbDirectoryName.Text;
            LdapDatasourceType ldt = new LdapDatasourceType();
            ldt.authenticationType = ddlLdapAuthentication.SelectedValue;
            ldt.server = tbLdapServer.Text;
            ldt.user = tbLdapUser.Text;
            ldt.userPassword = tbLdapUserPassword.Text;
            ldt.targetOU = tbLdapTargetOu.Text;
            ldt.nbPages = Convert.ToInt32(tbLdapNbPages.Text);
            ldt.pageSize = Convert.ToInt32(tbLdapPageSize.Text);
            ldt.ldapFilter = tbLdapFilter.Text;
            List<string> attributes = new List<string>();
            foreach (ListItem li in lbLdapAttribute.Items)
            {
                attributes.Add(li.Text);
            }
            ldt.ldapAttributes = attributes.ToArray();
            dir.Item = ldt;
            return dir;
        }

        private DirectoryType InitCiscoDirectory()
        {
            DirectoryType dir = new DirectoryType();
            dir.name = tbDirectoryName.Text;
            CiscoDatasourceType cdt = new CiscoDatasourceType();
            cdt.server = tbCiscoServer.Text;
            cdt.axluser = tbAXLUser.Text;
            cdt.axluserpwd = tbAXLUserPassword.Text;
            dir.Item = cdt;
            return dir;
        }

        private FieldType GetFieldType(string type)
        {
            FieldType ft = FieldType.Other;
            ft = (FieldType)Enum.Parse(typeof(FieldType), type);
            //switch (type)
            //{
            //    case "Other":
            //        ft = FieldType.Other;
            //        break;
            //    case "Telephone":
            //        ft = FieldType.Telephone;
            //        break;
            //    case "Identity":
            //        ft = FieldType.Identity;
            //        break;
            //    case "Mail":
            //        ft = FieldType.Mail;
            //        break;
            //    case "GSM":
            //        ft = FieldType.GSM;
            //}
            return ft;
        }
    }
}
