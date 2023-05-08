<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="forgotPass.aspx.cs" Inherits="WebApplication3.Users.forgotPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Reset Password</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <div class="login">
            <h2 class="login-heading">Reset</h2>
            <div class="login-label-text">
                <label class="login-label" for="txtUsername">Username</label>

                <asp:TextBox ID="txtUsername" class="login-text" placeholder="Enter your Username" runat="server"></asp:TextBox>
            </div>
            <div class="login-label-text">
                <label class="login-label" for="txtEmail">Email</label>

                <asp:TextBox ID="txtEmail" class="login-text" placeholder="Enter your Email" runat="server"></asp:TextBox>
            </div>
            <div class="login-label-text">
                <label class="login-label" for="txtPass">Mobile Number</label>

                <asp:TextBox ID="txtPhone" class="login-text" TextMode="number" placeholder="Enter your Mobile number" runat="server"></asp:TextBox>
            </div>
            <div class="login-label-text">
                <label class="login-label" for="txtPass">New password</label>

                <asp:TextBox ID="txtPass" class="login-text" TextMode="password" placeholder="Enter your new Password" runat="server"></asp:TextBox>
            </div>
            <asp:Button ID="btnReset" class="login-button" OnClick="btnReset_Click" runat="server" Text="Reset" />
            <p class="login-signUp">Don't have an Account?<span><a class="login-link" href="registration.aspx"> Sign Up</a></span></p>
        </div>
    </div>
</asp:Content>
