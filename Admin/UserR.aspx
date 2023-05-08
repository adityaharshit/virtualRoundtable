<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserR.aspx.cs" Inherits="WebApplication3.Admin.UserR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pcoded-inner-content pt-0">
        <div class="align-align-self-end">
            <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
        </div>
        <div <%--class="main-body"--%>>
            <div <%--class="page-wrapper"--%>>

                <div <%--class="page-body"--%>>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header">
                                </div>
                                <div class="card-block">
                                    <div class="row">
                                         

                                        <div class ="col-sm-6 col-md-8 col-lg-12 mobile-inputs">
                                            <h4 class="sub-title">Reported Posts</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="rReportedUser" runat="server" OnItemCommand="rReportedUser_ItemCommand" >
                                                        <ItemTemplate>
                                                            <div class="d-flex row">
                                                                <div class="d-flex flex-column col-lg-5">
                                                                    <asp:LinkButton runat="server" ID="lnbProfile" CommandName="linkToProfile" CommandArgument='<%# Eval("UId") %>' ><h3><strong><%# Eval("Name") %></strong></h3></asp:LinkButton>
                                                                    <%--<asp:Label runat="server" ID="lblTitle"><h3><strong><%# Eval("Name") %></strong></h3></asp:Label>--%>
                                                                    <img class="rounded-5 forum-nav-profile" src='../<%# Eval("ProfilePic") %>' />
                                                                </div>
                                                                <div class="d-flex col-lg-6 flex-column">
                                                                    <asp:Label runat="server" ID="lblDescription"><h4>Email : </h4><span style="font-size:20px"> <%# Eval("Email") %></span></asp:Label>
                                                                    <asp:Label runat="server" ID="lblLikes"><span style="font-size:20px;font-weight:bold;">Username :  <%# Eval("Username") %></span></asp:Label>
                                                                    <asp:Label runat="server" ID="lblComments"><span style="font-size:20px;font-weight:bold;">Joined On : <%# Eval("JoinedOn") %></span></asp:Label>
                                                                    <asp:Label runat="server" ID="lblCount"><span style="font-size:20px;font-weight:bold;"> Report Count : <%# Eval("Count") %></span></asp:Label>
                                                                    <div>
                                                                        <span style="font-size:20px;font-weight:bold;">Ban : </span>
                                                                        <asp:LinkButton runat="server" ID ="lnbBlock" CssClass="badge badge-danger" CommandArgument='<%# Eval("UId") %>' CommandName="Block">
                                                                            <i class="ti-trash"></i>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                <div>
                                                                    <span style="font-size:20px;font-weight:bold;">Delete : </span>
                                                                        <asp:LinkButton runat="server" ID ="lnbDelete" CssClass="badge badge-dark" CommandArgument='<%# Eval("UId") %>' CommandName="Delete">
                                                                            <i class="ti-trash"></i>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    
                                                                </div>
                                                            </div>
                                                            <hr />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
