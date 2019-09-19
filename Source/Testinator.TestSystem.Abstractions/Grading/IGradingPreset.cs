using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Testinator.TestSystem.Abstractions
{
    public interface IGradingPreset
    {
        string Name { get; }

        DateTime CreationDate { get;  }

        DateTime LastEdited { get; }

        ReadOnlyCollection<KeyValuePair<int, IGrade>> PercentageThresholds { get; }
    }
}
