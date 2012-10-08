using System;
using System.Collections.Generic;

namespace FIfairyDomain
{
    public interface  IReleaseRepository
    {
        IEnumerable<IReleaseModel> GetReleases();
        IEnumerable<IReleaseModel> GetReleases(DateTime dateTo);
    }
}