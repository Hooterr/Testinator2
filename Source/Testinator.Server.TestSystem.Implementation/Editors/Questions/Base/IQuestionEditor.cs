namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Basic interface to edit or create any question
    /// </summary>
    /// <typeparam name="TQuestion">The type of question that to edit/create </typeparam>
    public interface IQuestionEditor<TQuestion>
    {
        /// <summary>
        /// Editor for the task of the question
        /// </summary>
        ITaskEditor Task { get; }

        /// <summary>
        /// Finalize the editing process
        /// </summary>
        /// <returns>The result of the editing process of type <see cref="TQuestion"/></returns>
        TQuestion Build();          
    }
}
