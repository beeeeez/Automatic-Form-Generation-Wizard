using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

/// <summary>
/// Summary description for Instance
/// </summary>
public class Instance : SQL
{
    public Instance()
    {
        
    }

    public void deleteInstance(string username, int formid, int instanceid)
    {
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Instance_" + instanceid.ToString();
        string sqlStr = "DROP TABLE " + tablename;
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
    }

    public void removeInstancefromMaster(string username, int formid, int instanceid)
    {
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Instance_Master" ;
        string sqlStr = "Delete From " + tablename + " where instanceid = @Instanceid";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        commandah.Parameters.AddWithValue("@Instanceid", instanceid);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
    }


    public void deleteInstanceMaster(string username, int formid)
    {
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Instance_Master";
        string sqlStr = "DROP TABLE " + tablename;
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
    }


    public void updateInstanceAnswers(string username, int formid, int instanceid, List<string> answers)
    {


        deleteInstance(username, formid, instanceid);
        createNewInstanceTable(username, formid, instanceid, answers);
        /*
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Instance_" + instanceid.ToString();
        string sqlStr = "update " + tablename + " set ";
        int i = 1;
        foreach(string answer in answers)
        {
            if (i == 1)
            {
                sqlStr += "a" + i + "=" + "'" + answer + "'";
            }
            else
            {
                sqlStr += ", a" + i + "=" + "'" + answer + "'";
            }
            i++;
        }
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
        */
    }

    public List<string> getInstanceAnswers(string username, int formid, int instanceid, int qCount)
    {
        List<string> answerList = new List<string>();
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Instance_" + instanceid.ToString();
        string sqlStr = "select * from " + tablename;
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        SqlDataReader dataRead = commandah.ExecuteReader();
        int i = 1;
        while (dataRead.Read()) { 
        while (i <= qCount)
        {
            answerList.Add(dataRead["a" + i].ToString());
            i++;
        }
    }

        connection.Close();
        return answerList;

    }
    public DataSet populateTrackingTable(string username, int formid)
    {
        string tablename = username + "_" + formid+ "_Instance_Master";
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

    public void filloutForm(string username, int formid, List<String> answers)
    {
        DateTime anchor = createNewInstanceID(username, formid);
        int instanceid = getNewInstanceID(username, formid, anchor);
        createNewInstanceTable(username, formid, instanceid, answers);
        populateInstanceTable(username, formid, instanceid, answers); 
    }

    public DateTime createNewInstanceID(string username, int formid)
    {

        DateTime anchorDate = DateTime.Now;
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Instance_Master";
        string sqlStr = "insert into " + tablename + "(fillout_date) VALUES (@Date)";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        commandah.Parameters.AddWithValue("@Date", anchorDate);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();

        return anchorDate;
    }

    public int getNewInstanceID(string username, int formid, DateTime anchorDate)
    {
        int instanceid = 0;
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Instance_Master";
        string sqlStr = "select * from " + tablename + " where fillout_date = @Date";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        commandah.Parameters.AddWithValue("@Date", anchorDate);
        connection.Open();
        SqlDataReader dataRead = commandah.ExecuteReader();
        while (dataRead.Read())
        {
            instanceid = (int)dataRead["instanceid"];
        }
        connection.Close();
        return instanceid;
    }

    public void createNewInstanceTable(string username, int formid, int instanceid, List<string> answers)
    {
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Instance_"+instanceid.ToString();
        string sqlStr = "create table " + tablename + " ( ";
        int i = 1;
        foreach (string x in answers)
        {
            if(i == 1)
            {
                sqlStr += "a" + i + " varchar(1500) null";
            }
            else
            {
                sqlStr += ",a" + i + " varchar(1500) null";
            }
            i++;
        }

        sqlStr += ")";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
    }

    public void populateInstanceTable(string username, int formid, int instanceid, List<string> answers)
    {
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Instance_" + instanceid.ToString();
        string sqlStr = "insert into " + tablename + " Values (";
        int i = 1;
        foreach (string x in answers)
        {
            if (i == 1)
            {
                sqlStr += "'" + x + "'";
            }
            else
            {
                sqlStr += ", " + "'" + x + "'";
            }
            i++;
        }
        sqlStr += ")";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();

    }

    public List<int> returnInstanceIDs(string username, int formid)
    {
        List<int> count = new List<int>();
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Instance_Master";
        string sqlStr = "Select * from " + tablename;
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        SqlDataReader dataRead = commandah.ExecuteReader();
        while (dataRead.Read())
        {
            int idnum = 0;
            Int32.TryParse(dataRead["instanceid"].ToString(), out idnum);
            count.Add(idnum);
        }
        connection.Close();
        return count;

    }



   
}