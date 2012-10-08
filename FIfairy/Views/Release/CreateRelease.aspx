<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FIfairyDomain.Release>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CreateRelease</title>
</head>
<body>
    <% using (Html.BeginForm("Create", "Release", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
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
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</body>
</html>

