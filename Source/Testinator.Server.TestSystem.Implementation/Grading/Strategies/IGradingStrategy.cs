using System.Collections.Generic;
using System.Collections.ObjectModel;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    internal interface IGradingStrategy 
    {
        ReadOnlyCollection<KeyValuePair<int, IGrade>> Thresholds { get; }

        bool ContainsPoints { get; }

        IGrade GetGrade(int pointScore);
    }
}
