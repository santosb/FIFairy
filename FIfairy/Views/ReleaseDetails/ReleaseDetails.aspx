<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FIfairyDomain.Release>" %>
<%@ Import Namespace="FIfairyDomain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Release</title>
</head>
<body>
    Release Number:<%: Model.ReleaseNumber %><br/>
    Team Name:<%= Model.TeamName %><br/>
    Release Date:<%= Model.ReleaseDate.ToShortDateString() %><br/>
    FI Instructions:<%: Model.ReleaseFiInstructions %><br/>    
    Pre Pat Email:<%= Model.PrePatEmailFileInfo.Name  %><br/>     
    <%=Html.ActionLink("Download", "DownloadPrePatEmailFile", "ReleaseDetails", new { filename = Model.PrePatEmailFileInfo.Name }, null)%><br/>  
    ServiceNow Ticket:<a href="<%= Model.ServiceNowTicketLink %>" id="snticket">Link</a>
    <p>        
        <%: Html.ActionLink("Back to List", "Index", "Release", null, null)%>               
    </p>

</body>
</html>

