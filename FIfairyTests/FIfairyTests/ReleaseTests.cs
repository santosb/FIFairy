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
    public class ReleaseTests
    {
        [Test]
        public void ShouldDisplayReleaseNumbersOfTheLastThreeMonths()
        {
            //given
            IEnumerable<ReleaseDetailsModel> expectedReleaseModel = new List<ReleaseDetailsModel>
                                                                  {
                                                                      new ReleaseDetailsModel ("Enzo", "REL1216", new DateTime(2011,10,20)),
                                                                      new ReleaseDetailsModel("Enzo", "REL54164", new DateTime(2011,10,20)),
                                                                      new ReleaseDetailsModel("Colombo", "REL1000", new DateTime(2011,10,20))
                                                                  };
                                                                      
            //when
            var releaseRepository = new Mock<IReleaseRepository>();
            DateTime dateNow = new DateTime(2012,01,21);
            DateTime dateTo = dateNow.AddMonths(-3);                        

            releaseRepository.Setup(x => x.GetReleases(dateTo)).Returns(expectedReleaseModel);
            var releaseController = new ReleaseController(releaseRepository.Object);
            ViewResult result = releaseController.ReleaseByDate(dateTo.Year, dateTo.Month, dateTo.Day);
            var model = (IEnumerable<ReleaseDetailsModel>)result.ViewData.Model;

            //then
            Assert.That(result.ViewName, Is.EqualTo("Release"));
            Assert.That(model, Is.EqualTo(expectedReleaseModel));
            Assert.That(model.First().TeamName, Is.EqualTo("Enzo"));
        }

        [Test]
        public void ShouldDisplayReleases()
        {
            IEnumerable<ReleaseDetailsModel> expectedReleaseModel = new List<ReleaseDetailsModel>
                       {
                           new ReleaseDetailsModel("Enzo", "REL1216",  new DateTime(2011,10,20)),
                           new ReleaseDetailsModel("Enzo", "REL54164", new DateTime(2011,11,20)),
                           new ReleaseDetailsModel("Enzo", "REL123", new DateTime(2011,03,20)),
                           new ReleaseDetailsModel("Enzo", "REL124", new DateTime(2011,02,27)),
                           new ReleaseDetailsModel("Enzo", "REL125", new DateTime(2011,09,26)),
                           new ReleaseDetailsModel("Colombo", "REL1000", new DateTime(2011,12,25)),
                           new ReleaseDetailsModel("Colombo", "REL11122", new DateTime(2011,11,04))
                       };

            var releaseRepository = new Mock<IReleaseRepository>();
            releaseRepository.Setup(x => x.GetReleases()).Returns(expectedReleaseModel);
            var releaseController = new ReleaseController(releaseRepository.Object);
            ViewResult result = releaseController.Index();

            var model = (IEnumerable<ReleaseDetailsModel>)result.ViewData.Model;

            Assert.That(result.ViewName, Is.EqualTo("Release"));
            Assert.That(model, Is.EqualTo(expectedReleaseModel));
            Assert.That(model.First().TeamName, Is.EqualTo("Enzo"));
        }


    }
}