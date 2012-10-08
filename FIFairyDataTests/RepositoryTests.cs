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
            var expectedReleaseDetailsModels = new List<Release>
                                                   {
                                                       new Release
                                                           {
                                                               ReleaseNumber = "REL01",
                                                               TeamName = "enzo",
                                                               PrePatEmail = "some email",
                                                               ServiceNowTicketLink = "ticket",
                                                               ReleaseFiInstructions = "instructions"
                                                           },
                                                       new Release
                                                           {
                                                               ReleaseNumber = "REL02",
                                                               TeamName = "enzo",
                                                               PrePatEmail = "some email",
                                                               ServiceNowTicketLink = "ticket",
                                                               ReleaseFiInstructions = "instructions"
                                                           }
                                                   };

            //when           
            foreach (Release expectedReleaseDetailsModel in expectedReleaseDetailsModels)
            {
                releaseRepository.SaveReleaseDetails(expectedReleaseDetailsModel);
            }

            IEnumerable<Release> releaseDetailsModels = releaseRepository.GetReleases();

            //then
            Assert.That(releaseDetailsModels, Is.EqualTo(expectedReleaseDetailsModels));
        }

        [Test]
        public void ShouldSaveReleaseData()
        {
            //given
            var releaseRepository = new ReleaseRepository();
            var expectedReleaseDetailsModel = new Release
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
            Release release = releaseRepository.GetReleaseDetails("REL01");
            Assert.That(release, Is.EqualTo(expectedReleaseDetailsModel));
        }
    }
}