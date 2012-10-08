<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<FIfairyDomain.ReleaseDetailsModel>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Releases Page</title>
    </head>
    <body>        
            <% foreach (var release in Model)
               { %>
                <div>
                    Team: <%= release.TeamName %> 
                    RelNumber: <%= release.ReleaseNumber %>
                    Date: <%= release.Date%>
                    <%=Html.ActionLink("Details", "Index", "ReleaseDetails", new { releaseNumber = release.ReleaseNumber }, null)%>
                </div>                  
            <% } %>       
    </body>
</html>