<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FillOut.aspx.cs" Inherits="FillOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
           .contain {
            width: 80%;
            margin: auto;
            margin-top: 5%;
        }

        .btn {
            margin-right: 2%;
            margin-top: 2%;
        }

        #inner {
            width: 45%;
        }

        #newContent {
            width: 45%;
        }

        .form-control {
            width: 45%;
        }

    </style>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
 <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
	
	<script type="text/javascript" src="timepick/bootstrap-datepicker.js"></script>
	<link rel="stylesheet" type="text/css" href="timepick/bootstrap-datepicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contain">
    <form action="#" method="post" runat="server">
   

    <asp:PlaceHolder ID="create" runat="server"></asp:PlaceHolder>
         <br />
        <asp:Button ID="Button1" runat="server" Text="Complete Form" OnClick="Button1_Click" CssStyle="btn btn-primary" />
    </form>
     </div>

</asp:Content>

