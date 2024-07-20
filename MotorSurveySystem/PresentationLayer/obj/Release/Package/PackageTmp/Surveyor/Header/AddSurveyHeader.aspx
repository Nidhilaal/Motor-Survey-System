<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Surveyor.Master" AutoEventWireup="true" CodeBehind="AddSurveyHeader.aspx.cs" Inherits="PresentationLayer.Surveyor.Header.AddSurveyHeader" %>

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
                <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" CssClass="btn btn-dark" />
            </div>
        </div>
    </div>
    <div style="margin-right: 10%; margin-left: 10%; margin-top: 2%; margin-bottom: initial">
        <asp:Label runat="server" ID="lblAddSurveyDetails"> <h4>Add Survey Details</h4></asp:Label>
        <asp:Label runat="server" ID="lblTxtSurNo" Font-Size="X-Large" Font-Bold="true">Survey No:</asp:Label>
        <asp:Label runat="server" ID="lblSurNo" Font-Size="Large"></asp:Label>
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 2.5%; margin-bottom: 2.5%;">
            <asp:HiddenField ID="hfSurClmUid" runat="server" />
            <asp:HiddenField ID="hfSurUid" runat="server" />
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtSurClmNo" Text="Claim No. "></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="30" CssClass="form-control" ID="txtSurClmNo"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtSurChassisNo" Text="Chassis No. "></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="30" CssClass="form-control" ID="txtSurChassisNo" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtSurRegnNo" Text="Registration No. "></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="12" CssClass="form-control" ID="txtSurRegnNo"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" for="txtSurEngineNo" Text="Engine No. "></asp:Label><font color="red">*</font>
                <asp:TextBox runat="server" MaxLength="30" CssClass="form-control" ID="txtSurEngineNo"></asp:TextBox>
            </div>
        </div>

        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 2%; margin-bottom: 2%;">

            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label2" Text="Currency "></asp:Label><font color="red">*</font>
                <asp:DropDownList runat="server"
                    ID="ddlSurrCurr"
                    CssClass="form-control"
                    ValidationGroup="vgSaveSurveyHeader">
                </asp:DropDownList>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label3" Text="Amount(FC) "></asp:Label>
                <asp:TextBox runat="server" Style="text-align: right" CssClass="form-control" ID="txtSurFcAmt"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label runat="server" ID="Label4" Text="Amount(LC)"></asp:Label>
                <asp:TextBox runat="server" Style="text-align: right" CssClass="form-control" ID="txtSurLcAmt"></asp:TextBox>
            </div>
        </div>

        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-sm-3">
                <asp:RequiredFieldValidator ID="rfvSurrCurr" runat="server"
                    ControlToValidate="ddlSurrCurr"
                    InitialValue="NA"
                    ErrorMessage="Select a value"
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="vgSaveSurveyHeader" />
            </div>
        </div>
        <div class="row" style="margin-top: 1%; margin-bottom: 1%;">
            <div class="d-flex align-items-center justify-content-center">
                <asp:Button ID="btnSaveSurveyHeader" runat="server" Text="Save" OnClick="btnSaveSurveyHeader_Click" ValidationGroup="vgSaveSurveyHeader" CssClass="btn btn-success" />
                <asp:Button ID="btnUpdateSurveyHeader" runat="server" Text="Update " ValidationGroup="vgSaveSurveyHeader" OnClick="btnUpdateSurveyHeader_Click" CssClass="btn btn-warning" />
                <asp:Button Style="margin-left: 0.4%;" ID="btnSubmitSurveyHeader" runat="server" Text="Submit " ValidationGroup="vgSaveSurveyHeader" OnClick="btnSubmitSurveyHeader_Click" CssClass="btn btn-success" />
            </div>
        </div>
    </div>

    <div class="container ">
        <div class="row" style="margin-left: 2%; margin-right: 2%; margin-top: 1%; margin-bottom: 1%;">
            <div class="col-1" style="margin-left: 99%;">
                <asp:Button ID="btnAddDetails" Text="Add Details" runat="server" OnClick="btnAddDetails_Click" CssClass="btn btn-primary" />
            </div>
            <div class="d-flex align-items-center justify-content-center">
                <asp:Label runat="server" ID="lblAccessories"><h4>Accessories</h4></asp:Label>
            </div>
            <div class="d-flex align-items-center justify-content-center" style="margin-top: 2%">
                <asp:Label runat="server" ID="lblEmptySurveyDetailsList" Font-Size="Large" ForeColor="#10898d"></asp:Label>
            </div>
        </div>
    </div>
    <div>
        <asp:GridView ID="gvSurveyDetails" runat="server"
            AutoGenerateColumns="false"
            CssClass="table table-striped table-bordered table-condensed"
            OnRowCommand="gvSurveyDetails_RowCommand"
            Style="width: 81%; margin: 0 auto;">
            <Columns>
                <asp:TemplateField HeaderText="Sur Dtl Uid" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurdSurUid" Visible="false" Text='<%#Eval("SURD_UID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sur Hdr Uid" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSurdSurClmNo" Visible="false" Text='<%#Eval("SURD_SUR_UID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
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
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEditSurveyDetails" runat="server" Text="Edit" CommandName="cmdEdit" CssClass="btn btn-warning btn-sm" CommandArgument='<%#Eval("SURD_UID") %>' />
                        <asp:Button ID="btnDeleteSurveyDetails" runat="server" OnClientClick='return confirm("Are you sure you want to Delete?")' Text="Delete" CommandName="cmdDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("SURD_UID")+","+Eval("SURD_SUR_UID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
