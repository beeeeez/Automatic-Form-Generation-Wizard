using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

public class Users : SQL
{
    public Users()
    {

    }

    public string findUsername(string email)
    {
        string username = ""; 
        SqlConnection connection = new SqlConnection(connString);
        string sqlStr = "Select username, email from USERS_KEY where email = @Email";
        SqlCommand commandah = new SqlCommand(sqlStr, connection);
        commandah.Parameters.AddWithValue("@Email", email);
        SqlDataAdapter dataShmata = new SqlDataAdapter();
        dataShmata.SelectCommand = commandah;
        connection.Open();
        SqlDataReader dataRead = commandah.ExecuteReader();
        while (dataRead.Read())
        {
            username = dataRead["username"].ToString();
        }
        connection.Close();
        if(username == "")
        {
            username = "No Username Found!";
        }
        return username;

    }

    public void loginAttempt(string user, string knock)
    {
        try
        {
            DataSet bung = new DataSet();
            SqlConnection connection = new SqlConnection(connString);
            string sqlStr = "Select username, email from USERS_KEY where username = @Username and pass = HASHBYTES('SHA2_512', CONVERT(VARCHAR, @knock))";
            SqlCommand commandah = new SqlCommand(sqlStr, connection);
            commandah.Parameters.AddWithValue("@Username", user);
            commandah.Parameters.AddWithValue("@knock", knock);
            SqlDataAdapter dataShmata = new SqlDataAdapter();
            dataShmata.SelectCommand = commandah;
            connection.Open();
            SqlDataReader dataRead = commandah.ExecuteReader();
            while (dataRead.Read())
            {
                HttpContext.Current.Session["username"] = dataRead["username"].ToString();
                HttpContext.Current.Session["loggedIn"] = true;
                HttpContext.Current.Session["email"] = dataRead["email"].ToString();
            }
            connection.Close();
        }
        catch (SqlException ex)
        {
            HttpContext.Current.Session["errorMsg"] = ex.ToString();
            HttpContext.Current.Session["loggedIn"] = false;
        }
    }

    public void registerUser(string user, string knock, string email)
    {
        try
        {
            SqlConnection connection = new SqlConnection(connString);
            string sqlStr = "insert into users_key (username, pass, email) VALUES (@user, HASHBYTES('SHA2_512', CONVERT(VARCHAR, @knock)), @email)";
            SqlCommand commandah = new SqlCommand(sqlStr, connection);
            commandah.Parameters.AddWithValue("@user", user);
            commandah.Parameters.AddWithValue("@knock", knock);
            commandah.Parameters.AddWithValue("@email", email);
            connection.Open();
            commandah.ExecuteNonQuery();
            connection.Close();

            string creationStr = "create table " + user + "_master" + " (formid int IDENTITY(1,1) PRIMARY KEY, form_title varchar(25) not null,creation_date datetime not null)"; // you cant parameteralize the table names :l
            commandah = new SqlCommand(creationStr, connection);
            connection.Open();
            commandah.ExecuteNonQuery();
            connection.Close();

        }
        catch(SqlException ex)
        {
            HttpContext.Current.Session["errorMsg"] = ex.ToString();
        }
    }



}