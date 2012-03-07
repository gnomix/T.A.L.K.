using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wybecom.TalkPortal
{
    public partial class CodifMgt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "InsertCode")
            {
                EntityDataSource1.InsertParameters["codif1"] = new Parameter("codif1", TypeCode.String, ((TextBox)FormView1.FindControl("TextBox1")).Text);
                EntityDataSource1.InsertParameters["active"] = new Parameter("active", TypeCode.Boolean, "true");
                FormView1.InsertItem(false);
            }
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            FormView1.ChangeMode(FormViewMode.Insert);
        }

    }
}
