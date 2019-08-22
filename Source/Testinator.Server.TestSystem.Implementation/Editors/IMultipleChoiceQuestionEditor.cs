using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IMultipleChoiceQuestionEditor : IQuestionEditor
    {
        OperationResult AddOption(string option);
    }
}
