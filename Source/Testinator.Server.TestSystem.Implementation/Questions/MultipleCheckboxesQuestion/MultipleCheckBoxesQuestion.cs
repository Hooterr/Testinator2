namespace Testinator.TestSystem.Implementation.Questions
{
    [Serializable]
    public sealed class MultipleCheckBoxesQuestion : BaseQuestion
    {
        public new MultipleCheckBoxesQuestionScoring Scoring { get; internal set; }
        public new MultipleCheckBoxesQuestionOptions Options { get; internal set; }
    }

}
