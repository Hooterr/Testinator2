using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions
{
    public interface IGradingPreset
    {
        string Name { get; }

        DateTime CreationDate { get;  }

        DateTime LastEdited { get; }

        // read-only stuff here
        SortedList<int, IGrade> PercentageThresholds { get; }
    }
}
