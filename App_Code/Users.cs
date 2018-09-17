using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

/*This "Users" class handles the SQL calls for the creation, updating, deletion, and searching of the users_key and users_master tables
 * it requires no arguments for construction
 * 
 * changePassword(string username, string password) - changes a users password to a new password
 * findUsername(string email) - uses email address to find a username
 * loginAttempt(string user, string knock) - searches for a username and password and sets the corresponding Session variables
 * registerUser(string user, string knock, string email) - registers a user into the users_key table
 * emailMatch(string user, string email) - returns a boolean whose value is based on whether there is a match with both username and email address
 *  emailChange(string user, string email) - changes a user's email address value in the users_key table
 * 
 * */

public class Users : SQL
{
    public Users()
    {

    }

    public string changePassword(string username, string password)
    {
        string message = "";
        try
        {
            SqlConnection connection = new SqlConnection(connString);
            string sqlStr = "update users_key set pass = HASHBYTES('SHA2_512', CONVERT(VARCHAR, @knock)) where username = @Username";
            SqlCommand commandah = new SqlCommand(sqlStr, connection);
            commandah.Parameters.AddWithValue("@knock", password);
            commandah.Parameters.AddWithValue("@Username", username);
            connection.Open();
            commandah.ExecuteNonQuery();
            connection.Close();
            message = "success";
            
        }
        catch(Exception ex)
        {
            message = ex.ToString();
        }
        return message;
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
        HttpContext.Current.Session["kosher"] = false;
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
            HttpContext.Current.Session["kosher"] = true;
        }
        catch (SqlException ex)
        {
            HttpContext.Current.Session["errorMsg"] = ex.ToString();
            HttpContext.Current.Session["loggedIn"] = false;
            HttpContext.Current.Session["kosher"] = false;
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

            string creationStr = "create table " + user + "_master" + " (formid int IDENTITY(1,1) PRIMARY KEY, form_title varchar(250) not null,creation_date datetime not null)"; // you cant parameteralize the table names :l
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

    public bool emailMatch(string user, string email)
    {
        bool kosher = false;
        try
        {
            SqlConnection connection = new SqlConnection(connString);
            string sqlStr = "Select username, email from USERS_KEY where email = @Email AND @Username = username";
            SqlCommand commandah = new SqlCommand(sqlStr, connection);
            commandah.Parameters.AddWithValue("@Email", email);
            commandah.Parameters.AddWithValue("@Username", user);
            SqlDataAdapter dataShmata = new SqlDataAdapter();
            dataShmata.SelectCommand = commandah;
            connection.Open();
            SqlDataReader dataRead = commandah.ExecuteReader();
            while (dataRead.Read())
            {
                if (dataRead["email"].ToString() == email)
                {
                    kosher = true;
                }
            }
            connection.Close();
        }
        catch(SqlException ex)
        {
            
        }
            return kosher;

    }

    public bool emailChange(string user, string email)
    {
        bool kosher = false;
        try
        {
            SqlConnection connection = new SqlConnection(connString);
            string sqlStr = "update users_key set email = @Email where username = @Username";
            SqlCommand commandah = new SqlCommand(sqlStr, connection);
            commandah.Parameters.AddWithValue("@Email", email);
            commandah.Parameters.AddWithValue("@Username", user);
            connection.Open();
            commandah.ExecuteNonQuery();
            connection.Close();
            kosher = true;

        }
        catch (Exception ex)
        {
            kosher = false;
        }
        return kosher;


    }



}