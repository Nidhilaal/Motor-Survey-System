<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="SurveyDetails.aspx.cs" Inherits="PresentationLayer.User.SurveyDetails.SurveyDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .centerHeaderText {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
            <div class="col-1" style="margin-left: 99.9%; margin-right: 2%">
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClientClick="javascript:history.back(); return false;" CssClass="btn btn-dark" />
            </div>
            <div class="d-flex align-items-center justify-content-center">
                <h4 style="font-weight: bold">SUMMARY</h4>
            </div>
        </div>
    </div>
    <div style="margin-right: 15%; margin-left: 20%; margin-top: 2%; margin-bottom: 3%">
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 2%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:HiddenField ID="hfSurUid" runat="server" />
                <asp:Label runat="server" Font-Size="Large" Text="Policy No: "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Bold="true" ID="lblClmPolNo"></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Size="Large" Text="Claim No: "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Bold="true" ID="lblSurClmNo"></asp:Label>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Size="Large" Text="Survey No: "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Bold="true" ID="lblSurNo"></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Size="Large" Text="Chassis No: "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Bold="true" ID="lblSurChassisNo"></asp:Label>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Size="Large" Text="Registration No: "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Bold="true" ID="lblSurRegnNo"></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Size="Large" Text="Engine No: "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Bold="true" ID="lblEngineNo"></asp:Label>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Size="Large" Text="Currency: "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Bold="true" ID="lblSurcurr"></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Size="Large" Text="Amount(FC): "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Bold="true" ID="lblSurFcAmt"></asp:Label>
            </div>
        </div>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Size="Large" Text="Amount(LC): "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Bold="true" ID="lblSurLcAmt"></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Size="Large" Text="Status: "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" Font-Bold="true" ID="lblSurApprSts"></asp:Label>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row" style="margin-top: 4%; margin-bottom: 2%">
            <div class="d-flex align-items-center justify-content-center">
                <h4>Spare Parts & Accessories</h4>
            </div>
        </div>
    </div>
    <asp:GridView ID="gvSurveyDetails" runat="server"
        AutoGenerateColumns="false"
        CssClass="table table-striped table-bordered table-condensed"
        Style="width: 88%; margin: 0 auto;">
        <Columns>
            <asp:TemplateField HeaderText="Item Name">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSurdItemCode" Text='<%#Eval("SURD_ITEM_CODE")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSurdType" Text='<%#Eval("SURD_TYPE")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="centerHeaderText">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSurdQty" Text='<%#Eval("SURD_QUANTITY","{0:N0}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Price" HeaderStyle-CssClass="centerHeaderText">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSurdUnitPrice" Text='<%#Eval("SURD_UNIT_PRICE","{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount(FC)" HeaderStyle-CssClass="centerHeaderText">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lbSurdFcAmt" Text='<%#Eval("SURD_FC_AMT","{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount(LC)" HeaderStyle-CssClass="centerHeaderText">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSurdLcAmt" Text='<%#Eval("SURD_LC_AMT","{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="margin-top: 1.5%">
        <div class="d-flex align-items-center justify-content-center" style="margin-top: 1.5%;">
            <asp:Button ID="btnApproveSurvey" runat="server" Text="Approve" OnClick="btnApproveSurvey_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnRejectSurvey" runat="server" Text="Reject" OnClick="btnRejectSurvey_Click" CssClass="btn btn-danger" Style="margin-left: 0.25%;" />
            <asp:Button ID="btnGenerateReport" runat="server" Text="Print" OnClick="btnGenerateReport_Click" OnClientClick="target ='_blank';" CssClass="btn btn-warning" />
        </div>
    </div>
</asp:Content>
