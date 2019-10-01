namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The base question options editor interface
    /// </summary>
    public interface IQuestionOptionsEditor
    {
        /// <summary>
        /// Gets the maximum count of the allowed options
        /// </summary>
        int MaximumCount { get; }

        /// <summary>
        /// Gets the minimum count of the allowed options
        /// </summary>
        int MinimumCount { get; }

        /// <summary>
        /// Gets the initial count of options for brand-new question
        /// </summary>
        int InitialCount { get; }
    }
}
