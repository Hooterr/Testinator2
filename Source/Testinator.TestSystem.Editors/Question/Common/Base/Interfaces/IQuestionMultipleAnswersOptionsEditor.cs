﻿namespace Testinator.TestSystem.Editors
{
    public interface IQuestionMultipleAnswersOptionsEditor : IQuestionOptionsEditor
    {
        /// <summary>
        /// Gets the maximum count of the allowed options
        /// </summary>
        int MaximumCount { get; }

        /// <summary>
        /// Gets the minimum count of the allowed options
        /// </summary>
        int MinimumCount { get; }
    }
}
