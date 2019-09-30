using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation.Questions
{
    public sealed class MultipleCheckBoxesQuestion : BaseQuestion
    {
        public new MultipleCheckBoxesQuestionScoring Scoring { get; internal set; }
        public new MultipleCheckBoxesQuestionOptions Options { get; internal set; }
    }

}
