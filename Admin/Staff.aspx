<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="Food_Ordering_Website.Admin.Staff" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pcoded-inner-content pt-0">
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
                                         

                                        <div class ="col-sm-6 col-md-8 col-lg-12 mobile-inputs">
                                            <h4 class="sub-title">Staff Lists</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="rStaff" runat="server" OnItemCommand="rStaff_ItemCommand" OnItemDataBound="rStaff_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                <tr>
                                                                    <th class="table-plus">Name</th>
                                                                    <th>Username</th>
                                                                    <th>Email</th>
                                                                    <th>Status</th>
                                                                    <th>Pincode</th>
                                                                    <th>Joined on</th>
                                                                    <th class="datatable-nosort">Action</th>
                                                                </tr>
                                                            </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"> <%# Eval("Name") %> </td>
                                                                <td><%# Eval("Username") %></td>
                                                                <td><%# Eval("Email") %></td>
                                                                <td><asp:Label ID="lbStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label></td>
                                                                <td><%# Eval("Pincode") %></td>
                                                                <td><%# Eval("JoinDate") %></td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" CommandName="delete"
                                                                        CssClass="badge bg-danger" CommandArgument='<%# Eval("DId") %>'
                                                                        OnClientClick="return confirm('Do You want to delete this User?');" >
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
                                                    <div>
                                        <asp:Button ID="btnAddNewStaff" runat="server" Text="Register New Staff"  CssClass="btn btn-primary" OnClick="btnAddNewStaff_Click"/>
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
    </div>
</asp:Content>
