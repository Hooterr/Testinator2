using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Implementation;

namespace Testinator.Server.Domain
{
    using IQuestionEditorSingleTextBox = IQuestionEditor<SingleTextBoxQuestion, ISingleTextBoxQuestionOptionsEditor, ISingleTextBoxQuestionScoringEditor>;
    using BaseQuestionVM = BaseQuestionsViewModel<SingleTextBoxQuestion, ISingleTextBoxQuestionOptionsEditor, ISingleTextBoxQuestionScoringEditor>;

    /// <summary>
    /// The view model for single text box question page in Test Creator
    /// </summary>
    public class QuestionsSingleTextBoxPageViewModel : BaseQuestionVM
    {
        #region Public Properties

        /// <summary>
        /// The possible answers for this question as simple strings
        /// </summary>
        public InputField<ObservableCollection<string>> Answers { get; set; }

        /// <summary>
        /// Indicates if answers are case sensitive
        /// </summary>
        public bool IsCaseSensitive { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to add new possible answer to the question
        /// </summary>
        public ICommand AddAnswerCommand { get; protected set; }

        /// <summary>
        /// The command to remove last answer from the question
        /// </summary>
        public ICommand RemoveAnswerCommand { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsSingleTextBoxPageViewModel(ApplicationSettingsViewModel settingsVM) : base(settingsVM) 
        {
            // Create default commands
            AddAnswerCommand = new RelayCommand(AddAnswer);
            RemoveAnswerCommand = new RelayCommand(RemoveAnswer);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Adds new possible answer to the question
        /// </summary>
        protected void AddAnswer()
        {
            // Make sure we don't exceed maximum answer limit
            var answersCount = Answers.Value.Count;
            if (answersCount >= mEditor.Scoring.MaximumCount)
                return;

            // Add new answer
            Answers.Value.Add(string.Empty);
        }

        /// <summary>
        /// Removes last answer from the question
        /// </summary>
        protected void RemoveAnswer()
        {
            // Make sure we can remove the answer and still meet the requirement
            var answersCount = Answers.Value.Count;
            if (answersCount <= mEditor.Scoring.MinimumCount)
                return;

            // Remove the last answer
            Answers.Value.RemoveAt(answersCount - 1);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this view model with provided editor for this question
        /// </summary>
        /// <param name="editor">The editor for this type of question containing all data</param>
        public override Func<IQuestion> InitializeEditor(IQuestionEditorSingleTextBox editor)
        {
            // Catch provided editor
            mEditor = editor;

            // Initialize every property based on current editor state
            // If we are editing existing question, editor will have it's data
            // If we are creating new one, editor will be empty but its still fine at this point
            TaskTextContent = mEditor.Task.Text.Content;
            Answers = mEditor.Scoring.CorrectAnswers.Keys.ToStringAnswers(mApplicationSettings.InitialSingleTextBoxAnswersAmount);
            IsCaseSensitive = mEditor.Scoring.IsCaseSensitive;
            Points = mEditor.Scoring.MaximumScore == 0 ? string.Empty : mEditor.Scoring.MaximumScore.ToString();

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
        public override IQuestion Submit()
        {
            // Pass all the changes user has made to the editor
            mEditor.Task.Text.Content = TaskTextContent;
            mEditor.Scoring.CorrectAnswers = Answers.Value.ToRatedAnswers();
            mEditor.Scoring.IsCaseSensitive = IsCaseSensitive;
            mEditor.Scoring.MaximumScore = int.TryParse(Points.Value, out var maxScore) ? maxScore : -1;

            // Return built question
            return mEditor.BuildQuestionFromEditor();
        }

        #endregion
    }
}
