using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    internal class SingleTextBoxQuestionOptionsEditor : NestedEditor<SingleTextBoxQuestionOptions, ISingleTextBoxQuestionOptionsEditor>, ISingleTextBoxQuestionOptionsEditor
    {
        public int MaximumCount => 6;

        public int MinimumCount => 1;

        public SingleTextBoxQuestionOptionsEditor(SingleTextBoxQuestionOptions options, int version) : base(options, version) { }
        public SingleTextBoxQuestionOptionsEditor(int version) : base(version) { }
        
        protected override SingleTextBoxQuestionOptions BuildObject()
        {
            return IsInEditorMode() ? OriginalObject : new SingleTextBoxQuestionOptions();
        }

        protected override bool Validate()
        {
            return true;
        }
    }
}
