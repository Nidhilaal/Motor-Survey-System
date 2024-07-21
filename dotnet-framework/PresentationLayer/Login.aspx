<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PresentationLayer.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Survey System | Login</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
    <link href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <link href="https://fonts.googleapis.com/css?family=Russo+One" rel="stylesheet" />
    <link href="Assets/css/Login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="vh-100" style="background-image: url(Assets/Images/Login-Background.jpg); background-size: cover">

                <div class="container py-5 h-100">
                    <%--                    <h3 class="text-center" style="color: white; margin-top: 3%; font-size: 350%; font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif; font-weight: bold">MOTOR SURVEY SYSTEM</h3>--%>
                    <svg viewBox="0 10 900 100">
                        <text x="45%" y="50%" dy=".35em" text-anchor="middle">MOTOR SURVEY SYSTEM</text>
                    </svg>
                    <div class="row d-flex justify-content-center align-items-center h-100" style="margin-top:2%;">
                        <div class="col-12 col-md-8 col-lg-4 col-xl-4" >
                            <div class="card shadow-2-strong" style="border-radius: 1rem;">
                                <div class="card-body p-5 text-center">
                                    <h3 class="mb-4">Login</h3>
                                    <div class="form-outline mb-3">
                                        <label for="txtUserName">User Id</label>
                                        <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfUserId"
                                            runat="server"
                                            ControlToValidate="txtUserId"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            Font-Size="Small"
                                            ValidationGroup="vgLogin"
                                            ErrorMessage="Enter the User Id!!!">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-outline mb-4">
                                        <label for="txtPassword">Password</label>
                                        <asp:TextBox type="password" ID="txtPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:Label runat="server" ID="lblLoginFailed" ForeColor="Red"></asp:Label>
                                        <asp:RequiredFieldValidator ID="rfPassword"
                                            runat="server"
                                            ControlToValidate="txtPassword"
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            Font-Size="Small"
                                            ValidationGroup="vgLogin"
                                            ErrorMessage="Enter the Password!!!">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnLogin" Text="Login" ValidationGroup="vgLogin" runat="server" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
