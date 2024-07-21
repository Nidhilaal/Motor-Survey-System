<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="EcmCodeListing.aspx.cs" Inherits="PresentationLayer.User.ECMCode.EcmCodeListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
            <div class="col-2" style="margin-left: 92%;">
                <asp:Button ID="btnAddEcm" Text="Add ECM" runat="server" OnClick="btnAddEcm_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClientClick="javascript:history.back(); return false;" CssClass="btn btn-dark" />
            </div>
            <div class="d-flex align-items-center justify-content-center ">
                <h4 style="font-weight: bold">ECM CODES</h4>
            </div>
            <div class="d-flex align-items-center justify-content-center" style="margin-top: 2%">
                <asp:Label runat="server" ID="lblEmptyEcmCodeList" Font-Size="Large" ForeColor="#10898d"></asp:Label>
            </div>
        </div>
    </div>
    <div>
        <asp:GridView ID="gvEcmCode" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="8"
            CssClass="table table-striped table-bordered table-condensed"
            OnRowCommand="gvEcmCode_RowCommand"
            OnPageIndexChanging="gvEcmCode_PageIndexChanging"
            EmptyDataText=""
            Style="width: 88%; margin: 0 auto;">
            <Columns>
                <asp:TemplateField HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCcCode" Text='<%#Eval("CC_CODE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCcType" Text='<%#Eval("CC_TYPE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCcDesc" Text='<%#Eval("CC_DESC")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MapCode">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCcMapCode" Text='<%#Eval("CC_MAP_CODE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Active">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCcActiveYn" Text='<%#Eval("CC_ACTIVE_YN")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="cmdEdit" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("CC_CODE")+","+Eval("CC_TYPE") %>' />
                        <asp:Button ID="btnDelete" runat="server" OnClientClick='return confirm("Are you sure you want to approve?")' Text="Delete" CommandName="cmdDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("CC_CODE")+","+Eval("CC_TYPE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
