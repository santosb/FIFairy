using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FIfairy.Controllers;
using FIfairyDomain;
using Moq;
using NUnit.Framework;

namespace FIfairyTests
{
    [TestFixture]
    internal class CreateReleaseTests
    {
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
                                         ReleaseNumber= "REL1216",
                                         ReleaseFiInstructions = "FI as Normal",
                                         TeamName = "ENZO",
                                         PrePatEmail = "we all love pre pat meetings",
                                         ServiceNowTicketLink = "www.google.co.uk"
                                     };

            //when                        
            releaseController.Create(releaseDetailsModel, null);

            //then                        
            releaseRepository.Verify(x =>x.SaveReleaseDetails(releaseDetailsModel));
            
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
            

            Assert.That(release.PrePatEmailFile.Name, Is.EqualTo(savedFileName));
            Assert.That(release.PrePatEmailFile.Length, Is.EqualTo(8192));
            
        }
    }
}