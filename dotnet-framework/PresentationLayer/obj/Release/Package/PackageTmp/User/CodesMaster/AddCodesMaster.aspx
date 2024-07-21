<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="AddCodesMaster.aspx.cs" Inherits="PresentationLayer.User.CodesMaster.AddCodesMaster" %>

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
        <h4>Add/Update Codes Master</h4>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 3%; margin-bottom: 1%;">
              <div class="col-sm-3">

                <asp:Label runat="server" for="txtCmType" Text="Type"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="12" CssClass="form-control" ID="txtCmType"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtCmCode" Text="Code"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="12" CssClass="form-control" ID="txtCmCode"></asp:TextBox>
            </div>
          
            <div class="col-sm-3">

                <asp:Label runat="server" for="txtCmDesc" Text="Description"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="240" CssClass="form-control" ID="txtCmDesc"></asp:TextBox>
            </div>
            <div class="col-sm-3">

                <asp:Label runat="server" for="txtCmValue" Text="Value"></asp:Label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCmValue"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvCmType"
                    runat="server"
                    ControlToValidate="txtCmType"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveCodesMaster"
                    ErrorMessage="Field Empty">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvCmCode"
                    runat="server"
                    ControlToValidate="txtCmCode"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveCodesMaster"
                    ErrorMessage="Field Empty">
                </asp:RequiredFieldValidator>
            </div>
            
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvCmDesc"
                    runat="server"
                    ControlToValidate="txtCmDesc"
                    ForeColor="Red"
                    ValidationGroup="vgSaveCodesMaster"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RangeValidator ID="rvCmValue"
                    runat="server"
                    ControlToValidate="txtCmValue"
                    Type="Double"
                    MaximumValue="9999999.99"
                    ErrorMessage="Value should be in integer/decimal"
                    ForeColor="Red"
                    ValidationGroup="vgSaveCodesMaster"
                    Display="Dynamic">
                </asp:RangeValidator>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%;">
            <div class="col-sm-3">
                <asp:Label runat="server" ID="lblActiveCodeMaster" Text="Active"></asp:Label>
                <asp:CheckBox ID="chkActiveCodeMaster" runat="server" />
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="d-flex align-items-center justify-content-center">
                <asp:Button ID="btnSaveCodesMaster" runat="server" Text="Save" ValidationGroup="vgSaveCodesMaster" OnClick="btnSaveCodesMaster_Click" CssClass="btn btn-success" />              
                <asp:Button ID="btnUpdateCodesMaster" runat="server" Text="Update" ValidationGroup="vgSaveCodesMaster" OnClick="btnUpdateCodesMaster_Click" CssClass="btn btn-warning" />
                 <asp:Button ID="btnResetCodesMaster" runat="server" Text="Reset"  onclick="btnResetCodesMaster_Click" CssClass="btn btn-danger" Style="margin-left: 0.5%;"/>
            </div>
        </div>
    </div>
</asp:Content>
