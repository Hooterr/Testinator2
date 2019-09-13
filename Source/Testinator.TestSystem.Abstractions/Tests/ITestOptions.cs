namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The options that determine how the test should be handled
    /// </summary>
    public interface ITestOptions
    {
        /// <summary>
        /// Indicates if the test should use all the questions
        /// </summary>
        bool ShouldUseAllQuestions { get; }

        // And more..
        

        // Maybe do a key value thing, not sure
    }
}
