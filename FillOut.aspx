<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="FillOut.aspx.cs" Inherits="FillOut" %>

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
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
 <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contain"> 
    <form action="#" method="post" runat="server"><div id="deletething"></div>
        <asp:Literal ID="header" runat="server"></asp:Literal><br />
       <asp:PlaceHolder ID="deleteBtnLit" runat="server"></asp:PlaceHolder>
   

    <asp:PlaceHolder ID="create" runat="server"></asp:PlaceHolder>
         <br /><br />
        <asp:Button ID="Button1" runat="server" Text="Complete Form" OnClick="Button1_Click" CssClass="btn btn-success" />
    </form>
     </div>
        <script>

            function jsDelete() {
                let del = '<input type="hidden" value="true" name="deleteInstance" id="deleteInstance" />';
                $("#deletething").append(del);
            };

    </script>
</asp:Content>

