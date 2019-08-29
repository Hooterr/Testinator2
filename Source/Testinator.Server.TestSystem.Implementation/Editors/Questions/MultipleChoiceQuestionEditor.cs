using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    [ConcreteEditorFor(typeof(MultipleChoiceQuestion))]
    internal class MultipleChoiceQuestionEditor : BaseQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IQuestionScoringEditor>
    {
        private MultipleChoiceQuestionOptionsEditor mOptionsEditor;


        public override IMultipleChoiceQuestionOptionsEditor Options => mOptionsEditor;

        public override IQuestionScoringEditor Scoring => throw new NotImplementedException();

        protected override void OnInitializing()
        {
            mOptionsEditor = new MultipleChoiceQuestionOptionsEditor();
        }

        protected override OperationResult<IQuestionOptions> BuildOptions()
        {
            throw new NotImplementedException();
        }

        protected override OperationResult<IQuestionScoring> BuildScoring()
        {
            throw new NotImplementedException();
        }

        public MultipleChoiceQuestionEditor(int version) : base(version) { }

        public MultipleChoiceQuestionEditor(MultipleChoiceQuestion question) : base(question) { }
    }
}
