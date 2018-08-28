<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    <div class="contain"><br />
        <form runat="server">
        <h1>User Registration</h1>

        <div class="form-group">
      <label>Username :</label>
      <asp:TextBox runat="server" type="text" class="form-control" id="username" aria-describedby="username" placeholder="Enter Username" required></asp:TextBox>
      </div>
    <div class="form-group">
      <label>Password :</label>
      <asp:TextBox runat="server" type="text" class="form-control" id="password" aria-describedby="password" placeholder="Enter Password" TextMode="Password" required></asp:TextBox>
      </div>
        <div class="form-group">
      <label>Confirm Password :</label>
      <asp:TextBox runat="server" type="text" class="form-control" id="confirm" placeholder="Confirm Password" TextMode="Password" required></asp:TextBox>
      </div>
        <div class="form-group">
      <label>Email Address :</label>
      <asp:TextBox runat="server" type="text" class="form-control" id="email" placeholder="Enter Email Address" required></asp:TextBox>
      </div><br />
        
        <div id="errorDiv" runat="server"></div>
          <div class="form-group">
        <asp:Button ID="registerBtn" class="btn btn-primary" runat="server" Text="Register" OnClick="registerBtn_Click" /><br /> <br />
          <a href="Default.aspx" class="btn btn-primary">Return To Login Page</a>
        </div><br />
            </form>
        </div>





</asp:Content>

