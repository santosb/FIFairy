using System.Web.Mvc;
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
            var redirectToRouteResult = (RedirectToRouteResult)releaseController.Create(new ReleaseDetailsModel());
            //then
            Assert.That(redirectToRouteResult.RouteValues["Controller"], Is.EqualTo("Release"));
            Assert.That(redirectToRouteResult.RouteValues["Action"], Is.EqualTo("Index"));
        }
        [Test]
        public void ShouldCreateRelease()
        {
            //given


            var releaseRepository = new Mock<IReleaseRepository>();
            var releaseController = new ReleaseController(releaseRepository.Object);
            releaseRepository.Setup(m => m.SaveReleaseDetails(It.IsAny<ReleaseDetailsModel>())).Verifiable();


            var releaseDetailsModel = new ReleaseDetailsModel()
                                     {
                                         ReleaseNumber= "REL1216",
                                         ReleaseFiInstructions = "FI as Normal",
                                         TeamName = "ENZO",
                                         PrePatEmail = "we all love pre pat meetings",
                                         ServiceNowTicketLink = "www.google.co.uk"
                                     };

            //when                        
            releaseController.Create(releaseDetailsModel);

            //then                        
            releaseRepository.Verify(
                x =>
                x.SaveReleaseDetails(releaseDetailsModel));
            
        }
    }
}