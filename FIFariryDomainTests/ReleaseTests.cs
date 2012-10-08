using System;
using System.Collections.Generic;
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
        public void ShouldTestPresenceOfPrePatEmail()
        {
            //given
            Release release = new Release();
            
            //when
            release.PrePatEmailFileInfo = null;            

            //then

        }
    }
}
