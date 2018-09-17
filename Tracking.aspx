<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Tracking.aspx.cs" Inherits="Tracking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
       /* .contain { width:80%; margin:auto; margin-top:5%;}
        #displayName {display:inline;}
        .tablething {padding:2%; border-style:none; margin-bottom:2%;}
        .butt {margin-left: 2%; margin-right: 2%;}
        #notify {color:green;}
        .right{float:right;}
        h3{display:inline-block; float:left;}*/

       .contain { width:80%; margin:auto; margin-top:5%;}
        #displayName {display:inline;}
        table{ border: none;}
        .butt {margin-left: 2%; margin-right: 2%;}
        .notify {color:green;}
        .btn {margin-bottom:0.5%; margin-right:0.5%;}
        .rightsideBtns{float:right; text-align:right;}
        .icon{font-size:28px; color:white;}
        .cell{text-align:center;}
        .header{color:#375a7f; font-size:16px;}
        .header span{margin-top:5px; display:inline-block;}
        .icon:hover{font-size:28px; color:#375a7f;}
        h3{display:inline-block;}
        

    </style>

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU" crossorigin="anonymous">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contain">
    <form runat="server">
           <asp:literal runat="server" id="header"></asp:literal><br />
        <asp:Literal ID="notify" runat="server"></asp:Literal>

        <asp:PlaceHolder ID="tpTablePH" runat="server"></asp:PlaceHolder>

    <!--
        <asp:GridView ID="putStuff" runat="server" Visible="false" AutoGenerateColumns="false" CssClass="table table-hover" BorderStyle="None" BorderWidth="0px" BorderColor="#222222">
            <Columns>
                 <asp:BoundField DataField="instanceid" HeaderText="Instance ID #" ItemStyle-CssClass="tablething"/>
                <asp:BoundField DataField="fillout_date" HeaderText="Creation Date" ItemStyle-CssClass="tablething" />
                  <asp:HyperLinkField Text="Edit Instance" DataNavigateUrlFormatString="fillOut.aspx?instanceid={0}" DataNavigateUrlFields="instanceid" ItemStyle-CssClass="btn btn-primary butt tablething" />
                 <asp:HyperLinkField Text="Print Instance" DataNavigateUrlFormatString="Printable.aspx?instanceid={0}" Target="_blank" DataNavigateUrlFields="instanceid" ItemStyle-CssClass="btn btn-primary butt tablething" />
                  
                 </Columns>
                </asp:GridView>

        -->
     

        <br />

        <div id="noForms" runat="server">

        </div>
        </form>

        </div>

        <script>/*
       function arrowDraw(n, dir) {
           $(".fa-sort-up").remove();
           $(".fa-sort-down").remove();

           if (n == 0) {
               if (dir == "asc") {
                   $(".instanceid").append('<i class="fas fa-sort-up"></i>');
               }
               else {
                   $(".instanceid").append('<i class="fas fa-sort-down"></i>');
               }
           }
           else if (n == 1) {
               if (dir == "asc") {
                   $(".date").append('<i class="fas fa-sort-up"></i>');
               }
               else {
                   $(".date").append('<i class="fas fa-sort-down"></i>');
               }
           }
           else if (n == 2) {
               if (dir == "asc") {
                   $(".firstquestion").append('<i class="fas fa-sort-up"></i>');
               }
               else {
                   $(".firstquestion").append('<i class="fas fa-sort-down"></i>');
               }
           }
         
       }

    function sortTable(n) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.querySelector(".table");
    switching = true;

    dir = "asc";

    while (switching)
    {
        switching = false;
        rows = table.rows;

        for( i = 1;1 < (rows.length - 1) ; i++)
        {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i].getElementsByTagName("TD")[n];

            if (dir == "asc")
            {
                if(x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase())
                {
                    shouldSwitch = true;
                    break;
                }
            }
            else if (dir == "desc")
            {
                if(x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase())
                {
                    shouldSwitch = true;
                    break;
                }
            }
        }

        if(shouldSwitch)
        {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        }
        else {
            if(switchcount ==0 && dir =="asc")
            {
                dir = "desc";
                switching = true;
            }
        }
    }

    arrowDraw(n, dir);
}*/
            function arrowDraw(n, dir) {
                $(".fa-sort-up").remove();
                $(".fa-sort-down").remove();

                if (n == 0) {
                    if (dir == "asc") {
                        $(".instanceid").append('<i class="fas fa-sort-up"></i>');
                    }
                    else {
                        $(".instanceid").append('<i class="fas fa-sort-down"></i>');
                    }
                }
                else if (n == 1) {
                    if (dir == "asc") {
                        $(".date").append('<i class="fas fa-sort-up"></i>');
                    }
                    else {
                        $(".date").append('<i class="fas fa-sort-down"></i>');
                    }
                }
                else if (n == 2) {
                    if (dir == "asc") {
                        $(".firstquestion").append('<i class="fas fa-sort-up"></i>');
                    }
                    else {
                        $(".firstquestion").append('<i class="fas fa-sort-down"></i>');
                    }
                }



            }




            function sortTable(n) {//thank you jay for being awesome and finding this w3 example 
                var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
                table = document.querySelector(".table");
                switching = true;
                //Set the sorting direction to ascending:
                dir = "asc";

                /*Make a loop that will continue until
                no switching has been done:*/
                while (switching) {
                    //start by saying: no switching is done:
                    switching = false;
                    rows = table.rows;
                    /*Loop through all table rows (except the
                    first, which contains table headers):*/
                    for (i = 1; i < (rows.length - 1) ; i++) {
                        //start by saying there should be no switching:
                        shouldSwitch = false;
                        /*Get the two elements you want to compare,
                        one from current row and one from the next:*/
                        x = rows[i].getElementsByTagName("TD")[n];
                        y = rows[i + 1].getElementsByTagName("TD")[n];
                        /*check if the two rows should switch place,
                        based on the direction, asc or desc:*/
                        if (dir == "asc") {
                            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                                //if so, mark as a switch and break the loop:
                                shouldSwitch = true;
                                break;
                            }
                        } else if (dir == "desc") {
                            if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                                //if so, mark as a switch and break the loop:
                                shouldSwitch = true;
                                break;
                            }
                        }
                    }
                    if (shouldSwitch) {
                        /*If a switch has been marked, make the switch
                        and mark that a switch has been done:*/
                        rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                        switching = true;
                        //Each time a switch is done, increase this count by 1:
                        switchcount++;
                    } else {
                        /*If no switching has been done AND the direction is "asc",
                        set the direction to "desc" and run the while loop again.*/
                        if (switchcount == 0 && dir == "asc") {
                            dir = "desc";
                            switching = true;
                        }
                    }

                }

                arrowDraw(n, dir);


            }


    </script>
</asp:Content>

