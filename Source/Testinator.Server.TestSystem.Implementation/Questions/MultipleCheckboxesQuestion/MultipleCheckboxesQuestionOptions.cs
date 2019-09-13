using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.TestSystem.Abstractions;

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
        [MaxCollectionCount(maxCount: 5)]
        // TODO minimum count
        public List<string> Options { get; internal set; }
    }
}
