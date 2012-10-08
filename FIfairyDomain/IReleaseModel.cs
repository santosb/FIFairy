using System.Collections.Generic;

namespace FIfairyDomain
{
    public interface IReleaseModel
    {
        List<string> Releases { get; }
        string TeamName { get; }        
    }
}