<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="Food_Ordering_Website.Admin.OrderHistory" %>
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
                                         

                                        <div class ="col-sm-6 col-md-12 col-lg-12 mobile-inputs">
                                            <h4 class="sub-title">Users Lists</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="rOrders" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                <tr>
                                                                    <th>Order No</th>
                                                                    <%--<th>Image</th>--%>
                                                                    <th>Ordered by</th>
                                                                    <th>Product</th>
                                                                    <th>Quantity</th>
                                                                    <th>Address</th>
                                                                    <th>Assigned To</th>
                                                                    <th>Status</th>
                                                                    <th>Payment Mode</th>
                                                                </tr>
                                                            </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"> <%# Eval("OrderNo") %> </td>
                                                                <%--<td><img alt="" width="40" src="<%# Utils.GetImageUrl(Eval("ImageUrl")) %>" /></td>--%>
                                                                <td><%# Eval("UserName") %></td>
                                                                <td><%# Eval("Product") %></td>
                                                                <td><%# Eval("Quantity") %></td>
                                                                <td><%# Eval("Address") %></td>
                                                                <td><%# Eval("DelMan") %></td>
                                                                <td><%# Eval("Status") %></td>
                                                                <td><%# Eval("Mode") %></td>
                                                               
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
