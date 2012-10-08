<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<FIfairyDomain.Release>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link href="../../Content/release.css" rel="stylesheet" type="text/css" />
        <link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
        <title>Releases Page</title>
    </head>
    <body class="releaseswrap">
        <div class="releases">
            <% Html.RenderPartial("ReleaseSummary", Model); %>
        </div>        
    </body>
</html>