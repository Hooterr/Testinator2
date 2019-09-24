﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
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

        /// <summary>
        /// The task for this question
        /// </summary>
        public InputField<string> Task { get; set; }

        /// <summary>
        /// The possible answers for this question as view models
        /// </summary>
        public InputField<ObservableCollection<AnswerSelectableViewModel>> Answers { get; set; }

        /// <summary>
        /// The points that are given for right answer
        /// </summary>
        public InputField<string> Points { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to select an answer as the right one
        /// </summary>
        public ICommand SelectAnswerCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsMultipleChoicePageViewModel()
        {
            // Create commands
            SelectAnswerCommand = new RelayParameterizedCommand(SelectAnswer);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Selects an answer as the right one
        /// </summary>
        /// <param name="param">The selected answer view model</param>
        private void SelectAnswer(object param)
        {
            // Get the actual view model from parameter
            var viewModel = param as AnswerSelectableViewModel;

            // Deselect any other answer before-hand
            foreach (var answer in Answers.Value)
                answer.IsSelected = false;

            // Mark provided answer as selected
            viewModel.IsSelected = true;
        }

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
            mEditor.Scoring.CorrectAnswerIdx = Answers.Value.GetIndexOfSelected();
            mEditor.Scoring.MaximumScore = int.Parse(Points);

            // Return built question
            return mEditor.BuildQuestionFromEditor();
        }

        #endregion
    }
}
