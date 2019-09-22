using System;
using System.Linq;
using Testinator.Core;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.Server.Domain
{
    using QuestionEditorMultipleChoice = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    /// <summary>
    /// The view model for multiple choice question page in Test Creator
    /// </summary>
    public class QuestionsMultipleChoicePageViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The editor for multiple choice question
        /// </summary>
        private QuestionEditorMultipleChoice mEditor;

        #endregion

        #region Public Properties

        public InputField<string> Task { get; set; }
        public InputField<string> AnswerA { get; set; }
        public InputField<string> AnswerB { get; set; }
        public InputField<string> AnswerC { get; set; }
        public InputField<string> Points { get; set; }
        public InputField<string> RightAnswer { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this view model with provided editor for this question
        /// </summary>
        /// <param name="editor">The editor for this type of question containing all data</param>
        public Func<IQuestion> InitializeEditor(QuestionEditorMultipleChoice editor)
        {
            // Get the editor itself
            mEditor = editor;

            // Initialize every property based on it
            // If we are editing existing question, editor will have it's data
            // If we are creating new one, editor will be empty but its still fine at this point
            Task = mEditor.Task.Text.Content;
            AnswerA = mEditor.Options.Options.FirstOrDefault();
            Points = mEditor.Scoring.MaximumScore.ToString();
            RightAnswer = mEditor.Scoring.CorrectAnswerIdx.ToString();

            // Return the submit action for the master view model to make use of
            return Submit;
        }

        /// <summary>
        /// Tries to submit the question to the editor
        /// </summary>
        public IQuestion Submit()
        {
            // Pass all the changes user has made to the editor
            mEditor.Task.Text.Content = Task;

            // Validate current state of data
            if (mEditor.Validate())
            {
                // Successful validation, return the question
                return mEditor.Build().Result;
            }

            // Validation failed, return null so the master page will react accordingly
            return null;
        }

        #endregion
    }
}
