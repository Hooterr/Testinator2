using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public class MultipleChoiceQuestionOptions : IQuestionOptions
    {
        /// <summary>
        /// ABC options for this question
        /// </summary>
        [MaxCollectionCount(maxCount: 5)]
        // TODO minimum collection count
        public List<string> Options { get; internal set; }
    }
}
