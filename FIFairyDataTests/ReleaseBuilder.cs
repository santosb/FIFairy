using System;
using FIfairyDomain;

namespace FIFairyDataTests
{
    public class ReleaseBuilder
    {
        readonly Release release = new Release()
                                      {
                                          ReleaseNumber = "REL00",
                                          TeamName = "enzo",
                                          PrePatEmail = "some email",
                                          ServiceNowTicketLink = "www.alink.com",
                                          ReleaseFiInstructions = "instructions",
                                          PrePatEmailFileInfo =
                                              new PrePatEmailFileInfo() {Length = 123, Name = @"fooo.msg"},
                                          ReleaseDate = DateTime.Today,                                          
                                      };

        public ReleaseBuilder WithReleaseNumber(string releaseNumber)
        {
            release.ReleaseNumber = releaseNumber;
            return this;
        }

        public Release  Build()
        {
            return release;
        }
    }
}