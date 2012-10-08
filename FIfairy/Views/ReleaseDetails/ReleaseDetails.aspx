<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FIfairyDomain.ReleaseDetailsModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ReleaseDetails</title>
</head>
<body>
    Release Number:<%: Model.ReleaseNumber %><br/>
    FI Instructions:<%: Model.ReleaseFiInstructions %><br/>
    Team Name:<%= Model.TeamName %><br/>
    Pre Pat Email:<%= Model.PrePatEmail %><br/>
    ServiceNow Ticket:<a href="<%= Model.ServiceNowTicketLink %>" id="snticket">Link</a>
    <p>        
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</body>
</html>

