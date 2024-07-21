<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="UsersMaster.aspx.cs" Inherits="PresentationLayer.User.UsersMaster.UsersMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
            <div style="position: absolute; right: 2%; width: 92%; text-align: right;">
                <asp:Button ID="btnAddUserMaster" Text="Add" runat="server" OnClick="btnAddUserMaster_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClientClick="javascript:history.back(); return false;" CssClass="btn btn-dark" />
            </div>
        </div>
    </div>
    <div style="margin-top: 2%; margin-bottom: initial" class="d-flex align-items-center justify-content-center ">
        <h4 style="font-weight: bold">USER MASTER</h4>
    </div>
    <div class="d-flex align-items-center justify-content-center" style="margin-top: 2%">
        <asp:Label runat="server" ID="lblEmptyUsersList" Font-Size="Large" ForeColor="#10898d"></asp:Label>
    </div>
    <div>
        <asp:GridView ID="gvUserMaster" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="8"
            CssClass="table table-striped table-bordered table-condensed"
            Style="width: 88%; margin: 0 auto;"
            OnPageIndexChanging="gvUserMaster_PageIndexChanging"
            OnRowCommand="gvUserMaster_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="User Id">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblUserId" Text='<%#Eval("USER_ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblUsername" Text='<%#Eval("USER_NAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Password">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblUserPassword" Text="******"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Type">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblUserType" Text='<%#Eval("USER_TYPE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="cmdEdit" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("USER_ID") %>' />
                        <asp:Button ID="btnDelete" runat="server" OnClientClick='return confirm("Are you sure you want to approve?")' Text="Delete" CommandName="cmdDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("USER_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
