namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Basic interface to edit or create any question
    /// </summary>
    /// <typeparam name="TQuestion">The type of question to edit/create</typeparam>
    public interface IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> : IEditor<TQuestion>
    {
        /// <summary>
        /// Editor for the task part of the question
        /// </summary>
        ITaskEditor Task { get; }

        /// <summary>
        /// Editor for the options part of the question
        /// </summary>
        TOptionsEditor Options { get; }

        /// <summary>
        /// Editor for the scoring part of the question
        /// </summary>
        TScoringEditor Scoring { get; }
    }
}
