<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <%=Html.ActionLink("Get All Releases", "Index", "Release") %>
    <%=Html.ActionLink("Get Last three month releases", "Indexes", "Release", new { dateFrom = DateTime.Now.AddMonths(-3), dateTo = DateTime.Now }, null) %>    
</asp:Content>
