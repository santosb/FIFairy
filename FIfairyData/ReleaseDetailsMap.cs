using FluentNHibernate.Mapping;

namespace FIfairyData
{
    class ReleaseDetailsMap : ClassMap<ReleaseDetails>
    {
        public ReleaseDetailsMap()
        {
            Id(x => x.Id);
            Map(x => x.TeamName);
            Map(x => x.ReleaseNumber);
            Map(x => x.PrePatEmail);
            Map(x => x.ReleaseFiInstructions);
            Map(x => x.ServiceNowTicketLink);            
        }
        
    }
}
