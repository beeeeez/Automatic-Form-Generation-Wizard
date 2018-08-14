﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = HttpContext.Current.Request.Url.AbsolutePath;
        if (Session["loggedIn"] == null)
        {
            Session["loggedIn"] = false;
        }

        if ((bool)Session["loggedIn"] == false)
        {
            if(path == "Homepage.aspx")
            {
                Response.BufferOutput = true;
                Response.Redirect("Default.aspx");
            }
        }


    }
}
