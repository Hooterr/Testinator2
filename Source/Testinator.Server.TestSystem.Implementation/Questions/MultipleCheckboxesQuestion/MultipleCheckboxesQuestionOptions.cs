using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors.Attributes;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public class MultipleCheckboxesQuestionOptions : IQuestionOptions
    {
        /// <summary>
        /// Options for this question
        /// Checkbox - text
        /// checkbox - text
        /// etc..
        /// </summary>
        [MaxCollectionCount(maxCount: 5, fromVersion: 1)]
        // TODO minimum count
        public List<string> Options { get; internal set; }
    }
}
