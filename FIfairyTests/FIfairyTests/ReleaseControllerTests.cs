using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FIfairy.Controllers;
using FIfairyDomain;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace FIfairyTests
{
    [TestFixture]
    public class ReleaseControllerTests
    {
        [Test]
        public void ShouldDisplayAllReleases()
        {
            // TODO: tests about different types of data should happen in the repository
            // like different teams and different dates
            IEnumerable<Release> expectedReleaseModel = new List<Release>
                                                            {
                                                                new Release("Enzo", "REL1216",
                                                                            new DateTime(2011, 10, 20)),
                                                            };

            var releaseRepository = new Mock<IReleaseRepository>();
            releaseRepository.Setup(x => x.GetReleases()).Returns(expectedReleaseModel);
            var releaseController = new ReleaseController(releaseRepository.Object);
            ViewResult result = releaseController.Index();

            var model = (IEnumerable<Release>) result.ViewData.Model;

            Assert.That(result.ViewName, Is.EqualTo("Release"));
            Assert.That(model, Is.EqualTo(expectedReleaseModel));
        }

        [Test]
        public void ShouldDisplayReleasesNewerThanDate()
        {
            // TODO: Should this be DisplayReleasesForTheLastThreeMonths?


            //given
            DateTime dateTo = new DateTime(2012, 01, 21);
            IEnumerable<Release> expectedReleases = new List<Release>
                                                        {new Release("Enzo", "REL1216", dateTo.AddDays(1)),};

            //when
            var releaseRepository = new Mock<IReleaseRepository>();
            releaseRepository.Setup(x => x.GetReleases(dateTo)).Returns(expectedReleases);

            var releaseController = new ReleaseController(releaseRepository.Object);
            ViewResult result = releaseController.ReleaseByDate(dateTo.Year, dateTo.Month, dateTo.Day);
            var model = (IEnumerable<Release>) result.ViewData.Model;

            //then
            Assert.That(result.ViewName, Is.EqualTo("Release"));
            Assert.That(model, Is.EqualTo(expectedReleases));
        }

        [Test]
        public void ShouldDisplayReleaseDetails()
        {
            //given

            string releaseNumber = "REL1216";

            var expectedReleaseDetailsModel = new Release
                                                  {
                                                      ReleaseNumber = releaseNumber,
                                                      ReleaseFiInstructions = "FI as Normal",
                                                      TeamName = "ENZO", PrePatEmailFileInfo = new PrePatEmailFileInfo(){Length=123, Name="prepatemailfile"},                                                      
                                                      ServiceNowTicketLink = "www.google.co.uk"
                                                  };

            var releaseRepository = new Mock<IReleaseRepository>();
            releaseRepository.Setup(x => x.GetReleaseDetails(releaseNumber)).Returns(expectedReleaseDetailsModel);
            var releaseDetailsController = new ReleaseDetailsController(releaseRepository.Object);

            //when            
            ViewResult result = releaseDetailsController.Index(releaseNumber);
            var model = (Release) result.ViewData.Model;

            //then
            Assert.That(result.ViewName, Is.EqualTo("ReleaseDetails"));
            Assert.That(model, Is.EqualTo(expectedReleaseDetailsModel));
        }

        [Test]
        public void ShouldBeAbleToCreateRelease()
        {
            //given
            var releaseRepository = new Mock<IReleaseRepository>();
            var releaseController = new ReleaseController(releaseRepository.Object);

            //when
            var release = new Release();
            var redirectToRouteResult = (RedirectToRouteResult) releaseController.Create(release, null);

            //then
            releaseRepository.Verify(x => x.SaveReleaseDetails(release), Times.Once());
            Assert.That(redirectToRouteResult.RouteValues["Controller"], Is.EqualTo("Release"));
            Assert.That(redirectToRouteResult.RouteValues["Action"], Is.EqualTo("Index"));
        }

        [Test]
        public void ShouldCreateReleaseWithoutPrePatEmail()
        {
            //given


            var releaseRepository = new Mock<IReleaseRepository>();
            var releaseController = new ReleaseController(releaseRepository.Object);

            var releaseDetailsModel = new Release()
                                          {
                                              ReleaseNumber = "REL1216",
                                              ReleaseFiInstructions = "FI as Normal",
                                              TeamName = "ENZO",                                              
                                              ServiceNowTicketLink = "www.google.co.uk"
                                          };

            //when                        
            releaseController.Create(releaseDetailsModel, null);

            //then                        
            releaseRepository.Verify(x => x.SaveReleaseDetails(releaseDetailsModel));
        }

        [Test]
        public void ShouldCreateReleaseWithPrePatEmail()
        {
            //given
            var releaseRepository = new Mock<IReleaseRepository>();
            var releaseController = new ReleaseController(releaseRepository.Object);

            //We'll need mocks (fake) of Context, Request and a fake PostedFile
            var postedfile = new Mock<HttpPostedFileBase>();
            var release = new Release();

            //Someone is going to ask for Request.File and we'll need a mock (fake) of that.
            string expectedFileName = "Enzo Pre-PAT release 19122011 ref REL11125.0.00";
            string expectedFileTypeExtension = ".msg";
            string savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                String.Concat(expectedFileName, expectedFileTypeExtension));
            Stream expectedStream = new MemoryStream(Encoding.ASCII.GetBytes("test"));

            postedfile.Setup(f => f.ContentLength).Returns(8192);
            postedfile.Setup(f => f.FileName).Returns(String.Concat(expectedFileName, expectedFileTypeExtension));

            releaseRepository.Setup(m => m.SaveReleaseDetails(It.IsAny<Release>()));


            postedfile.Setup(x => x.InputStream).Returns(expectedStream);

            //when
            releaseController.Create(release, postedfile.Object);

            //Then            
            releaseRepository.Verify(x => x.SaveReleaseDetails(It.IsAny<Release>()), Times.Once());

            releaseRepository.Verify(
                x => x.SavePrePatEmailFile(expectedFileName + expectedFileTypeExtension, expectedStream), Times.Once());

            Assert.That(release.PrePatEmailFileInfo.Name, Is.EqualTo(expectedFileName + expectedFileTypeExtension));
            Assert.That(release.PrePatEmailFileInfo.Length, Is.EqualTo(8192));
        }

        [Test]
        public void ShouldDownloadPrePatEmailFile()
        {
            //given
            var releaseRepository = new Mock<IReleaseRepository>();
            var release = new ReleaseDetailsController(releaseRepository.Object);
            string expectedFileName = "Enzo Pre-PAT release 19122011 ref REL11125.0.00";


            MemoryStream expectedStream = new MemoryStream(Encoding.ASCII.GetBytes("test"));
            releaseRepository.Setup(x => x.GetPrePatEmailFile(expectedFileName)).Returns(expectedStream);

            //when            
            FileStreamResult fileResult = (FileStreamResult) release.DownloadPrePatEmailFile(expectedFileName);

            Assert.That(fileResult.FileStream, Is.EqualTo(expectedStream));
            Assert.That(fileResult.ContentType, Is.EqualTo("application/vnd.ms-outlook"));
            Assert.That(fileResult.FileDownloadName, Is.EqualTo(expectedFileName));
        }

        [Test]
        public void ShouldDisplayLastFiveReleasesOrderedByDate()
        {
            //given
            IEnumerable<Release> expectedReleases = new List<Release>
                                                        {                                                            
                                                            new Release("Enzo", "REL4",DateTime.Today.AddMonths(-1).AddDays(2)),
                                                            new Release("Enzo", "REL5",DateTime.Today.AddMonths(-1).AddDays(1)),
                                                            new Release("Bob", "REL6", DateTime.Today.AddMonths(-1)),
                                                            new Release("Enzo", "REL1", DateTime.Today.AddMonths(-2)),
                                                            new Release("Phoenix", "REL2", DateTime.Today.AddMonths(-2)),
                                                            new Release("Fire", "REL3", DateTime.Today.AddMonths(-2))
                                                        };

            //when
            var releaseRepository = new Mock<IReleaseRepository>();
            releaseRepository.Setup(x => x.GetLastFiveReleases()).Returns(expectedReleases);

            var dashboardController = new ReleaseController(releaseRepository.Object);
            ViewResult result = dashboardController.LastFiveReleases();
            var model = (IEnumerable<Release>) result.ViewData.Model;

            //then
            Assert.That(result.ViewName, Is.EqualTo("ReleaseSummary"));
            Assert.That(model, Is.EqualTo(expectedReleases));
        }
    }
}