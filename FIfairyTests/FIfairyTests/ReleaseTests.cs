using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FIfairy.Controllers;
using FIfairyDomain;
using Moq;
using NUnit.Framework;

namespace FIfairyTests
{
    [TestFixture]
    public class ReleaseTests
    {
        [Test]
        public void ShouldDisplayReleaseNumbersOfTheLastThreeMonths()
        {
            //given
            IEnumerable<IReleaseModel> expectedReleaseModel = new List<IReleaseModel>
                                                                  {
                                                                      new ReleaseModel("Enzo",
                                                                                       new List<string>
                                                                                           {
                                                                                               "REL1216",
                                                                                               "REL54164",
                                                                                               "REL123",
                                                                                               "REL124",
                                                                                               "REL125"
                                                                                           }),
                                                                      new ReleaseModel("Phoenix",
                                                                                       new List<string> {"REL1210"}),
                                                                      new ReleaseModel("Colombo",
                                                                                       new List<string>
                                                                                           {"REL1000", "REL11122"})
                                                                  };
            //when
            var releaseRepository = new Mock<IReleaseRepository>();
            DateTime dateFrom = DateTime.Today.AddMonths(-3);
            DateTime dateTo = DateTime.Today;

            releaseRepository.Setup(x => x.GetReleases(dateFrom, dateTo)).Returns(expectedReleaseModel);
            var releaseController = new ReleaseController(releaseRepository.Object);
            ViewResult result = releaseController.Indexes(dateFrom, dateTo);

            //then
            Assert.That(result.ViewName, Is.EqualTo("ReleaseView"));
            Assert.That(result.ViewData.Model, Is.EqualTo(expectedReleaseModel));
        }

        [Test]
        public void ShouldDisplayReleases()
        {
            IEnumerable<IReleaseModel> expectedReleaseModel = new List<IReleaseModel>
                                                                  {
                                                                      new ReleaseModel("Enzo",
                                                                                       new List<string>
                                                                                           {"REL1216", "REL54164"}),
                                                                      new ReleaseModel("Colombo",
                                                                                       new List<string> {"REL1000"})
                                                                  };

            var releaseRepository = new Mock<IReleaseRepository>();
            releaseRepository.Setup(x => x.GetReleases()).Returns(expectedReleaseModel);
            var releaseController = new ReleaseController(releaseRepository.Object);
            ViewResult result = releaseController.Index();
            Assert.That(result.ViewName, Is.EqualTo("ReleaseView"));
            Assert.That(result.ViewData.Model, Is.EqualTo(expectedReleaseModel));
        }

        [Test]
        public void ShouldDisplayTeamName()
        {
            //given

            IEnumerable<IReleaseModel> expectedReleaseModel = new List<IReleaseModel>
                                                                  {
                                                                      new ReleaseModel("Enzo",
                                                                                       new List<string>
                                                                                           {"REL1216", "REL54164"}),
                                                                      new ReleaseModel("Colombo",
                                                                                       new List<string> {"REL1000"})
                                                                  };

            //when
            var releaseRepository = new Mock<IReleaseRepository>();
            releaseRepository.Setup(x => x.GetReleases()).Returns(expectedReleaseModel);
            var releaseController = new ReleaseController(releaseRepository.Object);
            ViewResult viewResult = releaseController.Index();

            //then
            Assert.That(viewResult.ViewData.Model, Is.EqualTo(expectedReleaseModel));
        }
    }
}