using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*this is an object that holds a single entry of what is listed on the tracking page table
 * its functions as a way of taking multiple sql queries and putting them into a parsable list
 * 
 * */
public class tpTableEntry
{

    public int instanceid;
    public DateTime creationdate;
    public string firstquestion;

    public tpTableEntry(int instanceid, DateTime creationdate, string firstquestion)
    {
        this.Instanceid = instanceid;
        this.Creationdate = creationdate;
        this.Firstquestion = firstquestion;
    }

    public int Instanceid
    {
        get { return instanceid; }
        set { instanceid = value; }
    }

    public DateTime Creationdate
    {
        get { return creationdate; }
        set { creationdate = value; }
    }

    public string Firstquestion
    {
        get { return firstquestion; }
        set { firstquestion = value; }
    }
}