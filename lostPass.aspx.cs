using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;


public partial class lostPass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void emailPass_Click(object sender, EventArgs e)
    {
        MailMessage msg = new MailMessage();
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        

        try
        {
            Users sql = new Users();
            string username = sql.findUsername(lostEmail.Text);
            if (username == "No Username Found!")
            {
                errormsg.Text = "<h4>No username has been found with that email address!</h4>";
            }
            else
            {

                msg.Subject = "Lost Pass";
                msg.Body = "passReset.aspx?username=" + username;
                msg.From = new MailAddress("automaticformgeneratorwizard@gmail.com");
                msg.To.Add(lostEmail.Text);
                msg.IsBodyHtml = true;
                client.Host = "smtp.gmail.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("automaticformgeneratorwizard@gmail.com", "w1zards!");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
            }
        }
        catch (Exception ex)
        {
            Session["emailStatus"] = ex.Message;
        }
    }
}