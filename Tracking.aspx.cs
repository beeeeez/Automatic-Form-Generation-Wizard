using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Tracking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["formid"] == null){
            Response.Redirect("Default.aspx");
        }
        if(Session["notify"] != null)
        {
            notify.Text = "<h3>" + Session["notify"].ToString() + "</h3>";
        }
        int formid;
        Int32.TryParse(Request.QueryString["formid"].ToString(), out formid);
        Instance temp = new Instance();
        DataSet bung = temp.populateTrackingTable(Session["username"].ToString(), formid);
        Session["formid"] = formid.ToString();
        if ((bool)Session["isthereData"] == true)
        {
            
            putStuff.Visible = true;
            putStuff.DataSource = bung;
            putStuff.DataMember = bung.Tables[0].TableName;
            putStuff.DataBind();
            Session["isthereData"] = false;
        }
        else
        {
            putStuff.Visible = false;
            noForms.Text = "<h6>Nobody has submitted this form yet!</h6>";

        }


    }
}