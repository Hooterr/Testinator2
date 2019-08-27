using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class MultipleChoiceQuestionEditor : BaseEditor<MultipleChoiceQuestion>, IMultipleChoiceQuestionEditor
    {

        public OperationResult AddOption(string option)
        {
            throw new NotImplementedException();
        }

        public int GetVersion()
        {
            return Version;
        }
    }
}
