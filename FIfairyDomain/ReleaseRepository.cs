using System;
using System.Collections.Generic;
using System.Linq;

namespace FIfairyDomain
{
    public class ReleaseRepository : IReleaseRepository
    {
        #region IReleaseRepository Members

        public IEnumerable<IReleaseModel> GetReleases()
        {
            return new List<IReleaseModel>
                       {
                           new ReleaseModel("Enzo", "REL1216",  new DateTime(2011,10,20)),
                           new ReleaseModel("Enzo", "REL54164", new DateTime(2011,11,20)),
                           new ReleaseModel("Enzo", "REL123", new DateTime(2011,03,20)),
                           new ReleaseModel("Enzo", "REL124", new DateTime(2011,02,27)),
                           new ReleaseModel("Enzo", "REL125", new DateTime(2011,09,26)),
                           new ReleaseModel("Colombo", "REL1000", new DateTime(2011,12,25)),
                           new ReleaseModel("Colombo", "REL11122", new DateTime(2011,11,04))
                       };
        }

        public IEnumerable<IReleaseModel> GetReleases(DateTime dateTo)
        {
            return GetReleases().Where(x => x.Date > dateTo);
        }

        #endregion
    }
}