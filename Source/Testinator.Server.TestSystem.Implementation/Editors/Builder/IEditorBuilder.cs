using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Builder class that sets up an editor ready to use
    /// </summary>
    /// <typeparam name="TEditor">The type of editor to build</typeparam>
    /// <typeparam name="TQuestion">The type of question the editor is going to operate on</typeparam>
    public interface IEditorBuilder<TEditor, TQuestion>
    {
        /// <summary>
        /// Creates the object using configuration provided
        /// </summary>
        /// <returns>Configured editor</returns>
        TEditor Build();

        /// <summary>
        /// Setup the editor to create a brand new question 
        /// </summary>
        /// <returns>Fluid interface</returns>
        IEditorBuilder<TEditor, TQuestion> NewQuestion();

        /// <summary>
        /// Setup the editor to edit a given question
        /// </summary>
        /// <param name="question">The question to edit. If null calls <see cref="NewQuestion"/></param>
        /// <returns>Fluid interface</returns>
        IEditorBuilder<TEditor, TQuestion> SetInitialQuestion(TQuestion question);

        /// <summary>
        /// Setup the editor to use a given question model version
        /// If the editor has been already setup to edit an existing question this will not change the version of the question
        /// but rather throw an exception
        /// </summary>
        /// <param name="version">Version of the question model to use</param>
        /// <returns>Fluid interface</returns>
        IEditorBuilder<TEditor, TQuestion> SetVersion(int version);

        /// <summary>
        /// Setup the editor to use the newest version of question model
        /// </summary>
        /// <returns>Fluid interface</returns>
        IEditorBuilder<TEditor, TQuestion> UseNewestVersion();    
    }
}
