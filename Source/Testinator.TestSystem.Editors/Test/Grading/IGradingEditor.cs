using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;
using Testinator.TestSystem.Implementation;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for the grading part for the test
    /// </summary>
    public interface IGradingEditor : IErrorListener<IGradingEditor>
    {
        /// <summary>
        /// The preset for this grading
        /// </summary>
        [EditorProperty]
        IGradingPreset Preset { get; set; }

        // TODO update comments

        /// <summary>
        /// If <see cref="Preset"/> is set this contains preset translated to point values for this test
        /// If <see cref="Custom"/> is set to true use this list to create custom point based grading
        /// </summary>
        [EditorProperty]
        List<KeyValuePair<int, IGrade>> Thresholds { get; set; }

        bool ContainsPoints { get; set; }

        /// <summary>
        /// Indicates if <see cref="Thresholds"/> contain custom point based grading
        /// </summary>
        bool Custom { get; set; } 
        
        // TODO more to come later

        // bool UseAllQuestions
        // some fun stuff with questions' categories
    }
}
