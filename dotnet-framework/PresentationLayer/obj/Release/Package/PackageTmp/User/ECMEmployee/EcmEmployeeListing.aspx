<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="EcmEmployeeListing.aspx.cs" Inherits="PresentationLayer.User.ECMEmployee.EcmEmployeeListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
             <div class="col-2" style="margin-left: 92%;">
                <asp:Button ID="btnAddEmployee" Text="Add" runat="server" OnClick="btnAddEmployee_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClientClick="javascript:history.back(); return false;" CssClass="btn btn-dark" />
            </div>
            <div class="d-flex align-items-center justify-content-center ">
                <h4 style="font-weight: bold">EMPLOYEE LIST</h4>
            </div>
            <div class="d-flex align-items-center justify-content-center" style="margin-top: 2%">
                <asp:Label runat="server" ID="lblEmptyEcmEmployeeList" Font-Size="Large" ForeColor="#10898d"></asp:Label>
            </div>
        </div>
    </div>
    <div>
        <asp:GridView ID="gvEcmEmployee" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="8"
            CssClass="table table-striped table-bordered table-condensed"
            OnRowCommand="gvEcmEmployee_RowCommand"
            OnPageIndexChanging="gvEcmEmployee_PageIndexChanging"
            Style="width: 88%; margin: 0 auto;">
            <Columns>
                <asp:TemplateField HeaderText="Emp Uid" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpUid" Visible="false" Text='<%#Eval("EMP_UID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpName" Text='<%#Eval("EMP_NAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Channel">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpChannel" Text='<%#Eval("EMP_CHANNEL")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpCode" Text='<%#Eval("EMP_CODE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpEmail" Text='<%#Eval("EMP_EMAIL")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblActiveYn" Text='<%#"05" + Eval("EMP_MOBILE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salary">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpSalary" Text='<%#Eval("EMP_SALARY","{0:N2}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Annual Salary">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblAnnualSalary" Text='<%#Eval("EMP_ANNUAL_SALARY","{0:N2}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Active">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpActiveYn" Text='<%#Eval("EMP_ACTIVE_YN")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="cmdEdit" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("EMP_UID") %>' />
                        <asp:Button ID="btnDelete" runat="server" OnClientClick='return confirm("Are you sure you want to approve?")' Text="Delete" CommandName="cmdDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("EMP_UID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
