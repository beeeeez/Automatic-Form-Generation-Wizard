using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FillOut : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Forms sql = new Forms();
        string user;
        if (Session["username"] == null)
        {
            user = Request.QueryString["username"];
        }
        else
        {
            user = Session["username"].ToString();
        }
        int formid;
        Int32.TryParse(Request.QueryString["formid"], out formid);
        List<question> qList = sql.getFormStructure(formid, user);
        

        int i = 1;
        int onum;
        foreach(question q in qList)
        {
            Literal lit = new Literal();
            TextBox text = new TextBox();
            DropDownList multiple = new DropDownList();
            CheckBoxList checkboxlist = new CheckBoxList();
            CheckBox checkbox = new CheckBox();
            Calendar calendar = new Calendar();
            lit.Text = "<hr />";
            create.Controls.Add(lit);
            lit.Text = "<h4>" + q.title + "<h4>";
            create.Controls.Add(lit);

            if (q.type == "short")
            {
                text.ID = "tb" + i;
                create.Controls.Add(text);
            }
            else if (q.type == "long")
            {
                text.ID = "tb" + i;
                text.Rows = 7;
                text.TextMode= TextBoxMode.MultiLine;
                create.Controls.Add(text);
            }
            else if (q.type == "multiple")
            {
                onum = q.opnum;
                multiple.ID = "ddl" + i;
                foreach(string option in q.options)
                {
                    multiple.Items.Add(option);
                 }
                lit.Text = "<br />";
                create.Controls.Add(lit);
                create.Controls.Add(multiple);
            }
            else if (q.type == "checkbox")
            {
                onum = q.opnum;
                checkboxlist.ID = "cb" + i;
                foreach (string option in q.options)
                {
                    checkboxlist.Items.Add(option);
                }
                lit.Text = "<br />";
                create.Controls.Add(lit);
                create.Controls.Add(checkboxlist);
            }
            else if (q.type == "datetime")
            {
                lit.Text = "<br />";
                calendar.ID = "cal" + i;
                create.Controls.Add(lit);
                create.Controls.Add(calendar);
            }

        }
    }

    protected void fillout_Submit()
    { 

    }


}