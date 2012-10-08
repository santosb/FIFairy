using FIfairyDomain;
using FluentNHibernate.Mapping;

namespace FIfairyData
{
    class ReleaseDetailsMap : ClassMap<ReleaseDetailsModel>
    {
        public ReleaseDetailsMap()
        {            
            Map(x => x.TeamName);
            Id(x => x.ReleaseNumber).GeneratedBy.Assigned();
            Map(x => x.PrePatEmail);
            Map(x => x.ReleaseFiInstructions);
            Map(x => x.ServiceNowTicketLink);            
        }
        
    }
}
