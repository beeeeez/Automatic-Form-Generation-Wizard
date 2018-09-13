<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
  <form runat="server">        <h1>Automatic Form Generation Wizard</h1><br />

        <div class="form-group">
      <label for="user">Username :</label>
      <asp:TextBox runat="server" CssClass="form-control" id="username" required></asp:TextBox>
      </div>
    <div class="form-group">
      <label for="pass">Password :</label>
      <asp:TextBox runat="server" CssClass="form-control" id="password" TextMode="Password" required></asp:TextBox>
      </div>
        <br />
      <asp:Literal ID="notify" runat="server"></asp:Literal>
    <asp:Literal ID="loginerror" runat="server" Visible="false"><h6>Incorrect Username or Password</h6>
        </asp:Literal>


    <div class="form-group">
        <asp:LinkButton ID="loginBtn" CssClass="btn btn-primary" runat="server" OnClick="loginBtn_Click" ><i class="fas fa-sign-in-alt"></i> Login</asp:LinkButton>
<br /> <br />
          <a href="Registration.aspx" class="btn btn-primary"><i class="fas fa-registered"></i> Register</a><br /><br />
         <a href="lostPass.aspx" class="btn btn-warning"><i class="fas fa-question"></i> Forgot Password</a>
        </div>



        </form>

    </div>

</asp:Content>

