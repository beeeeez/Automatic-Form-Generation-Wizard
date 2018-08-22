using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class passReset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["username"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        
    }

    protected void changepass_Click(object sender, EventArgs e)
    {
        Users temp = new Users();
        string message = temp.changePassword(Request.QueryString["username"].ToString(), pass.Text);
        if (message == "success")
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            errmsg.Text = message;
        }

    }
}