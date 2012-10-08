using FIfairyDomain;
using FluentNHibernate.Mapping;

namespace FIfairyData
{
    class ReleaseDetailsMap : ClassMap<Release>
    {
        public ReleaseDetailsMap()
        {            
            Map(x => x.TeamName);
            Id(x => x.ReleaseNumber).GeneratedBy.Assigned();
            Map(x => x.PrePatEmail);
            Map(x => x.ReleaseFiInstructions);
            Map(x => x.ServiceNowTicketLink);
            Map(x => x.ReleaseDate);    
        }
        
    }
}
