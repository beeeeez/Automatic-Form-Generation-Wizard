using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Printable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int formid;
        Forms temp = new Forms();

        if (Request.QueryString["formid"] == null)
        {
            Int32.TryParse(Session["formid"].ToString(), out formid);
        }
        else
        {
            Int32.TryParse(Request.QueryString["formid"], out formid);
        }

        List<question> qList = temp.getFormStructure(formid, Session["username"].ToString());

        /*if else statement that looks if the formid or the instanceid is not null 
         if the formdid is not null it will go through each question on the form and display it on the screen
         */

        if (Request.QueryString["formid"] != null)
        {

            int i = 1;

            Literal statictext = new Literal();

            TextBox text = new TextBox();

            //this foreach loops takes all the questions in order to see how many questions and their types
            foreach (question q in qList)
            {
                Literal lit = new Literal();
                lit.Text = "<h4>" + i + ". " + q.title + "</h4>";

                Literal shorty = new Literal();
                Literal longy = new Literal();
                Literal msgg = new Literal();
                Literal msgyy = new Literal();
                CheckBox checky = new CheckBox();
                Literal datee = new Literal();

                create.Controls.Add(lit);
                i++;
                
                //if else statements in which depending on the type of question it creates a format to differentiate them
                if (q.type == "short")
                {

                    string sline = "_____________________________________________________________<br /><br />";
                    shorty.Text = sline;
                    create.Controls.Add(shorty);
                }
                else if (q.type == "long")
                {
                    string lline = "________________________________________________________________________________________________________________<br /> ________________________________________________________________________________________________________________ <br /> ________________________________________________________________________________________________________________ <br /> ________________________________________________________________________________________________________________ <br /> ________________________________________________________________________________________________________________<br /><br /> ";
                    longy.Text = lline;
                    create.Controls.Add(longy);
                }
                else if (q.type == "multiple")
                {
                    string msgy = "Select the best option: <br />";
                    msgyy.Text = msgy;
                    create.Controls.Add(msgyy);
                    int j = 0;
                    foreach (string option in q.options)
                    {
                        j++;
                        Literal opt = new Literal();
                        opt.Text = "⵲  " + option + "<br/ >";

                        if (j == q.options.Count())
                        {
                            opt.Text += "<br/ >";
                        }
                        create.Controls.Add(opt);

                    }
                }
                else if (q.type == "checkbox")
                {
                    string msg = "Select all that apply: <br />";
                    msgg.Text = msg;
                    create.Controls.Add(msgg);
                    int k = 0;
                    foreach (string option in q.options)
                    {
                        k++;
                        Literal op = new Literal();
                        op.Text = "⵲  " + option + " <br /> ";
                        if (k == q.options.Count())
                        {
                            op.Text += "<br/ >";
                        }
                        create.Controls.Add(op);
                    }
                }
                else if (q.type == "datetime")
                {
                    string datey = "____/____/_____  &nbsp;&nbsp;&nbsp; ____:____ &nbsp; AM / PM  ";
                    datee.Text = datey;
                    create.Controls.Add(datee);
                }



            }
        }
        /*this the else part that checks if the instance id is not null
         this part will get the filled form and print it
         */
        else if (Request.QueryString["instanceid"] != null)
        {
            Instance tempt = new Instance();
            string username = Session["username"].ToString();
            int instanceid;
            Int32.TryParse(Request.QueryString["instanceid"], out instanceid);
            List<string> answersList = tempt.getInstanceAnswers(username, formid, instanceid, qList.Count);
            int i = 0;
            int j = 1;

            //this foreach loop goes through the answers from the filled form and adds them to page 
            foreach (string answer in answersList)
            {


                Literal multiii = new Literal();
                Literal checkiii = new Literal();

                Literal qt = new Literal();
                Literal a = new Literal();
                qt.Text = "<h4>" + j + ". " + qList[i].title + "</h4><br />";
                a.Text = "<h5>" + answer + "</h5><br /> <br/>";
                create.Controls.Add(qt);
                j++;
                create.Controls.Add(a);

                /*this if else statement checks if whether the question type is a multiple choice or
                 a checkbox, and depending on what type the question is it will add a checked or unchecked box for the answer*/
              //  int s = 0;
                if (qList[i].type == "multiple")
                {
                    string amulti = answer;
                    foreach (string option in qList[i].options)
                    {
                        Literal opt = new Literal();


                        if (option == answer)
                        {
                            string duh = "<img src=" + '"' + "./checked.png" + '"' + '"' + " /> ";
                            multiii.Text = duh + " " + answer + "<br />";
                            create.Controls.Remove(a);
                            create.Controls.Add(multiii);
                        }
                        else
                        {
                            string no = "<img src=" + '"' + "./unchecked.png" + '"' + '"' + "/>";
                            opt.Text = no + " " +option + "<br /> ";
                            create.Controls.Remove(a);
                            create.Controls.Add(opt);
                        }

                    }

                    Literal spaceee = new Literal();
                    spaceee.Text = "<br />";
                    create.Controls.Add(spaceee);
                }
                else if (qList[i].type == "checkbox")
                {
                    string[] checkedList = answer.Split(',');
                   
                    foreach (string options in qList[i].options)
                    {
                        Literal op = new Literal();
                        string nope = "<img src=" + '"' + "./unchecked.png" + '"' + '"' + "/>";
                        op.Text = nope + " " + options + "<br / >";
                        create.Controls.Remove(a);
                        create.Controls.Add(op);                              
                        
                        foreach(string checkedAns in checkedList)
                        {
                            if(options == checkedAns)
                            {
                                Literal checkedBox = new Literal();
                                string sii = "<img src=" + '"' + "./checked.png" + '"' + '"' + " />";
                                checkedBox.Text = sii + " " + checkedAns + "<br />";
                                create.Controls.Remove(op);
                                create.Controls.Add(checkedBox);
                            }
                        }

                    }
                    Literal sp = new Literal();
                    sp.Text = "<br />";
                    create.Controls.Add(sp);


                    /*

                    string achecki = answer;
                    string[] checkedList = answer.Split(',');

                    foreach (string options in qList[i].options)
                    {
                        Literal op = new Literal();                                            

                            if (options == answer)
                            {
                                string sii = "<img src=" + '"' + "./checked.png" + '"' + '"' + " />";
                                checkiii.Text = sii + " " + answer + "<br />";
                                create.Controls.Remove(a);
                                create.Controls.Add(checkiii);
                            }
                            else
                            {
                                string nope = "<img src=" + '"' + "./unchecked.png" + '"' + '"' + "/>";
                                op.Text = nope + " " + options + "<br / >";
                                create.Controls.Remove(a);
                                create.Controls.Add(op);
                            }
                        }


                    Literal sp = new Literal();
                    sp.Text = "<br />";
                    create.Controls.Add(sp);
                    */
                }



                    i++;
            }
        }

    }


}