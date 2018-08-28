<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="passReset.aspx.cs" Inherits="passReset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style>
        .contain{
            width:80%;
            margin:auto;
            margin-top:5%;
            text-align:center;
        }
        .form-group{
            width:25%;
            margin:auto;
            margin-top:3%;
        }

    </style>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contain">

        <h2>Reset your Password!</h2><br />
        <asp:Label ID="Label1" runat="server" Text="Label">New Password</asp:Label><br />
        <asp:TextBox ID="pass" runat="server" CssClass="form-group"></asp:TextBox><br /><br />
        <asp:Label ID="Label2" runat="server" Text="Label">Confirm Password</asp:Label><br />
        <asp:TextBox ID="confirm" runat="server"  CssClass="form-group"></asp:TextBox><br /><br />
        <asp:Literal ID="errmsg" runat="server"></asp:Literal>
        <br />
        <asp:Button ID="changepass" runat="server" Text="Change Password" CssClass="btn btn-success" OnClick="changepass_Click" /><br /><br />
        <a href="Default.aspx">Return to Login</a>

    </div>
</asp:Content>

