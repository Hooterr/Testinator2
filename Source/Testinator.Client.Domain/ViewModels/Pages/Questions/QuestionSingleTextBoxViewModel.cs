﻿using System.Windows.Input;
using Testinator.Core;

namespace Testinator.Client.Domain
{
    /// <summary>
    /// A viewmodel for <see cref="SingleTextBoxQuestion"/>
    /// </summary>
    public class QuestionSingleTextBoxViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ITestHost mTestHost;

        #endregion

        #region Public Properties

        /// <summary>
        /// The question for this view model to show
        /// </summary>
        public SingleTextBoxQuestion Question { get; set; }

        /// <summary>
        /// The title which shows question id
        /// </summary>
        public string QuestionPageCounter =>
            IsReadOnly ? "Pytanie " + DisplayIndex + " / " + mTestHost.Questions.Count : "Pytanie " + mTestHost.CurrentQuestionString;

        /// <summary>
        /// Current answer for this question
        /// </summary>
        public SingleTextBoxAnswer UserAnswer { get; set; }

        /// <summary>
        /// Current answer written by the user
        /// </summary>
        public string UserAnswerString { get; set; }

        /// <summary>
        /// Sets the visibility of the no-answer warning
        /// When user chooses the answer automatically is set to false
        /// </summary>
        public bool NoAnswerWarning { get; set; } = false;

        /// <summary>
        /// Indicates whether the view should be enabled for changes
        /// ReadOnly mode is used while presenting the result to the user
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Indicates if the answer given by the user is correct
        /// Makes sense only if <see cref="IsReadOnly"/> is set to true
        /// </summary>
        public bool IsAnswerCorrect { get; set; }

        /// <summary>
        /// The index of the viewmodel in a viewmodels list
        /// Usefull to match the click event
        /// Used in ResultPage
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The index of the question displayed on the list
        /// (Just index+1 to start indexing from 1 not from 0)
        /// Makes sense if ReadOnly mode is active
        /// </summary>
        public int DisplayIndex => Index + 1;

        /// <summary>
        /// Indicates if the text field with correct answer should be visible
        /// </summary>
        public bool IsCorrectAnswerVisible => IsReadOnly && !IsAnswerCorrect;

        /// <summary>
        /// The color of the textbox background
        /// Light White if not in readonly mode
        /// Green if answer is correct, red if not
        /// </summary>
        public string TextBoxBackground => IsReadOnly ? (IsAnswerCorrect ? "57A598" : "FF0000" ) : "FEFDFE";

        /// <summary>
        /// The type of this question
        /// </summary>
        public QuestionType Type => QuestionType.SingleTextBox;

        #endregion

        #region Commands

        /// <summary>
        /// Submits the current question and procceds to the next question
        /// </summary>
        public ICommand SubmitCommand { get; set; }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionSingleTextBoxViewModel(ITestHost testHost)
        {
            // Inject DI services
            mTestHost = testHost;

            // Create commands
            SubmitCommand = new RelayCommand(Submit);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Submits the current question
        /// </summary>
        private void Submit()
        {
            // If the textbox is empty show warning to the user
            if (string.IsNullOrWhiteSpace(UserAnswerString))
            {
                NoAnswerWarning = true;
                return;
            }

            // Save the answer
            var answer = new SingleTextBoxAnswer()
            {
                UserAnswer = UserAnswerString,
            };

            UserAnswer = answer;

            mTestHost.SaveAnswer(answer);

            // Go to next question page
            mTestHost.GoNextQuestion();
        }

        #endregion

        #region Public Helpers

        /// <summary>
        /// Adds question this view model will be based on
        /// NOTE: needs to be done before attaching this view model to the page
        /// </summary>
        /// <param name="question">The question to be attached to this viewmodel</param>
        /// <param name="ReadOnly">Indicates if this viewmodel is readonly</param>
        public void AttachQuestion(SingleTextBoxQuestion question, bool ReadOnly = false)
        {
            IsReadOnly = ReadOnly;

            // Save the question
            Question = question;

            if (question.CheckAnswer(UserAnswer) > 0)
                IsAnswerCorrect = true;
        }

        #endregion
    }
}
