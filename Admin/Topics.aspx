<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Topics.aspx.cs" Inherits="WebApplication3.Admin.Topics" %>
<%@ Import Namespace="WebApplication3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        //For disappearing alert messages
        window.onload = function () {
            var seconds = 5;
            setTimeout(function (){
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
                $('#<%=imgCategory.ClientID%>').prop('src', e.target.result)
                    .width(200)
                    .height(200);
            };
            reader.readAsDataURL(input.files[0]);
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="pcoded-inner-content pt-0 kuch bhi">
        <div class="align-align-self-end">
            <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="main-body">
            <div class="page-wrapper">

                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header">
                                </div>
                                <div class="card-block">
                                    <div class="row">
                                        <div calss ="col-sm-6 col-md-4 col-lg-4">
                                            <h4 class="sub-title">Topics</h4>
                                            <div>
                                                <div class="form-group">
                                                    <label>Topic Name</label>
                                                    <div>
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"
                                                            placeholder="Enter Topic Name" required></asp:TextBox>
                                                        <asp:HiddenField ID="hdnID" runat="server" Value="0" />
                                                    </div>
                                                </div>
                                                <div class ="form-group">
                                                    <label>Background Image</label>
                                                    <div>
                                                        <asp:FileUpload ID="fuCategoryImage" runat="server" CssClass="form-control"
                                                            onchange="ImagePreview(this);"/>
                                                    </div>
                                                </div>
                                                <div class="form-check pl-4">
                                                    <asp:TextBox ID="txtDescription" placeholder="Description about the topic" CssClass="form-control" required runat="server"></asp:TextBox>
                                                </div>
                                                <div class="pb-5">
                                                    <asp:Button ID="btnAddOrUpdate" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddOrUpdate_Click"                                                        />
                                                    &nbsp;
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary"
                                                        CausesValidation="false" OnClick="btnClear_Click"/>
                                                </div>
                                                <div>
                                                    <asp:Image ID="imgCategory" runat="server" CssClass="img-thumbnail"/>
                                                </div>
                                            </div>
                                        </div> 

                                        <div class ="col-sm-6 col-md-8 col-lg-8 mobile-inputs">
                                            <h4 class="sub-title">Topic Lists</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="rTopics" runat="server" OnItemCommand="rTopics_ItemCommand" OnItemDataBound="rTopics_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                <tr>
                                                                    <th class="table-plus">Name</th>
                                                                    <th>Image</th>
                                                                    <th>Description</th>
                                                                    <th>Total Posts</th>
                                                                    <th>CreatedDate</th>
                                                                    <th class="datatable-nosort">Action</th>
                                                                </tr>
                                                            </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"> <%# Eval("TName") %> </td>
                                                                <td><img alt="" width="40" src="<%# Utils.GetImageUrl(Eval("BackImage")) %>" /></td>
                                                                <th><%# Eval("Description") %></th>
                                                                
                                                                <td><%# Eval("countPosts") %></td>
                                                                <td><%# Eval("CreatedDate") %></td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" CssClass="badge badge-primary"
                                                                        CommandArgument ='<%# Eval("TopicId") %>' CommandName="edit">
                                                                        <i class="ti-pencil"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" CommandName="delete"
                                                                        CssClass="badge bg-danger" CommandArgument='<%# Eval("TopicId") %>'
                                                                        OnClientClick="return confirm('Do You want to delete this Topic?');" >
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
