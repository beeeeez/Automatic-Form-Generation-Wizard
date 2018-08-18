using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




public partial class FormCreate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //saveSubmit.Visible = false;
        if (IsPostBack == true)
        {
         

            Forms create = new Forms();
            DateTime anchorDate = create.createFormid(Request.Form["formtitle"].ToString());
            int formid = create.getNewFormID(anchorDate);
            List<question> qList = parsePage();
            if (formid != 0)
            {
                int totalQ;
                Int32.TryParse(Request.Form["totalQ"], out totalQ);
                create.createInstanceMaster(formid);
                create.createStructure(formid, totalQ, qList);
                create.fillStructure(formid, totalQ, qList);
            }
            Response.Redirect("Homepage.aspx");
        }
        else
        {
         
        }
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
