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
        [CollectionCount(max: 5, min: 5, fromVersion: 1)]
        [StringLength(max: 150, min: 1, fromVersion: 1)]
        [CollectionItemsOnlyDistinct(value: true, fromVersion: 1)]
        public List<string> Options { get; internal set; }
    }
}