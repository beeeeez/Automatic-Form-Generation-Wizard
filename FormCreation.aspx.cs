using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

public partial class FormCreation : System.Web.UI.Page
{


   public PlaceHolder ph = new PlaceHolder();
   public TextBox tb = new TextBox();
   public Label lbl = new Label();
   public Button del = new Button();
   public DropDownList dd = new DropDownList();


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

    protected void populateDropDown()
    {
        dd.Items.Add(new ListItem("Short Text"));
        dd.Items.Add(new ListItem("Long Text"));
        dd.Items.Add(new ListItem("Multiple Choice"));
        dd.Items.Add(new ListItem("Checkboxes"));
        dd.Items.Add(new ListItem("Date & Time"));

    }
    protected void populateQuestions()
    {

        if((List<question>)Session["masterlist"])
            List<question> clay = (List<question>)Session["masterlist"];
            int i = 0;
            foreach (question bb in clay) {        

            ph.ID = "ph" + i+1;
            newContent.Controls.Add(ph);
            lbl.Text = "What is Question #" + i+1;
            tb.Text = bb.title;
            del.Text = "Delete this Question";
            del.Click += new EventHandler(this.deleteQuestion_Click);


            ph.Controls.Add(lbl);
            ph.Controls.Add(new LiteralControl("<br />"));
            ph.Controls.Add(tb);
            ph.Controls.Add(del);
            ph.Controls.Add(new LiteralControl("<br />"));
            ph.Controls.Add(dd);

            if (bb.type == "multple" || bb.type == "checkbox")
            {
                foreach(string x in bb.options)
                {
                    tb.Text = x;
                    del.Text = "Delete this Option";
                    del.ID = ph.ID;
                    del.Click += new EventHandler(this.deleteOption_Click);
                    ph.Controls.Add(tb);
                    ph.Controls.Add(del);
                    del.ID = ph.ID;
                    del.Text = "Add Option";
                    del.Click += new EventHandler(this.addOption_Click);
                    ph.Controls.Add(del);
                }

            }


            i++;

        }
          
    }


    protected void deleteQuestion_Click(object sender, EventArgs e)
    {



    }


    protected void addOption_Click(object sender, EventArgs e)
    {



    }




    protected void deleteOption_Click(object sender, EventArgs e)
    {

    }
    
    

    protected void question_Click(object sender, EventArgs e)
    {
        
        string[] options = null;
        question add = new question("", "short", options);
        List<question> clay = (List<question>)Session["masterlist"];
        clay.Add(add);
        Session["masterlist"] = clay;
        Session["quesNum"] = (int)Session["quesNum"] + 1;

    }

    protected void text_Click(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {

    }
}