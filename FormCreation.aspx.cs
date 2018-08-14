using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

public partial class FormCreation : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        
    }
protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["quesNum"] == null)
        {
            Session["quesNum"] = 0;

        }
        else
        {
            int i = 0;
            while ((int)Session["quesNum"] > i)
            {
                generateNewQuestion();
                i++;
            }
        }
}

    protected void question_Click(object sender, EventArgs e)
    {
        Session["quesNum"] = (int)Session["quesNum"] + 1;
     //   generateNewQuestion();

        //Cache["q" +(int)Session["quesNum"]] =  
       
    }


    protected void generateNewQuestion()
    {

        PlaceHolder ph = new PlaceHolder();
        TextBox tb = new TextBox();
        Label lbl = new Label();

        newContent.Controls.Add(ph);
        lbl.Text = "What is Question #" + (int)Session["quesNum"];
        tb.ID = "q" + (int)Session["quesNum"];
        ph.Controls.Add(lbl);
        ph.Controls.Add(new LiteralControl("<br />"));
        ph.Controls.Add(tb);


    }

    protected void text_Click(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {

    }
}