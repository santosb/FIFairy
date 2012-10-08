using System;

namespace FIfairyDomain
{
    public class ReleaseDetailsModel
    {
        public ReleaseDetailsModel()
        {
            
        }

        public ReleaseDetailsModel(string teamName, string releaseNumber, DateTime releaseDate)
        {
            TeamName = teamName;
            ReleaseNumber = releaseNumber;
            ReleaseDate = releaseDate;
        }

        protected virtual DateTime ReleaseDate { get; set; }

        public virtual string ReleaseNumber { get; set; }
        public virtual string ReleaseFiInstructions { get; set; }
        public virtual string TeamName { get; set; }
        public virtual string PrePatEmail { get; set; }
        public virtual string ServiceNowTicketLink { get; set; }


        public virtual bool Equals(ReleaseDetailsModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.ReleaseNumber, ReleaseNumber) && Equals(other.ReleaseFiInstructions, ReleaseFiInstructions) && Equals(other.TeamName, TeamName) && Equals(other.PrePatEmail, PrePatEmail) && Equals(other.ServiceNowTicketLink, ServiceNowTicketLink);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ReleaseDetailsModel)) return false;
            return Equals((ReleaseDetailsModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (ReleaseNumber != null ? ReleaseNumber.GetHashCode() : 0);
                result = (result*397) ^ (ReleaseFiInstructions != null ? ReleaseFiInstructions.GetHashCode() : 0);
                result = (result*397) ^ (TeamName != null ? TeamName.GetHashCode() : 0);
                result = (result*397) ^ (PrePatEmail != null ? PrePatEmail.GetHashCode() : 0);
                result = (result*397) ^ (ServiceNowTicketLink != null ? ServiceNowTicketLink.GetHashCode() : 0);
                return result;
            }
        }
    }
}