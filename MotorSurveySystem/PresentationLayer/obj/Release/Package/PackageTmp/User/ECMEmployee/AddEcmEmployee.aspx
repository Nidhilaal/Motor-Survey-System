<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/User.Master" AutoEventWireup="true" CodeBehind="AddEcmEmployee.aspx.cs" Inherits="PresentationLayer.User.ECMEmployee.AddEcmEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function formatSalary(event, element) {
            if (event.which < 48 || event.which > 57) {
                event.preventDefault();
            }
            let value = element.value.replace(/,/g, '');
            value = value.replace(/\B(?=(\d{2})+(?!\d))/g, ",");
            value = value.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            if (value.replace(/,/g, '').length > 8) {
                event.preventDefault();
            } else {
                element.value = value;
            }
        }
       

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" style="margin-top: 1%; margin-bottom: 1%">
            <div style="position: absolute; right: 2%; width: 92%; text-align: right;">
                <asp:Button ID="btnAddNew" Text="Add New" runat="server" OnClick="btnAddNew_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" CssClass="btn btn-dark" Style="float: right; margin-left: 0.4%;" />
            </div>
        </div>
    </div>
    <div style="margin-right: 10%; margin-left: 10%; margin-top: 5%; margin-bottom: initial">
        <asp:Label runat="server" ID="lblAddEmployeeDetails">
            <h4>Add Employee Details</h4>
        </asp:Label>
        <%-- <asp:Label runat="server" ID="lblTxtEmpUid" Font-Size="X-Large" ForeColor="#10898d" Font-Bold="true">Employee Uid:</asp:Label>
        <asp:Label runat="server" ID="lblEmpUid" Font-Size="Large"></asp:Label>--%>
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 3%; margin-bottom: 1%;">
                    <div class="col-sm-3">
                        <asp:HiddenField ID="hfEmpUid" runat="server" />
                        <asp:Label runat="server" for="txtEmpName" Text="Name"></asp:Label>
                        <font color="red">*</font>
                        <asp:TextBox runat="server" MaxLength="100" CssClass="form-control" ID="txtEmpName"></asp:TextBox>
                    </div>

                    <div class="col-sm-3">
                        <asp:Label runat="server" for="ddlEmpChannel" Text="Channel"></asp:Label>
                        <font color="red">*</font>
                        <asp:DropDownList runat="server"
                            ID="ddlEmpChannel"
                            CssClass="form-control"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlEmpChannel_SelectedIndexChanged"
                            ValidationGroup="vgSaveEmployee">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label runat="server" for="ddlEmpcode" Text="Code"></asp:Label>
                        <font color="red">*</font>
                        <asp:DropDownList runat="server"
                            ID="ddlEmpcode"
                            CssClass="form-control"
                            ValidationGroup="vgSaveEmployee">
                        </asp:DropDownList>
                    </div>

                    <div class="col-sm-3">
                        <asp:Label runat="server" for="txtEmpEmail" Text="Email"></asp:Label>
                        <font color="red">*</font>
                        <asp:TextBox runat="server" MaxLength="100" CssClass="form-control" ID="txtEmpEmail"></asp:TextBox>
                    </div>

                </div>

                <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
                    <div class="col-sm-3">
                        <asp:RequiredFieldValidator ID="rfvEmpName"
                            runat="server"
                            ControlToValidate="txtEmpName"
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="vgSaveEmployee"
                            ErrorMessage="Field Empty!!!">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-3">
                        <asp:RequiredFieldValidator ID="rfvEmpChannel"
                            runat="server"
                            ControlToValidate="ddlEmpChannel"
                            InitialValue="NA"
                            ErrorMessage="Select a value!!!"
                            Display="Dynamic"
                            ForeColor="Red"
                            ValidationGroup="vgSaveEmployee" />
                    </div>
                    <div class="col-sm-3">
                        <asp:RequiredFieldValidator ID="rfvddlEmpCode"
                            runat="server"
                            ControlToValidate="ddlEmpCode"
                            InitialValue="NA"
                            ErrorMessage="Select a value!!!"
                            Display="Dynamic"
                            ForeColor="Red"
                            ValidationGroup="vgSaveEmployee" />
                    </div>
                    <div class="col-sm-3">
                        <asp:RequiredFieldValidator ID="rfvEmpEmail"
                            runat="server"
                            ControlToValidate="txtEmpEmail"
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="vgSaveEmployee"
                            ErrorMessage="Field Empty!!!">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmpEmail"
                            runat="server"
                            ControlToValidate="txtEmpEmail"
                            ForeColor="Red"
                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                            Display="Dynamic"
                            ErrorMessage="Invalid email address" />
                    </div>
                </div>
                <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 2%; margin-bottom: 1%;">
                    <div class="col-sm-3">
                        <asp:Label runat="server" for="txtEmpMobile" Text="Mobile No"></asp:Label>
                        <font color="red">*</font>
                        <asp:TextBox runat="server" ClientIDMode="Static" TextMode="Number" CssClass="form-control" ID="txtEmpMobile"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label runat="server" for="txtEmpSalary" Text="Salary"></asp:Label>
                        <font color="red">*</font>
                        <asp:TextBox runat="server" onkeypress="formatSalary(event, this)" Style="text-align: right" CssClass="form-control" ID="txtEmpSalary"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label runat="server" for="chkActiveEmployee" Text="Active"></asp:Label>
                        <br />
                        <asp:CheckBox ID="chkActiveEmployee" runat="server" />
                    </div>
                </div>
                <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
                    <div class="col-sm-3">
                        <asp:RequiredFieldValidator ID="rfvEmpMobile"
                            runat="server"
                            ControlToValidate="txtEmpMobile"
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="vgSaveEmployee"
                            ErrorMessage="Field Empty!!!">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmpMobile"
                            runat="server"
                            ControlToValidate="txtEmpMobile"
                            ForeColor="Red"
                            Font-Size="Small" 
                            ValidationExpression="^05\d{10}$"
                            ValidationGroup="vgSaveEmployee"
                            ErrorMessage="Mobile number must start with '05' & exactly 12 digits"
                            Display="Dynamic">
                        </asp:RegularExpressionValidator>
                    </div>
                    <div class="col-sm-3">
                        <asp:RequiredFieldValidator ID="rfvEmpSalary"
                            runat="server"
                            ControlToValidate="txtEmpSalary"
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="vgSaveEmployee"
                            ErrorMessage="Field Empty!!!">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 2%; margin-bottom: 1%;">
            <div class="d-flex align-items-center justify-content-center">
                <asp:Button ID="btnSaveEmployee" runat="server" Text="Save" OnClick="btnSaveEmployee_Click" ValidationGroup="vgSaveEmployee" CssClass="btn btn-success" />
                <asp:Button ID="btnUpdateEmployee" runat="server" Text="Update" OnClick="btnUpdateEmployee_Click" ValidationGroup="vgSaveEmployee" CssClass="btn btn-warning" />
            </div>
        </div>
    </div>
</asp:Content>
