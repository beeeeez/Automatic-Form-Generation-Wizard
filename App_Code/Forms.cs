﻿using System;
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