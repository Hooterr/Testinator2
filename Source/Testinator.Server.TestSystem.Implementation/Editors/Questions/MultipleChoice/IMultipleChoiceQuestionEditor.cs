using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IMultipleChoiceQuestionEditor : IQuestionEditor<MultipleChoiceQuestion>
    {
        OperationResult AddOption(string option);
    }
}
