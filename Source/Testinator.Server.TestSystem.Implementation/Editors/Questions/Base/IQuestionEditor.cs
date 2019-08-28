namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Basic interface to edit or create any question
    /// </summary>
    /// <typeparam name="TQuestion">The type of question to edit/create</typeparam>
    public interface IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> : IEditor<TQuestion>
    {
        /// <summary>
        /// Editor for the task of the question
        /// </summary>
        ITaskEditor Task { get; }

        TOptionsEditor Options { get; }

        TScoringEditor Scoring { get; }
    }
}
