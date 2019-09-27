using System;
using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Implementation.Questions
{
    [Serializable]
    public class MultipleChoiceQuestionOptions : IQuestionOptions
    {
        /// <summary>
        /// ABC options for this question
        /// </summary>
        [CollectionCount(min: 2, max: 5, fromVersion: 1)]
        [StringLength(min: 1, max: 150, fromVersion: 1)]
        [CollectionItemsOnlyDistinct(value: true, fromVersion: 1)]
        public List<string> Options { get; internal set; }
    }
}