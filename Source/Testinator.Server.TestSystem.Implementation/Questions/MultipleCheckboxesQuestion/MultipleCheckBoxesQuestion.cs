using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public sealed class MultipleCheckboxesQuestion : BaseQuestion
    {
        public MultipleCheckboxesQuestion(
            IQuestionTask task,
            MultipleCheckboxesQuestionScoring scoring,
            MultipleCheckboxesQuestionOptions options,
            string author,
            Category category,
            int version)
            : base(task, scoring, options, author, category, version) { }


    }

}
