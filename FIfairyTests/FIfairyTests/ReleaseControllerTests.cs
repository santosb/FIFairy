using System;
using System.Collections.Generic;
using System.IO;
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
        public void ShouldDisplayReleaseNumbersOfTheLastThreeMonths()
        {
            //given
            IEnumerable<Release> expectedReleaseModel = new List<Release>
                                                                  {
                                                                      new Release ("Enzo", "REL1216", new DateTime(2011,10,20)),
                                                                      new Release("Enzo", "REL54164", new DateTime(2011,10,20)),
                                                                      new Release("Colombo", "REL1000", new DateTime(2011,10,20))
                                                                  };
                                                                      
            //when
            var releaseRepository = new Mock<IReleaseRepository>();
            DateTime dateNow = new DateTime(2012,01,21);
            DateTime dateTo = dateNow.AddMonths(-3);                        

            releaseRepository.Setup(x => x.GetReleases(dateTo)).Returns(expectedReleaseModel);
            var releaseController = new ReleaseController(releaseRepository.Object);
            ViewResult result = releaseController.ReleaseByDate(dateTo.Year, dateTo.Month, dateTo.Day);
            var model = (IEnumerable<Release>)result.ViewData.Model;

            //then
            Assert.That(result.ViewName, Is.EqualTo("Release"));
            Assert.That(model, Is.EqualTo(expectedReleaseModel));
            Assert.That(model.First().TeamName, Is.EqualTo("Enzo"));
        }

        [Test]
        public void ShouldDisplayReleases()
        {
            IEnumerable<Release> expectedReleaseModel = new List<Release>
                       {
                           new Release("Enzo", "REL1216",  new DateTime(2011,10,20)),
                           new Release("Enzo", "REL54164", new DateTime(2011,11,20)),
                           new Release("Enzo", "REL123", new DateTime(2011,03,20)),
                           new Release("Enzo", "REL124", new DateTime(2011,02,27)),
                           new Release("Enzo", "REL125", new DateTime(2011,09,26)),
                           new Release("Colombo", "REL1000", new DateTime(2011,12,25)),
                           new Release("Colombo", "REL11122", new DateTime(2011,11,04))
                       };

            var releaseRepository = new Mock<IReleaseRepository>();
            releaseRepository.Setup(x => x.GetReleases()).Returns(expectedReleaseModel);
            var releaseController = new ReleaseController(releaseRepository.Object);
            ViewResult result = releaseController.Index();

            var model = (IEnumerable<Release>)result.ViewData.Model;

            Assert.That(result.ViewName, Is.EqualTo("Release"));
            Assert.That(model, Is.EqualTo(expectedReleaseModel));
            Assert.That(model.First().TeamName, Is.EqualTo("Enzo"));
        }

        [Test]
        public void ShouldDisplayReleaseDetails()
        {
            //given

            string teamName = "ENZO";
            string releaseNumber = "REL1216";

            string releaseFIInstructions = "FI as Normal";
            string prePatEmail = "we all love pre pat meetings";
            string serviceNowTicketLink = "www.google.co.uk";
            var expectedReleaseDetailsModel = new Release
            {
                ReleaseNumber = releaseNumber,
                ReleaseFiInstructions = releaseFIInstructions,
                TeamName = teamName,
                PrePatEmail = prePatEmail,
                ServiceNowTicketLink = serviceNowTicketLink
            };

            var releaseRepository = new Mock<IReleaseRepository>();
            releaseRepository.Setup(x => x.GetReleaseDetails(releaseNumber)).Returns(expectedReleaseDetailsModel);
            var releaseDetailsController = new ReleaseDetailsController(releaseRepository.Object);

            //when            
            ViewResult result = releaseDetailsController.Index(releaseNumber);

            var model = (Release)result.ViewData.Model;

            //then
            Assert.That(result.ViewName, Is.EqualTo("ReleaseDetails"));
            Assert.That(model, Is.EqualTo(expectedReleaseDetailsModel));
            Assert.That(model.ReleaseFiInstructions, Is.EqualTo(releaseFIInstructions));
            Assert.That(model.ReleaseNumber, Is.EqualTo(releaseNumber));
            Assert.That(model.TeamName, Is.EqualTo(teamName));

            Assert.That(model.PrePatEmail, Is.EqualTo(prePatEmail));

            Assert.That(model.ServiceNowTicketLink, Is.EqualTo(serviceNowTicketLink));

        }

        [Test]
        public void ShouldBeAbleToCreateRelease()
        {
            //given
            var releaseRepository = new Mock<IReleaseRepository>();
            var releaseController = new ReleaseController(releaseRepository.Object);

            //when
            var redirectToRouteResult = (RedirectToRouteResult)releaseController.Create(new Release(), null);
            //then
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
                PrePatEmail = "we all love pre pat meetings",
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

            postedfile.Setup(f => f.ContentLength).Returns(8192);
            postedfile.Setup(f => f.FileName).Returns(String.Concat(expectedFileName, expectedFileTypeExtension));

            releaseRepository.Setup(m => m.SaveReleaseDetails(It.IsAny<Release>()));

            //when
            releaseController.Create(release, postedfile.Object);

            //Then            
            releaseRepository.Verify(x => x.SaveReleaseDetails(It.IsAny<Release>()), Times.Once());
            postedfile.Verify(x => x.SaveAs(savedFileName), Times.Once());


            Assert.That(release.PrePatEmailFileInfo.Name, Is.EqualTo(savedFileName));
            Assert.That(release.PrePatEmailFileInfo.Length, Is.EqualTo(8192));

        }
    }
}