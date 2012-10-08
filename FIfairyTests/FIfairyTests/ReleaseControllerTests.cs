using System;
using System.Collections.Generic;
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
    }
}