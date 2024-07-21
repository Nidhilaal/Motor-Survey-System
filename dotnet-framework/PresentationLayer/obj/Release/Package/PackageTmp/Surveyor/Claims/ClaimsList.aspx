<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Surveyor.Master" AutoEventWireup="true" CodeBehind="ClaimsList.aspx.cs" Inherits="PresentationLayer.Surveyor.Claims.ClaimsList" %>

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
                <h4 style="font-weight: bold">PENDING CLAIMS</h4>
            </div>
            <div class="d-flex align-items-center justify-content-center" style="margin-top: 2%">
                <asp:Label runat="server" ID="lblEmptyClaimsList" Font-Size="Large" ForeColor="#10898d"></asp:Label>
            </div>
        </div>
    </div>
    <div>
        <asp:GridView ID="gvClaim" 
            runat="server"
            AutoGenerateColumns="false"
            CssClass="table table-striped table-bordered table-condensed"
            OnRowCommand="gvClaim_RowCommand"
            AllowPaging="true"
            PageSize="8"
            OnPageIndexChanging="gvClaim_PageIndexChanging"
            Style="width: 88%; margin: 0 auto;">
            <Columns>
                <asp:TemplateField HeaderText="Claim UID" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" Visible="false" ID="lblClmUid" Text='<%#Eval("CLM_UID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Claim No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmNo" Text='<%#Eval("CLM_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Policy From">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmTypeFmDt" Text='<%# DataBinder.Eval(Container.DataItem,"CLM_POL_FM_DT","{0:dd-MM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Policy To">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmPolToDt" Text='<%# DataBinder.Eval(Container.DataItem,"CLM_POL_TO_DT","{0:dd-MM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Chassis No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmChassisNo" Text='<%#Eval("CLM_VEH_CHASSIS_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vehicle Value" HeaderStyle-CssClass="centerHeaderText">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmVehValue" Text='<%#Eval("CLM_VEH_VALUE","{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Survey Created">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmSurApprYn" Text='<%#Eval("CLM_SUR_CR_YN")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" Text="Start Survey" CommandName="cmdView" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("CLM_UID")+","+Eval("CLM_NO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
