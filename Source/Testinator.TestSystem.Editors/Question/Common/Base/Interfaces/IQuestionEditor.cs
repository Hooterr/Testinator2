using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Basic interface to edit or create any question
    /// </summary>
    /// <typeparam name="TQuestion">The type of question to edit/create</typeparam>
    public interface IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> : IBuildable<TQuestion>, IErrorListener<IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor>>
    {
        /// <summary>
        /// Editor for the task part of the question
        /// </summary>
        [Editor]
        ITaskEditor Task { get; }

        /// <summary>
        /// Editor for the options part of the question
        /// </summary>
        [Editor]
        TOptionsEditor Options { get; }

        /// <summary>
        /// Editor for the scoring part of the question
        /// </summary>
        [Editor]
        TScoringEditor Scoring { get; }
    }
}
