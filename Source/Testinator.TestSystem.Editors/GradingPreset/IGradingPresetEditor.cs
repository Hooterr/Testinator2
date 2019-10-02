using System;
using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;
using Testinator.TestSystem.Implementation;

namespace Testinator.TestSystem.Editors
{
    public interface IGradingPresetEditor : IBuildable<IGradingPreset>, IErrorListener<IGradingPresetEditor>
    {
        [EditorProperty]
        string Name { get; set; }

        [EditorProperty]
        List<KeyValuePair<int, IGrade>> Thresholds { get; set; }

        DateTime CreatedDate { get; }

        DateTime LastModified { get; }
    }
}
