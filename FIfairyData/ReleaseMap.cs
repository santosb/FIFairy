using FIfairyDomain;
using FluentNHibernate.Mapping;

namespace FIfairyData
{
    internal class ReleaseMap : ClassMap<Release>
    {
        public ReleaseMap()
        {
            Map(x => x.TeamName);
            Id(x => x.ReleaseNumber).GeneratedBy.Assigned();
            Map(x => x.PrePatEmail);
            Map(x => x.ReleaseFiInstructions);
            Map(x => x.ServiceNowTicketLink);
            Map(x => x.ReleaseDate);
            //Map(x => x.PrePatEmailFileInfo);
            Component(x => x.PrePatEmailFileInfo, m =>
                                                      {
                                                          m.Map(x => x.Length);
                                                          m.Map(x => x.Name);
                                                      }
                );
        }
    }
}