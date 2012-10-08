using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FIfairyDomain;
using Moq;
using NUnit.Framework;

namespace FIFariryDomainTests
{
    [TestFixture]
    public class ReleaseTests
    {
        [Test]
        //TODO:WTF IS THIS FOR?
        public void ShouldTestPresenceOfPrePatEmail()
        {
            //given
            Release release = new Release();
            
            //when
            release.PrePatEmailFileInfo = null;            

            //then

        }

        [Test]
        public void ShouldPopulateTeams()
        {
            //given
            //var release = new Release();
           
            //when            

            //then
            Assert.That(Release.TeamNames.Count(), Is.EqualTo(7));
            Assert.That(Release.TeamNames.First().TeamName, Is.EqualTo("Fire"));
            Assert.That(Release.TeamNames.First().TeamId, Is.EqualTo(1));
            Assert.That(Release.TeamNames.Last().TeamName, Is.EqualTo("Ops"));      
            Assert.That(Release.TeamNames.Last().TeamId, Is.EqualTo(7));            
        }
    }    
}


