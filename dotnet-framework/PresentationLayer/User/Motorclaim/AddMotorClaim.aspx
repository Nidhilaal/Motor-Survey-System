<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="AddMotorClaim.aspx.cs" Inherits="PresentationLayer.User.Motorclaim.AddMotorClaim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Assets/js/AddMotorClaim.js"></script>
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
        <asp:Label runat="server" ID="lblAddClaimDetails"><h4>Add Claim Details</h4></asp:Label>
        <asp:Label runat="server" ID="lbltxtClaimNo" Font-Size="X-Large" ForeColor="#10898d" Font-Bold="true">Claim No:</asp:Label>
        <asp:Label runat="server" ID="lblClaimNo" Font-Size="Large"></asp:Label>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 3%; margin-bottom: 2%;">
            <div class="col-sm-3">
                <asp:HiddenField ID="hfClmUid" runat="server" />
                <asp:HiddenField ID="hfClmSurNo" runat="server" />
                <asp:Label runat="server" for="txtClaimPolNo" Text="Policy No"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="30" CssClass="form-control" ID="txtClaimPolNo"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtClaimFromDate" Text="Policy From"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" ID="txtClaimFromDate" AutoPostBack="true" ClientIDMode="Static" OnTextChanged="txtClaimFromDate_TextChanged" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtClaimToDate" Text="Policy To"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" AutoPostBack="true" ReadOnly="true" ClientIDMode="Static" CssClass="form-control" ID="txtClaimToDate"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label2" Text="Loss Date"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control" ID="txtClaimPolicyLossDate"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvClaimPolNo"
                    runat="server"
                    ControlToValidate="txtClaimPolNo"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvClaimFromDate"
                    runat="server"
                    ControlToValidate="txtClaimFromDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvClaimToDate"
                    runat="server"
                    ControlToValidate="txtClaimToDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvClaimPolicyLossDate"
                    runat="server"
                    ControlToValidate="txtClaimPolicyLossDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvClaimPolicyLossDate1"
                    runat="server"
                    ControlToValidate="txtClaimPolicyLossDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    Font-Size="Small"
                    Type="Date"
                    CultureInvariantValues="true"
                    ControlToCompare="txtClaimFromDate"
                    Operator="GreaterThanEqual"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Date must be Between policy period!!!">
                </asp:CompareValidator>
                <asp:CompareValidator ID="cvClaimPolicyLossDate2"
                    runat="server"
                    ControlToValidate="txtClaimPolicyLossDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    Font-Size="Small"
                    Type="Date"
                    CultureInvariantValues="true"
                    ControlToCompare="txtClaimToDate"
                    Operator="LessThanEqual"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Date must be Between policy period!!!">
                </asp:CompareValidator>
                <asp:CompareValidator ID="cvClaimPolicyLossDate3"
                    runat="server"
                    ControlToValidate="txtClaimPolicyLossDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    Font-Size="Small"
                    Type="Date"
                    CultureInvariantValues="true"
                    ControlToCompare="txtClaimRegnDate"
                    Operator="LessThanEqual"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Loss Date must be before Registration date!!!">
                </asp:CompareValidator>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 2%; margin-bottom: 2%;">
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label3" Text="Claim Intimation Date"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control" ID="txtClaimIntmDate"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" ID="lblRegnDate" Text="Claim Registration Date"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control" ID="txtClaimRegnDate"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label1" Text="Police Report No"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="30" CssClass="form-control" ID="txtClaimPolRepNo" OnTextChanged="txtClaimPolRepNo_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label5" Text="Loss Description "></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="120" CssClass="form-control" ID="txtClaimLossDesc"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvClaimIntmDate"
                    runat="server"
                    ControlToValidate="txtClaimIntmDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvClaimIntmDate1"
                    runat="server"
                    ControlToValidate="txtClaimIntmDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    Font-Size="Small"
                    Type="Date"
                    CultureInvariantValues="true"
                    ControlToCompare="txtClaimRegnDate"
                    Operator="LessThanEqual"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Date must be Before Registration Date!!!">
                </asp:CompareValidator>
                <asp:CompareValidator ID="cvClaimIntmDate2"
                    runat="server"
                    ControlToValidate="txtClaimIntmDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    Font-Size="Small"
                    Type="Date"
                    CultureInvariantValues="true"
                    ControlToCompare="txtClaimFromDate"
                    Operator="GreaterThanEqual"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Date must be between Policy period!!!">
                </asp:CompareValidator>
                <asp:CompareValidator ID="cvClaimIntmDate3"
                    runat="server"
                    ControlToValidate="txtClaimIntmDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    Font-Size="Small"
                    Type="Date"
                    CultureInvariantValues="true"
                    ControlToCompare="txtClaimPolicyLossDate"
                    Operator="GreaterThanEqual"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Date must be After Loss Date!!!">
                </asp:CompareValidator>
                <asp:CompareValidator ID="cvClaimIntmDate4"
                    runat="server"
                    ControlToValidate="txtClaimIntmDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    Font-Size="Small"
                    Type="Date"
                    CultureInvariantValues="true"
                    ControlToCompare="txtClaimToDate"
                    Operator="LessThanEqual"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Date must be between Policy period!!!">
                </asp:CompareValidator>
            </div>
            <div class="col-sm-3">
                <asp:CompareValidator ID="cvClaimRegnDate1"
                    runat="server"
                    ControlToValidate="txtClaimRegnDate"
                    ForeColor="Red"
                    Display="Dynamic"
                    Font-Size="Small"
                    Type="Date"
                    CultureInvariantValues="true"
                    ControlToCompare="txtClaimIntmDate"
                    Operator="GreaterThanEqual"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Date must be after intimation Date!!!">
                </asp:CompareValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvClaimPolRepNo"
                    runat="server"
                    ControlToValidate="txtClaimPolRepNo"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvClaimLossDesc"
                    runat="server"
                    ControlToValidate="txtClaimLossDesc"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 2%; margin-bottom: 2%;">
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label6" Text="Vehicle Chassis Number"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="30" CssClass="form-control" ID="txtVehChassisNo"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label7" Text="Vehicle Engine Number"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="30" CssClass="form-control" ID="txtVehEngineNo"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label8" Text="Vehicle Registration No"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="12" CssClass="form-control" ID="txtVehRegnNo"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label9" Text="Vehicle Value"></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="10" Style="text-align: right" CssClass="form-control" ID="txtVehValue"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvVehChassisNo"
                    runat="server"
                    ControlToValidate="txtVehChassisNo"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvVehEngineNo"
                    runat="server"
                    ControlToValidate="txtVehEngineNo"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvVehRegnNo"
                    runat="server"
                    ControlToValidate="txtVehRegnNo"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvVehValue"
                    runat="server"
                    ControlToValidate="txtVehValue"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim"
                    ErrorMessage="Field Empty!!!">
                </asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvVehValue"
                    runat="server"
                    ControlToValidate="txtVehValue"
                    Type="Double"
                    MaximumValue="9999999.99"
                    MinimumValue="1000"
                    ErrorMessage="Value should be Number between 1000 & 9999999.99!!!"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="vgSaveClaim">
                </asp:RangeValidator>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 2%; margin-bottom: 1%;">
            <div class="d-flex align-items-center justify-content-center">
                <asp:Button ID="btnSaveClaim" runat="server" Text="Save" OnClick="btnSaveClaim_Click" ValidationGroup="vgSaveClaim" CssClass="btn btn-success" />
                <asp:Button ID="btnUpdateClaim" runat="server" Text="Update" OnClick="btnUpdateClaim_Click" ValidationGroup="vgSaveClaim" CssClass="btn btn-warning" />
                <asp:Button ID="btnViewSurvey" runat="server" Text="View Survey" OnClick="btnViewSurvey_Click" ValidationGroup="vgSaveClaim" CssClass="btn btn-info" />
                <asp:Button ID="btnResetClaim" runat="server" Text="Reset" OnClick="btnResetClaim_Click" CssClass="btn btn-danger" Style="margin-left: 0.25%;" />
            </div>
        </div>
    </div>
</asp:Content>
