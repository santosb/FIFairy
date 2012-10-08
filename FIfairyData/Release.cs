using System;

namespace FIfairyData
{
    public class Release
    {
        public virtual string TeamName { get; set; }

        public virtual string ReleaseNumber { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual int Id { get;  set; }
    }
}