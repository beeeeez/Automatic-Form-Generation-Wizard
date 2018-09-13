using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FillOut : System.Web.UI.Page
{

    protected void Page_Init(Object sender, EventArgs e)
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
        int formid = 0;
        if (Request.QueryString["formid"] != null)
        {
            Int32.TryParse(Request.QueryString["formid"], out formid);

        }
        else
        {
            Int32.TryParse(Session["formid"].ToString(), out formid);
        }
        if (Request.Form["deleteInstance"] != null)
        {
            deleteInstance(formid);
        }
        else
        {
            string formT = sql.getFormTitle(formid, user);
            header.Text = "<h3>Fill Out  - " + formT + "</h3><a href=" + '"' + "homepage.aspx" + '"' + " class=" +'"'+ "btn btn-primary right"+'"'+ "><i class="+'"'+"fas fa-home"+'"'+"></i> Return to Homepage</a>";

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

            int i = 0;
            int onum;

            
            foreach (question q in qList)
            {
                i++;
                Literal lit = new Literal();
                Literal staticText = new Literal();
                Literal hidden = new Literal();
                Literal script = new Literal();
                TextBox text = new TextBox();
                Literal gap = new Literal();
                DropDownList multiple = new DropDownList();
                CheckBoxList checkboxlist = new CheckBoxList();
                CheckBox checkbox = new CheckBox();


                lit.Text = "<br /><h4>" + q.title + "</h4>";
                create.Controls.Add(lit);

                TextBox typeBox = new TextBox();
                typeBox.ID = "type" + i.ToString();
                typeBox.Text = q.type;
                typeBox.Visible = false;

                create.Controls.Add(typeBox);


                if (q.type == "short")
                {
                    text.ID = "tb" + i;
                    text.CssClass = "form-control";
                    create.Controls.Add(text);
                }
                else if (q.type == "long")
                {
                    text.ID = "tb" + i;
                    text.Rows = 7;
                    text.Columns = 55;
                    text.CssClass = "form-control";
                    text.TextMode = TextBoxMode.MultiLine;
                    create.Controls.Add(text);
                }
                else if (q.type == "static")
                {
                    staticText.Text = "<h4>" + q.title + "</h4>";
                }
                else if (q.type == "multiple")
                {
                    onum = q.opnum;
                    multiple.ID = "ddl" + i;
                    multiple.CssClass = "custom-select";
                    foreach (string option in q.options)
                    {
                        multiple.Items.Add(option);
                    }

                    create.Controls.Add(multiple);
                }
                else if (q.type == "checkbox")
                {
                    onum = q.opnum;
                    checkboxlist.ID = "cb" + i;
                    checkboxlist.CssClass = "custom-checkbox";
                    foreach (string option in q.options)
                    {
                        checkboxlist.Items.Add(option);
                    }

                    create.Controls.Add(checkboxlist);
                }
                else if (q.type == "datetime")
                {

                    script.Text = "<script>$(function() {$('#ContentPlaceHolder1_date" + i.ToString() + "').datepicker(); });</script>";
                    /*
                    script.Text = "<script>$(function() {$(" + '"' + "#datepicker" + '"' + ").datepicker();});</script><input type=" + '"' + "text" + '"' + "id =" + '"' + "datepicker" + '"' + " name=" + '"' + "date" + i.ToString() + '"' + " > At";
                    */
                    text.ID = "date" + i;
                    create.Controls.Add(script);
                    create.Controls.Add(text);
                    gap.Text = "   At   ";
                    create.Controls.Add(gap);
                    DropDownList time = populateTimes();
                    time.ID = "time" + i.ToString();
                    create.Controls.Add(time);
                }

            }
            count.Text = i.ToString();
            count.Visible = false;
            create.Controls.Add(count);
        }




    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Request.QueryString["instanceid"] != null)
        {

            int instanceid;
            Int32.TryParse(Request.QueryString["instanceid"], out instanceid);


            fillOutFields(instanceid);
            string deleteIcon = "<i class=" + '"' + "fas fa-trash - alt" + '"' + "></i>";
            LinkButton deleteBtn = new LinkButton();
            deleteBtn.CssClass = "btn btn-danger right";
            deleteBtn.OnClientClick = "jsDelete()";
            deleteBtn.Text = deleteIcon + " Delete Instance";
            deleteBtnLit.Controls.Add(deleteBtn);

            Forms sql = new Forms();
            int formid;
            if (Session["formid"] == null)
            {
                Int32.TryParse(Request.QueryString["formid"], out formid);

            }
            else
            {
                Int32.TryParse(Session["formid"].ToString(), out formid);
            }
            string user;
            if (Session["username"] == null)
            {
                user = Request.QueryString["username"];
            }
            else
            {
                user = Session["username"].ToString();

            }
            string formT = sql.getFormTitle(formid, user);
            header.Text = "<h3>Editing Instance # " + instanceid.ToString() + " - " + formT + "</h3><a href=" + '"' + "tracking.aspx?formid=" + formid.ToString() + '"' + " class="+'"'+"btn btn-primary right"+'"'+ "> <i class="+'"'+"fas fa-undo - alt"+'"'+"></i> Return to Tracking</a>";
        }
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
            ddl.Items.Add(h.ToString() + t + am);
            ddl.Items.Add(h.ToString() + f + am);

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

    protected void deleteInstance(int formid)
    {

        string user = Session["username"].ToString();
        int instanceid = 0;
        Int32.TryParse(Request.QueryString["instanceid"], out instanceid);
        Instance temp = new Instance();
        temp.deleteInstance(user, formid, instanceid);
        temp.removeInstancefromMaster(user, formid, instanceid);
        Session["notify"] = "Instance #" + instanceid.ToString() + " has been deleted!";
        Response.Redirect("Tracking.aspx?formid=" + formid.ToString());
    }

    protected void fillOutFields(int instanceid)
    {

        ContentPlaceHolder mcph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
        PlaceHolder cph = (PlaceHolder)mcph.FindControl("create");

        Instance temp = new Instance();
        TextBox usernameBox = cph.FindControl("user") as TextBox;
        TextBox formidBox = cph.FindControl("formid") as TextBox;
        int formid;
        Int32.TryParse(formidBox.Text, out formid);

        TextBox qCountBox = (TextBox)cph.FindControl("qCount");
        int i = 1;
        int qCount;
        Int32.TryParse(qCountBox.Text, out qCount);

        List<string> answerList = temp.getInstanceAnswers(usernameBox.Text, formid, instanceid, qCount);



        foreach (string answer in answerList)
        {

            TextBox typeBox = (TextBox)cph.FindControl("type" + i.ToString());
            string type = typeBox.Text;

            if (type == "short" || type == "long")
            {
                TextBox val = (TextBox)cph.FindControl("tb" + i.ToString());
                val.Text = answer;
            }
            else if (type == "multiple")
            {
                DropDownList ddlval = (DropDownList)cph.FindControl("ddl" + i.ToString());
                ddlval.SelectedValue = answer;
            }
            else if (type == "checkbox")
            {
                CheckBoxList cbval = (CheckBoxList)cph.FindControl("cb" + i.ToString());
                string[] checkedList = answer.Split(',');

                foreach (string selected in checkedList)
                {
                    foreach (ListItem cbitem in cbval.Items)
                    {
                        if (cbitem.Text == selected)
                        {
                            cbitem.Selected = true;
                        }

                    }
                }


            }
            else if (type == "datetime")
            {
                TextBox date = (TextBox)cph.FindControl("date" + i.ToString());
                DropDownList time = (DropDownList)cph.FindControl("time" + i.ToString());

                string[] datetimeString = new string[1];
                datetimeString = answer.Split('-');
                if (datetimeString.Length > 1)
                {
                    date.Text = datetimeString[0];
                    time.Text = datetimeString[1];
                }

            }

            i++;
        }


    }







    protected void Button1_Click(object sender, EventArgs e)
    {
        ContentPlaceHolder mcph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
        PlaceHolder cph = (PlaceHolder)mcph.FindControl("create");

        TextBox qCountBox = (TextBox)cph.FindControl("qCount");
        int i = 1;



        int qCount;
        Int32.TryParse(qCountBox.Text, out qCount);
        List<string> answerList = new List<string>();
        while (i <= qCount)
        {

            TextBox typeBox = (TextBox)cph.FindControl("type" + i.ToString());
            string type = typeBox.Text;

            if (type == "short" || type == "long")
            {
                TextBox val = (TextBox)cph.FindControl("tb" + i.ToString());
                answerList.Add(val.Text);
            }
            else if (type == "multiple")
            {
                DropDownList ddlval = (DropDownList)cph.FindControl("ddl" + i.ToString());
                answerList.Add(ddlval.SelectedValue);
            }
            else if (type == "checkbox")
            {
                CheckBoxList cbval = (CheckBoxList)cph.FindControl("cb" + i.ToString());
                string sb = "";
                int throwaway = 1;
                foreach (ListItem selected in cbval.Items)
                {
                    if (selected.Selected == true)
                    {
                        if (throwaway == 1)
                        {
                            sb += selected.Text;
                        }
                        else
                        {
                            sb += "," + selected.Text;
                        }
                        throwaway++;
                    }
                }
                answerList.Add(sb);
            }
            else if (type == "datetime")
            {
                TextBox date = (TextBox)cph.FindControl("date" + i.ToString());
                DropDownList time = (DropDownList)cph.FindControl("time" + i.ToString());

                string sb = date.Text + "-" + time.Text;
                answerList.Add(sb);

            }
            else if (type == "static")
            {

                answerList.Add("static_text");

            }


            i++;
        }
        TextBox usernameBox = cph.FindControl("user") as TextBox;
        TextBox formidBox = cph.FindControl("formid") as TextBox;
        Instance instance = new Instance();
        int formid = 0;
        Int32.TryParse(formidBox.Text, out formid);
        if (Request.QueryString["instanceid"] != null)
        {
            int instanceid = 0;
            Int32.TryParse(Request.QueryString["instanceid"].ToString(), out instanceid);
            instance.updateInstanceAnswers(usernameBox.Text, formid, instanceid, answerList);
            Session["notify"] = "Instance Updated!";
            Response.Redirect("Tracking.aspx?formid=" + formid);
        }
        else
        {
            instance.filloutForm(usernameBox.Text, formid, answerList);
            Session["notification"] = "Form Fill Out Completed!";
            Response.Redirect("Homepage.aspx");
        }
    }

}