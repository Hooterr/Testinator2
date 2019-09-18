using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    public interface IGradingPresetEditor : IBuildable<IGradingPreset>, IErrorListener<IGradingPresetEditor>
    {
        [EditorProperty]
        string Name { get; set; }

        [EditorProperty]
        List<KeyValuePair<int, IGrade>> Thresholds { get; set; }
    }
}
