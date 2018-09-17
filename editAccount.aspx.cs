using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class editAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void passChangeBtn_Click(object sender, EventArgs e)// this script runs when the change password button is clicked
    {

        bool kosher;
        kosher = isformEmpty(currentPassBox.Text);//empty form validation checks
        kosher = isformEmpty(newPassBox.Text);
        kosher = isformEmpty(confirmPassBox.Text);
        if (kosher == false)
        {
            passErr.Text = "Please fill out all of the forms.";
        }
        else
        {
            kosher = doformsMatch(newPassBox.Text, confirmPassBox.Text);
            if (kosher == false)
            {
                passErr.Text = "Your passwords don't match.";
            }
            else
            {
                Users temp = new Users();
                temp.loginAttempt(Session["username"].ToString(), currentPassBox.Text);
                kosher = (bool)Session["kosher"];
                if (kosher == false)
                {
                    passErr.Text = "Wrong password.";
                }
                else
                {
                    string message = temp.changePassword(Session["username"].ToString(), newPassBox.Text);
                    if (message != "success")
                    {
                        passErr.Text = "There was an error with your input.";
                    }
                    else
                    {
                        passErr.Text = "Your password has been successfully changed.";
                    }
                }
            }
        }
        if (passErr.Text != "")
        {
            passErr.Text += "<br /><br />";
        }
    }


    protected bool isformEmpty(string input)// empty form validation
    {
        bool kosher = true;

        if (input == "")
        {
            kosher = false;
        }
        return kosher;

    }

    protected bool doformsMatch(string form1, string form2) // form matching validation
    {
        bool kosher = true;
        if (form1 != form2)
        {
            kosher = false;
        }
        return kosher;
    }


    protected void emailChangeBtn_Click(object sender, EventArgs e)//script that runs when the change email address button is clicked 
    {

        bool kosher;
        kosher = isformEmpty(currentEmailBox.Text);
        kosher = isformEmpty(newEmailBox.Text);
        kosher = isformEmpty(confirmEmailBox.Text);
        if (kosher == false)
        {
            mailErr.Text = "Please fill out all of the forms.";
        }
        else
        {
            kosher = doformsMatch(newEmailBox.Text, confirmEmailBox.Text);
            if (kosher == false)
            {
                mailErr.Text = "Your email accounts don't match.";
            }
            else
            {
                Users temp = new Users();
                kosher = temp.emailMatch(Session["username"].ToString(), currentEmailBox.Text);
                if(kosher == false)
                {
                    mailErr.Text = "Wrong Email";
                }
                else
                {
                    kosher = temp.emailChange(Session["username"].ToString(), newEmailBox.Text);

                    if(kosher == false)
                    {
                        mailErr.Text = "There was a problem with your input.";
                    }
                    else
                    {
                        mailErr.Text = "Your email address has been successfully changed.";
                    }

                }
            }
        }
        if(mailErr.Text != "")
        {
            mailErr.Text += "<br /><br />";
        }
    }
}