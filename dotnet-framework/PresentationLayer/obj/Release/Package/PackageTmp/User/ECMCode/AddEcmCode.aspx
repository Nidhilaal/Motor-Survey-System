<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="AddEcmCode.aspx.cs" Inherits="PresentationLayer.User.ECMCode.AddEcmCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
            <div style="position: absolute; right: 2%; width: 92%; text-align: right;">
                <asp:Button ID="btnAddNew" Text="Add New" runat="server" onclick="btnAddNew_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" CssClass="btn btn-dark" Style="float: right; margin-left: 0.4%;" />
            </div>
        </div>
    </div>
    <div style="margin-right: 10%; margin-left: 10%; margin-top: 5%; margin-bottom: initial">
        <h4>Add/Update ECM Details</h4>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 3%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtCcCode" Text="Code"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="12" CssClass="form-control" ID="txtCcCode"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server"></asp:Label>
                <asp:Label runat="server" for="txtCcType" Text="Type"></asp:Label><font color="red">*</font>
                  <asp:DropDownList runat="server"
                    ID="ddlCcType"
                    CssClass="form-control"
                    ValidationGroup="vgSaveEcmCode">
                </asp:DropDownList>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server"></asp:Label>
                <asp:Label runat="server" for="txtCcDesc" Text="Description"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="240" CssClass="form-control" ID="txtCcDesc"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server"></asp:Label>
                <asp:Label runat="server" for="txtCcMapCode" Text="Map Code"></asp:Label>
                <asp:TextBox runat="server" MaxLength="12" CssClass="form-control" ID="txtCcMapCode"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvCcCode"
                    runat="server"
                    ControlToValidate="txtCcCode"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveEcmCode"
                    ErrorMessage="Field Empty">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                 <asp:RequiredFieldValidator ID="rfvCcType" 
                     runat="server"
                    ControlToValidate="ddlCcType"
                    InitialValue="NA"
                    ErrorMessage="Select a value!!!"
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="vgSaveEcmCode" />
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvCcDesc"
                    runat="server"
                    ControlToValidate="txtCcDesc"
                    ForeColor="Red"
                    ValidationGroup="vgSaveEcmCode"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>           
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%;">
            <div class="col-sm-3">
                <asp:Label runat="server" ID="lblActiveEcmCode" Text="Active"></asp:Label>
                <asp:CheckBox ID="chkActiveEcmCode" runat="server" />
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="d-flex align-items-center justify-content-center">
                <asp:Button ID="btnSaveEcmCode" runat="server" Text="Save" ValidationGroup="vgSaveEcmCode" OnClick="btnSaveEcmCode_Click" CssClass="btn btn-success" />
                <asp:Button ID="btnUpdateEcmCode" runat="server" Text="Update" ValidationGroup="vgSaveEcmCode" OnClick="btnUpdateEcmCode_Click"  CssClass="btn btn-warning" />
            </div>
        </div>
    </div>
</asp:Content>
