using System;
using System.Collections.Generic;
using System.Linq;
using FIfairyDomain;

namespace FIfairyData
{
    public class ReleaseRepository : IReleaseRepository
    {
        #region IReleaseRepository Members

        private List<ReleaseModel> _releases = new List<ReleaseModel>
                       {
                           new ReleaseModel("Enzo", "REL1216",  new DateTime(2011,10,20)),
                           new ReleaseModel("Enzo", "REL54164", new DateTime(2011,11,20)),
                           new ReleaseModel("Enzo", "REL123", new DateTime(2011,03,20)),
                           new ReleaseModel("Enzo", "REL124", new DateTime(2011,02,27)),
                           new ReleaseModel("Enzo", "REL125", new DateTime(2011,09,26)),
                           new ReleaseModel("Colombo", "REL1000", new DateTime(2011,12,25)),
                           new ReleaseModel("Colombo", "REL11122", new DateTime(2011,11,04))
                       };

        public IEnumerable<ReleaseModel> GetReleases()
        {            
            return _releases;
        }

        public IEnumerable<ReleaseModel> GetReleases(DateTime dateTo)
        {
            return GetReleases().Where(x => x.Date > dateTo);
        }

        public ReleaseDetailsModel GetReleaseDetails(string releaseNumber)
        {
            return new  ReleaseDetailsModel
                                                  {
                                                      ReleaseNumber = releaseNumber,
                                                      ReleaseFiInstructions = "FI as Normal.",
                                                      TeamName = "ENZO",
                                                      PrePatEmail = "pre pat meetings are cool...", 
                                                      ServiceNowTicketLink="www.google.co.uk"
                                                  };

        }

        public void SaveReleaseDetails(ReleaseDetailsModel releaseDetailsModel)
        {
            ReleaseModel _release = new ReleaseModel(releaseDetailsModel.TeamName, releaseDetailsModel.ReleaseNumber,
                                                     DateTime.Now);
            _releases.Add(_release);
        }

        #endregion
    }
}