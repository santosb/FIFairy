<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<FIfairyDomain.IReleaseModel>>" %>
<%@ Import Namespace="FIfairyDomain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Releases Page</title>
    </head>
    <body>        
            <% foreach (IReleaseModel release in Model)
               { %>
                <div>
                    Team: <%= release.TeamName %> 
                    RelNumber: <%= release.ReleaseNumber %>
                    Date:
                </div>                  
            <% } %>       
    </body>
</html>