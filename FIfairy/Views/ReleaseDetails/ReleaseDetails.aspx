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
    <div class="editor-label">
        <label>FI Instructions:</label>
    </div>
    <div class="editor-field">
        <%= HttpUtility.HtmlDecode(Html.DisplayTextFor(model => model.ReleaseFiInstructions).ToHtmlString())%><br/>             
    </div>
    <% if(Model.PrePatEmailFileInfo!=null) { %>
        Pre Pat Email:<%= Model.PrePatEmailFileInfo.Name  %><br/>     
        <%=Html.ActionLink("Download", "DownloadPrePatEmailFile", "ReleaseDetails", new { filename = Model.PrePatEmailFileInfo.Name }, null)%><br/>         
    <% } %>
    ServiceNow Ticket:<a href="<%= Model.ServiceNowTicketLink %>" id="snticket">Link</a>
    <p>        
        <%: Html.ActionLink("Back to List", "Index", "Release", null, null)%>               
    </p>

</body>
</html>

