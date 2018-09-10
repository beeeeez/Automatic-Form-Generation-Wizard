<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="editAccount.aspx.cs" Inherits="editAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .contain{
            width:35%;
            margin:auto;
            margin-top:5%;
                    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="contain">
        <form runat="server">
        <asp:HyperLink ID="returnHome" runat="server" CssClass="btn btn-primary" Text="" NavigateUrl="Homepage.aspx"><span class="fas fa-home"></span>Return to Homepage</asp:HyperLink><br /><br />
        <h3>Change Password:</h3>
        <label>Current Password :</label><br />
        <asp:TextBox ID="currentPassBox" runat="server" CssClass="form-control"></asp:TextBox>
         <label>New Password :</label><br />
        <asp:TextBox ID="newPassBox" runat="server" CssClass="form-control"></asp:TextBox>
        <label>Confirm New Password :</label><br />
        <asp:TextBox ID="confirmPassBox" runat="server" CssClass="form-control"></asp:TextBox><br />
        <asp:Button ID="passChangeBtn" runat="server" Text="Save Password Changes" class="btn btn-success"/>
        <br /><hr />
                <h3>Change Email:</h3>
        <label>Current Email :</label><br />
        <asp:TextBox ID="currentEmailBox" runat="server" CssClass="form-control"></asp:TextBox>
         <label>New Email :</label><br />
        <asp:TextBox ID="newEmailBox" runat="server" CssClass="form-control"></asp:TextBox>
        <label>Confirm New Email :</label><br />
        <asp:TextBox ID="confirmEmailBox" runat="server" CssClass="form-control"></asp:TextBox><br />
        <asp:Button ID="emailChangeBtn" runat="server" Text="Save Email Changes" class="btn btn-success"/>
            </form>
    </div>
</asp:Content>

