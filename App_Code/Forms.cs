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
    public void createStructure(int formid, int totalQ, List<question> qList)
    {
        SqlConnection connection = new SqlConnection(connString);
        string tablename = HttpContext.Current.Session["username"].ToString() + "_" + formid.ToString() + "_Structure";
        string sqlStr = "create table " + tablename + " (qnum int not null";
        int qCount = 1;
        foreach(question q in qList)
        {
            sqlStr += ", q" + qCount + " varchar(250) not null";
            sqlStr += ", q" + qCount + "_type varchar(100) not null";
            if (q.type == "multiple" || q.type== "checkbox")
            {

                int opNum = q.opnum;
                sqlStr += ", q" + qCount + "_opnum int not null";
             for(int i =1; i <= opNum; i++)
                {
                    sqlStr += ", q" + qCount + "_op" + i + " varchar(250) not null";
                }
            }
            qCount++;
        }
        sqlStr += ")";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
    }

    public void fillStructure(int formid, int totalQ, List<question> qList)
    {
        SqlConnection connection = new SqlConnection(connString);
        string tablename = HttpContext.Current.Session["username"].ToString() + "_" + formid.ToString() + "_Structure";
        string sqlStr = "insert into " + tablename + " Values (" + totalQ;
        foreach(question q in qList)
        {
            sqlStr += ", " +"'" + q.Title + "'";
            sqlStr += ", " +"'" + q.Type + "'";
            if (q.type == "multiple" || q.type == "checkbox")
            {
                sqlStr += ", " + q.opnum;
                foreach(string str in q.options)
                {
                    sqlStr += ", " + "'" + str + "'";
                }
            }
        }
        sqlStr += ")";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
    }

    public void createInstanceMaster(int formid)
    {
        SqlConnection connection = new SqlConnection(connString);
        string tablename = HttpContext.Current.Session["username"].ToString() + "_"+formid.ToString()+"_Instance_Master";
        string sqlStr = "create table " + tablename + " (instanceid int IDENTITY(1,1) PRIMARY KEY, fillout_date datetime not null)";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
    }

    public int getNewFormID(DateTime anchorDate)
    {
        int formid=0;
        SqlConnection connection = new SqlConnection(connString);
        string tablename = HttpContext.Current.Session["username"].ToString() + "_master";
        string sqlStr = "select * from " + tablename + " where creation_date = @Date";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        commandah.Parameters.AddWithValue("@Date", anchorDate);
        connection.Open();
        SqlDataReader dataRead = commandah.ExecuteReader();
        while (dataRead.Read())
        {
            formid = (int)dataRead["formid"];
        }
        return formid;
        }

    public DateTime createFormid(string formtitle)
    {
        DateTime anchorDate = DateTime.Now;
        SqlConnection connection = new SqlConnection(connString);
        string tablename =  HttpContext.Current.Session["username"].ToString() + "_master";
        string sqlStr = "insert into " + tablename +" (form_title, creation_date) Values ('"+formtitle+"',  @Date)";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        commandah.Parameters.AddWithValue("@Date", anchorDate);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
        return anchorDate;
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