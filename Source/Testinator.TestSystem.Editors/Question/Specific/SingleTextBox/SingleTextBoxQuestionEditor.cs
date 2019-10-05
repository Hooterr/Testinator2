using System;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Implementation;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    [EditorFor(typeof(SingleTextBoxQuestion))]
    internal class SingleTextBoxQuestionEditor : BaseQuestionEditor<SingleTextBoxQuestion, IQuestionOptionsEditor, IQuestionScoringEditor>
    {
        private SingleTextBoxQuestionOptionsEditor mOptions;
        private SingleTextBoxQuestionScoringEditor mScoring;

        public override IQuestionOptionsEditor Options => mOptions;

        public override IQuestionScoringEditor Scoring => mScoring;

        public SingleTextBoxQuestionEditor(SingleTextBoxQuestion question) : base(question) { }

        public SingleTextBoxQuestionEditor(int version) : base(version) { }

        protected override void CreateNestedEditorExistingObject()
        {
            base.CreateNestedEditorExistingObject();
            mOptions = new SingleTextBoxQuestionOptionsEditor(OriginalObject.Options, mVersion);
            mScoring = new SingleTextBoxQuestionScoringEditor(OriginalObject.Scoring, mVersion);
        }

        protected override void CreateNestedEditorsNewObject()
        {
            base.CreateNestedEditorsNewObject();
            mOptions = new SingleTextBoxQuestionOptionsEditor(mVersion);
            mScoring = new SingleTextBoxQuestionScoringEditor(mVersion);
        }

        protected override void OnEditorsCreated()
        {
            base.OnEditorsCreated();
            mOptions.SetInternalErrorHandler(mInternalErrorHandler);
            mOptions.Initialize();
            mScoring.SetInternalErrorHandler(mInternalErrorHandler);
            mScoring.Initialize();
        }

        protected override OperationResult<IQuestionOptions> BuildOptions()
        {
            var originalBuild =  mOptions.Build();
            return OperationResult<IQuestionOptions>.Convert<IQuestionOptions, SingleTextBoxQuestionOptions>(originalBuild);
        }

        protected override OperationResult<IQuestionScoring> BuildScoring()
        {
            var originalBuild = mScoring.Build();
            return OperationResult<IQuestionScoring>.Convert<IQuestionScoring, SingleTextBoxQuestionScoring>(originalBuild);
        }

        protected override bool Validate()
        {
            // Nothing to check
            return true;
        }
    }
}
