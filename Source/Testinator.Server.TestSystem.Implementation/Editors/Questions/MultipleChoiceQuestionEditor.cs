using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    [ConcreteEditorFor(typeof(MultipleChoiceQuestion))]
    internal class MultipleChoiceQuestionEditor : BaseQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IQuestionScoringEditor>
    {
        private MultipleChoiceQuestionOptionsEditor mOptionsEditor;


        public override IMultipleChoiceQuestionOptionsEditor Options => mOptionsEditor;

        public override IQuestionScoringEditor Scoring => throw new NotImplementedException();

        protected override void InitializeEditor()
        {
            mOptionsEditor = new MultipleChoiceQuestionOptionsEditor();
        }

        public MultipleChoiceQuestionEditor(int version) : base(version) { }

        public MultipleChoiceQuestionEditor(MultipleChoiceQuestion question) : base(question) { }
    }
}
