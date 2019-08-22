using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IMultipleChoiceQuestionEditor
    {
        OperationResult AddOption(string option);
    }
}
