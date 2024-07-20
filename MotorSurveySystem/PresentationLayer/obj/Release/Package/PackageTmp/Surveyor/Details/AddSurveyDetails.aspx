<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Surveyor.Master" AutoEventWireup="true" CodeBehind="AddSurveyDetails.aspx.cs" Inherits="PresentationLayer.Surveyor.Details.AddSurveyDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Assets/js/AddSurveyDetails.js"></script>
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
        <h4>SpareParts Details</h4>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 2%; margin-bottom: 2%;">
            <div class="col-sm-3">
                <asp:HiddenField ID="hfSurdUid" runat="server" />
                <asp:HiddenField ID="hfSurdSurUid" runat="server" />
                <asp:Label runat="server" for="txtSurdItemCode" Text="Item"></asp:Label><font color="red">*</font>
                <asp:DropDownList runat="server"
                    ID="ddlSurdItemCode"
                    CssClass="form-control"
                    ValidationGroup="vgSaveSurveyDetail">
                </asp:DropDownList>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtSurdType" Text="Type"></asp:Label><font color="red">*</font>
                <asp:DropDownList runat="server"
                    ID="ddlSurdType"
                    CssClass="form-control"
                    ValidationGroup="vgSaveSurveyDetail">
                </asp:DropDownList>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtSurdUnitPrice" Text="Unit Price"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="9"  ClientIDMode="Static" Style="text-align: right" OnChange="return FnGetCurrencyExchange();" OnKeyUp="formatNumber(this);" CssClass="form-control" ID="txtSurdUnitPrice"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtSurdQty" Text="Quantity"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="9"  ClientIDMode="Static" OnChange="return FnGetCurrencyExchange();" CssClass="form-control" ID="txtSurdQty"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvSurdItemCode" runat="server"
                    ControlToValidate="ddlSurdItemCode"
                    InitialValue="NA"
                    ErrorMessage="Select a value!!!"
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="vgSaveSurveyDetail" />
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvSurdType" runat="server"
                    ControlToValidate="ddlSurdType"
                    InitialValue="NA"
                    ErrorMessage="Select a value!!!"
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="vgSaveSurveyDetail" />
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvSurdUnitPrice"
                    runat="server"
                    ControlToValidate="txtSurdUnitPrice"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveSurveyDetail"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvSurdUnitPrice"
                    runat="server"
                    ControlToValidate="txtSurdUnitPrice"
                    Type="Double"
                    MaximumValue="9999999.99"
                    MinimumValue="0.01"
                    Font-Size="Small"
                    ErrorMessage="Value should be in integer/decimal!!!"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveSurveyDetail">
                </asp:RangeValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvSurdQty"
                    runat="server"
                    ControlToValidate="txtSurdQty"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveSurveyDetail"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvSurdQty"
                    runat="server"
                    ControlToValidate="txtSurdQty"
                    Type="Integer"
                    MaximumValue="999999999"
                    MinimumValue="1"                    
                    ErrorMessage="Value should be in Integer"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveSurveyDetail">
                </asp:RangeValidator>
            </div>
        </div>

        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 2%;">
            <div class="col-sm-3">
                <div style="display: none">
                    <asp:TextBox runat="server" ID="txtSurrCurr" OnChange="return FnGetCurrencyExchange();" ClientIDMode="Static"></asp:TextBox>
                </div>
                <asp:Label runat="server" ID="Label3" Text="Amount(FC)"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" Style="text-align: right" CssClass="form-control" ID="txtSurdFcAmt"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label4" Text="Amount(LC)"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" Style="text-align: right" CssClass="form-control" ID="txtSurdLcAmt"></asp:TextBox>
            </div>
        </div>

        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvSurdFcAmt"
                    runat="server"
                    ControlToValidate="txtSurdFcAmt"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveSurveyDetail"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
                 <asp:RangeValidator ID="rvrSurdFcAmt"
                    runat="server"
                    ControlToValidate="txtSurdFcAmt"
                    Type="Double"
                    MaximumValue="9999999.99"
                    MinimumValue="0.01"
                    Font-Size="Small"
                    ErrorMessage="Value should be between 1 and 9999999.99!!!"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveSurveyDetail">
                </asp:RangeValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvSurdLcAmt"
                    runat="server"
                    ControlToValidate="txtSurdLcAmt"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveSurveyDetail"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
                 <asp:RangeValidator ID="rvrSurdLcAmt"
                    runat="server"
                    ControlToValidate="txtSurdLcAmt"
                    Type="Double"
                    MaximumValue="9999999.99"
                    MinimumValue="0.01"
                    Font-Size="Small"
                    ErrorMessage="Value should be between 1 and 9999999.99!!!"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveSurveyDetail">
                </asp:RangeValidator>
            </div>
        </div>
        <div class="row" style="margin-top: 1%; margin-bottom: 1%;">
            <div class="d-flex align-items-center justify-content-center">
                <asp:Button ID="btnSaveSurveyDetails" runat="server" Text="Save" OnClick="btnSaveSurveyDetails_Click" ValidationGroup="vgSaveSurveyDetail" CssClass="btn btn-success" />
                <asp:Button ID="btnUpdateSurveyDetails" runat="server" Text="Update" OnClick="btnUpdateSurveyDetails_Click" ValidationGroup="vgSaveSurveyDetail" CssClass="btn btn-warning" />
                <asp:Button ID="btnResetSurveyDetails" runat="server" Text="Reset" onclick="btnResetSurveyDetails_Click" CssClass="btn btn-danger" Style="margin-left: 0.25%;" />

            </div>
        </div>
    </div>
</asp:Content>
