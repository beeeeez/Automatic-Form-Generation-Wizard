using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for question
/// </summary>
public class question
{
    public string title;
    public string type;
    public string[] options;


    public question(string title, string type, string[] options)
    {
        this.Title = title;
        this.Type = type;
        this.Options = options;
    }

    public string Title
    {
        get
        {
            return title;
        }
        set
        {
            title = value;
        }
    }

    public string Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }


    public string[] Options
    {
        get
        {
            return options;
        }
        set
        {
            options = value;
        }
    }
}