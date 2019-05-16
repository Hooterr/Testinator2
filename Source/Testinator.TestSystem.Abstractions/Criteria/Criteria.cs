using System;

namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The 
    /// </summary>
    public abstract class Criteria : ICriteriaEvaluatable
    {
        /// <summary>
        /// The name of this criteria
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date when this criteria was created
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// The date when this criteria was last edited
        /// </summary>
        public DateTime LastEditionDate { get; set; }

        /// <summary>
        /// Calculates scoring result based on provided scoring
        /// </summary>
        public abstract IScoringResult CalculateScoringResult(IEvaluable scoring);
    }
}
