<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<FIfairyDomain.Release>>" %>
<h2>Releases</h2>
<% foreach (var release in Model)
               { %>
                <div class="release-item">
                    <h3>Team: <%= release.TeamName %></h3>
                    <p>RelNumber: <%= release.ReleaseNumber %></p>
                    <p>Date: <%= release.ReleaseDate%></p>
                    <p><%=Html.ActionLink("Details", "Index", "ReleaseDetails", new { releaseNumber = release.ReleaseNumber }, null)%></p>
                </div>                  
<% } %>       