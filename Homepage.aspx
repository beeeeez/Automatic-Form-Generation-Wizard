﻿<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="Homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .contain { width:80%; margin:auto; margin-top:5%;}
        #displayName {display:inline;}
        table{ border: none;}
        .butt {margin-left: 2%; margin-right: 2%;}
        .notify {color:green;}
        .btn {margin-bottom:0.5%; margin-right:0.5%;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contain">
        <form runat="server">

        <h2>Welcome back, <span runat="server" id="displayName"></span>!</h2><asp:Button ID="logout" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="logout_Click" /><a href="editAccount.aspx" class="btn btn-warning">Edit User Account Details</a><br /><hr />
        <a href="FormCreate.aspx" class="btn btn-success btn-lg">Create a New Form</a><br />
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

</asp:Content>

