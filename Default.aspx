﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
        <h1>Automatic Form Generation Wizard</h1><br />

        <div class="form-group">
      <label for="user">Username :</label>
      <asp:TextBox runat="server" type="text" class="form-control" id="username" aria-describedby="username" placeholder="Enter Username"></asp:TextBox>
      </div>
    <div class="form-group">
      <label for="pass">Password :</label>
      <asp:TextBox runat="server" type="text" class="form-control" id="password" aria-describedby="password" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
      </div>
        <br />
    <asp:Literal ID="loginerror" runat="server" Visible="false"><h6>Incorrect Username or Password</h6>
        </asp:Literal>


    <div class="form-group">
        <asp:Button ID="loginBtn" class="btn btn-primary" runat="server" Text="Login" OnClick="loginBtn_Click" /><br /> <br />
          <a href="Registration.aspx" class="btn btn-primary">Register</a>
        </div>




    </div>

</asp:Content>

