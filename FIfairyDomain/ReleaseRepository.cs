using System;
using System.Collections.Generic;

namespace FIfairyDomain
{
    public class ReleaseRepository : IReleaseRepository
    {

        public IEnumerable<IReleaseModel> GetReleases()
        {
            return new List<IReleaseModel>
                       {
                           new ReleaseModel("Enzo",
                                            new List<string> {"REL1216", "REL54164", "REL123", "REL124", "REL125"}),
                           new ReleaseModel("Phoenix", new List<string> {"REL1210"}),
                           new ReleaseModel("Colombo", new List<string> {"REL1000", "REL11122"})
                       };
        }

        public IEnumerable<IReleaseModel> GetReleases(DateTime dateFrom, DateTime dateTo)
        {
            return new List<IReleaseModel>
                       {
                           new ReleaseModel("Enzo", new List<string> {"REL1216", "REL54164"}),
                           new ReleaseModel("Colombo", new List<string> {"REL1000"})
                       };
        }
    }
}