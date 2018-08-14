using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class Homepage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["quesNum"] = 0;
        Forms temp = new Forms();

        displayName.InnerHtml = Session["username"].ToString();
        DataSet bung = temp.populateFormsTable();
        if((bool)Session["isthereData"] == true )
        {
            putStuff.Visible = true;
            putStuff.DataSource = bung;
            putStuff.DataMember = bung.Tables[0].TableName;
            putStuff.DataBind();
        }
        else
        {
            putStuff.Visible = false;
            noForms.InnerHtml = "<h6>Create a new form by clicking the create form button!</h6>";
        }

    }

    protected void logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }
}