using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) // checks for notifications
    {
        if(Session["notify"] != null)
        {
            notify.Text = Session["notify"].ToString();
            Session["notify"] = "";
        }
        else if (Session["notification"] != null)
        {
            notify.Text = Session["notification"].ToString();
            Session["notification"] = "";
        }

      //  loginBtn.Text= "<i class="+'"'+"fas fa-sign-in"+'"'+"></i>";
    }

    protected void loginBtn_Click(object sender, EventArgs e) // login button click
    {
        Users temp = new Users();
        temp.loginAttempt(username.Text, password.Text);
        if ((bool)Session["loggedIn"] == true)
        {
            Response.BufferOutput = true;
            Response.Redirect("Homepage.aspx");
        }
        else
        {
            loginerror.Visible = true;
        }

    }


}

