using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public InputField<ObservableCollection<AnswerSelectableViewModel>> Answers { get; set; }
        public InputField<string> Points { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this view model with provided editor for this question
        /// </summary>
        /// <param name="editor">The editor for this type of question containing all data</param>
        public Func<IQuestion> InitializeEditor(QuestionEditorMultipleChoice editor)
        {
            // If we don't get valid editor, we can't do anything
            if (editor == null)
                return null;

            // Get the editor itself
            mEditor = editor;

            // Initialize every property based on it
            // If we are editing existing question, editor will have it's data
            // If we are creating new one, editor will be empty but its still fine at this point
            Task = mEditor.Task.Text.Content;
            Answers = mEditor.Options.ABCD.ToAnswerViewModels();
            Points = mEditor.Scoring.MaximumScore.ToString();

            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x.Task.Text.Content, (e) => Task.ErrorMessage = e); 
            mEditor.OnErrorFor(x => x.Options.ABCD, (e) => Answers.ErrorMessage = e);
            mEditor.OnErrorFor(x => x.Scoring.MaximumScore, (e) => Points.ErrorMessage = e);

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
            mEditor.Options.ABCD = Answers.Value.ToOptionsInEditor();

            // Return built question
            return mEditor.BuildQuestionFromEditor();
        }

        #endregion
    }
}
