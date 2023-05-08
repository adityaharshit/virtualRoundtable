<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="forumask.aspx.cs" Inherits="WebApplication3.Users.forumask" %>

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
    <%--Show Image Preview --%>

    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPost.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="heading_container">
            <div class="align-self-end">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <h1 class="card-title">Create a new Discussion</h1>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="form_container">
                    <br />
                    <br />
                    <div>
                        <h2>Title</h2>
                        <asp:Label ID="lblTitle" CssClass="col-form-label-sm" runat="server" Text="Be Specific and imagine you're asking a question to another person"></asp:Label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" PlaceHolder="e.g. Is there an R function for finding the index of an element in a vector?"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Title is required"
                            ControlToValidate="txtTitle" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                    <br />
                    <br />
                    <div>
                        <h2>Description</h2>
                        <asp:Label ID="lblDescription" CssClass="col-form-label-sm" runat="server" Text="Introduce the problem and expand on what you put in the title. Minimum 20 characters."></asp:Label>
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" Rows="4" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDesc" runat="server" ErrorMessage="Description is required"
                            ControlToValidate="txtDesc" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                    <br />
                    <br />
                    <div class="form-group">
                        <h2>Image</h2>
                        <asp:FileUpload ID="imagePost" runat="server" CssClass="form-control" onchange="ImagePreview(this);" />
                    </div>
                    <div>
                        <asp:Image ID="imgPost" runat="server" CssClass="img-thumbnail" />
                    </div>
                    <br />
                    <br />
                    <div class="form-group">
                        <asp:Label ID="lblTopic" CssClass="col-form-label-lg" runat="server" Text="Select Topic"></asp:Label>
                        <asp:DropDownList CssClass="form-control" ID="ddlTopic" runat="server" DataSourceID="SqlDataSource1" DataTextField="TName" DataValueField="TopicId" AutoPostBack="true">
                            <asp:ListItem Text="Select Topic" Enabled="true" Value="0" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Database1 %>" SelectCommand="SELECT [TopicId], [TName] FROM [Topic]"></asp:SqlDataSource>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSubTopic" CssClass="col-form-label-lg" runat="server" Text="Select Sub-Topic"></asp:Label>
                        <asp:DropDownList ID="ddlSubTopic" CssClass="form-control" runat="server" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="SubTId" AutoPostBack="true">
                            <asp:ListItem Text="Select Sub-Topic" Enabled="true" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Database1 %>" SelectCommand="SELECT [SubTId], [Name] FROM [SubTopic] WHERE ([TopicId] = @TopicId)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlTopic" Name="TopicId" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>

                    <%--<div class="form-group">
                               <asp:Label ID="lblCategory" CssClass="col-form-label-lg" runat="server" Text="Select Category"></asp:Label>
                                <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" DataSourceID="SqlDataSource3" DataTextField="Name" DataValueField="CatId" AutoPostBack="true">
                                    <asp:ListItem Text="Select Category" Enabled="false"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Database1 %>" SelectCommand="SELECT [CatId], [Name] FROM [Categories] WHERE ([SubTId] = @SubTId)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlSubTopic" Name="SubTId" PropertyName="SelectedValue" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                           </div>--%>
                    <br />
                    <br />
                    <div class="form-group">
                        <h2>Tags</h2>
                        <asp:Label ID="lblTags" CssClass="col-form-label-sm" runat="server" Text="Add comma separated tags."></asp:Label>
                        <asp:TextBox ID="txtTags" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:CheckBox ID="chkIsPrivate" runat="server" Text="Private Topic?" />
                    </div>
                </div>
                <br />
                <br />

                <%--<div class="col-lg-3">
                            <div class="form-group">
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                            </div>
                        </div>--%>
                <div class="btn-box">
                    <asp:Button ID="btnAsk" runat="server" Text="Ask" Width="100px" CssClass=" btn btn-primary pl-4 pr-4 text-white" OnClick="btnAsk_Click" />
                </div>
                <br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
