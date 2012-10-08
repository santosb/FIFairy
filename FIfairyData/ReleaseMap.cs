using FluentNHibernate.Mapping;

namespace FIfairyData
{
    public class ReleaseMap : ClassMap<Release>
    {
        public ReleaseMap()
        {
            Id(x => x.Id);
            Map(x => x.TeamName);
            Map(x => x.ReleaseNumber);
            Map(x => x.Date);
        }
    }
}