<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="post.aspx.cs" Inherits="WebApplication3.Users.post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div class="row">
        <div class="col-lg-3"></div>
        <div class="col-lg-6 col-md-10 bg-white rounded-1 align-items-center justify-content-center p-3">
            <asp:Repeater ID="rPosts" runat="server" OnItemCommand="rPosts_ItemCommand" OnItemDataBound="rPosts_ItemDataBound">
                <ItemTemplate>


                    <div class="d-flex align-items-center justify-content-between">
                        <div class="d-flex flex-row align-items-center">

                            <img class="rounded-5 forum-nav-profile" src='../<%# Eval("ProfilePic") %>' style="width: 50px; height: 50px" />
                            <div class="d-flex flex-column align-items-center m-2">
                                <div class="d-flex align-items-center">
                                    <p style="font-size: 20px; margin-right: 10px"><%# Eval("Username") %></p>
                                    <asp:LinkButton CssClass="forum-post-userInfo-nf-follow" ID="lnkFollow" runat="server" CommandArgument='<%# Eval("UId") %>' CausesValidation="false" CommandName="Follow" Text="Follow"></asp:LinkButton>
                                    <asp:HiddenField ID="hdnUId" runat="server" Value='<%# Eval("UId") %>' />
                                    <asp:HiddenField ID="hdnPostId" runat="server" Value='<%# Eval("PostId") %>' />
                                    <asp:HiddenField ID="hdnDesc" runat="server" Value='<%# Eval("PostDescription") %>' />

                                </div>
                                <div>
                                    <p class="" style="font-size: 12px; display: inline-block;"><%# Convert.ToDateTime( Eval("CreatedDate")).ToString("dd/MM/yyyy") %></p>
                                    <asp:Label ID="lblNegative" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <asp:LinkButton runat="server" CssClass="rndclass mr-2" ID="lnbDown" ForeColor="Red" CommandName="hideDelete" CommandArgument='<%# Eval("UId") %>'><i class="fa-solid fa-xmark"></i></asp:LinkButton>
                    </div>
                    <div class="">
                        <h3 class=" forum-post-topic"><strong><%# Eval("postTitle") %></strong></h3>
                        <p class="forum-post-description"><%# Eval("postDescription") %></p>
                        <img class="rounded img-fluid" src="../<%# Eval("postImage") %>" alt="">
                    </div>
                    <br />
                    <div class="forum-post-bottom align-items-center">
                        <div class="forum-post-bottom-components">
                            <asp:LinkButton ID="lnkLikes" runat="server" CommandName="lnkLike" CommandArgument='<%# Eval("PostId") %>'>
                    <i class="fa-regular fa-thumbs-up cursor-pointer fa-2x"></i>
                            </asp:LinkButton>
                            <asp:Label ID="likeCount" runat="server" Text='<%# Eval("Likes") %>'></asp:Label>
                            <asp:LinkButton ID="lnkDislike" runat="server" CommandName="lnkDislike" CommandArgument='<%# Eval("PostId") %>'>
                    <i class="fa-regular fa-thumbs-down cursor-pointer fa-2x"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="forum-post-bottom-components">
                            <asp:LinkButton ID="lnkComment" runat="server" CommandName="Comment" CommandArgument='<%# Eval("PostId") %>'>
                    <i class="fa-regular fa-message cursor-pointer fa-xl"></i>
                            </asp:LinkButton>
                            <label for="fa-message"><%# Eval("TotalComments") %> comments</label>
                        </div>

                        <div class="forum-post-bottom-components">
                            <i class="fa-solid fa-eye"></i>
                            <label for="fa-message"><%# Eval("Views") %> views</label>
                        </div>

                        <div class="forum-post-bottom-components">
                            <i class="fa-solid fa-ellipsis-vertical forum-post-menu cursor-pointer fa-xl"></i>
                        </div>

                        <div class="forum-post-bottom-menu hidden">
                            <div class="forum-post-bottom-menu-content">

                                <i class="fa-regular fa-flag"></i>
                                <asp:LinkButton ID="lnbReportPost" runat="server" CommandArgument='<%# Eval("PostId") %>' CausesValidation="false" CommandName="ReportPost" Text="Report this post">
                                </asp:LinkButton>
                            </div>
                            <hr>
                            <div class="forum-post-bottom-menu-content">

                                <i class="fa-regular fa-flag"></i>
                                <asp:LinkButton ID="lnbReportUser" runat="server" CommandArgument='<%# Eval("UId") %>' CausesValidation="false" CommandName="ReportUser" Text="Report this user"></asp:LinkButton>

                            </div>
                            <hr>
                            <div class="forum-post-bottom-menu-content">
                                <i class="fa-regular fa-bookmark"></i>
                                <asp:LinkButton ID="lnbSave" runat="server" CommandArgument='<%# Eval("PostId") %>' CausesValidation="false" CommandName="Save" Text="Save"></asp:LinkButton>

                            </div>

                            <hr>
                            <div class="forum-post-bottom-menu-content">
                                <i class="fa-solid fa-ban"></i>
                                <asp:LinkButton ID="lnbBlockUser" runat="server" CommandArgument='<%# Eval("UId") %>' CausesValidation="false" CommandName="BlockUser" Text="Block this user"></asp:LinkButton>

                            </div>
                        </div>


                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div style="margin: 10px 0" class="d-flex flex-column align-content-center">
                <br />
                <asp:Label ID="lblPrivate" runat="server" CssClass="align-self-center" Text="This is a private post. Click on the Button to request access from the user"></asp:Label>
                <br />
                <asp:Button CssClass="btn btn-primary btn-color-primary rounded-2 align-self-center" OnClick="btnAccess_Click" ID="btnAccess" runat="server" Text="Request Access" />
                <br />
            </div>

            <div style="margin: 10px 0">

                <%-- Add New Comments --%>
                <asp:Label ID="lblThoughts" runat="server"></asp:Label>
                <div class="">
                    <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" CssClass="form-control" placeholder="Share your thoughts?"></asp:TextBox>
                    <asp:Button runat="server" ID="btnComment" CssClass="btn rounded-2 btn-dark m-1" Text="Comment" OnClick="btnComment_Click" />
                </div>

                <%-- Comments --%>
                <asp:Repeater ID="rComments" runat="server" OnItemCommand="rComments_ItemCommand">
                    <ItemTemplate>

                        <div style="background-color: #dbe1e6; width: 90%;" class="rounded">
                            <div class="d-flex justify-content-between align-items-center m-2">
                                <div class="d-flex align-items-center">
                                    <img class="rounded-5 forum-nav-profile" style="height: 50px; width: 50px" src='../<%# Eval("ProfilePic") %>' />
                                    <div class="d-flex flex-column align-items-center m-2">
                                        <div>
                                            <p style="font-size: 20px"><%# Eval("Name") %></p>
                                            <asp:HiddenField ID="hdn" runat="server" Value="0" />
                                        </div>
                                        <div>
                                            <p class="" style="font-size: 12px; display: inline-block;"><%# Convert.ToDateTime( Eval("CreatedDate")).ToString("dd/MM/yyyy") %></p>
                                        </div>
                                    </div>
                                </div>

                                <asp:PlaceHolder runat="server" Visible='<%# Session["userId"].ToString() == Eval("UId").ToString() ||  Session["userId"].ToString() == Eval("PostUId").ToString()%>'>
                                    <asp:LinkButton runat="server" CssClass="rndclass mr-2" ID="lnbDown" OnClientClick="return confirm('Do You want to delete this comment?');" ForeColor="Red" CommandName="hideDelete" CommandArgument='<%# Eval("ComId") %>'><i class="fa-solid fa-xmark"></i></asp:LinkButton>
                                </asp:PlaceHolder>

                                <asp:HiddenField runat="server" ID="hdnUID" Value='<%# Eval("UId") %>' />
                                <asp:HiddenField runat="server" ID="hdnPostID" Value='<%# Eval("PostId") %>' />
                            </div>
                            <p style="margin: 0 60px; font-size: 20px"><%# Eval("Comment") %></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
        <div class="col-lg-3 ">
            <asp:Label runat="server" ID="lblMngAccess">Manage Access:</asp:Label>
            <asp:Repeater ID="rRequests" runat="server" OnItemDataBound="rRequests_ItemDataBound" OnItemCommand="rRequests_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-hover justify-content-center" style="text-align: center">
                        <thead>
                            <tr>
                                <th colspan="1"></th>
                                <th>User</th>
                                <th>Action</th>

                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>

                    <tr>
                        <td class="table-plus" colspan="1">
                            <img alt="" width="40px" height="40px" style="border-radius: 2px" src='../<%# Eval("ProfilePic") %>' />
                        </td>
                        <td class="text-align-center" style="text-align: center;"><%# Eval("Username") %></td>
                        <td>
                            <asp:LinkButton ID="lnkAccept" Text="Accept" runat="server" CssClass="badge badge-primary"
                                CommandArgument='<%# Eval("Id")%>' CommandName="accept" CausesValidation="false">
                                        <i class="ti-check"></i>
                            </asp:LinkButton>
                            <asp:HiddenField runat="server" ID="hdnStatus" Value='<%# Eval("Status") %>' />
                            <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" CommandName="delete"
                                CssClass="badge bg-danger" CommandArgument='<%# Eval("Id") %>'
                                OnClientClick="return confirm('Do You want to delete this category?');"
                                CausesValidation="false">
                                        <i class="ti-trash"></i>
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
    <br />
    <br />
</asp:Content>
