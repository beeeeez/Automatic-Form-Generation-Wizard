﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

/*This "Forms" class handles the SQL calls for the creation, updating, deletion, and searching of the form structure table
 * it requires no arguments for construction
 * 
 * getFormStructure(int formid, string username) - returns a list of question objects from the structure table
 * updateStructure(int formid, int totalQ, List<question> qList) - updates the structure table by dropping it and then runs functions to recreate a new version of it
 * createStructure(int formid, int totalQ, List<question> qList) - called by updateStructure and creates the structure table in the database based on the list of question objects its handed
 * fillStructure(int formid, int totalQ, List<question> qList) - also called by updateStructure and fills in the newly created table with the relevant answers
 * createInstanceMaster(int formid) - creates the Instance Master table for a newly created form
 * public int getNewFormID(DateTime anchorDate) - returns the newly created formid, used during form creation
 * getFormTitle(int formid, string username) - returns the form title by using the formid as a reference
 * createFormid(string formtitle) - inserts a new record in the structure table, thus creating a new incremental formid
 * deleteFormStructure(string username, int formid) - drops the structure table
 * removeFormEntry(string username, int formid) - called by removeFormEntry to remove the deleted structure's entry in the master table
 * deleteAllInstances(string username, int formid) - creates an instance object and runs a number of instance functions to delete all instances and the instance master table of a deleted structure
 * pullMasterList(string username) - returns a list of hpTableEntry objects. Used in the creation of the table on the homepage
 * getNumberOfInstances(string username, int formid) - returns the number of instances listed in the instance master table -- not used as of 9/11 
 * 
 * */
public class Forms : SQL
{
    public Forms()
    {

    }

    public List<question> getFormStructure(int formid, string username)
    {
        List<question> qList = new List<question>();
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_" + formid.ToString() + "_Structure";
        string sqlStr = "select * from " + tablename;
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        SqlDataReader dataRead = commandah.ExecuteReader();
        int qnum;
        
        string[] stringArr = new string[0];
        while (dataRead.Read())
        {
           qnum = (int)dataRead["qnum"];
            for(int i = 1; i <= qnum; i++)
            {
                question temp = new question(null, null, 0, null);
                temp.title = dataRead["q" + i].ToString();
                temp.type = dataRead["q" + i + "_type"].ToString();
                if (temp.type == "multiple" || temp.type == "checkbox")
                {
                    temp.opnum = (int)dataRead["q" + i + "_opnum"];
                    stringArr = new string[temp.opnum];

                    for (int j = 0, k = 1; j < temp.opnum; j++, k++)
                    {
                        stringArr[j] = dataRead["q" + i + "_op" + k].ToString();
                    }
                    temp.options = stringArr;
                }
                qList.Add(temp);
            }

        }
        connection.Close();
        return qList;
    }

    public void updateStructure(int formid, int totalQ, List<question> qList)
    {
        SqlConnection connection = new SqlConnection(connString);
        string tablename = HttpContext.Current.Session["username"].ToString() + "_" + formid.ToString() + "_Structure";
        string sqlStr = "drop table " + tablename;
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
        createStructure(formid, totalQ, qList);
        fillStructure(formid, totalQ, qList);
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

    public string getFormTitle(int formid, string username)
    {
        string formtitle = "";
        SqlConnection connection = new SqlConnection(connString);
        string tablename = username + "_master";
        string sqlStr = "select * from " + tablename + " where formid = @Formid";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        commandah.Parameters.AddWithValue("@Formid", formid);
        connection.Open();
        SqlDataReader dataRead = commandah.ExecuteReader();
        while (dataRead.Read())
        {
            formtitle = (string)dataRead["form_title"];
        }
        return formtitle;
    }

    public DateTime createFormid(string formtitle)
    {
        DateTime anchorDate = DateTime.Now;
        SqlConnection connection = new SqlConnection(connString);
        string tablename =  HttpContext.Current.Session["username"].ToString() + "_master";
        string sqlStr = "insert into " + tablename +" (form_title, creation_date) Values (@Formtitle,  @Date)";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        commandah.Parameters.AddWithValue("@Formtitle", formtitle);
        commandah.Parameters.AddWithValue("@Date", anchorDate);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
        return anchorDate;
    }

   /* public DataSet populateFormsTable() // this is defunct
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
    }*/

    public void deleteFormStructure(string username, int formid)
    {
        string tablename = username + "_" + formid.ToString() + "_Structure";
        SqlConnection connection = new SqlConnection(connString);
        string sqlStr = "Drop Table " + tablename;
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();
        removeFormEntry(username, formid);
    }

    public void deleteAllInstances(string username, int formid)
    {
        Instance temp = new Instance();
        List<int> instanceIdList =temp.returnInstanceIDs(username, formid);
        foreach(int instanceid in instanceIdList)
        {
            temp.deleteInstance(username, formid, instanceid);
        }
        temp.deleteInstanceMaster(username, formid);
        
    }

    public void removeFormEntry(string username, int formid)
    {
        string tablename = username + "_Master";
        SqlConnection connection = new SqlConnection(connString);
        string sqlStr = "Delete from " + tablename + " where formid = @Formid";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        commandah.Parameters.AddWithValue("@Formid", formid);
        connection.Open();
        commandah.ExecuteNonQuery();
        connection.Close();

    }

    public List<hpTableEntry> pullMasterList(string username)
    {
        List<hpTableEntry> masterList = new List<hpTableEntry>();
        string tablename = username + "_Master";
        SqlConnection connection = new SqlConnection(connString);
        string sqlStr = "Select * from " + tablename;
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();       
        SqlDataReader dataRead = commandah.ExecuteReader();
        while (dataRead.Read())
        {
            int tempID = 0;
            Int32.TryParse(dataRead["formid"].ToString(), out tempID);
            string tempTitle = dataRead["form_title"].ToString();
            DateTime tempDT = (DateTime)dataRead["creation_date"];
            //int tempInstanceCount = getNumberOfInstances(username, tempID);
            int tempInstanceCount = 0;
            hpTableEntry tempEntry = new hpTableEntry(tempID, tempTitle, tempDT, tempInstanceCount);
            masterList.Add(tempEntry);

        }
        connection.Close();
        masterList = insertInstanceCount(masterList, username);

        return masterList;
    }

    public List<hpTableEntry> insertInstanceCount(List<hpTableEntry> masterList, string username)
    {
        foreach(hpTableEntry entry in masterList)
        {
            int tempInstanceCount = getNumberOfInstances(username, entry.formid);
            entry.completedforms = tempInstanceCount;
        }


        return masterList;
    }

    public int getNumberOfInstances(string username, int formid)
    {
        int numIns = 0;
        string tablename = username + "_" + formid.ToString() + "_Instance_Master";
        SqlConnection connection = new SqlConnection(connString);
        string sqlStr = "Select * from " + tablename;
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        connection.Open();
        SqlDataReader dataRead = commandah.ExecuteReader();
        while (dataRead.Read())
        {
            numIns++;
        }
        
        connection.Close();
        return numIns;
    }



    }