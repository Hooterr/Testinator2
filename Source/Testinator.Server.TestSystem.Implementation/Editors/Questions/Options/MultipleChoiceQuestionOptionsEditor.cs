using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class MultipleChoiceQuestionOptionsEditor : BaseEditor<MultipleChoiceQuestionOptions, IMultipleChoiceQuestionOptionsEditor>, IEditor<MultipleChoiceQuestionOptions>, IMultipleChoiceQuestionOptionsEditor
    {
        public OperationResult<MultipleChoiceQuestionOptions> Build()
        {
            throw new NotImplementedException();
        }

        public MultipleChoiceQuestionOptionsEditor(int version) : base(version) { }

        public MultipleChoiceQuestionOptionsEditor(MultipleChoiceQuestionOptions objToEdit, int version) : base(objToEdit, version) { }
    }
}
