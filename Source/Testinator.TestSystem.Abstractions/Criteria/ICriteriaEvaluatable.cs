namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The interface to implement for criteria that can be evaluated
    /// </summary>
    public interface ICriteriaEvaluatable
    {
        /// <summary>
        /// Calculates scoring result based on provided scoring
        /// </summary>
        IScoringResult CalculateScoringResult(IQuestionScoring scoring);
    }
}
