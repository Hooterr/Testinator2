using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public sealed class MultipleCheckboxesQuestion : BaseQuestion
    {
        public new MultipleCheckboxesQuestionScoring Scoring { get; internal set; }
        public new MultipleCheckboxesQuestionOptions Options { get; internal set; }

        internal MultipleCheckboxesQuestion() { }
    }

}
