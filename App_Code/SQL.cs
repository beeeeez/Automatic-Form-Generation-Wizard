using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

//This is the base class for Forms and Instance objects. All it has is the connection string. 
public class SQL
{
    public string connString = "Data Source = sql.neit.edu,4500; Initial Catalog =SE265_CBAW; User ID=SE265_CBAW; Password= w1zards;";
    public SQL()
    {
        HttpContext.Current.Session["errorMsg"] = "";

    }
      

}