<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="AddUsersMaster.aspx.cs" Inherits="PresentationLayer.User.UsersMaster.AddUsersMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
            <div style="position: absolute; right: 2%; width: 92%; text-align: right;">
                <asp:Button ID="btnAddNew" Text="Add New" runat="server" OnClick="btnAddNew_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" CssClass="btn btn-dark" Style="float: right; margin-left: 0.4%;" />
            </div>
        </div>
    </div>
    <div style="margin-right: 10%; margin-left: 10%; margin-top: 4%; margin-bottom: initial">
        <h4>Add/Update User</h4>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtUserId" Text="User ID "></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="12" CssClass="form-control" ID="txtUserId"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtUsername" Text="Username "></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="30" CssClass="form-control" ID="txtUsername"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtPassword" Text="Password "></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="24" CssClass="form-control" ID="txtPassword"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtUserType" Text="User Type "></asp:Label><font color="red">*</font>
                <asp:DropDownList runat="server"
                    ID="ddlUserType"
                    CssClass="form-control"
                    ValidationGroup="vgSaveUser">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvUserId"
                    runat="server"
                    ControlToValidate="txtUserId"
                    ForeColor="Red"
                    ValidationGroup="vgSaveUser"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvUsername"
                    runat="server"
                    ControlToValidate="txtUsername"
                    ForeColor="Red"
                    ValidationGroup="vgSaveUser"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvPassword"
                    runat="server"
                    ControlToValidate="txtPassword"
                    ForeColor="Red"
                    ValidationGroup="vgSaveUser"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfUserType" runat="server"
                    ControlToValidate="ddlUserType"
                    InitialValue="NA"
                    ErrorMessage="Select a value!!!"
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="vgSaveUser" />
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="d-flex align-items-center justify-content-center">
                <asp:Button ID="btnSaveUser" OnClick="btnSaveUser_Click" ValidationGroup="vgSaveUser" runat="server" Text="Save" CssClass="btn btn-success" />
                <asp:Button ID="btnUpdateUser" OnClick="btnUpdateUser_Click" ValidationGroup="vgSaveUser" runat="server" Text="Update" CssClass="btn btn-warning" />
                <asp:Button ID="btnResetUserMaster" runat="server" Text="Reset" onclick="btnResetUserMaster_Click" CssClass="btn btn-danger" Style="margin-left: 0.25%;" />
            </div>
        </div>
    </div>
</asp:Content>
