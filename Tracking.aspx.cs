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

        int formid;
        Int32.TryParse(Request.QueryString["formid"].ToString(), out formid);

        Forms ftemp = new Forms();
        string formtitle = ftemp.getFormTitle(formid, Session["username"].ToString());

        header.Text = "<h3>Tracking - " + formtitle + " </h3><a href=" + '"' + "homepage.aspx" + '"' + " class=" + '"' + "btn btn-primary rightsideBtns" + '"' + "> <i class=" + '"' + "fas fa-home" + '"' + "></i> Return to Homepage</a><br />";

        Instance temp = new Instance();
        DataSet bung = temp.populateTrackingTable(Session["username"].ToString(), formid);
        Session["formid"] = formid.ToString();

        /*if else statement that checks if the formid is not null, if its not null it will generate the tracking table,
         if it is null it takes them to the login page
         */
    
        if (Request.QueryString["formid"] != null)
        {
            List<tpTableEntry> instancesList = temp.pullMasterInstanceList(Session["username"].ToString(), formid);
            instancesList = sortListEntries(instancesList);

            Table trackingTable = generateTrackingTable(instancesList);
            trackingTable.CssClass = "table table-hover";
            tpTablePH.Controls.Add(trackingTable);

        }
        else
        {
            Response.Redirect("Default.aspx");
        }
        //shows a notification
        if (Session["notify"] != null)
        {
            notify.Text = "<h4>" + Session["notify"].ToString() + "</h4>";
            Session["notify"] = "";
        }

    }

    protected Table generateTrackingTable(List<tpTableEntry> instancesList)//creates the table
    {
        Table trackingTable = new Table();
        TableHeaderRow header = generateHeaderRow();
        trackingTable.Controls.Add(header);

        foreach (tpTableEntry entryy in instancesList)
        {
            string editIcon = "<i class=" + '"' + "fas fa-cogs" + '"' + "></i>";
            string printIcon = "<i class=" + '"' + "fas fa-print" + '"' + "></i>";

            TableRow tempRow = new TableRow();

            TableCell instanceid = new TableCell();
            instanceid.Text = entryy.instanceid.ToString();
            tempRow.Controls.Add(instanceid);

            TableCell creationDate = new TableCell();
            creationDate.Text = entryy.creationdate.ToString();
            tempRow.Controls.Add(creationDate);

            TableCell firstquestion = new TableCell();
            firstquestion.Text = entryy.firstquestion.ToString();
            tempRow.Controls.Add(firstquestion);

            TableCell editCell = new TableCell();
            editCell.CssClass = "cell";
            HyperLink editLink = new HyperLink();
            editLink.NavigateUrl = "FillOut.aspx?instanceid=" + entryy.instanceid.ToString();
            editLink.Text = editIcon;
            editLink.CssClass = "icon";
            editCell.Controls.Add(editLink);
            tempRow.Controls.Add(editCell);

            TableCell printInstance = new TableCell();
            printInstance.CssClass = "cell";
            HyperLink printableInstanceLink = new HyperLink();
            printableInstanceLink.NavigateUrl = "Printable.aspx?instanceid=" + entryy.instanceid.ToString();
            printableInstanceLink.Text = printIcon;
            printableInstanceLink.Target = "_blank";
            printableInstanceLink.CssClass = "icon";
            printInstance.Controls.Add(printableInstanceLink);
            tempRow.Controls.Add(printInstance);

            trackingTable.Controls.Add(tempRow);

        }
        return trackingTable;
    }

    protected TableHeaderRow generateHeaderRow()//creates the headers on each row
    {
        TableHeaderRow header = new TableHeaderRow();

        TableHeaderCell instanceid = new TableHeaderCell();
        LinkButton instanceidBtn = new LinkButton();
        instanceidBtn.Text = "Instance ID # ";
        instanceidBtn.Attributes.Add("onclick", "sortTable(0); return false;");
        instanceidBtn.CssClass = "btn btn-outline-primary instanceid";
        instanceid.Controls.Add(instanceidBtn);

        TableHeaderCell creationdate = new TableHeaderCell();
        LinkButton creationdateBtn = new LinkButton();
        creationdateBtn.Text = "Creation Date ";
        creationdateBtn.Attributes.Add("onclick", "sortTable(1); return false;");
        creationdateBtn.CssClass = "btn btn-outline-primary date";
        creationdate.Controls.Add(creationdateBtn);

        TableHeaderCell firstquestion = new TableHeaderCell();
        LinkButton firstquestionBtn = new LinkButton();
        firstquestionBtn.Text = "First Question ";
        firstquestionBtn.Attributes.Add("onclick", "sortTable(2); return false;");
        firstquestionBtn.CssClass = "btn btn-outline-primary firstquestion";
        firstquestion.Controls.Add(firstquestionBtn);

        TableHeaderCell editInstances = new TableHeaderCell();
        editInstances.Text = "<span>Edit Instance</span>";
        editInstances.CssClass = "header";

        TableHeaderCell printInstance = new TableHeaderCell();
        printInstance.Text = "<span>Print Instance</span>";
        printInstance.CssClass = "header";

        if (Session["sortBy"] != null)//defunct
        {
            if (Session["sortDir"].ToString() == "asc")
            {
                if (Session["sortBy"].ToString() == "instanceid")
                {
                    instanceidBtn.Text += " <i class=" + '"' + "fas fa-sort-up" + '"' + "></i>";
                }
                else if (Session["sortBy"].ToString() == "date")
                {
                    creationdateBtn.Text += " <i class=" + '"' + "fas fa-sort-up" + '"' + "></i>";
                }
                else if (Session["sortBy"].ToString() == "firstquestion")
                {
                    firstquestionBtn.Text += " <i class=" + '"' + "fas fa-sort-up" + '"' + "></i>";
                }
            }
            else
            {
                if (Session["sortBy"].ToString() == "instanceid")
                {
                    instanceidBtn.Text += " <i class=" + '"' + "fas fa-sort-down" + '"' + "></i>";
                }
                else if (Session["sortBy"].ToString() == "date")
                {
                    creationdateBtn.Text += " <i class=" + '"' + "fas fa-sort-down" + '"' + "></i>";

                }
                else if (Session["sortBy"].ToString() == "firstquestion")
                {
                    firstquestionBtn.Text += " <i class=" + '"' + "fas fa-sort-down" + '"' + "></i>";
                }
            }

        }

        header.Controls.Add(instanceid);
        header.Controls.Add(creationdate);
        header.Controls.Add(firstquestion);
        header.Controls.Add(editInstances);
        header.Controls.Add(printInstance);

        return header;

    }

    protected void sortID(object sender, EventArgs e)
    {
        Session["sortBy"] = "instanceid";
        if (Session["sortDir"].ToString() == "dsc")
        {
            Session["sortDir"] = "asc";

        }
        else
        {
            Session["sortDir"] = "dsc";
        }

    }

    protected void sortDate(object sender, EventArgs e)
    {
        Session["sortBy"] = "date";
        if (Session["sortDir"].ToString() == "dsc")
        {
            Session["sortDir"] = "asc";

        }
        else
        {
            Session["sortDir"] = "dsc";
        }

    }

    protected void sortFirstQuestion(object sender, EventArgs e)
    {
        Session["sortBy"] = "firstquestion";
        if (Session["sortDir"].ToString() == "dsc")
        {
            Session["sortDir"] = "asc";

        }
        else
        {
            Session["sortDir"] = "dsc";
        }

    }

    protected List<tpTableEntry> sortListEntries(List<tpTableEntry> instancesList)
    {

        if (Session["sortBy"] == null)
        {
            Session["sortBy"] = "instanceid";
            Session["sortDir"] = "asc";
        }
        else if (Session["sortBy"].ToString() == "instanceid")
        {
            if (Session["sortDir"].ToString() == "asc")
            {
                instancesList = instancesList.OrderBy(C => C.instanceid).ToList();
            }
            else

            {
                instancesList = instancesList.OrderByDescending(C => C.instanceid).ToList();
            }
        }
        else if (Session["sortBy"].ToString() == "date")
        {
            if (Session["sortDir"].ToString() == "asc")
            {
                instancesList = instancesList.OrderBy(C => C.creationdate).ToList();
            }
            else
            {
                instancesList = instancesList.OrderByDescending(C => C.creationdate).ToList();
            }
        }
        else if (Session["sortBy"].ToString() == "firstquestion")
        {
            if (Session["sortDir"].ToString() == "asc")
            {
                instancesList = instancesList.OrderBy(C => C.firstquestion).ToList();
            }
            else
            {
                instancesList = instancesList.OrderByDescending(C => C.firstquestion).ToList();
            }
        }




        return instancesList;
    }

    protected void logout_Click(object sender, EventArgs e)//logs the user out
    {
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }

    protected void putStuff_Sorting(object sender, GridViewSortEventArgs e)
    {
        putStuff.DataBind();
    }


    /*
    if(Request.QueryString["formid"] == null){
        Response.Redirect("Default.aspx");
    }
    if(Session["notify"] != null)
    {
        notify.Text = "<h4>" + Session["notify"].ToString() + "</h4>";
        Session["notify"] = "";
    }
    int formid;
    Int32.TryParse(Request.QueryString["formid"].ToString(), out formid);
    Forms ftemp = new Forms();
    string formtitle = ftemp.getFormTitle(formid, Session["username"].ToString());
    header.Text = "<h3>Tracking - " + formtitle + " </h3><a href=" + '"' + "homepage.aspx" + '"' + " class="+'"'+ "btn btn-primary right"+'"'+ "> <i class="+'"'+"fas fa-home"+'"'+"></i> Return to Homepage</a><br />";
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


}*/
}