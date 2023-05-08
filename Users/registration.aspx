<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="WebApplication3.Users.registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        //For disappearing alert messages
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
    <%--Show Image Preview --%>

    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgUser.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="registration">
        <h2 class="registration-heading">Registration</h2>
        <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
        <div class="row">
            <div class="col-lg-2"></div>

            <div class="col-lg-8">

                <div class="registration-input-fields">

                    <div class="registration-label-text">
                        <label class="login-label" for="nameusername">Name</label>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name is Required" ControlToValidate="txtName"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revName" runat="server" ErrorMessage="Name should be in characters only"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true"
                            ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txtName"></asp:RegularExpressionValidator>
                        <br>
                        <asp:TextBox class="login-text registration-text" ID="txtName" runat="server" placeholder="Enter your Name"></asp:TextBox>
                    </div>


                    <div class="registration-label-text">
                        <label class="login-label" for="email">Email</label>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Wrong e-mail address"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true"
                            ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="E-mail is Required" ControlToValidate="txtEmail"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <br>
                        <asp:TextBox class="login-text registration-text" ID="txtEmail" runat="server" placeholder="Enter your Email"></asp:TextBox>
                    </div>


                    <div class="registration-label-text">
                        <label class="login-label" for="username">Username</label>
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Username is Required" ControlToValidate="txtUsername"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <br>
                        <asp:TextBox class="login-text registration-text" ID="txtUsername" runat="server" placeholder="Enter your username"></asp:TextBox>
                    </div>



                    <div class="registration-label-text">
                        <label class="login-label" for="mobileNum">Mobile Number</label>
                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ErrorMessage="Mobile Number is Required" ControlToValidate="txtNumber"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revMobile" runat="server" ErrorMessage="Mobile Number should be of 10 digits only"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true"
                            ValidationExpression="^[0-9]{10}$" ControlToValidate="txtNumber"></asp:RegularExpressionValidator>
                        <br>
                        <asp:TextBox TextMode="number" class="login-text registration-text" ID="txtNumber" runat="server" placeholder="Enter your Mobile Number"></asp:TextBox>

                    </div>



                    <div class="registration-label-text">
                        <label class="login-label" for="DOB">DOB</label>
                        <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ErrorMessage="DOB is Required" ControlToValidate="txtdob"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rvDOB" runat="server" ForeColor="Red" ErrorMessage="You must be 18 years or above" MaximumValue="01-01-2003" MinimumValue="01-01-1900" ControlToValidate="txtdob" Type="Date"></asp:RangeValidator>
                        <br>
                        <asp:TextBox TextMode="Date" class="login-text registration-text" ID="txtdob" runat="server"></asp:TextBox>
                    </div>



                    <div class="registration-label-text">
                        <label class="login-label" for="gender">Gender</label>
                        <asp:RequiredFieldValidator ID="rfvGender" runat="server" ErrorMessage="Gender is Required" ControlToValidate="txtgender"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <br>
                        <asp:TextBox class="login-text registration-text" ID="txtgender" runat="server" placeholder="Enter your gender"></asp:TextBox>

                    </div>



                    <div class="registration-label-text">
                        <label class="login-label" for="password">Password</label>
                        <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Password is Required" ControlToValidate="pass"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <br>
                        <asp:TextBox ID="pass" TextMode="Password" class="login-text registration-text" runat="server" placeholder="Enter your password"></asp:TextBox>
                    </div>



                    <div class="registration-label-text">
                        <asp:Label ID="lblImage" runat="server" class="login-label">Profile Pic</asp:Label>
                        <asp:FileUpload ID="fuUserImage" runat="server" CssClass="form-control" ToolTip="User Image" onChange="ImagePreview(this);" />

                    </div>
                </div>
                <div class="row pt-5">
                    <div style="align-items: center">
                        <asp:Image ID="imgUser" runat="server" CssClass="img-thumbnail" />
                    </div>
                </div>
                <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" CssClass="registration-signUp" OnClick="btnSignUp_Click" />

            </div>
        </div>
    </div>

</asp:Content>
