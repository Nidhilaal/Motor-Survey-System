<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Surveyor.Master" AutoEventWireup="true" CodeBehind="SurveyHeader.aspx.cs" Inherits="PresentationLayer.Surveyor.Header.SurveyHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
            <div class="col-1" style="margin-left: 99.9%; margin-right: 2%">
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClientClick="javascript:history.back(); return false;" CssClass="btn btn-dark" />
            </div>
            <div class="d-flex align-items-center justify-content-center">
                <h4 style="font-weight: bold">SURVEYS</h4>
            </div>
            <div class="d-flex align-items-center justify-content-center" style="margin-top: 2%">
                <asp:Label runat="server" ID="lblEmptySurveyHeader" Font-Size="Large" ForeColor="#10898d"></asp:Label>
            </div>
        </div>
    </div>
    <div>
        <asp:GridView ID="gvSurveyHeader" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="8"
            CssClass="table table-striped table-bordered table-condensed"
            OnRowCommand="gvSurveyHeader_RowCommand"
            OnRowDataBound="gvSurveyHeader_RowDataBound"
            OnPageIndexChanging="gvSurveyHeader_PageIndexChanging"
            Style="width: 88%; margin: 0 auto;">
            <Columns>
                <asp:TemplateField HeaderText="Survey Uid" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurUid" Visible="false" Text='<%#Eval("SUR_UID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Claim No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurClmNo" Text='<%#Eval("SUR_CLM_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Survey No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurNo" Text='<%#Eval("SUR_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Chassis No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurChassisNo" Text='<%#Eval("SUR_CHASSIS_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Registration No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurRegnNo" Text='<%#Eval("SUR_REGN_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Engine No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurEngineNo" Text='<%#Eval("SUR_ENGINE_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Survey Status">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurSts" Text='<%#Eval("SUR_STATUS")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Approval Status">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurApprSts" Text='<%#Eval("SUR_APPR_STS")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEditSurvey" runat="server" Text="Edit" CommandName="cmdEdit" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("SUR_UID") %>' />
                        <asp:Button ID="btnViewSurvey" runat="server" Text="View" CommandName="cmdView" CssClass="btn btn-info btn-sm" CommandArgument='<%#Eval("SUR_UID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
