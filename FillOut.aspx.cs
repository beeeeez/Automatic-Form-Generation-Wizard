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
        TextBox formidBox = new TextBox();
        formidBox.Visible = false;
        formidBox.ID = "formid";
        formidBox.Text = formid.ToString();
        TextBox username = new TextBox();
        username.Visible = false;
        username.ID = "user";
        username.Text = user;
        TextBox count = new TextBox();
        count.Visible = false;
        count.ID = "qCount";

        create.Controls.Add(formidBox);
        create.Controls.Add(username);

        int i = 1;
        int onum;
        foreach(question q in qList)
        {
            Literal lit = new Literal();
            Literal hidden = new Literal();
            Literal script = new Literal();
            TextBox text = new TextBox();
            DropDownList multiple = new DropDownList();
            CheckBoxList checkboxlist = new CheckBoxList();
            CheckBox checkbox = new CheckBox();

            
            lit.Text += "<hr />";
            lit.Text += "<h4>" + q.title + "</h4>";
            create.Controls.Add(lit);
            TextBox typeBox = new TextBox();
            typeBox.ID = "type" + i.ToString();
            typeBox.Visible = false;
            typeBox.Text = q.type;
            create.Controls.Add(typeBox);
            if (q.type == "short")
            {
                text.ID = "tb" + i;
                create.Controls.Add(text);
            }
            else if (q.type == "long")
            {
                text.ID = "tb" + i;
                text.Rows = 7;
                text.Columns = 55;
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

                create.Controls.Add(checkboxlist);
            }
            else if (q.type == "datetime")
            {

                script.Text = "<script>$(function() {$('#ContentPlaceHolder1_date"+i.ToString()+"').datepicker(); });</script>";
                /*
                script.Text = "<script>$(function() {$(" + '"' + "#datepicker" + '"' + ").datepicker();});</script><input type=" + '"' + "text" + '"' + "id =" + '"' + "datepicker" + '"' + " name=" + '"' + "date" + i.ToString() + '"' + " > At";
                */
                text.ID = "date" + i;
                create.Controls.Add(script);
                create.Controls.Add(text);
                DropDownList time = populateTimes();
                time.ID = "time" + i.ToString();
                create.Controls.Add(time);
            }
            i++;
        }
        count.Text = i.ToString();
        create.Controls.Add(count);

    }

    protected DropDownList populateTimes()
    {
        DropDownList ddl = new DropDownList();
        
        string f = ":15";
        string t = ":30";
        string on = ":00";
        string am = " AM";
        string pm = " PM";
        ddl.Items.Add("12" + on + am);
        ddl.Items.Add("12" + f + am);
        ddl.Items.Add("12" + t + am);
        ddl.Items.Add("12" + f + am);
        for (int h = 1; h != 12; h++)
        {
            ddl.Items.Add(h.ToString() + on + am);
            ddl.Items.Add(h.ToString() + f + am);
            ddl.Items.Add(h.ToString() + t +am);
            ddl.Items.Add(h.ToString() + f +am);

        }
        ddl.Items.Add("12" + on + pm);
        ddl.Items.Add("12" + f + pm);
        ddl.Items.Add("12" + t + pm);
        ddl.Items.Add("12" + f + pm);

        for (int h = 1; h != 13; h++)
        {
            ddl.Items.Add(h.ToString() + on + pm);
            ddl.Items.Add(h.ToString() + f + pm);
            ddl.Items.Add(h.ToString() + t + pm);
            ddl.Items.Add(h.ToString() + f + pm);

        }

        return ddl;


    }









    protected void Button1_Click(object sender, EventArgs e)
    {
        ContentPlaceHolder mcph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
        PlaceHolder cph = (PlaceHolder)mcph.FindControl("create");

        TextBox qCountBox = (TextBox)cph.FindControl("qCount");
        int qCount;
        Int32.TryParse(qCountBox.Text, out qCount);
        List<String> answerList = new List<String>();
        for (int i =1; i<=qCount;i++)
        {
            TextBox type = (TextBox)cph.FindControl("type" + i.ToString());
            if(type.Text == "short" || type.Text == "long")
            {
                TextBox val = (TextBox)cph.FindControl("tb" + i.ToString());
                answerList.Add(val.Text);
            }
            else if (type.Text == "multiple")
            {
                DropDownList val = (DropDownList)cph.FindControl("ddl" + i.ToString());
                answerList.Add(val.Text);
            }
            else if (type.Text == "checkbox")
            {
                CheckBoxList val = (CheckBoxList)cph.FindControl("cb" + i.ToString());
                string sb = "";
                int throwaway = 1;
                foreach (ListItem selected in val.Items)
                {
                    if (throwaway == 1)
                    {
                        sb += selected.Text;
                    }
                    else
                    {
                        sb += "," +selected.Text ;
                    }
                    throwaway++;
                }
                answerList.Add(sb);                
            }
            else if (type.Text == "datetime")
            {
                TextBox date = (TextBox)cph.FindControl("date" + i.ToString());
                TextBox time = (TextBox)cph.FindControl("time" + i.ToString());

                string sb = date.Text + "-" + time.Text;
                answerList.Add(sb);

            } 


        }
        TextBox usernameBox = cph.FindControl("username") as TextBox;
        TextBox formidBox = cph.FindControl("formid") as TextBox;
        Instance instance = new Instance();
        int formid = 0;
        Int32.TryParse(formidBox.Text, out formid);
        instance.filloutForm(usernameBox.Text, formid, answerList);
        Response.Redirect("Default.aspx");
    }
}