﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using Testinator.Core;

namespace Testinator.Client.Core
{
    /// <summary>
    /// A viewmodel for <see cref="SingleTextBoxQuestion"/>
    /// </summary>
    public class QuestionSingleTextBoxViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The question this view model is based on
        /// </summary>
        private SingleTextBoxQuestion mQuestion = new SingleTextBoxQuestion();

        #endregion

        #region Public Properties

        /// <summary>
        /// The task of the question
        /// </summary>
        public string Task => mQuestion.Task;

        /// <summary>
        /// Current answer written by the user
        /// </summary>
        public string CurrentAnswer { get; set; }

        /// <summary>
        /// Sets the visibility of the no-answer warning
        /// When user chooses the answer automatically is set to false
        /// </summary>
        public bool NoAnswerWarning { get; set; } = false;

        /// <summary>
        /// Points gained for the correct answer
        /// </summary>
        public int PointScore => mQuestion.PointScore;

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
        public QuestionSingleTextBoxViewModel()
        {
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
            if (string.IsNullOrWhiteSpace(CurrentAnswer))
            {
                NoAnswerWarning = true;
                return;
            }

            var answer = new SingleTextBoxAnswer(mQuestion, CurrentAnswer);
            // TODO: save the answer and show the next question
        }

        #endregion

        #region Public Helpers

        /// <summary>
        /// Adds question this view model will be based on
        /// NOTE: needs to be done before attaching this view model to the page
        /// </summary>
        /// <param name="question">The question to be attached to this viewmodel</param>
        public void AttachQuestion(SingleTextBoxQuestion question)
        {
            // Save the question
            mQuestion = question;
        }

        #endregion
    }
}
