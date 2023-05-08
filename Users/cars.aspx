<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="cars.aspx.cs" Inherits="WebApplication3.Users.cars" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="programHeader" runat="server">
      <%--<div id="ind-nav"></div>--%>
      <div class="pg-header header-text">
        <h1><% Response.Write(Session["Topic"]); %></h1>
      </div>
    </div>
    <div class="second-div">
      <h1 class="second-heading">What do you want to talk about?</h1>
      <div class="card-horizontal hidden">
        <button class="arrow-button left-arrow">
          <i class="fa-solid fa-chevron-left fa-2x"></i>
        </button>
        <div class="cards-scroll">
        </div>
        <button class="arrow-button right-arrow">
          <i class="fa-solid fa-chevron-right fa-2x"></i>
        </button>
      </div>
      <div class="cards">
          <asp:Repeater ID="rSubTopics" runat="server">
              <ItemTemplate>

          <div class="card1" style="<%# "background-image: url(../" + Eval("Image") + ");" %>">
              <div class="card1-content">	 
                  <h3 class="card1-heading"><%# Eval("Name") %></h3>
                  <p class="card1-body"><%# Eval("Description") %></p>
                  <a href="forum.aspx?id=<%# Request.QueryString["id"] %>&stid=<%# Eval("SubTId") %>" class="button">Go</a>
              </div>
            </div>
              </ItemTemplate>
          </asp:Repeater>
      </div>
      <div class="cards-models"></div>
      <div class="sub-cards"></div>
    </div>
</asp:Content>
