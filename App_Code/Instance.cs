using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

/*This "Instance" class handles the SQL calls for the creation, updating, deletion, and searching of the instance_master and each individual instance table
 * it requires no arguments for construction
 * 
 * deleteInstance(string username, int formid, int instanceid) - drops a specific instance table
 * removeInstancefromMaster(string username, int formid, int instanceid) - removes an entry from the instance_master table - used in instance deletion
 * deleteInstanceMaster(string username, int formid) - drops the instance_master table for a specific form - used in form deletion
 * updateInstanceAnswers(string username, int formid, int instanceid, List<string> answers) - drops the orignal instance table and recreates it with new values
 * getInstanceAnswers(string username, int formid, int instanceid, int qCount) - returns a list of answers from a specific instance table
 * pullMasterInstanceList(string username, int formid) - parses the instance_master table for every entry and creates a list of tpTableEntry objects that hold the values generated in the tracking page table
 * insertFirstQuestion(string username, int formid, List<tpTableEntry> masterInstanceList) - grabs the first answer from the instance table and ammends the list of tpTableEntry objects with the appropriate values
 * filloutForm(string username, int formid, List<String> answers) - handles the creation of a new instance (when somebody submits a form fillout)
 * createNewInstanceID(string username, int formid) - makes a new entry in the instance_master table
 * getNewInstanceID(string username, int formid, DateTime anchorDate) - returns the newly created instance id from the entry in the instance_master table
 * createNewInstanceTable(string username, int formid, int instanceid, List<string> answers) - creates a new instance table 
 * populateInstanceTable(string username, int formid, int instanceid, List<string> answers)- inserts the answers into the newly created instance table
 * returnInstanceIDs(string username, int formid) - returns a list of instance ids stored in the instance master table
 * 
 * 
 * 
 * 
 * */
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
        populateInstanceTable(username, formid, instanceid, answers);
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

    public List<tpTableEntry> pullMasterInstanceList(string username, int formid)
    {
        List<tpTableEntry> masterInstanceList = new List<tpTableEntry>();
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid + "_Instance_Master";
        string sqlStr = "select * from " + tablename;
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        SqlDataReader dataRead = commandah.ExecuteReader();
        while(dataRead.Read())
        {
            int tempid = 0;
            Int32.TryParse(dataRead["instanceid"].ToString(), out tempid);

            tpTableEntry temp = new tpTableEntry(tempid, (DateTime)dataRead["fillout_date"], "");
            masterInstanceList.Add(temp);
        }
        connection.Close();
        masterInstanceList = insertFirstQuestion(username, formid, masterInstanceList);
        return masterInstanceList;
    }

    public List<tpTableEntry> insertFirstQuestion(string username, int formid, List<tpTableEntry> masterInstanceList)
    {
        foreach(tpTableEntry entry in masterInstanceList)
        {
            SqlConnection connection = new SqlConnection(connString);
            string tablename = username + "_" + formid + "_Instance_" + entry.instanceid;
            string sqlStr = "select a1 from " + tablename;
            SqlCommand commandah = new SqlCommand(sqlStr, connection);
            connection.Open();
            SqlDataReader dataRead = commandah.ExecuteReader();
            while (dataRead.Read())
            {
                entry.firstquestion = dataRead["a1"].ToString();
            }
            connection.Close();
        }
        return masterInstanceList;
    }

   public DataSet populateTrackingTable(string username, int formid) // defunct
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