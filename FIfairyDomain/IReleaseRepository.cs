using System;
using System.Collections.Generic;
using System.IO;

namespace FIfairyDomain
{
    public interface  IReleaseRepository
    {
        IEnumerable<Release> GetReleases();
        IEnumerable<Release> GetReleases(DateTime dateTo);
        Release GetReleaseDetails(string releaseNumber);
        void SaveReleaseDetails(Release release);
        Stream GetPrePatEmailFile(string filename);
        void SavePrePatEmailFile(string filename, Stream inputStream);
        IEnumerable<Release> GetReleasesOfLastThreeMonths();
    }
}