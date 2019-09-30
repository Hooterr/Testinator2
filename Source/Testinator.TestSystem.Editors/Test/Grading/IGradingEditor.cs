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
        /// Contains thresholds, either percentage or points, depends on <see cref="ContainsPoints"/> flag
        /// </summary>
        [EditorProperty]
        // TODO write a wrapper around it that has a ContainsPoints flag, not super important right now tho
        List<KeyValuePair<int, IGrade>> Thresholds { get; set; }

        /// <summary>
        /// Number of points one can get for all correct answers from this test
        /// </summary>
        int TotalPointScore { get; }

        /// <summary>
        /// Gets the maximum amount of thresholds
        /// </summary>
        int MaxThresholdsCount { get; }

        /// <summary>
        /// Gets the minimum amount of threshold
        /// </summary>
        int MinThresholdCount { get; }
        int InitialThresholdCount { get; }

        /// <summary>
        /// Indicates if <see cref="Thresholds"/> contains points thresholds or percentage thresholds
        /// </summary>
        bool ContainsPoints { get; set; }

        /// <summary>
        /// Sets <see cref="Thresholds"/> according to the provided preset
        /// </summary>
        /// <param name="preset">The preset</param>
        void UsePreset(IGradingPreset preset);
        
        // bool UseAllQuestions - shouldn't this go to test options btw? yeah, maybe. It can go anywhere tbh
        // some fun stuff with questions' categories
    }
}
