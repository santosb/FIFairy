using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FIfairyDomain
{
    public class Release
    {
        public Release()
        {
            ReleaseDate = DateTime.Today;            
        }

        public Release(string teamName, string releaseNumber, DateTime releaseDate)
        {
            TeamName = teamName;
            ReleaseNumber = releaseNumber;
            ReleaseDate = releaseDate;            
        }

        public virtual DateTime ReleaseDate { get; set; }

        public virtual string ReleaseNumber { get; set; }
        [AllowHtml]
        [DisplayFormat(NullDisplayText = "No Forward Integration instructions available!")]
        public virtual string ReleaseFiInstructions { get; set; }
        public virtual string TeamName { get; set; }        
        public virtual string ServiceNowTicketLink { get; set; }

        public virtual PrePatEmailFileInfo PrePatEmailFileInfo { get; set; }

        public static IEnumerable<Team> TeamNames
        {
            get
            {
               return new List<Team>
                    {
                        new Team {TeamId = 1, TeamName = "Fire"},
                        new Team {TeamId = 2, TeamName = "Colombo"},
                        new Team {TeamId = 3, TeamName = "Topgun"},
                        new Team {TeamId = 4, TeamName = "Phoenix"},
                        new Team {TeamId = 5, TeamName = "Magnum"},
                        new Team {TeamId = 6, TeamName = "Enzo"},
                        new Team {TeamId = 7, TeamName = "Ops"}
                    };
            }
        }
    

        #region Equality 
        public virtual bool Equals(Release other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.ReleaseDate.Equals(ReleaseDate) && Equals(other.ReleaseNumber, ReleaseNumber) && Equals(other.ReleaseFiInstructions, ReleaseFiInstructions) && Equals(other.TeamName, TeamName) && Equals(other.ServiceNowTicketLink, ServiceNowTicketLink) && Equals(other.PrePatEmailFileInfo, PrePatEmailFileInfo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Release)) return false;
            return Equals((Release) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = ReleaseDate.GetHashCode();
                result = (result*397) ^ (ReleaseNumber != null ? ReleaseNumber.GetHashCode() : 0);
                result = (result*397) ^ (ReleaseFiInstructions != null ? ReleaseFiInstructions.GetHashCode() : 0);
                result = (result*397) ^ (TeamName != null ? TeamName.GetHashCode() : 0);                
                result = (result*397) ^ (ServiceNowTicketLink != null ? ServiceNowTicketLink.GetHashCode() : 0);
                result = (result*397) ^ (PrePatEmailFileInfo != null ? PrePatEmailFileInfo.GetHashCode() : 0);
                return result;
            }
        }
        #endregion
    }
}