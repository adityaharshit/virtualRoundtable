<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="forum.aspx.cs" Inherits="WebApplication3.Users.forum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="forum-main-content">
<!-- -------------------------------------------------------------------------- LEFT---------------------------------------------------------------------- -->
      <div class="forum-leftnav">
        <div class="forum-leftnav-topics">
          <div class="forum-left-headings">
            <!-- <i class="fa-solid fa-list-check fa-2x"></i> -->
            <h3>TOPICS</h3>
          </div>
          <ul class="forum-left-topics-list">
              <asp:Repeater ID="rTopics" runat="server">
                  <ItemTemplate>

            <li>
              <i class="fa-solid fa-gamepad fa-xl"></i><a href="forum.aspx?id=<%# Eval("TopicId") %>"><%# Eval("TName") %></a>
            </li>
                  </ItemTemplate>
              </asp:Repeater>
          </ul>
        </div>
        <div class="forum-leftnav-subs">
          <div class="forum-left-headings">
            <!-- <i class="fa-regular fa-chart-bar fa-2x"></i> -->
            <h3>SUB-TOPICS</h3>
          </div>
          <ul class="forum-left-subt-list" id="SubtopicUl">
                            <asp:Repeater ID="rSubTopic" runat="server" OnItemCommand="rSubTopic_ItemCommand" ><ItemTemplate>
                                <li>
              <a href="forum.aspx?id=<%# Eval("TopicId") %>&sbid=<%# Eval("SubTId") %>" id="subta" ><%# Eval("Name") %></a>
                                     <asp:LinkButton ID="lnkDelete" runat="server" CommandName="faclick" CssClass="hidden"
                                                                        CommandArgument='<%# Eval("SubTId") %>' PostBackUrl="#"
                                                                        CausesValidation="false">
                                                                            
              <i class="fa-solid fa-chevron-down forum-left-fa" runat="server" ></i>
                                                                    </asp:LinkButton>
            </li>
                                <div class="forum-left-subt-list-items hidden">
            </div>
                                                                        </ItemTemplate></asp:Repeater>
              
            
          </ul>
        </div>
      </div>
