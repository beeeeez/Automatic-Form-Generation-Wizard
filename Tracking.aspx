<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Tracking.aspx.cs" Inherits="Tracking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .contain { width:80%; margin:auto; margin-top:5%;}
        #displayName {display:inline;}
        .tablething {padding:2%; border-style:none; margin-bottom:2%;}
        .butt {margin-left: 2%; margin-right: 2%;}
        #notify {color:green;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contain">
    <form runat="server">
           <asp:literal runat="server" id="header"></asp:literal><br />
        <asp:Literal ID="notify" runat="server"></asp:Literal>

        <asp:PlaceHolder ID="tpTablePH" runat="server"></asp:PlaceHolder>
    <asp:GridView ID="putStuff" runat="server" Visible="false" AutoGenerateColumns="false" CssClass="table table-hover" BorderStyle="None" BorderWidth="0px" BorderColor="#222222">
            <Columns>
                 <asp:BoundField DataField="instanceid" HeaderText="Instance ID #" ItemStyle-CssClass="tablething"/>
                <asp:BoundField DataField="fillout_date" HeaderText="Creation Date" ItemStyle-CssClass="tablething" />
                  <asp:HyperLinkField Text="Edit Instance" DataNavigateUrlFormatString="fillOut.aspx?instanceid={0}" DataNavigateUrlFields="instanceid" ItemStyle-CssClass="btn btn-primary butt tablething" />
                 <asp:HyperLinkField Text="Print Instance" DataNavigateUrlFormatString="Printable.aspx?instanceid={0}" Target="_blank" DataNavigateUrlFields="instanceid" ItemStyle-CssClass="btn btn-primary butt tablething" />
                  
                 </Columns>
                </asp:GridView>

        </form>

    <asp:Literal ID="noForms" runat="server"></asp:Literal>
        </div>
</asp:Content>

