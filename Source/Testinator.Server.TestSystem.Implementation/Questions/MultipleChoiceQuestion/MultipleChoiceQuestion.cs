using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public class MultipleChoiceQuestion : Question
    {
        public override IQuestionTask Task { get; protected set; }

        public override IQuestionAnswer Answer { get; protected set; }

        public override IEvaluable Scoring { get; protected set; }

    }
}
