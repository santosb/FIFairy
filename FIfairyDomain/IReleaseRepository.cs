using System;
using System.Collections.Generic;

namespace FIfairyDomain
{
    public interface  IReleaseRepository
    {
        IEnumerable<ReleaseModel> GetReleases();
        IEnumerable<ReleaseModel> GetReleases(DateTime dateTo);
        ReleaseDetailsModel GetReleaseDetails(string releaseNumber);
        void SaveReleaseDetails(ReleaseDetailsModel expectedReleaseDetailsModel);
    }
}