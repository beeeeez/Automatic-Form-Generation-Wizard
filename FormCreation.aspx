<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FormCreation.aspx.cs" Inherits="FormCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
         .contain { width:80%; margin:auto; margin-top:5%;}
         .btn{margin-right:2%; margin-top:2%}
         #inner{width:45%;}
         #newContent{width:45%;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contain">

        <h3>New Form Creation -- <a href="Homepage.aspx">Return to Homepage</a></h3><br />
        <div id="inner" runat="server">
        <label>What is your new form's title? :</label><br />
                <input type="text" name="formtitle" placeholder="Enter a form title." class="form-control"  />
                <hr />
            <asp:Panel ID="newContent" runat="server"></asp:Panel>
                <br />

                <asp:Button ID="question" runat="server" Text="Add a new question" CssClass="btn btn-primary" OnClick="question_Click" />
                <asp:Button ID="text" runat="server" Text="Add text" CssClass="btn btn-primary" OnClick="text_Click"/><br />
                <asp:Button ID="save" runat="server" Text="Save Form" CssClass="btn btn-success" OnClick="Button3_Click"/>
       

            <!-- 
        <asp:TextBox ID="formtitle" runat="server" class="form-control" MaxLength="25"></asp:TextBox>
            <br />

             

            </div>
            <hr />
            -->

            </div>

    </div>



</asp:Content>

