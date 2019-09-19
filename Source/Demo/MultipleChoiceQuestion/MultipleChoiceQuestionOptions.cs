using System;
using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Demo
{
    [Serializable]
    public class MultipleChoiceQuestionOptions
    {
        /// <summary>
        /// ABC options for this question
        /// </summary>
        public List<string> Options { get; internal set; }
    }
}