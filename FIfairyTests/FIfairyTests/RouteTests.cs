using System.Web;
using System.Web.Routing;
using FIfairy;
using Moq;
using NUnit.Framework;

namespace FIfairyTests
{
    [TestFixture]
    internal class RouteTests
    {
        private HttpContextBase CreateHttpContext(string targetUrl = null,
                                                  string httpMethod = "GET")
        {
            // create the mock request
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);
            // create the mock response
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(
                It.IsAny<string>())).Returns<string>(s => s);
            // create the mock context, using the request and response
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);
            // return the mocked context
            return mockContext.Object;
        }

        [Test]
        public void ShouldRouteReleaseByDate()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var route = routes.GetRouteData(CreateHttpContext("~/ReleasesByDate/2011/12/23"));

            Assert.That(route, Is.Not.Null);
            Assert.That(route.Values["controller"], Is.EqualTo("Release"));
            Assert.That(route.Values["action"], Is.EqualTo("ReleaseByDate"));
            Assert.That(route.Values["year"], Is.EqualTo("2011"));
            Assert.That(route.Values["month"], Is.EqualTo("12"));
            Assert.That(route.Values["day"], Is.EqualTo("23"));
        }
    }
}