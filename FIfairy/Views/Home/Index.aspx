<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FIfairyDomain" %>

<asp:Content ID="cHeader" ContentPlaceHolderID="HeaderContent" runat="server" >
    <link href="~/Content/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css"/>
    <link href="~/Content/themes/base/jquery.ui.base.css" rel="stylesheet" type="text/css"/>    
    <link href="~/Content/themes/base/jquery.ui.theme.css" rel="stylesheet" type="text/css"/>
    <link href="~/Content/themes/base/jquery.ui.button.css" rel="stylesheet" type="text/css"/>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css"/>
    <link href="../../Content/release.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-ui-1.8.18.min.js"></script>    
    <script type="text/javascript" src="/Scripts/jquery.ui.button.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".fibutton").button();
        });
    </script>
</asp:Content>
<asp:Content ID="cTitle" ContentPlaceHolderID="TitleContent" runat="server">
    FI Fairy Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">       
    <%=Html.ActionLink("Create Release", "Create", "Release", new {},new { @class = "fibutton" })%>
    <%=Html.ActionLink("Get All Releases", "Index", "Release", new {} ,new { @class = "fibutton" })%>
    <%=Html.ActionLink("Get Last three month releases", "ReleaseByDate", "Release", new { year = DateTime.Now.AddMonths(-3).Year, month = DateTime.Now.AddMonths(-3).Month, day = DateTime.Now.AddMonths(-3).Day }, new { @class = "fibutton" })%>    

<div class="releases">
    <% Html.RenderAction("LastFiveReleases", "Release"); %>
</div>
</asp:Content>


