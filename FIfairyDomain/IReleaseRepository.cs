using System;
using System.Collections.Generic;

namespace FIfairyDomain
{
    public interface  IReleaseRepository
    {
        IEnumerable<ReleaseDetailsModel> GetReleases();
        IEnumerable<ReleaseDetailsModel> GetReleases(DateTime dateTo);
        ReleaseDetailsModel GetReleaseDetails(string releaseNumber);
        void SaveReleaseDetails(ReleaseDetailsModel expectedReleaseDetailsModel);
    }
}