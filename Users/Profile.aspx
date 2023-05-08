<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebApplication3.Users.Profile" %>
<%@ Import Namespace="WebApplication3" %>
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
     <% 
        string imageUrl = "../"+Session["ProfilePic"].ToString();
    %>
    <br />
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </div>
                <h2>My Profile</h2>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="card-title mb-4" >
                                <div class="d-flex justify-content-lg-start">
                                    <div class="image-container">
                                        <img src="<%= Utils.GetImageUrl(imageUrl) %>" id="imgProfile" style="width:150px; height:150px;"
                                            class="img-thumbnail"/>
                                        <div class="middle pt-2">
                                            <asp:LinkButton runat="server" ID="lnbRegistration" class="btn btn-warning" OnClick="lnbRegistration_Click"><i class="fa fa-pencil"></i>Edit Details</asp:LinkButton>
                                            <%--<a href="registration.aspx?id=<% Response.Write(Session["userId"]); %>" class="btn btn-warning"><i class="fa fa-pencil"></i>Edit Details</a>--%>
                                        </div>
                                    </div>
                                    <div class="userData ml-3">
                                        <h2 class="d-block" style="font-size: 1.5rem; font-weight: bold">
                                            <a href="javascript:void(0);" style="text-decoration:none; color:black">
                                                <% Response.Write(Session["UName"]); %></a>
                                        </h2>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0)" style="text-decoration:none; color:black">
                                                <asp:Label ID="lblUsername" runat="server" ToolTip="Unique Username">
                                                    <% Response.Write(Session["Username"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0)" style="text-decoration:none; color:black">
                                                <asp:Label ID="lblCreatedDate" runat="server" ToolTip="Account Created On">
                                                    <% Response.Write(Session["JoinedDate"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <ul class="nav nav-tabs mb-4" id="myTab" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active text-info" id="basicInfo-tab" data-toggle="tab" href="#basicInfo" role="tab"
                                                aria-control="basicInfo" aria-selected="true"><i class="fa fa-id-badge mr-2"></i>Basic Info</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link text-info" id="savedPosts-tab" data-toggle="tab" href="#savedPosts" role="tab"
                                                aria-control="savedPosts" aria-selected="false"><i class="fa fa-id-badge mr-2"></i>Saved Posts</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link text-info" id="myPosts-tab" data-toggle="tab" href="#myPosts" role="tab"
                                                aria-control="myPosts" aria-selected="false"><i class="fa fa-id-badge mr-2"></i>My Posts</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link text-info" id="blockedUser-tab" data-toggle="tab" href="#blockedUser" role="tab"
                                                aria-control="blockedUser" aria-selected="false"><i class="fa fa-id-badge mr-2"></i>Blocked Users</a>
                                        </li>
                                        
                                        <li class="nav-item">
                                            <a class="nav-link text-info" id="likedPosts-tab" data-toggle="tab" href="#likedPosts" role="tab"
                                                aria-control="likedPosts" aria-selected="false"><i class="fa fa-id-badge mr-2"></i>Liked Posts</a>
                                        </li>
                                        
                                        <li class="nav-item">
                                            <a class="nav-link text-info" id="dislikedPosts-tab" data-toggle="tab" href="#dislikedPosts" role="tab"
                                                aria-control="dislikedPosts" aria-selected="false"><i class="fa fa-id-badge mr-2"></i>Disliked Posts</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link text-info" id="comments-tab" data-toggle="tab" href="#comments" role="tab"
                                                aria-control="comments" aria-selected="false"><i class="fa fa-id-badge mr-2"></i>Comments</a>
                                        </li>
                                    </ul>
                                    <div class="tab-content ml-1" id="myTabContent" role="tablist">
                                        <%-- BAsic user info --%>
                                        <div class="tab-pane fade show active" id="basicInfo" role="tabpanel" aria-labelledby="basicInfo-tab">
                                            <asp:Repeater ID="rfUserProfile" runat="server"  >
                                                <ItemTemplate>

                                                    <div class="row">
                                                        <div class="col-sm-3 colmd-2 col-5">
                                                            <label style="font-weight:bold;">Full Name</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Name") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 colmd-2 col-5">
                                                            <label style="font-weight:bold;">Username</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Username") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 colmd-2 col-5">
                                                            <label style="font-weight:bold;">Mobile No.</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Phone") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 colmd-2 col-5">
                                                            <label style="font-weight:bold;">Email</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Email") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 colmd-2 col-5">
                                                            <label style="font-weight:bold;">Total Posts</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%=postCount %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 colmd-2 col-5">
                                                            <label style="font-weight:bold;">Followers</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%=followerCount %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 colmd-2 col-5">
                                                            <label style="font-weight:bold;">Following</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%=followingCount %>
                                                        </div>
                                                    </div>
                                                    
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Products.Name AS Product, Orders.Quantity, Orders.Status, Staff.Name AS Delivery_Staff, Staff.MobNo AS Staff_Mobile, Orders.Otp FROM Orders INNER JOIN Products ON Orders.ProductId = Products.ProductId INNER JOIN Staff ON Orders.DMan = Staff.DId"></asp:SqlDataSource>--%>
                                        </div>
                                        <div class="tab-pane fade" id="savedPosts" role="tabpanel" aria-labelledby="savedPosts-tab">
                                            <h3>Saved Posts</h3>
                                            <h4 class="sub-title"></h4>
                                            <div class="row">

                                            <asp:Repeater runat="server" ID="rSaved" OnItemCommand="rSaved_ItemCommand" OnItemDataBound="rSaved_ItemDataBound">
                                                <ItemTemplate>
                                                    <div class="col-lg-4 col-md-5 col-sm-10" id="savedPost">
                                                        <asp:LinkButton runat="server" ID="savedTitle" CommandArgument='<%# Eval("PostId") %>' CommandName="linkToPost"><%# Eval("PostTitle") %></asp:LinkButton>
                                                        <%--<asp:Label runat="server" ID="savedTitle"><%# Eval("PostTitle") %></asp:Label>--%>
                                                        <asp:HiddenField runat="server" ID="hdnSavedPostID" Value='<%# Eval("PostId") %>'/>
                                                        <asp:PlaceHolder runat="server" Visible='<%# Eval("PostImage")!=null%>'>
                                                            <img src="../<%# Eval("PostImage") %>" id="imgProfile" class="img-thumbnail" />
                                                        </asp:PlaceHolder>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </div>
                                            
                                        </div>
                                        <div class="tab-pane fade" id="myPosts" role="tabpanel" aria-labelledby="myPosts-tab">
                                            <h3></h3>
                                            <h4 class="sub-title"></h4>
                                            <div class="row">

                                            <asp:Repeater runat="server" ID="rMyPosts" OnItemCommand="rMyPosts_ItemCommand">
                                                <ItemTemplate>
                                                    <div class="col-lg-4 col-md-5 col-sm-10 mt-4" id="savedPost">
                                                        <asp:LinkButton runat="server" ID="savedTitle" CommandArgument='<%# Eval("PostId") %>' CommandName="linkToPost"><%# Eval("PostTitle") %></asp:LinkButton>
                                                        <asp:HiddenField runat="server" ID="hdnSavedPostID" Value='<%# Eval("PostId") %>'/>
                                                        <img src="../<%# Eval("PostImage") %>" id="imgProfile" class="img-thumbnail"/>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </div>
                                            
                                        </div>
                                        <div class="tab-pane fade" id="blockedUser" role="tabpanel" aria-labelledby="blockedUser-tab">
                                            <h3>Blocked Users</h3>
                                            <h4 class="sub-title"></h4>
                                            <asp:Repeater runat="server" ID="rBlocked" OnItemCommand="rBlocked_ItemCommand">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-lg-1">
                                                            <img src='../<%# Eval("ProfilePic") %>' id="blkProfile" style="width:50px; height:50px;"
                                                            class="img-thumbnail"/>
                                                        </div>
                                                        <div class="col-lg-2 align-self-center">
                                                             <asp:Label runat="server" ID="lblBlkName"><%# Eval("UserBlockedName") %></asp:Label>
                                                        </div>
                                                        <div class="col-lg-2 align-self-center">
                                                             <asp:Label runat="server" ID="lblBlkUsername"><%# Eval("UBUserName") %></asp:Label>
                                                        </div>
                                                        <div class="col-lg-2 align-self-center">
                                                            <asp:LinkButton runat="server" ID="lnbUnblock" Text="Unblock" CssClass="badge badge-danger" CommandArgument='<%# Eval("UserBlockedId") %>' CommandName="Unblock"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                
                                            </asp:Repeater>

                                        </div>
                                        <div class="tab-pane fade" id="likedPosts" role="tabpanel" aria-labelledby="likedPosts-tab">
                                            <h3>Liked Posts</h3>
                                            <h4 class="sub-title"></h4>
                                            <div class="row">

                                            <asp:Repeater runat="server" ID="rLiked" OnItemCommand="rMyPosts_ItemCommand">
                                                <ItemTemplate>
                                                    <div class="col-lg-4 col-md-5 col-sm-10" id="savedPost">
                                                        <asp:LinkButton runat="server" ID="likedTitle" CommandArgument='<%# Eval("PostId") %>' CommandName="linkToPost"><%# Eval("PostTitle") %></asp:LinkButton>
                                                        <asp:HiddenField runat="server" ID="hdnLikedPostID" Value='<%# Eval("PostId") %>'/>
                                                        <img src="../<%# Eval("PostImage") %>" id="imgProfile" class="img-thumbnail"/>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="dislikedPosts" role="tabpanel" aria-labelledby="dislikedPosts-tab">
                                            <h3>Disliked Posts</h3>
                                            <h4 class="sub-title"></h4>
                                            <div class="row">

                                            <asp:Repeater runat="server" ID="rDisliked" OnItemCommand="rDisliked_ItemCommand">
                                                <ItemTemplate>
                                                    <div class="col-lg-4 col-md-5 col-sm-10" id="savedPost">
                                                        <asp:LinkButton runat="server" ID="dislikedTitle" CommandArgument='<%# Eval("PostId") %>' CommandName="linkToPost"><%# Eval("PostTitle") %></asp:LinkButton>
                                                        <asp:HiddenField runat="server" ID="hdnDisLikedPostID" Value='<%# Eval("PostId") %>'/>
                                                        <img src="../<%# Eval("PostImage") %>" id="imgProfile" class="img-thumbnail"/>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="comments" role="tabpanel" aria-labelledby="privatePosts-tab">
                                            <h3>Comments</h3>
                                            <h4 class="sub-title"></h4>
                                            <div class="row">
                                                <asp:Repeater ID="rPrivate" runat="server" OnItemCommand="rPrivate_ItemCommand">
                                                    <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                <tr>
                                                                    <th class="table-plus">Title</th>
                                                                    <th>Image</th>
                                                                    <th>Comment</th>
                                                                    <th>Commented on</th>
                                                                    <th>Go to post</th>
                                                                </tr>
                                                            </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"> <%# Eval("PostTitle") %> </td>
                                                                <td><img alt="" width="40" src="<%# Utils.GetImageUrl(Eval("PostImage")) %>" /></td>
                                                                <td><%# Eval("Comment") %></td>
                                                                <td><%# Eval("CreatedDate") %></td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" CssClass="badge badge-primary"
                                                                        CommandArgument ='<%# Eval("PostId")%>' CommandName="linkToPost"  CausesValidation="false">
                                                                        <i class="ti-link"></i>
                                                                    </asp:LinkButton>

                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody> 
                                                            </table>
                                                        </FooterTemplate>
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
    </section>
    <br /><br />
</asp:Content>
