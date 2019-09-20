﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    public class GradingPreset : IGradingPreset
{
        public string Name { get; internal set; }

        public DateTime CreationDate { get; internal set; }

        public DateTime LastEdited { get; internal set; }

        public ReadOnlyCollection<KeyValuePair<int, IGrade>> PercentageThresholds { get; internal set; }
    }
}