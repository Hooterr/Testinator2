using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class MultipleChoiceQuestionEditor : BaseEditor<MultipleChoiceQuestion>, IMultipleChoiceQuestionEditor
    {

        public ITaskEditor Task => mTaskEditor;

        public OperationResult AddOption(string option)
        {
            throw new NotImplementedException();
        }

        public MultipleChoiceQuestion GetQuestion()
        {
            var question = new MultipleChoiceQuestion();
            var task = mTaskEditor.AssembleQuestionContent();
            question.TaskImpl = task;
            return question;
        }

        public int GetVersion()
        {
            return Version;
        }
    }
}
