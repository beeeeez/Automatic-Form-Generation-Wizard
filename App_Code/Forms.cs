using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

/// <summary>
/// Summary description for Forms
/// </summary>
public class Forms : SQL
{
    public Forms()
    {

    }

    public DataSet populateFormsTable()
    {
        try
        {
            string tablename = HttpContext.Current.Session["username"].ToString() + "_master";
            DataSet bung = new DataSet();
            SqlConnection connection = new SqlConnection(connString);
            string sqlStr = "Select * from " + tablename;
            SqlCommand commandah = new SqlCommand(sqlStr, connection);
            SqlDataAdapter dataShmata = new SqlDataAdapter();
            dataShmata.SelectCommand = commandah;
            connection.Open();
            dataShmata.Fill(bung, "fff");
            connection.Close();
            HttpContext.Current.Session["isthereData"] = true;
            return bung;

        }
        catch (SqlException ex)
        {

            HttpContext.Current.Session["errorMsg"] = ex.ToString();
            HttpContext.Current.Session["isthereData"] = false;
            DataSet bung = new DataSet();
            return bung;
          
        }
    }
}