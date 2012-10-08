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
                                                                      new ReleaseModel("Enzo", "REL1216", new DateTime(2011,10,20)),
                                                                      new ReleaseModel("Enzo", "REL54164", new DateTime(2011,10,20)),
                                                                      new ReleaseModel("Colombo", "REL1000", new DateTime(2011,10,20))
                                                                  };
                                                                      
            //when
            var releaseRepository = new Mock<IReleaseRepository>();
            DateTime dateNow = new DateTime(2012,01,21);
            DateTime dateTo = dateNow.AddMonths(-3);                        

            releaseRepository.Setup(x => x.GetReleases(dateTo)).Returns(expectedReleaseModel);
            var releaseController = new ReleaseController(releaseRepository.Object);
            ViewResult result = releaseController.Indexes(dateTo);

            //then
            Assert.That(result.ViewName, Is.EqualTo("ReleaseView"));
            Assert.That(result.ViewData.Model, Is.EqualTo(expectedReleaseModel));
        }

        [Test]
        public void ShouldDisplayReleases()
        {
            IEnumerable<IReleaseModel> expectedReleaseModel = new List<IReleaseModel>
                       {
                           new ReleaseModel("Enzo", "REL1216",  new DateTime(2011,10,20)),
                           new ReleaseModel("Enzo", "REL54164", new DateTime(2011,11,20)),
                           new ReleaseModel("Enzo", "REL123", new DateTime(2011,03,20)),
                           new ReleaseModel("Enzo", "REL124", new DateTime(2011,02,27)),
                           new ReleaseModel("Enzo", "REL125", new DateTime(2011,09,26)),
                           new ReleaseModel("Colombo", "REL1000", new DateTime(2011,12,25)),
                           new ReleaseModel("Colombo", "REL11122", new DateTime(2011,11,04))
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
                                                                      new ReleaseModel("Enzo", "REL1216", new DateTime(2011,10,20)),
                                                                      new ReleaseModel("Enzo", "REL54164", new DateTime(2011,10,20)),
                                                                      new ReleaseModel("Colombo", "REL1000", new DateTime(2011,10,20))
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