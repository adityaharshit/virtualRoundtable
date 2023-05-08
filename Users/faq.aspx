<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="faq.aspx.cs" Inherits="WebApplication3.Users.faq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>FAQ</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12" style="background:linear-gradient(to right,#ff4066,#fff16a); height:300px;text-align:center;">
            <div style="margin:80px 0 0; color:#fefefe;">  
                <h2 style="font-size:5em;font-weight:bold;">FAQ</h2>
                <h4>Frequently Asked Questions</h4>
            </div>
        </div>
    </div>
    <div class="row m-5 d-flex">
        <br />

        <div class="col-lg-7 m-1 p-3" >
            <asp:Repeater runat="server" ID="rFAQ" OnItemCommand="rFAQ_ItemCommand">
                <ItemTemplate>

            <div style="background-color:#fefefe;" class="p-3 mb-2">

            <div class="d-flex justify-content-between align-items-center rounded-2">
                <p style="font-size:27px"><%# Eval("Question") %></p>
                <%--<asp:LinkButton runat="server" CssClass="rndclass" ID="lnbDown" ForeColor="Red" CommandName="showHide"><i class="ti-arrow-down"></i></asp:LinkButton>--%>
            </div>
            <asp:Label runat="server" ID="lblAnswer"  Text='<%# Eval("Answer") %>'></asp:Label>
            </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
        <div class="col-lg-1"></div>
        <div class="col-lg-3 p-3 rounded-3" >
            <asp:Label runat="server" Visible="true" ID="lblNew" Text="Got a new Question?" CssClass="m-2 form-label"></asp:Label>
            <asp:TextBox runat="server" ID ="txtQuestion" CssClass="form-control" placeholder="Enter your question." TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:TextBox runat="server" ID ="txtEmail" CssClass="form-control" placeholder="Enter your Email" TextMode="Email"></asp:TextBox>
            <br />
            <asp:Button runat="server" ID="btnSubmit" CssClass="form-control btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
    </div>
</asp:Content>


