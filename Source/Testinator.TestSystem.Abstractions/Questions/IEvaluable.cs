namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// Represents an evaluable object
    /// </summary>
    public interface IEvaluable
    {
        /// <summary>
        /// Evaluates the answer
        /// </summary>
        /// <param name="correctPercentage">Percentage of the answer that was correct</param>
        /// <returns>Point score that corresponding to the correct percentage</returns>
        int Evalute(int correctPercentage);
        int GetMaximumPossibleScore();
        bool IsWellDefined();
    }
}
