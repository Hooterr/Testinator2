using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Implementation.Questions
{
    public class MultipleCheckBoxesQuestionOptions : IQuestionOptions
    {
        /// <summary>
        /// Options for this question
        /// Checkbox - text
        /// checkbox - text
        /// etc..
        /// </summary>
        // min = 1? or 2? I dunno
        [CollectionCount(min: 1, max: 5, fromVersion: 1)]
        public List<string> Options { get; internal set; }
    }
}
