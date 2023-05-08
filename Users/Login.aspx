<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication3.Users.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <div class="login">
            <h2 class="login-heading">Login</h2>
            <div class="login-label-text">
                <label class="login-label" for="txtUsername">Username or email</label>
                <br>
                <asp:TextBox ID="txtUsername" class="login-text" placeholder="Enter your Username" runat="server"></asp:TextBox>
            </div>
            <div class="login-label-text">
                <label class="login-label" for="txtPass">Password</label>
                <br>
                <asp:TextBox ID="txtPass" class="login-text" TextMode="Password" placeholder="Enter your password" runat="server"></asp:TextBox>
            </div>
            <div class="login-remember-forgot">
                <div class="login-show">
                    <asp:CheckBox ID="chkShowPass" runat="server" class="login-show-checkbox" />
                    <label for="chkShowPass">Show Password</label>
                </div>
                <a class="login-link" href="forgotPass.aspx">Forgot Password</a>
            </div>
            <asp:Button ID="btnLogin" class="login-button" OnClick="btnLogin_Click" runat="server" Text="Log In" />
            <p class="login-signUp">Don't have an Account?<span><a class="login-link" href="registration.aspx"> Sign Up</a></span></p>
        </div>
    </div>
<script>
    const chkShowPass = document.getElementById('ContentPlaceHolder1_chkShowPass');
    const txtPass = document.getElementById('ContentPlaceHolder1_txtPass');
    chkShowPass.addEventListener('change', function () {
        if (chkShowPass.checked) {
            txtPass.type = 'text';
        } else {
            txtPass.type = 'password';
        }
    });

</script>    
</asp:Content>
