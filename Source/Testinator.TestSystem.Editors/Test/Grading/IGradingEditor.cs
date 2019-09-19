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
        /// Contains custom thresholds, either percentage or points, depends on <see cref="Custom"/> flag
        /// </summary>
        [EditorProperty]
        List<KeyValuePair<int, IGrade>> CustomThresholds { get; set; }

        /// <summary>
        /// Indicates if <see cref="CustomThresholds"/> contains points thresholds or percentage thresholds
        /// </summary>
        bool ContainsPoints { get; set; }

        /// <summary>
        /// Indicates if <see cref="Preset"/> or <see cref="CustomThresholds"/> should be used
        /// True: <see cref="CustomThresholds"/> are used
        /// False: <see cref="Preset"/> is used
        /// </summary>
        bool Custom { get; set; } 
        
        // Don't bother for now

        // bool UseAllQuestions
        // some fun stuff with questions' categories
    }
}
