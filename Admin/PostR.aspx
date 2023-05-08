<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="PostR.aspx.cs" Inherits="WebApplication3.Admin.PostR" %>

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


                                        <div class="col-sm-6 col-md-8 col-lg-12 mobile-inputs">
                                            <h4 class="sub-title">Reported Posts</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="rReportedPosts" runat="server" OnItemCommand="rReportedPosts_ItemCommand">
                                                        <ItemTemplate>
                                                            <div class="d-flex row">
                                                                <div class="d-flex flex-column col-lg-5">
                                                                    <asp:Label runat="server" ID="lblTitle"><h3><strong><%# Eval("PostTitle") %></strong></h3></asp:Label>
                                                                    <img class="rounded-5 forum-nav-profile" src='../<%# Eval("PostImage") %>' />
                                                                </div>
                                                                <div class="d-flex col-lg-5 flex-column">
                                                                    <asp:Label runat="server" ID="lblDescription"><h3>Description : </h3><span style="font-size:20px"> <%# Eval("PostDescription") %></span></asp:Label>
                                                                    <asp:Label runat="server" ID="lblLikes"><span style="font-size:20px;font-weight:bold;">Likes :  <%# Eval("Likes") %></span></asp:Label>
                                                                    <asp:Label runat="server" ID="lblComments"><span style="font-size:20px;font-weight:bold;">Comments : <%# Eval("TotalComments") %></span></asp:Label>
                                                                    <asp:Label runat="server" ID="lblCount"><span style="font-size:20px;font-weight:bold;"> Report Count : <%# Eval("Count") %></span></asp:Label>
                                                                    <div>
                                                                        <span style="font-size: 20px; font-weight: bold;">Link to Post:</span><asp:LinkButton runat="server" ID="lnbLinkToPost" CssClass="badge badge-primary ml-1" CommandArgument='<%# Eval("PostId") %>' CommandName="linkToPost">
                                                                            <i class="ti-link"></i>
                                                                        </asp:LinkButton>
                                                                        <br />
                                                                        <span style="font-size: 20px; font-weight: bold;">Ban this post:</span><asp:LinkButton runat="server" ID="lnbBlock" CssClass="badge badge-danger ml-1" CommandArgument='<%# Eval("PostId") %>' CommandName="Block">
                                                                            <i class="ti-angle-right"></i>
                                                                        </asp:LinkButton>
                                                                        <br />
                                                                        <span style="font-size: 20px; font-weight: bold;">Delete report:</span><asp:LinkButton runat="server" ID="lnbDelete" CssClass="badge badge-dark ml-1" CommandArgument='<%# Eval("PostId") %>' CommandName="Delete">
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
