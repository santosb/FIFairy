using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FIfairyData;
using FIfairyDomain;
using Moq;
using NUnit.Framework;

namespace FIFairyDataTests
{
    [TestFixture]
    internal class RepositoryTests
    {
        private const string TestPrePatEmail = @"Enzo Pre-PAT release 19122011 ref REL11125.0.00.msg";
        //create tests in releaserep (data) to get and create the file. int test

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
                                                       new ReleaseBuilder().WithReleaseNumber("REL01").Build(),
                                                       new ReleaseBuilder().WithReleaseNumber("REL02").Build(),
                                                       new ReleaseBuilder().Build(),
                                                       new ReleaseBuilder().WithReleaseNumber("REL1234").Build()
                                                   };

            //when           
            foreach (Release expectedReleaseDetailsModel in expectedReleaseDetailsModels)
            {
                releaseRepository.SaveReleaseDetails(expectedReleaseDetailsModel);
            }

            // then
            IEnumerable<Release> releaseDetailsModels = releaseRepository.GetReleases();
            Assert.That(releaseDetailsModels, Is.EqualTo(expectedReleaseDetailsModels));            
        }

        [Test]
        public void ShouldSaveReleaseData()
        {
            //given
            var releaseRepository = new ReleaseRepository();

            //todo: put this inside the builder (for instante on the Build method as default)
            var expectedReleaseDetailsModel = new ReleaseBuilder().WithReleaseNumber("REL01").Build();
            //when
            releaseRepository.SaveReleaseDetails(expectedReleaseDetailsModel);

            //then
            Release release = releaseRepository.GetReleaseDetails("REL01");
            Assert.That(release, Is.EqualTo(expectedReleaseDetailsModel));
        }

        [Test]
        public void ShouldGetPrePatEmailFile()
        {
            //given
            var releaseRepository = new ReleaseRepository();
            Stream expectedFile = File.OpenRead(TestPrePatEmail);

            //when            
            using (FileStream file = (FileStream) releaseRepository.GetPrePatEmailFile(TestPrePatEmail))
            {
                //then
                Assert.That(file, Is.EqualTo(expectedFile));
                Assert.That(file.Name, Is.EqualTo(ToAbsolutePath(TestPrePatEmail)));
            }
        }

        [Test]
        public void ShouldSavePrePatEmailFile()
        {
            //given
            Stream expectedStream = File.OpenRead(TestPrePatEmail);
            var releaseRepository = new ReleaseRepository();
            string filename = TestPrePatEmail + DateTime.Now.ToString("-yy-MM-dd-HH-mm-ss") + ".msg";

            //when
            releaseRepository.SavePrePatEmailFile(filename, expectedStream);

            string actualFilename = ToAbsolutePath(filename);
            using (FileStream file = File.OpenRead(actualFilename))
            {
                Assert.That(file, Is.EqualTo(expectedStream));
                Assert.That(file.Name, Is.EqualTo(actualFilename));
            }

            //TearDown
            File.Delete(actualFilename);
        }

        private string ToAbsolutePath(string filename)
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"\" + filename;
        }
    }
}