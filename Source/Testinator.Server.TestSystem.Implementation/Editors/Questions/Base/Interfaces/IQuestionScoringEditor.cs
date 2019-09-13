using Testinator.Server.TestSystem.Implementation.Attributes;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// The base question scoring editor interface
    /// </summary>
    public interface IQuestionScoringEditor
    {
        /// <summary>
        /// Maximum point score that is granted for the correct answer
        /// </summary>
        [EditorProperty]
        int MaximumScore { get; set; }
    }
}
