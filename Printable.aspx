<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Printable.aspx.cs" Inherits="Printable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <style>
        .contain {
            width: 80%;
            margin: auto;
            margin-top: 5%;
            background-color:rgba(0,0,0,0);
            color:black;
            font-family:'Times New Roman', Times, serif;

        }
    body{
        background-color:white;
    }
       img{
           width:15px;
           height:13px;
       }
               
    </style>
    <script>
        $(document).load(printedpage());
        function printedpage() {
            window.print();
            
            return false;
        }
        </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contain">
        <form  runat="server">

            <asp:PlaceHolder ID="create" runat="server"></asp:PlaceHolder>
        <!--    <asp:button runat="server" text="Print this page" OnClientClick="print()"></asp:button> -->
            
        </form>
   </div>
</asp:Content>

