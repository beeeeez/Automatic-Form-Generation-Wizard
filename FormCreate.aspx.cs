 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




public partial class FormCreate : System.Web.UI.Page
{

    protected void Page_Init(Object sender, EventArgs e)
    {
        if(Request.Form["delete"] != null)
        {
            deleteForm();

        }
        else if (IsPostBack == true && Request.QueryString["formid"] != null && Request.Form["editFormID"] != null)
        {
            Forms create = new Forms();
            List<question> qList = parsePage();
            int totalQ;
            Int32.TryParse(Request.Form["totalQ"], out totalQ);
            int formid;
            Int32.TryParse(Request.Form["editFormID"].ToString(), out formid);
            create.updateStructure(formid, totalQ, qList);
            Session["notification"] = Request.Form["formtitle"].ToString() + " Updated!";
            Response.Redirect("Homepage.aspx");

        }
        else if (IsPostBack == true && Request.QueryString["formid"] == null)
        {
            Forms create = new Forms();
            List<question> qList = parsePage();
            int totalQ;
            Int32.TryParse(Request.Form["totalQ"], out totalQ);
            DateTime anchorDate = create.createFormid(Request.Form["formtitle"].ToString());
            int formid = create.getNewFormID(anchorDate);

            if (formid != 0)
            {

                create.createInstanceMaster(formid);
                create.createStructure(formid, totalQ, qList);
                create.fillStructure(formid, totalQ, qList);
            }
            Session["notification"] = "Form Created!";
            Response.Redirect("Homepage.aspx");
        }

        else if (Request.QueryString["formid"] != null)
        {
            int formid;
            Int32.TryParse(Request.QueryString["formid"], out formid);
            TextBox formidBox = new TextBox();
            formidBox.ID = "sformid";
            formidBox.Text = Request.QueryString["formid"];
            LinkButton deleteBtn = new LinkButton();
            deleteBtn.CssClass = "btn btn-danger right";
            deleteBtn.OnClientClick = "jsDelete()";
            deleteBtn.Text = "<i class="+'"'+"fas fa-times"+'"'+"></i> Delete this Form";
            deleteBtnLit.Controls.Add(deleteBtn);





            editPH.Controls.Add(formidBox);
            Forms temp = new Forms();
            List<question> qList = temp.getFormStructure(formid, Session["username"].ToString());
            int i = 0;
            foreach (question q in qList)
            {
                i++;
                TextBox title = new TextBox();
                title.Text = q.Title;
                title.ID = "sq" + i;

                editPH.Controls.Add(title);

                TextBox type = new TextBox();
                type.Text = q.type;
                type.ID = "sqtype" + i;

                editPH.Controls.Add(type);
                if (q.type == "multiple" || q.type == "checkbox")
                {
                    TextBox opnum = new TextBox();
                    opnum.Text = q.opnum.ToString();
                    opnum.ID = "sopnum" + i;

                    editPH.Controls.Add(opnum);
                    int j = 1;
                    foreach (string option in q.options)
                    {
                        TextBox optionBox = new TextBox();
                        optionBox.Text = option;
                        optionBox.ID = "s" + i + "option" + j;

                        editPH.Controls.Add(optionBox);
                        j++;
                    }
                }


            }
            string titlething = temp.getFormTitle(formid, Session["username"].ToString());
            TextBox questionNum = new TextBox();
            questionNum.Text = i.ToString();
            questionNum.ID = "sqnum";
            editPH.Controls.Add(questionNum);
            TextBox titleThingBox = new TextBox();
            titleThingBox.Text = titlething;
            titleThingBox.ID = "titleThing";
            editPH.Controls.Add(titleThingBox);
            header.Text = "<h3>Editing Form " + titlething +"</h3>";

        }


        //saveSubmit.Visible = false;

        else
        {

            TextBox formidBox = new TextBox();
            formidBox.Text = 0.ToString();
            formidBox.Visible = false;
            editPH.Controls.Add(formidBox);

            header.Text = "<h3>New Form Creation</h3>";

        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void deleteForm()
    {
        Forms temp = new Forms();
        int formid = 0;
        Int32.TryParse(Request.QueryString["formid"].ToString(), out formid);
        temp.deleteAllInstances(Session["username"].ToString(), formid);
        temp.deleteFormStructure(Session["username"].ToString(), formid);
        Session["notification"] = Request.Form["formtitle"].ToString() + " has been deleted!";
        Response.Redirect("Homepage.aspx");

    }


    protected List<question> parsePage()
    {
        List<question> qList = new List<question>();

        int totalQ;
        Int32.TryParse(Request.Form["totalQ"], out totalQ);

        for (int i = 1; i <= totalQ; i++)
        {
            int totalO = 0;
            string[] opHold = new string[0];

            if (Request.Form["ddl" + i] == "multiple" || Request.Form["ddl" + i] == "checkbox")
            {
                Int32.TryParse(Request.Form["q" + i + "OptionsTotal"], out totalO);
                opHold = new string[totalO];
                int k;
                for (int j = 0; j < totalO; j++)
                {
                    k = j + 1;
                    opHold[j] = Request.Form["q"+i+"Option" + k];
                }
            }

            question qHold = new question(Request.Form["tb" + i], Request.Form["ddl" + i], totalO, opHold);
            qList.Add(qHold);

        }
        return qList;
    }

}
