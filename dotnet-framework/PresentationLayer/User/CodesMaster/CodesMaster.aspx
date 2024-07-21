<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="CodesMaster.aspx.cs" Inherits="PresentationLayer.User.CodesMaster.CodesMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
            <div style="position: absolute; right: 2%; width: 92%; text-align: right;">
                <asp:Button ID="btnAddCm" Text="Add" runat="server" OnClick="btnAddCm_click" CssClass="btn btn-primary" />
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClientClick="javascript:history.back(); return false;" CssClass="btn btn-dark" />
            </div>
        </div>
    </div>
    <div style="margin-top: 3%; margin-bottom: initial" class="d-flex align-items-center justify-content-center ">
        <h4 style="font-weight: bold">CODES MASTER</h4>
    </div>
    <div class="d-flex align-items-center justify-content-center" style="margin-top: 2%">
        <asp:Label runat="server" ID="lblEmptyCodesMasterList" Font-Size="Large" ForeColor="#10898d"></asp:Label>
    </div>
    <div>
        <asp:GridView ID="gvCodeMaster" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="8"
            CssClass="table table-striped table-bordered table-condensed"
            OnRowCommand="gvCode_RowCommand"
            OnPageIndexChanging="gvCodeMaster_PageIndexChanging"
            Style="width: 88%; margin: 0 auto;">
            <Columns>
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCmType" Text='<%#Eval("CM_TYPE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCmCode" Text='<%#Eval("CM_CODE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCmDesc" Text='<%#Eval("CM_DESC")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Value">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCmValue" Text='<%#Eval("CM_VALUE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Active">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblActiveYn" Text='<%#Eval("CM_ACTIVE_YN")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="cmdEdit" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("CM_CODE")+","+Eval("CM_TYPE") %>' />
                        <asp:Button ID="btnDelete" runat="server" OnClientClick='return confirm("Are you sure you want to approve?")' Text="Delete" CommandName="cmdDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("CM_CODE")+","+Eval("CM_TYPE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
