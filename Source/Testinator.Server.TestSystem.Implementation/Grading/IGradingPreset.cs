using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    public interface IGradingPreset
    {
        string Name { get; }

        DateTime CreationDate { get;  }

        DateTime LastEdited { get; }

        ReadOnlyCollection<KeyValuePair<int, IGrade>> PercentageThresholds { get; }
    }
}
