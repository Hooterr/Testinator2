using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for the grading part for the test
    /// </summary>
    public interface IGradingEditor : IErrorListener<IGradingEditor>
    {
        [EditorProperty]
        IGradingPreset Preset { get; set; }

        [EditorProperty]
        List<KeyValuePair<int, IGrade>> Thresholds { get; set; }
    }
}
