using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*this is an object that holds a single entry of what is listed on the homepage table
 * its functions as a way of taking multiple sql queries and putting them into a parsable list
 * 
 * */
public class hpTableEntry
{
    public int formid;
    public string formtitle;
    public DateTime creationdate;
    public int completedforms;
    public hpTableEntry(int formid, string formtitle, DateTime creationdate, int completedforms)
    {
        this.Formid = formid;
        this.Formtitle = formtitle;
        this.Creationdate = creationdate;
        this.Completedforms = completedforms;
    }

    public int Formid
    {
        get { return formid; }
        set { formid = value; }
    }

    public string Formtitle
    {
        get { return formtitle; }
        set { formtitle = value; }
    }
    public DateTime Creationdate
    {
        get { return creationdate; }
        set { creationdate = value; }
    }

    public int Completedforms
    {
        get { return completedforms; }
        set { completedforms = value; }
    }
}