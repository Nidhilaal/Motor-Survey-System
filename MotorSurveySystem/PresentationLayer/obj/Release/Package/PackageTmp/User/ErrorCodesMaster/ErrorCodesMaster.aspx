<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="ErrorCodesMaster.aspx.cs" Inherits="PresentationLayer.User.ErrorCodesMaster.ErrorCodesMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
             <div style="position: absolute; right: 2%; width: 92%; text-align: right;">
                <asp:Button ID="btnAddEcm" Text="Add" runat="server" OnClick="btnAddEcm_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClientClick="javascript:history.back(); return false;" CssClass="btn btn-dark" />
            </div>
            </div>
        </div>
            <div style="margin-top: 3%; margin-bottom: initial" class="d-flex align-items-center justify-content-center">
                <h4 style="font-weight: bold">ERROR CODES MASTER</h4>
            </div>
            <div class="d-flex align-items-center justify-content-center" style="margin-top: 2%">
                <asp:Label runat="server" ID="lblEmptyErrorCodesList" Font-Size="Large" ForeColor="#10898d"></asp:Label>
            </div>

    
    <div>
        <asp:GridView ID="gvErrorCodeMaster" 
            runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="8"
            CssClass="table table-striped table-bordered table-condensed"
            OnRowCommand="gvErrorCodeMaster_RowCommand"
            OnPageIndexChanging="gvErrorCodeMaster_PageIndexChanging"
            Style="width: 88%; margin: 0 auto;">
            <Columns>
                   <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEcmType" Text='<%#Eval("ERR_TYPE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEcmCode" Text='<%#Eval("ERR_CODE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>          
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEcmDesc" Text='<%#Eval("ERR_DESC")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="cmdEdit" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("ERR_CODE") %>' />
                        <asp:Button ID="btnDelete" runat="server" OnClientClick='return confirm("Are you sure you want to approve?")' Text="Delete" CommandName="cmdDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("ERR_CODE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
