using System;
using System.Collections.Generic;

namespace FIfairyDomain
{
    public class ReleaseModel 
    {
        private readonly string _teamName;
        private readonly string _releaseNumber;
        private readonly DateTime _dateTime;

        public ReleaseModel(string teamName, string releaseNumber, DateTime dateTime)
        {
            _teamName = teamName;
            _releaseNumber = releaseNumber;
            _dateTime = dateTime;
        }

        public DateTime Date
        {
            get { return _dateTime; }
        }

        public string ReleaseNumber
        {
            get { return _releaseNumber; }
        }

        public string TeamName
        {
            get { return _teamName; }
        }

      
    }
}