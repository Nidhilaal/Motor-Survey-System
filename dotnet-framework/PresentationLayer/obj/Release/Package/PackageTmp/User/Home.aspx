<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PresentationLayer.user.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Assets/css/Home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container center-content">
        <h1 style="font-family: serif; font-weight: bold; color:#10898d">Hello, User</h1>
    </div>
</asp:Content>
