using System;
using System.Collections.Generic;

namespace FIfairyDomain
{
    public class ReleaseModel 
    {
        private readonly DateTime _dateTime;

        public ReleaseModel()
        {
            
        }
        
        public ReleaseModel(string teamName, string releaseNumber, DateTime dateTime)
        {
            TeamName = teamName;
            ReleaseNumber = releaseNumber;
            _dateTime = dateTime;
        }

        public virtual DateTime Date { get; set; }

        public virtual string ReleaseNumber { get;  set; }

        public virtual string TeamName { get; set; }
    }
}