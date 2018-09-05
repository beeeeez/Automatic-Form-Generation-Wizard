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
        Session["instanceid"] = null;
        Session["quesNum"] = 0;
        Forms temp = new Forms();
        displayName.InnerHtml = Session["username"].ToString();
        List<hpTableEntry> masterList = temp.pullMasterList(Session["username"].ToString());
        /*
        DataSet bung = temp.populateFormsTable();
        if((bool)Session["isthereData"] == true )
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
            noForms.InnerHtml = "<h6>Create a new form by clicking the create form button!</h6>";
        }
        */
        if (Session["notification"] !=null && Session["notification"].ToString() != "")
        {
            Literal notify = new Literal();
            notify.Text = "<h4 class="+'"'+"notify"+'"'+">" + Session["notification"].ToString() + "</h4>";
            
            Notifcation.Controls.Add(notify);
            Session["notification"] = "";
        }

    }

    protected Table generateTable(List<hpTableEntry> masterList)
    {
        Table homepageTable = new Table();
        TableHeaderRow header = generateHeaderRow();
        homepageTable.Controls.Add(header);

        //var newList = masterList.OrderBy(x => x.formtitle).ToList();

        foreach (hpTableEntry entry in masterList)
        {
            TableRow tempRow = new TableRow();

            TableCell formid = new TableCell();
            formid.Text = entry.formid.ToString();
            tempRow.Controls.Add(formid);

            TableCell formtitle = new TableCell();
            formtitle.Text = entry.formtitle.ToString();
            tempRow.Controls.Add(formtitle);

            TableCell creationDate = new TableCell();
            creationDate.Text = entry.creationdate.ToString();
            tempRow.Controls.Add(creationDate);

            TableCell numberofInstances = new TableCell();
            numberofInstances.Text = entry.completedforms.ToString();
            numberofInstances.Controls.Add(numberofInstances);

        }

        return homepageTable;
    }

    protected TableHeaderRow generateHeaderRow()
    {
        TableHeaderRow header = new TableHeaderRow();

        TableHeaderCell formid = new TableHeaderCell();
        Button formidBtn = new Button();
        formidBtn.Text = "Form ID # :";
        formidBtn.Click += new EventHandler(this.sortTitle);
        formid.Controls.Add(formidBtn);

        TableHeaderCell formtitle = new TableHeaderCell();
        Button formtitleBtn = new Button();
        formtitleBtn.Text = "Form Title :";
        formtitleBtn.Click += new EventHandler(this.sortTitle);
        formtitle.Controls.Add(formtitleBtn);

        TableHeaderCell creationDate = new TableHeaderCell();
        Button creationDateBtn = new Button();
        creationDateBtn.Text = "Form Title :";
        creationDateBtn.Click += new EventHandler(this.sortTitle);
        creationDate.Controls.Add(creationDateBtn);

        TableHeaderCell numberofInstances = new TableHeaderCell();
        Button numberofInstancesBtn = new Button();
        numberofInstancesBtn.Text = "# of Completed Instances : ";
        numberofInstancesBtn.Click += new EventHandler(this.sortTitle);
        numberofInstances.Controls.Add(numberofInstancesBtn);

        header.Controls.Add(formid);
        header.Controls.Add(formtitle);
        header.Controls.Add(creationDate);
        header.Controls.Add(numberofInstances);

        return header;
    }

    protected void sortTitle(object sender, EventArgs e)
    {


    }

    protected void logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }

    protected void putStuff_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void putStuff_Sorting(object sender, GridViewSortEventArgs e)
    {
        putStuff.DataBind();
    }
}