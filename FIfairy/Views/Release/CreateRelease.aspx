<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FIfairyDomain.Release>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CreateRelease</title>
    <link href="~/Content/themes/base/jquery.ui.base.css" rel="stylesheet" type="text/css"/>
    <link href="~/Content/themes/base/jquery.ui.datepicker.css" rel="stylesheet" type="text/css"/>
    <link href="~/Content/themes/base/jquery.ui.theme.css" rel="stylesheet" type="text/css"/>
    <link href="~/Content/themes/base/jquery.ui.button.css" rel="stylesheet" type="text/css"/>
    
<%--    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>--%>
    
    <script type="text/javascript" src="/Scripts/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-ui-1.8.18.min.js"></script>    
  
  <script type="text/javascript" >
      $(function() {
        $("#datepicker").datepicker({ beforeShowDay: $.datepicker.noWeekends });          
        $("#datepicker").datepicker("option", "dateFormat", "dd/mm/yy");          
      });
  </script>
  
</head>
<body>
    <% using (Html.BeginForm("Create", "Release", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
        <%: Html.ValidationSummary(true) %>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ReleaseNumber) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ReleaseNumber) %>
                <%: Html.ValidationMessageFor(model => model.ReleaseNumber) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ReleaseFiInstructions) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ReleaseFiInstructions) %>
                <%: Html.ValidationMessageFor(model => model.ReleaseFiInstructions) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.TeamName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.TeamName) %>
                <%: Html.ValidationMessageFor(model => model.TeamName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.PrePatEmail) %>                
            </div>
            <div class="editor-field">                
                <input type="file" name="prepatfile"/>                             
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ServiceNowTicketLink) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ServiceNowTicketLink) %>
                <%: Html.ValidationMessageFor(model => model.ServiceNowTicketLink) %>
            </div>
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ReleaseDate) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ReleaseDate, new { @id = "datepicker" })%>
                <%: Html.ValidationMessageFor(model => model.ServiceNowTicketLink) %>
            </div>                                    
            <p>
                <input type="submit" value="Create" />
            </p>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index", "Home", null, null) %>
    </div>

</body>
</html>

