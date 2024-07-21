<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="PresentationLayer.User.HistoryTable.History" %>

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
                <h4 style="font-weight: bold">Survey History</h4>
            </div>
        </div>
    </div>
    <div>
        <asp:GridView ID="gvHistoryTable"
            runat="server"
            AllowPaging="true"
            PageSize="10"
            AutoGenerateColumns="false"
            OnPageIndexChanging="gvHistoryTable_PageIndexChanging"
            Style="width: 94%; margin: 0 auto;"
            CssClass="table table-striped table-bordered table-condensed">
            <Columns>
                <asp:TemplateField HeaderText="Claim No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmSurNo" Text='<%#Eval("CLM_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Survey No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurNo" Text='<%#Eval("SUR_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurdhItemCode" Text='<%#Eval("SURDH_ITEM_CODE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Srl No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurdhSrl" Text='<%#Eval("SURDH_SRL")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurdhAction" Text='<%#Eval("SURDH_ACTION")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurdhType" Text='<%#Eval("SURDH_TYPE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Currency">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurCurr" Text='<%#Eval("SUR_CURR")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="centerHeaderText">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurdhQty" Text='<%#Eval("SURDH_QUANTITY","{0:N0}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-CssClass="centerHeaderText">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurdhUnitPrice" Text='<%#Eval("SURDH_UNIT_PRICE","{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount(FC)" HeaderStyle-CssClass="centerHeaderText">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurdhFcAmt" Text='<%#Eval("SURDH_FC_AMT","{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount(LC)" HeaderStyle-CssClass="centerHeaderText">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurdhLcAmt" Text='<%#Eval("SURDH_LC_AMT","{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
