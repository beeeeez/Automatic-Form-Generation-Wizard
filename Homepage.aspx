<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="Homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .contain { width:80%; margin:auto; margin-top:5%;}
        #displayName {display:inline;}
        table{ border: none;}
        .butt {margin-left: 2%; margin-right: 2%;}
        .notify {color:green;}
        .btn {margin-bottom:0.5%; margin-right:0.5%;}
        .rightsideBtns{float:right; width:50%; text-align:right;}
        .icon{font-size:28px; color:white;}
        .cell{text-align:center;}
        .header{color:#375a7f; font-size:16px;}
        .header span{margin-top:5px; display:inline-block;}
        .icon:hover{font-size:28px; color:#375a7f;}
        
    </style>

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU" crossorigin="anonymous">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contain">
        <form runat="server">

        <h2>Welcome back, <span runat="server" id="displayName"></span>!</h2><div class="rightsideBtns"><asp:LinkButton ID="logout" runat="server" CssClass="btn btn-danger" OnClick="logout_Click" ><i class="fas fa-sign-out-alt"></i>Logout</asp:LinkButton><a href="editAccount.aspx" class="btn btn-warning"><i class="fas fa-cogs"></i> Edit Account</a></div><hr />
        <a href="FormCreate.aspx" class="btn btn-success btn-lg"><i class="fas fa-plus"></i>   Create a New Form</a><br />
            <asp:placeholder runat="server" id="Notifcation"></asp:placeholder>
            <br />
            <asp:PlaceHolder ID="homepageTablePH" runat="server"></asp:PlaceHolder>
            <!-- datagrid views are weak
        <asp:GridView ID="putStuff" runat="server" Visible="false" AutoGenerateColumns="false" CssClass="table table-hover" BorderStyle="None" BorderWidth="0px" BorderColor="#222222"  Onsorting="putStuff_Sorting" AllowSorting="true">
            <Columns>
                <asp:BoundField DataField="formid" HeaderText="Form ID #" ItemStyle-CssClass="tablething" SortExpression="formid" />
                <asp:BoundField DataField="form_title" HeaderText="Form Title" ItemStyle-CssClass="tablething"   SortExpression="form_title"/>
                <asp:BoundField DataField="creation_date" HeaderText="Creation Date" ItemStyle-CssClass="tablething"  SortExpression="creation_date"/>
                <asp:HyperLinkField Text="Tracking" DataNavigateUrlFormatString="tracking.aspx?formid={0}" DataNavigateUrlFields="formid" ItemStyle-CssClass="btn btn-primary butt tablething" />
                <asp:HyperLinkField Text="Edit Form" DataNavigateUrlFormatString="FormCreate.aspx?formid={0}" DataNavigateUrlFields="formid" ItemStyle-CssClass="btn btn-primary butt tablething" />
                <asp:HyperLinkField Text="Generate URL" DataNavigateUrlFormatString="generateURL.aspx?formid={0}" DataNavigateUrlFields="formid" ItemStyle-CssClass="btn btn-primary butt tablething popup" />
                <asp:HyperLinkField Text="Print a blank copy" DataNavigateUrlFormatString="printableBlank.aspx?formid={0}" DataNavigateUrlFields="formid" ItemStyle-CssClass="btn btn-primary butt tablething" />
                <asp:HyperLinkField Text="Fill-Out Form" DataNavigateUrlFormatString="fillout.aspx?formid={0}" DataNavigateUrlFields="formid" ItemStyle-CssClass="btn btn-primary butt tablething" />
                </Columns>
        </asp:GridView> -->
        <br />
        <div id="noForms" runat="server">

        </div>
        </form>
    </div>
    <script>function sortTable(n) {
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

    

    
}
        
    </script>

</asp:Content>

