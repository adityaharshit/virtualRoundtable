<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Food_Ordering_Website.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Report</h2>
    <br />
    
        <%--<ItemTemplate>

            <div class="row">
                <div class="col-sm-3 colmd-2 col-5">
                    <label style="font-weight: bold;">Total Registered Users</label>
                </div>
                <div class="col-md-8 col-6">
                    <%Response.Write(Session["totUsers"]); %>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-sm-3 colmd-2 col-5">
                    <label style="font-weight: bold;">Total Staff</label>
                </div>
                <div class="col-md-8 col-6">
                    <%Response.Write(Session["totStaff"]); %>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-sm-3 colmd-2 col-5">
                    <label style="font-weight: bold;">Total Orders Placed</label>
                </div>
                <div class="col-md-8 col-6">
                    <%Response.Write(Session["totOrders"]); %>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-sm-3 colmd-2 col-5">
                    <label style="font-weight: bold;">Orders Placed Today</label>
                </div>
                <div class="col-md-8 col-6">
                    <%Response.Write(Session["ordersToday"]); %>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-sm-3 colmd-2 col-5">
                    <label style="font-weight: bold;">Pending Orders</label>
                </div>
                <div class="col-md-8 col-6">
                    <%Response.Write(Session["penOrders"]); %>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-sm-3 colmd-2 col-5">
                    <label style="font-weight: bold;">Orders Delivered Today</label>
                </div>
                <div class="col-md-8 col-6">
                    <%Response.Write(Session["ordDelToday"]); %>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-sm-3 colmd-2 col-5">
                    <label style="font-weight: bold;">Total Income</label>
                </div>
                <div class="col-md-8 col-6">
                    <%Response.Write(Session["totIncome"]); %>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-sm-3 colmd-2 col-5">
                    <label style="font-weight: bold;">Total Categories</label>
                </div>
                <div class="col-md-8 col-6">
                    <%Response.Write(Session["totCategories"]); %>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-sm-3 colmd-2 col-5">
                    <label style="font-weight: bold;">Total Products</label>
                </div>
                <div class="col-md-8 col-6">
                    <%Response.Write(Session["totProducts"]); %>
                </div>
            </div>
            <hr />
        </ItemTemplate>--%>
    
    <%--<asp:Label ID="lbluser" style="font-weight:bold;" runat="server" Text="Total Registered User"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblStaff" style="font-weight:bold;" runat="server" Text="Total Staff" ></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblOrders" style="font-weight:bold;" runat="server" Text="Total Orders Placed"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblToOrder" style="font-weight:bold;" runat="server" Text="Orders Placed Today"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblPenOrders" style="font-weight:bold;" runat="server" Text="Pending Orders"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblToDel" style="font-weight:bold;" runat="server" Text="Orders Delivered Today"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblIncome" style="font-weight:bold;" runat="server" Text="Total Income"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblCategories" style="font-weight:bold;" runat="server" Text="Total Categories"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblProducts" style="font-weight:bold;" runat="server" Text="Total Products"></asp:Label>--%>
</asp:Content>
