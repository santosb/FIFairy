using System.Collections.Generic;

namespace FIfairyDomain
{
    public class ReleaseModel : IReleaseModel
    {
        private readonly string _teamName;
        private readonly List<string> _releases;

        public ReleaseModel(string teamName, List<string> releases)
        {
            _teamName = teamName;
            _releases = releases;
        }

        public List<string> Releases
        {
            get { return _releases; }
        }

        public string TeamName
        {
            get { return _teamName; }
        }
    }
}