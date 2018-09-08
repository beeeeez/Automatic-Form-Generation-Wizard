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
        Table homepageTable = generateTable(masterList);
        homepageTable.CssClass = "table table-hover";
        homepageTablePH.Controls.Add(homepageTable);

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


            TableCell trackingCell = new TableCell();
            HyperLink trackingLink = new HyperLink();
            trackingLink.NavigateUrl = "tracking.aspx?formid=" + entry.formid.ToString();
            trackingLink.Text = "Tracking";
            trackingCell.Controls.Add(trackingLink);
            tempRow.Controls.Add(trackingCell);

            TableCell editCell = new TableCell();
            HyperLink editLink = new HyperLink();
            editLink.NavigateUrl = "FormCreate.aspx?formid=" + entry.formid.ToString();
            editLink.Text = "Edit Form";
            editCell.Controls.Add(editLink);
            tempRow.Controls.Add(editCell);

            TableCell printableBlankCell = new TableCell();
            HyperLink printableBlankLink = new HyperLink();
            printableBlankLink.NavigateUrl = "printableBlank.aspx?formid=" + entry.formid.ToString();
            printableBlankLink.Text = "Print Blank Form";
            printableBlankCell.Controls.Add(printableBlankLink);
            tempRow.Controls.Add(printableBlankCell);

            TableCell filloutCell = new TableCell();
            HyperLink filloutLink = new HyperLink();
            filloutLink.NavigateUrl = "fillout.aspx?formid=" + entry.formid.ToString();
            filloutLink.Text = "Fill Out Form";
            filloutCell.Controls.Add(filloutLink);
            tempRow.Controls.Add(filloutCell);


            TableCell generateCell = new TableCell();
            Label generateLabel = new Label();
            generateLabel.Text = "Sharable URL :    ";
            TextBox generateBox = new TextBox();
            generateBox.CssClass = "form-control";
            generateBox.Text = "fillout.aspx?formid = " + entry.formid.ToString()+"&username="+Session["username"].ToString();
            generateBox.ReadOnly = true;
            generateCell.Controls.Add(generateLabel);
            generateCell.Controls.Add(generateBox);
            tempRow.Controls.Add(generateCell);



            homepageTable.Controls.Add(tempRow);

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
        formidBtn.CssClass = "btn btn-outline-primary";
        formid.Controls.Add(formidBtn);

        TableHeaderCell formtitle = new TableHeaderCell();
        Button formtitleBtn = new Button();
        formtitleBtn.Text = "Form Title :";
        formtitleBtn.Click += new EventHandler(this.sortTitle);
        formtitleBtn.CssClass = "btn btn-outline-primary";
        formtitle.Controls.Add(formtitleBtn);

        TableHeaderCell creationDate = new TableHeaderCell();
        Button creationDateBtn = new Button();
        creationDateBtn.Text = "Creation Date :";
        creationDateBtn.Click += new EventHandler(this.sortTitle);
        creationDateBtn.CssClass = "btn btn-outline-primary";
        creationDate.Controls.Add(creationDateBtn);

        /*
        TableHeaderCell numberofInstances = new TableHeaderCell();
        Button numberofInstancesBtn = new Button();
        numberofInstancesBtn.Text = "# of Completed Instances : ";
        numberofInstancesBtn.Click += new EventHandler(this.sortTitle);
        numberofInstancesBtn.CssClass = "btn btn-outline-primary";
        numberofInstances.Controls.Add(numberofInstancesBtn);
        */

        header.Controls.Add(formid);
        header.Controls.Add(formtitle);
        header.Controls.Add(creationDate);
       // header.Controls.Add(numberofInstances);

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