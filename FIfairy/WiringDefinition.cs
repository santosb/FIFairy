using FIfairyDomain;
using StructureMap;

namespace FIfairy
{
    public class WiringDefinition 
    {
        public void Configure(IContainer container)
        {
            container.Configure(c => c.For<IReleaseRepository>().Use<ReleaseRepository>());
            container.Configure(c => c.For<IReleaseModel>().Use<ReleaseModel>());
        }
    }
}