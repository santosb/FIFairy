using System.Web.Mvc;
using FIfairy.Controllers;
using FIfairyDomain;
using Moq;
using NUnit.Framework;

namespace FIfairyTests
{
    [TestFixture]
    internal class ReleaseDetailsTests
    {
        [Test]
        public void ShouldDisplayReleaseDetails()
        {
            //given

            string teamName = "ENZO";
            string releaseNumber = "REL1216";

            string releaseFIInstructions = "FI as Normal";
            string prePatEmail = "we all love pre pat meetings";
            string serviceNowTicketLink = "www.google.co.uk";
            var expectedReleaseDetailsModel = new ReleaseDetailsModel
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

            var model = (ReleaseDetailsModel) result.ViewData.Model;

            //then
            Assert.That(result.ViewName, Is.EqualTo("ReleaseDetails"));
            Assert.That(model, Is.EqualTo(expectedReleaseDetailsModel));
            Assert.That(model.ReleaseFiInstructions, Is.EqualTo(releaseFIInstructions));
            Assert.That(model.ReleaseNumber, Is.EqualTo(releaseNumber));
            Assert.That(model.TeamName, Is.EqualTo(teamName));
            
            Assert.That(model.PrePatEmail, Is.EqualTo(prePatEmail));
            
            Assert.That(model.ServiceNowTicketLink, Is.EqualTo(serviceNowTicketLink));

        }
    }
}