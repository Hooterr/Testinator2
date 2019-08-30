using System;
using System.Collections.Generic;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class MultipleChoiceQuestionOptionsEditor : BaseEditor<MultipleChoiceQuestionOptions, IMultipleChoiceQuestionOptionsEditor>, IEditor<MultipleChoiceQuestionOptions>, IMultipleChoiceQuestionOptionsEditor
    {
        public List<string> Options { get; set; }

        public OperationResult<MultipleChoiceQuestionOptions> Build()
        {
            throw new NotImplementedException();
        }

        public int GetMaximumCount()
        {
            throw new NotImplementedException();
        }

        protected override void OnInitialize()
        {
            if (IsInCreationMode())
                Options = new List<string>();
            else
                Options = new List<string>(OriginalObject.Options);
        }

        public MultipleChoiceQuestionOptionsEditor(int version) : base(version) { }

        public MultipleChoiceQuestionOptionsEditor(MultipleChoiceQuestionOptions objToEdit, int version) : base(objToEdit, version) { }
    }
}
