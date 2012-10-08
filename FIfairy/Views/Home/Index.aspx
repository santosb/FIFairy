<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    FI Fairy Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <%=Html.ActionLink("Create Release", "Create", "Release") %>
    <%=Html.ActionLink("Get All Releases", "Index", "Release") %>
    <%=Html.ActionLink("Get Last three month releases", "ReleaseByDate", "Release", new { year = DateTime.Now.AddMonths(-3).Year, month = DateTime.Now.AddMonths(-3).Month, day = DateTime.Now.AddMonths(-3).Day }, null)%>    
</asp:Content>