<!-- -------------------------------------------------------------------------- CENTER ---------------------------------------------------------------------- -->
      <div class="forum-center">
        <div class="forum-ask-post">
          <div class="forum-ask-img-text">
            <img class="forum-nav-profile" src="../assets/images/profile.png" />
            <input
              type="text"
              class="forum-ask-textbox"
              placeholder="Just a random space"
            />
          </div>
          <div class="forum-ask-question">

            <div class="forum-ask-divs">
              <i class="fa-regular fa-circle-question"></i>
              <h3><a href="forumask.aspx" style="text-decoration:none;color:#4e4e52;">Ask</a></h3>
            </div>
          </div>
        </div>
        <div class="background-blur"></div>
        <div class="forum-ask">
          
        </div>
          <asp:Repeater ID="rPosts" runat="server" OnItemCommand="rPosts_ItemCommand" OnItemDataBound="rPosts_ItemDataBound">
              <ItemTemplate>

              
        <div class="forum-post">
          <div class="forum-post-userInfo">
            <div class="forum-post-userInfo-part1">
              <img class="forum-nav-profile" style="height: 50px; width: 50px" src="../<%# Eval("ProfilePic") %>" />
              <div class="forum-post-userInfo-middle">
                <div class="forum-post-userInfo-nf">
                  <h4><%# Eval("UserName") %></h4>
                    <asp:LinkButton CssClass="forum-post-userInfo-nf-follow" ID="lnkFollow" runat="server" CommandArgument='<%# Eval("UId") %>' CausesValidation="false" CommandName="Follow" Text="Follow"></asp:LinkButton>
                    <asp:HiddenField ID="hdnUId" runat="server" Value='<%# Eval("UId") %>'/>
                    <asp:HiddenField ID="hdnPostId" runat="server" Value='<%# Eval("PostId") %>'/>
                    <asp:HiddenField ID="hdnDesc" runat="server" Value='<%# Eval("PostDescription") %>'/>
                    <asp:HiddenField ID="hdnBanned" runat="server" Value='<%# Eval("Banned") %>'/>
                    <asp:HiddenField ID="hdnUBanned" runat="server" Value='<%# Eval("UBanned") %>'/>

                </div>
                  <div>

                    <p class="forum-post-userInfo-date" style="display: inline-block;">17/02/2023</p>
                      <asp:Label ID="lblNegative" runat="server"></asp:Label>
                    <%--<asp:Label CssClass="pill rounded-pill" ID="lblPrivate" runat="server"><%# Eval("isPrivate") %></asp:Label>--%>
                      </div>
              </div>
            </div>
              <asp:LinkButton runat="server" CssClass=" mr-2" OnClientClick="return confirm('Do You want to delete this?');"
                  ID="lnbDown" ForeColor="Red" CommandName="hideDelete" CommandArgument='<%# Eval("UId") + "," +Eval("PostId") %>'><i class="fa-solid fa-xmark"></i></asp:LinkButton>
            <%--<i class="fa-solid fa-xmark"></i>--%>
          </div>
          <div class="forum-post-content">
            <h3 class="forum-post-topic"><%# Eval("PostTitle") %></h3>
            <p class="forum-post-description"><%# Eval("PostDescription") %></p>

            <img class="forum-post-image" src="../<%# Eval("PostImage") %>" alt="">
          </div>
          <div class="forum-post-bottom ">
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
                  <asp:LinkButton ID="lnbReportPost"   runat="server" CommandArgument='<%# Eval("PostId") %>' CausesValidation="false" CommandName="ReportPost" Text="Report this post">
                  </asp:LinkButton>
              </div>
              <hr>
              <div class="forum-post-bottom-menu-content">

                <i class="fa-regular fa-flag"></i>
                  <asp:LinkButton ID="lnbReportUser"   runat="server" CommandArgument='<%# Eval("UId") %>' CausesValidation="false" CommandName="ReportUser" Text="Report this user"></asp:LinkButton>
                
              </div>
                <hr>
                <div class="forum-post-bottom-menu-content">
                  <i class="fa-regular fa-bookmark"></i>
                    <asp:LinkButton ID="lnbSave"   runat="server" CommandArgument='<%# Eval("PostId") %>' CausesValidation="false" CommandName="Save" Text="Save"></asp:LinkButton>
                  
                </div>
              
              <hr>
              <div class="forum-post-bottom-menu-content">
                <i class="fa-solid fa-ban"></i>
                  <asp:LinkButton ID="lnbBlockUser"   runat="server" CommandArgument='<%# Eval("UId") %>' CausesValidation="false" CommandName="BlockUser" Text="Block this user"></asp:LinkButton>
                
              </div>
            </div>
          </div>
        </div>
                  </ItemTemplate>
          </asp:Repeater>

        </div>

<!-- -------------------------------------------------------------------------- RIGHT ---------------------------------------------------------------------- -->

      <div class="forum-rightnav">
        <div class="forum-left-headings">
          <h3>FILTER</h3>
        </div>
        <div class="forum-right-fields">
          <ul class="forum-right-fields-ul ">
            <%--<li class="forum-right-active"><a href="#">New</a></li>--%>
            <li class="forum-right-active"><asp:LinkButton runat="server" ID="lnbNew" OnClick="lnbNew_Click" Text="New"></asp:LinkButton></li>
            <li><asp:LinkButton runat="server" ID="lnbHits" OnClick="lnbHits_Click" Text="Hits"></asp:LinkButton></li>
            <li><asp:LinkButton runat="server" ID="lnbRandom" OnClick="lnbRandom_Click" Text="Random"></asp:LinkButton></li>
            <li><asp:LinkButton runat="server" ID="lnbFollowing" OnClick="lnbFollowing_Click" Text="Following"></asp:LinkButton></li>
          </ul>
         
        </div>
        <div class="forum-right-tags-cont">
          <div class="forum-left-headings">
            <h3>TAGS<span class="colon"> :</span></h3>      
          </div>
          <div class="forum-right-tags">
              <asp:Repeater ID="rTags" runat="server" OnItemCommand="rTags_ItemCommand">
                  <ItemTemplate>
                    <p><asp:LinkButton runat="server" ID="lnkTags" CommandArgument='<%# Eval("TagName") %>' CommandName="clkTags"><%# Eval("TagName") %></asp:LinkButton></p>
                  </ItemTemplate>
              </asp:Repeater>
          </div>
        </div>
      </div>
    </div>
    
</asp:Content>
