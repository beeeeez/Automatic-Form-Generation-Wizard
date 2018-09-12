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
        if (Session["username"] != null && Session["username"].ToString() != "") { 
        displayName.InnerHtml = Session["username"].ToString();
            List<hpTableEntry> masterList = temp.pullMasterList(Session["username"].ToString());
            masterList = sortListEntries(masterList);
            //

            Table homepageTable = generateTable(masterList);
            homepageTable.CssClass = "table table-hover";
            homepageTablePH.Controls.Add(homepageTable);
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
        

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
            notify.Text = "<br /><h4 class="+'"'+"notify"+'"'+">" + Session["notification"].ToString() + "</h4>";
            
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
            string trackingIcon = "<i class=" + '"' + "fas fa-clipboard - list" + '"' + "></i>";
            string editIcon = "<i class=" + '"' + "fas fa-pen" + '"' + "></i>";
            string printIcon = "<i class=" + '"' + "fas fa-print" + '"' + "></i>";
            string filloutIcon = "<i class=" + '"' + "fas fa-plus-circle" + '"' + "></i>";

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

            TableCell numIns = new TableCell();
            numIns.Text = entry.completedforms.ToString();
            tempRow.Controls.Add(numIns);

            TableCell trackingCell = new TableCell();
            trackingCell.CssClass = "cell";
            HyperLink trackingLink = new HyperLink();
            trackingLink.NavigateUrl = "tracking.aspx?formid=" + entry.formid.ToString();
            trackingLink.Text = trackingIcon;
            trackingLink.CssClass = "icon";
            trackingCell.Controls.Add(trackingLink);
            tempRow.Controls.Add(trackingCell);

            TableCell editCell = new TableCell();
            editCell.CssClass = "cell";
            HyperLink editLink = new HyperLink();
            editLink.NavigateUrl = "FormCreate.aspx?formid=" + entry.formid.ToString();
            editLink.Text = editIcon;
            editLink.CssClass = "icon";
            editCell.Controls.Add(editLink);
            tempRow.Controls.Add(editCell);

            TableCell printableBlankCell = new TableCell();
            printableBlankCell.CssClass = "cell";
            HyperLink printableBlankLink = new HyperLink();
            printableBlankLink.NavigateUrl = "Printable.aspx?formid=" + entry.formid.ToString();
            printableBlankLink.Text = printIcon;
            printableBlankLink.Target = "_blank";
            printableBlankLink.CssClass = "icon";
            printableBlankCell.Controls.Add(printableBlankLink);
            tempRow.Controls.Add(printableBlankCell);

            TableCell filloutCell = new TableCell();
            filloutCell.CssClass = "cell";
            HyperLink filloutLink = new HyperLink();
            filloutLink.NavigateUrl = "fillout.aspx?formid=" + entry.formid.ToString();
            filloutLink.Text = filloutIcon;
            filloutLink.CssClass = "icon";
            filloutCell.Controls.Add(filloutLink);
            tempRow.Controls.Add(filloutCell);


            TableCell generateCell = new TableCell();
            TextBox generateBox = new TextBox();
            generateBox.CssClass = "form-control";
            generateBox.Text = "fillout.aspx?formid=" + entry.formid.ToString()+"&username="+Session["username"].ToString();
            generateBox.ReadOnly = true;
            //generateCell.Controls.Add(generateLabel);
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
        LinkButton formidBtn = new LinkButton();
        formidBtn.Text = "Form ID # ";
        formidBtn.Attributes.Add("onclick", "sortTable(0); return false;");
        //formidBtn.Click += new EventHandler(this.sortID);
        formidBtn.CssClass = "btn btn-outline-primary";
        formid.Controls.Add(formidBtn);

        TableHeaderCell formtitle = new TableHeaderCell();
        LinkButton formtitleBtn = new LinkButton();
        formtitleBtn.Text = "Form Title ";
        formtitleBtn.Attributes.Add("onclick", "sortTable(1); return false;");
        //formtitleBtn.Click += new EventHandler(this.sortTitle);
        formtitleBtn.CssClass = "btn btn-outline-primary";
        formtitle.Controls.Add(formtitleBtn);

        TableHeaderCell creationDate = new TableHeaderCell();
        LinkButton creationDateBtn = new LinkButton();
        creationDateBtn.Text = "Creation Date ";
        creationDateBtn.Attributes.Add("onclick", "sortTable(2); return false;");
        //creationDateBtn.Click += new EventHandler(this.sortDate);
        creationDateBtn.CssClass = "btn btn-outline-primary";
        creationDate.Controls.Add(creationDateBtn);

        
        TableHeaderCell numberofInstances = new TableHeaderCell();
        LinkButton numberofInstancesBtn = new LinkButton();
        numberofInstancesBtn.Text = "# of Instances ";
        numberofInstancesBtn.Attributes.Add("onclick", "sortTable(3); return false;");
        //numberofInstancesBtn.Click += new EventHandler(this.sortNum);
        numberofInstancesBtn.CssClass = "btn btn-outline-primary";
        numberofInstances.Controls.Add(numberofInstancesBtn);

        TableHeaderCell tracking = new TableHeaderCell();
        tracking.Text = "<span>Track Form</span>";
        tracking.CssClass = "header";


        TableHeaderCell editForms = new TableHeaderCell();
        editForms.Text = "<span>Edit Form</span>";
        editForms.CssClass = "header";


        TableHeaderCell printBlank = new TableHeaderCell();
        printBlank.Text = "<span>Print Blank Form</span>";
        printBlank.CssClass = "header";

        TableHeaderCell fillForms = new TableHeaderCell();
        fillForms.Text = "<span>Fill Out Form</span>";
        fillForms.CssClass = "header";

        TableHeaderCell generate = new TableHeaderCell();
        generate.Text = "<span>Generated URL</span>";
        generate.CssClass = "header";

        if(Session["sortBy"] != null)
        {
            if(Session["sortDir"].ToString() == "asc")
            {

                if(Session["sortBy"].ToString() == "formid")
                {
                    formidBtn.Text += " <i class=" + '"' + "fas fa-sort-up" + '"' + "></i>";
                }
                else if (Session["sortBy"].ToString() == "formtitle")
                {
                    formtitleBtn.Text += " <i class=" + '"' + "fas fa-sort-up" + '"' + "></i>";
                }
                else if (Session["sortBy"].ToString() == "numIns")
                {
                    numberofInstancesBtn.Text += " <i class=" + '"' + "fas fa-sort-up" + '"' + "></i>";
                }
                else if (Session["sortBy"].ToString() == "date")
                {
                    creationDateBtn.Text += " <i class=" + '"' + "fas fa-sort-up" + '"' + "></i>";
                }
            }

            else
            {
                if (Session["sortBy"].ToString() == "formid")
                {
                    formidBtn.Text += " <i class=" + '"' + "fas fa-sort-down" + '"' + "></i>";
                }

                else if (Session["sortBy"].ToString() == "formtitle")
                {
                    formtitleBtn.Text += " <i class=" + '"' + "fas fa-sort-down" + '"' + "></i>";
                }
                else if (Session["sortBy"].ToString() == "numIns")
                {
                    numberofInstancesBtn.Text += " <i class=" + '"' + "fas fa-sort-down" + '"' + "></i>";
                }
                else if (Session["sortBy"].ToString() == "date")
                {
                    creationDateBtn.Text += " <i class=" + '"' + "fas fa-sort-down" + '"' + "></i>";
                }
            }
        }

        header.Controls.Add(formid);
        header.Controls.Add(formtitle);
        header.Controls.Add(creationDate);
        header.Controls.Add(numberofInstances);
        header.Controls.Add(tracking);
        header.Controls.Add(editForms);
        header.Controls.Add(printBlank);
        header.Controls.Add(fillForms);
        header.Controls.Add(generate);


        return header;
    }

    protected void sortTitle(object sender, EventArgs e)
    {
        Session["sortBy"] = "formtitle";
        if(Session["sortDir"].ToString() == "dsc")
        {
            Session["sortDir"] = "asc";
        }
        else
        {
            Session["sortDir"] = "dsc";
        }
    }

    protected void sortID(object sender, EventArgs e)
    {
        Session["sortBy"] = "formid";
        if (Session["sortDir"].ToString() == "dsc")
        {
            Session["sortDir"] = "asc";
        }
        else
        {
            Session["sortDir"] = "dsc";
        }
    }

    protected void sortNum(object sender, EventArgs e)
    {
        Session["sortBy"] = "numIns";
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

    protected List<hpTableEntry> sortListEntries(List<hpTableEntry> masterList)
    {
        if(Session["sortBy"] == null)
        {
            Session["sortBy"] = "formid";
            Session["sortDir"] = "asc";
        }
        else if(Session["sortBy"].ToString() == "formid")
        {
            if (Session["sortDir"].ToString() == "asc")
            {
                masterList = masterList.OrderBy(C => C.formid).ToList();
            }
            else
            {
                masterList = masterList.OrderByDescending(C => C.formid).ToList();
            }
        }
        else if (Session["sortBy"].ToString() == "formtitle")
        {
            if (Session["sortDir"].ToString() == "asc")
            {
                masterList = masterList.OrderBy(C => C.formtitle).ToList();
            }
            else
            {
                masterList = masterList.OrderByDescending(C => C.formtitle).ToList();
            }
        }
        else if (Session["sortBy"].ToString() == "date")
        {
            if (Session["sortDir"].ToString() == "asc")
            {
                masterList = masterList.OrderBy(C => C.creationdate).ToList();
            }
            else
            {
                masterList = masterList.OrderByDescending(C => C.creationdate).ToList();
            }
        }
        else if (Session["sortBy"].ToString() == "numIns")
        {
            if (Session["sortDir"].ToString() == "asc")
            {
                masterList = masterList.OrderBy(C => C.completedforms).ToList();
            }
            else
            {
                masterList = masterList.OrderByDescending(C => C.completedforms).ToList();
            }
        }



        return masterList;
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