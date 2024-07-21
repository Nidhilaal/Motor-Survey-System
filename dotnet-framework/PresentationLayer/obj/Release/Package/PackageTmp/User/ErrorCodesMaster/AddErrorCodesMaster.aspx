<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="AddErrorCodesMaster.aspx.cs" Inherits="PresentationLayer.User.ErrorCodesMaster.AddErrorCodesMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
            <div style="position: absolute; right: 2%; width: 92%; text-align: right;">
                <asp:Button ID="btnAddNew" Text="Add" runat="server" OnClick="btnAddNew_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" CssClass="btn btn-dark" Style="float: right; margin-left: 0.4%;" />
            </div>
        </div>
    </div>
    <div style="margin-right: 10%; margin-left: 10%; margin-top: 5%; margin-bottom: initial">
        <h4>Add Error Codes Master</h4>

        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 3%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtErrType" Text="Type"></asp:Label><font color="red">*</font>

                <asp:DropDownList runat="server"
                    ID="ddlErrType"
                    CssClass="form-control"
                    ValidationGroup="vgSaveErrCodesMaster">
                </asp:DropDownList>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtErrCode" Text="Code"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="12" CssClass="form-control" ID="txtErrCode"></asp:TextBox>
            </div>

            <div class="col-sm-3">
                <asp:Label runat="server" for="txtErrDesc" Text="Description"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="240" CssClass="form-control" ID="txtErrDesc"></asp:TextBox>
            </div>
        </div>

        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 0.5%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvSurdItemCode" runat="server"
                    ControlToValidate="ddlErrType"
                    InitialValue="NA"
                    ErrorMessage="Select a value!!!"
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="vgSaveErrCodesMaster" />
            </div>
            <div class="col-sm-3">

                <asp:RequiredFieldValidator ID="rfvErrCode"
                    runat="server"
                    ControlToValidate="txtErrCode"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveErrCodesMaster"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>

            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvErrDesc"
                    runat="server"
                    ControlToValidate="txtErrDesc"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveErrCodesMaster"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 2%; margin-bottom: 1%;">
            <div class="d-flex align-items-center justify-content-center">
                <asp:Button ID="btnSaveErrorCodesMaster" runat="server" ValidationGroup="vgSaveErrCodesMaster" OnClick="btnSaveErrorCodesMaster_Click" Text="Save" CssClass="btn btn-success" />
                <asp:Button ID="btnUpdateErrorCodesMaster" runat="server" Text="Update" ValidationGroup="vgSaveErrCodesMaster" OnClick="btnUpdateErrorCodesMaster_Click" CssClass="btn btn-warning" />
                <asp:Button ID="btnResetErrorCodesMaster" runat="server" Text="Reset" OnClick="btnResetErrorCodesMaster_Click" CssClass="btn btn-danger" Style="margin-left: 0.25%;" />

            </div>
        </div>
    </div>
</asp:Content>
