using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void registerBtn_Click(object sender, EventArgs e)
    {
        if (username.Text == "" || password.Text == "" || confirm.Text == "" || email.Text == "")
        {
            errorDiv.InnerHtml = "You need to fill out all of the fields";
        }
        else if (password.Text != confirm.Text)
        {
            errorDiv.InnerHtml = "Your passwords do not match!";
        }
        else{
            Users temp = new Users();
            temp.registerUser(username.Text, password.Text, email.Text);
            if (Session["errorMsg"].ToString() == "")
            {
                Response.BufferOutput = true;
                Response.Redirect("Default.aspx");
            }

            else
            {
                errorDiv.InnerHtml = Session["errorMsg"].ToString();
            }
        }


    }
}