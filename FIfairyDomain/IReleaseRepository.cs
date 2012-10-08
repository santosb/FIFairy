using System;
using System.Collections.Generic;

namespace FIfairyDomain
{
    public interface  IReleaseRepository
    {
        IEnumerable<Release> GetReleases();
        IEnumerable<Release> GetReleases(DateTime dateTo);
        Release GetReleaseDetails(string releaseNumber);
        void SaveReleaseDetails(Release release);
    }
}