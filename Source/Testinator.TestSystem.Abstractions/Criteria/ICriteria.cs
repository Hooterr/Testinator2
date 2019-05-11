using System;

namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The interface for every criteria to implement
    /// </summary>
    public interface ICriteria
    {
        /// <summary>
        /// The name of this criteria
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The date when this criteria was created
        /// </summary>
        DateTime CreationDate { get; set; }

        /// <summary>
        /// The date when this criteria was last edited
        /// </summary>
        DateTime LastEditionDate { get; set; }

        /// <summary>
        /// Calculates scoring result based on provided scoring
        /// </summary>
        IScoringResult CalculateScoringResult(IScoring scoring);
    }
}
