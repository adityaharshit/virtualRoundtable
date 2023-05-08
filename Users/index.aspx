<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication3.Users.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="header">

      <div class="cont">
        <div class="header-text">
          <h1>The Virtual <span class="span-text">Roundtable</span></h1>
          <h5>A Place to share ideas and perspective</h5>
        </div>
       
      </div>
    </div>

    <section id="feature">
      <h1 class="feature-head">Striking Features of Our Platform</h1>
      <div class="row justify-content-center">
        <div class="col-lg-3 col-md-4 col-sm-8 features-box">
          <i class="fa-sharp fa-solid fa-graduation-cap fa-5x"></i>
          <p class="feature-title">Learn & contribute</p>
          <p class="feature-description">
            A place for you to learn from others, explore new ideas, and expand
            your knowledge
          </p>
        </div>
        <div class="col-lg-3 col-md-4 col-sm-8 features-box">
          <i class="fa-sharp fa-solid fa-universal-access fa-5x"></i>
          <p class="feature-title">Safe & Secure Environment</p>
          <p class="feature-description">
            We prioritize your safety and provide a platform that is free from
            harassment, bullying, and hate speech.
          </p>
        </div>
        <div class="col-lg-3 col-md-4 col-sm-8 features-box">
          <i class="fa-solid fa-lock fa-5x"></i>
          <p class="feature-title">Private Discussions</p>
          <p class="feature-description">
            Organise private discussions and gain control over who can join your
            discussions.
          </p>
        </div>
        
      </div>
    </section>

    <hr class="index-hr"/>

    <div id="service">
      <h1 class="feature-head">Our Communities</h1>
      <div class="services">
          <asp:Repeater ID="rTopics" runat="server">
              <ItemTemplate>
                   <div class="services-box">
          <i class="fa-solid fa-code fa-3x"></i>
          <h2><%# Eval("TName") %></h2>
          <p>
            <%# Eval("Description") %>
          </p>
          <button class="about-button"><a  style="text-decoration:none; color:white;" href="cars.aspx?id=<%# Eval("TopicId") %>">Go</a></button>
        </div>
              </ItemTemplate>
          </asp:Repeater>
       
      </div>
    </div>

    

    
</asp:Content>
