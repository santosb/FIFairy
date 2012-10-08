using System.Collections.Generic;
using System.IO;
using System.Linq;
using FIfairyData;
using FIfairyDomain;
using NUnit.Framework;

namespace FIFairyDataTests
{
    [TestFixture]
    internal class RepositoryTests
    {

        //rename REleasedetailsmodel to only release. !!!!!!!!!!!!!!
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            File.Delete("FIFairy.db");
        }

        #endregion

        [Test]
        public void ShouldGetAllReleases()
        {
            //given
            var releaseRepository = new ReleaseRepository();
            var expectedReleaseDetailsModels = new List<ReleaseDetailsModel>
                                                   {
                                                       new ReleaseDetailsModel
                                                           {
                                                               ReleaseNumber = "REL01",
                                                               TeamName = "enzo",
                                                               PrePatEmail = "some email",
                                                               ServiceNowTicketLink = "ticket",
                                                               ReleaseFiInstructions = "instructions"
                                                           },
                                                       new ReleaseDetailsModel
                                                           {
                                                               ReleaseNumber = "REL02",
                                                               TeamName = "enzo",
                                                               PrePatEmail = "some email",
                                                               ServiceNowTicketLink = "ticket",
                                                               ReleaseFiInstructions = "instructions"
                                                           }
                                                   };

            //when           
            foreach (ReleaseDetailsModel expectedReleaseDetailsModel in expectedReleaseDetailsModels)
            {
                releaseRepository.SaveReleaseDetails(expectedReleaseDetailsModel);
            }

            IEnumerable<ReleaseDetailsModel> releaseDetailsModels = releaseRepository.GetReleases();

            //then
            Assert.That(releaseDetailsModels, Is.EqualTo(expectedReleaseDetailsModels));
        }

        [Test]
        public void ShouldSaveReleaseData()
        {
            //given
            var releaseRepository = new ReleaseRepository();
            var expectedReleaseDetailsModel = new ReleaseDetailsModel
                                                  {
                                                      ReleaseNumber = "REL01",
                                                      TeamName = "enzo",
                                                      PrePatEmail = "some email",
                                                      ServiceNowTicketLink = "ticket",
                                                      ReleaseFiInstructions = "instructions"
                                                  };
            //when
            releaseRepository.SaveReleaseDetails(expectedReleaseDetailsModel);

            //then
            ReleaseDetailsModel releaseDetailsModel = releaseRepository.GetReleaseDetails("REL01");
            Assert.That(releaseDetailsModel, Is.EqualTo(expectedReleaseDetailsModel));
        }
    }
}