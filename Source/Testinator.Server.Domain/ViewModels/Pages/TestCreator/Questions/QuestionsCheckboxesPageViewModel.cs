using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.Server.Domain
{
    using IQuestionEditorMultipleCheckBoxes = IQuestionEditor<MultipleCheckBoxesQuestion, IMultipleCheckBoxesQuestionOptionsEditor, IMultipleCheckBoxesQuestionScoringEditor>;

    /// <summary>
    /// The view model for multiple checkboxes question page in Test Creator
    /// </summary>
    public class QuestionsMultipleCheckBoxesPageViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The editor for multiple choice question
        /// </summary>
        private IQuestionEditorMultipleCheckBoxes mEditor;

        #endregion

        #region Public Propertie

        /// <summary>
        /// The task for this question
        /// </summary>
        public InputField<string> TaskTextContent { get; set; }

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
        /// The command to add new possible answer to the question
        /// </summary>
        public ICommand AddAnswerCommand { get; private set; }

        /// <summary>
        /// The command to remove last answer from the question
        /// </summary>
        public ICommand RemoveAnswerCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsMultipleCheckBoxesPageViewModel()
        {
            // Create commands
            AddAnswerCommand = new RelayCommand(AddAnswer);
            RemoveAnswerCommand = new RelayCommand(RemoveAnswer);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Adds new possible answer to the question
        /// </summary>
        private void AddAnswer()
        {
            // Make sure we don't exceed maximum answer limit
            var answersCount = Answers.Value.Count;
            if (answersCount >= mEditor.Options.MaximumCount)
                return;

            // Add new answer
            Answers.Value.Add(new AnswerSelectableViewModel());
        }

        /// <summary>
        /// Removes last answer from the question
        /// </summary>
        private void RemoveAnswer()
        {
            // Make sure we can remove the answer and still meet the requirement
            var answersCount = Answers.Value.Count;
            if (answersCount <= mEditor.Options.MinimumCount)
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
        public Func<IQuestion> InitializeEditor(IQuestionEditorMultipleCheckBoxes editor)
        {
            // If we don't get valid editor, we can't do anything
            if (editor == null)
                return null;

            // Get the editor itself
            mEditor = editor;

            // Initialize every property based on current editor state
            // If we are editing existing question, editor will have it's data
            // If we are creating new one, editor will be empty but its still fine at this point
            TaskTextContent = mEditor.Task.Text.Content;
            Answers = mEditor.Options.Boxes.ToAnswerViewModels(mEditor.Options.InitialCount);
            Points = mEditor.Scoring.MaximumScore.ToString();

            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x.Task.Text.Content, TaskTextContent.ErrorMessages); 
            mEditor.OnErrorFor(x => x.Options.Boxes, Answers.ErrorMessages);
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
            mEditor.Options.Boxes = Answers.Value.ToOptionsInEditor();
            mEditor.Scoring.MaximumScore = int.TryParse(Points.Value, out var maxScore) ? maxScore : -1;

            // Return built question
            return mEditor.BuildQuestionFromEditor();
        }

        #endregion
    }
}
