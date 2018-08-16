using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

public partial class FormCreation : System.Web.UI.Page
{
    public List<PlaceHolder> phList = new List<PlaceHolder>();
    public List<TextBox> tbList = new List<TextBox>();
    public List<DropDownList> ddList = new List<DropDownList>();
    public List<List<string>> opList = new List<List<String>>();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        
    }
protected void Page_Load(object sender, EventArgs e)
    {
        populateDropDown();
        if (Session["quesNum"] == null)
        {
            Session["quesNum"] = 0;
        }
        if (Session["masterlist"] == null)
        {
            Session["masterlist"] = new List<question>();
        }
        else
        {
            populateQuestions();
        }
}

    protected DropDownList populateDropDown()
    {
        DropDownList dd = new DropDownList();
        
        dd.Items.Add(new ListItem("Short Text"));
        dd.Items.Add(new ListItem("Long Text"));
        dd.Items.Add(new ListItem("Multiple Choice"));
        dd.Items.Add(new ListItem("Checkboxes"));
        dd.Items.Add(new ListItem("Date & Time"));
        
        return dd;

    }
    protected void populateQuestions()
    {
     
    List<question> clay = (List<question>)Session["masterlist"];
        if (clay.Count > 0)
        {
            int i = 0;
            int p = 1;

            foreach (question bb in clay)
            {
                PlaceHolder ph = new PlaceHolder();
                TextBox tb = new TextBox();
                Label lbl = new Label();
                Button del = new Button();
                DropDownList dd = populateDropDown();
                dd.AutoPostBack = true;
                dd.SelectedIndexChanged += new EventHandler(this.ddChange);
                newContent.Controls.Add(ph);
                lbl.Text = "What is Question #" + p;
                tb.Text = bb.title;
                tb.ID = "tb" + p;
                tbList.Add(tb);
                ddList.Add(dd);
                dd.ID = "ph" + i;
                dd.Text = bb.type;
                del.Text = "Delete this Question";
                del.CommandName = i.ToString();
                del.Command += new CommandEventHandler(this.deleteQuestion_Click);


                ph.Controls.Add(lbl);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(tb);
                ph.Controls.Add(del);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(dd);

                if (bb.type == "multple" || bb.type == "checkbox")
                {
                    PlaceHolder op = new PlaceHolder();
                    op.ID = "op" + p;
                    int u = 1;
                   

                    
                    foreach (string x in bb.options)
                    {
                        tb.Text = x;
                        opList[i][u] = tb.Text;
                        del.Text = "Delete this Option";
                   
                        del.CommandName = u.ToString();
                        del.Command += new CommandEventHandler(this.deleteOption_Click);
                        op.Controls.Add(tb);
                        opList[i][u] = tb.Text;
                        op.Controls.Add(del);
                        del.CommandName = u.ToString();
                        del.Text = "Add Option";
                        del.Command += new CommandEventHandler(this.addOption_Click);
                        op.Controls.Add(del);
                        u++;
                    }

                    ph.Controls.Add(op);
                  
                }
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<hr />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                phList.Add(ph);
                i++;
                p++;

            }

        }
    }

    protected void deleteQuestion_Click(object sender, CommandEventArgs e)
   { 
        int i;
        Int32.TryParse(e.CommandName, out i);
        List<question> clay = (List<question>)Session["masterlist"];
        clay.Remove(clay[i]);
        Session["masterlist"] = clay;
        Response.Redirect(Request.RawUrl);
    }

    protected void ddChange(object sender, EventArgs e)
    {            
        DropDownList self = sender as DropDownList;

        if(self.Text == "Multiple Choice" || self.Text == "Checkbox")
        {
            int i = 0;
            foreach(DropDownList dd in ddList)
            {
                if(dd.ID == self.ID)
                {
                    opList[i][0] = "Untitled Option 1";
                }
                i++;
            }
        }
        Response.Redirect(Request.RawUrl);


    }


    protected void addOption_Click(object sender, CommandEventArgs e)
    {



    }




    protected void deleteOption_Click(object sender, CommandEventArgs e)
    {

    }

    protected void saveList()
    {
        List<question> clay = (List<question>)Session["masterlist"];
        int i = 0;
        foreach(question bb in clay)
        {
            clay[i].title = tbList[i].Text;
            clay[i].type = ddList[i].Text;
            if (clay[i].type == "Multple Choice" || clay[i].type == "Checkbox")
            {
            //    clay[i].options = opList[i];
            }
            i++;
        }
        Session["masterlist"] = clay;

    }

    protected void question_Click(object sender, EventArgs e)
    {

        string[] options = null;
        saveList();
        question add = new question("", "short", options);
        List<question> clay = (List<question>)Session["masterlist"];
        clay.Add(add);
        Session["masterlist"] = clay;
        Session["quesNum"] = (int)Session["quesNum"] + 1;
        Response.Redirect(Request.RawUrl);
        
       
    }

    protected void text_Click(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {

    }
}