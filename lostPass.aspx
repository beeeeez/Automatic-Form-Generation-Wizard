<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="lostPass.aspx.cs" Inherits="lostPass" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form runat="server">
    <div class="contain">
        <h4>What is your email address?</h4>
        <asp:TextBox runat="server" ID="lostEmail" CssClass="form-group" required></asp:TextBox>
        <br />
        <br />
        <asp:Literal ID="errormsg" runat="server"></asp:Literal>
        <br />
        <asp:Button ID="emailPass" runat="server" Text="Send Email" OnClick="emailPass_Click" CssClass="btn btn-success"/><br /><br />
        <a href="Default.aspx" class="btn btn-primary">Return to Login Page</a>
        </form>
    </div>
</asp:Content>

