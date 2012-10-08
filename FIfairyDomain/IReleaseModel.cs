using System;
using System.Collections.Generic;

namespace FIfairyDomain
{
    public interface IReleaseModel
    {
        string ReleaseNumber { get; }
        string TeamName { get; }
        DateTime Date { get; }
    }
}