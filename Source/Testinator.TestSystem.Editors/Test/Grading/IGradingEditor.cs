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
        /// Contains custom thresholds, either percentage or points, depends on <see cref="ContainsPoints"/> flag
        /// </summary>
        [EditorProperty]
        List<KeyValuePair<int, IGrade>> CustomThresholds { get; set; }

        /// <summary>
        /// Number of points one can get for all correct answers from this test
        /// </summary>
        int TotalPointScore { get; }

        int MaxThresholdsCount { get; }

        int MinThresholdCount { get; }
        int InitialThresholdCount { get; }

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

        // bool UseAllQuestions - shouldn't this go to test options btw?
        // some fun stuff with questions' categories
    }
}
