<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FillOut.aspx.cs" Inherits="FillOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contain">
        <form runat="server">
   

    <asp:PlaceHolder ID="create" runat="server"></asp:PlaceHolder>
            <asp:Button ID="submitBtn" runat="server" Text="Submit Form" OnClick="fillout_submit()" />
    </form>
     </div>

</asp:Content>

