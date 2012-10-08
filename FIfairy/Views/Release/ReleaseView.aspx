<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<FIfairyDomain.IReleaseModel>>" %>
<%@ Import Namespace="FIfairyDomain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Releases Page</title>
    </head>
    <body>
        <ul>
            <% foreach (IReleaseModel releases in Model)
               { %>
                <li>Team: <%= releases.TeamName %></li>  
                <ul>
                     
                    <% foreach (string release in releases.Releases)
                       { %>
                        <li><%= release %></li>
                    <% } %>         
               
                </ul>    
            <% } %>
        </ul>
    </body>
</html>