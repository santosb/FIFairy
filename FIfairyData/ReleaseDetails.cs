using System;

namespace FIfairyData
{
    public class ReleaseDetails
    {
        public virtual string ReleaseNumber { get; set; }
        public virtual string ReleaseFiInstructions { get; set; }
        public virtual string TeamName { get; set; }
        public virtual string PrePatEmail { get; set; }
        public virtual string ServiceNowTicketLink { get; set; }

        public virtual int Id { get; set; }
    }
}