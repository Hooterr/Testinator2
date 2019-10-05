using System;
using Testinator.Core;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Implementation;

namespace Testinator.Server.Domain
{
    using IQuestionEditorSingleTextBox = IQuestionEditor<SingleTextBoxQuestion, ISingleTextBoxQuestionOptionsEditor, ISingleTextBoxQuestionScoringEditor>;
    using BaseQuestionVM = BaseQuestionsMultipleAnswersViewModel<SingleTextBoxQuestion, ISingleTextBoxQuestionOptionsEditor, ISingleTextBoxQuestionScoringEditor>;

    /// <summary>
    /// The view model for single text box question page in Test Creator
    /// </summary>
    public class QuestionsSingleTextBoxPageViewModel : BaseQuestionVM
    {
        #region Public Properties

        /// <summary>
        /// Indicates if answers are case sensitive
        /// </summary>
        public bool IsCaseSensitive { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsSingleTextBoxPageViewModel(ApplicationSettingsViewModel settingsVM) : base(settingsVM) { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this view model with provided editor for this question
        /// </summary>
        /// <param name="editor">The editor for this type of question containing all data</param>
        public override Func<IQuestion> InitializeEditor(IQuestionEditorSingleTextBox editor)
        {
            // Do base stuff with editor validation and catch the submit action
            base.InitializeEditor(editor);

            // Initialize every property based on current editor state
            // If we are editing existing question, editor will have it's data
            // If we are creating new one, editor will be empty but its still fine at this point
            TaskTextContent = mEditor.Task.Text.Content;
            Answers = mEditor.Scoring.CorrectAnswers.Keys.ToAnswerViewModels(mApplicationSettings.InitialSingleTextBoxAnswersAmount);
            IsCaseSensitive = mEditor.Scoring.IsCaseSensitive;
            Points = mEditor.Scoring.MaximumScore.ToString();

            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x.Task.Text.Content, TaskTextContent.ErrorMessages); 
            mEditor.OnErrorFor(x => x.Scoring.CorrectAnswers, Answers.ErrorMessages);
            mEditor.OnErrorFor(x => x.Scoring.MaximumScore, Points.ErrorMessages);

            // Return the submit action for the master view model to make use of
            return Submit;
        }

        /// <summary>
        /// Tries to submit the question to the editor
        /// </summary>
        public IQuestion Submit()
        {
            // Pass all the changes user has made to the editor
            mEditor.Task.Text.Content = TaskTextContent;
            mEditor.Scoring.CorrectAnswers = Answers.Value.ToOptionsInEditor().ToRatedAnswers();
            mEditor.Scoring.IsCaseSensitive = IsCaseSensitive;
            mEditor.Scoring.MaximumScore = int.TryParse(Points.Value, out var maxScore) ? maxScore : -1;

            // Return built question
            return mEditor.BuildQuestionFromEditor();
        }

        #endregion
    }
}
