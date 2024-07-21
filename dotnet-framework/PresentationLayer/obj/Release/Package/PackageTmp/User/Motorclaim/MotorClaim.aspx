<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="MotorClaim.aspx.cs" Inherits="PresentationLayer.User.MotorClaim" %>

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
            <div style="position: absolute; right: 2%; width: 92%; text-align: right;">
                <asp:Button ID="btnAddClaim" Text="Add" runat="server" OnClick="btnAddClaim_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClientClick="javascript:history.back(); return false;" CssClass="btn btn-dark" />
            </div>
        </div>
    </div>
    <div style="margin-top: 3%; margin-bottom: initial" class="d-flex align-items-center justify-content-center">
        <h4 style="font-weight: bold">CLAIM DETAILS</h4>
    </div>

    <div class="d-flex align-items-center justify-content-center" style="margin-top: 2%">
        <asp:Label runat="server" ID="lblEmptyClaimTable" Font-Size="Large" ForeColor="#10898d"></asp:Label>
    </div>

    <div>
        <asp:GridView ID="gvClaim" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="8"
            CssClass="table table-striped table-bordered table-condensed"
            OnRowCommand="gvClaim_RowCommand"
            OnRowDataBound="gvClaim_RowDataBound"
            OnPageIndexChanging="gvClaim_PageIndexChanging"
            Style="width: 88%; margin: 0 auto;">
            <Columns>
                <asp:TemplateField HeaderText="Claim UID" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmUid" Visible="false" Text='<%#Eval("CLM_UID")%>'></asp:Label>
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
                        <asp:Label runat="server" ID="lblClmPolToDt" Text='<%#DataBinder.Eval(Container.DataItem,"CLM_POL_TO_DT","{0:dd-MM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Chassis No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmChassisNo" Text='<%#Eval("CLM_VEH_CHASSIS_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Report No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmPolRepNo" Text='<%#Eval("CLM_POL_REP_NO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vehicle Value" HeaderStyle-CssClass="centerHeaderText">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmVehValue" Text='<%#Eval("CLM_VEH_VALUE","{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Survey created">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmSurCrYn" Text='<%#Eval("CLM_SUR_CR_YN")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Survey Approved">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblClmSurApprYn" Text='<%#Eval("CLM_SUR_APPR_YN")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="cmdEdit" CssClass="btn btn-warning  btn-sm" CommandArgument='<%#Eval("CLM_UID") %>' />
                        <asp:Button ID="btnDelete" runat="server" OnClientClick='return confirm("Are you sure you want to approve?")' Text="Delete" CommandName="cmdDelete" CssClass="btn btn-danger  btn-sm" CommandArgument='<%#Eval("CLM_UID") %>' />
                        <asp:Button ID="btnView" runat="server" Text="View" CommandName="cmdView" CssClass="btn btn-info btn-sm" CommandArgument='<%#Eval("CLM_UID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

