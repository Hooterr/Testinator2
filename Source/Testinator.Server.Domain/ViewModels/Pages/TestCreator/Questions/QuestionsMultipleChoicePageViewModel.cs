using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public bool DebugView { get; set; } = false;

        public List<string> MarkupTypes { get; } = new List<string>() { "plain text", "HTML" };

        /// <summary>
        /// The task for this question
        /// </summary>
        public InputField<string> TaskTextContent { get; set; }
        public ErrorCollection TaskErrors { get; set; }
        public ErrorCollection TaskTextErrors { get; set; }
        public ErrorCollection TaskTextContentErrors { get; set; }
        public ErrorCollection TaskTextMarkupErrors { get; set; }
        public ErrorCollection TaskImagesErrors { get; set; }

        /// <summary>
        /// The possible answers for this question as view models
        /// </summary>
        public InputField<ObservableCollection<AnswerSelectableViewModel>> Answers { get; set; }
        public ErrorCollection OptionsErrors { get; set; }
        public ErrorCollection OptionsABCDErrors { get; set; }

        /// <summary>
        /// The points that are given for right answer
        /// </summary>
        public InputField<string> Points { get; set; }
        public ErrorCollection ScoringErrors { get; set; }
        public ErrorCollection ScoringMaxPointsErrors { get; set; }
        public ErrorCollection ScoringCorrectAnswerErrors { get; set; }
        public ErrorCollection General { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to select an answer as the right one
        /// </summary>
        public ICommand SelectAnswerCommand { get; private set; }

        /// <summary>
        /// The command to add new possible answer to the question
        /// </summary>
        public ICommand AddAnswerCommand { get; private set; }

        /// <summary>
        /// The command to remove last answer from the question
        /// </summary>
        public ICommand RemoveAnswerCommand { get; private set; }

        public ICommand ToggleDebugViewCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsMultipleChoicePageViewModel()
        {
            // Create commands
            SelectAnswerCommand = new RelayParameterizedCommand(SelectAnswer);
            AddAnswerCommand = new RelayCommand(AddAnswer);
            RemoveAnswerCommand = new RelayCommand(RemoveAnswer);
            ToggleDebugViewCommand = new RelayCommand(() => DebugView ^= true);
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
            var answerTitle = (char)('A' + answersCount);
            Answers.Value.Add(new AnswerSelectableViewModel
            {
                // Set appropriate title
                Title = answerTitle.ToString()
            });
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
        public Func<IQuestion> InitializeEditor(QuestionEditorMultipleChoice editor)
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
            Answers = mEditor.Options.ABCD.ToAnswerViewModels(mEditor.Options.MinimumCount);
            Points = mEditor.Scoring.MaximumScore.ToString();

            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x.Task.Text.Content, TaskTextContent.ErrorMessages); 
            mEditor.OnErrorFor(x => x.Options.ABCD, Answers.ErrorMessages);
            mEditor.OnErrorFor(x => x.Scoring.MaximumScore, Points.ErrorMessages);

            // DEBUG
            TaskErrors = new ErrorCollection();
            TaskTextErrors = new ErrorCollection();
            TaskTextContentErrors = new ErrorCollection();
            TaskTextMarkupErrors = new ErrorCollection();
            TaskImagesErrors = new ErrorCollection();
            OptionsErrors = new ErrorCollection();
            OptionsABCDErrors = new ErrorCollection();
            ScoringErrors = new ErrorCollection();
            ScoringMaxPointsErrors = new ErrorCollection();
            ScoringCorrectAnswerErrors = new ErrorCollection();
            General = new ErrorCollection();

            //mEditor.OnErrorFor(x => x.Task, TaskErrors);
            //mEditor.OnErrorFor(x => x.Task.Text, TaskTextErrors);
            mEditor.OnErrorFor(x => x.Task.Text.Content, TaskTextContent.ErrorMessages);
            //mEditor.OnErrorFor(x => x.Task.Text.Content, TaskTextContentErrors);
            //mEditor.OnErrorFor(x => x.Task.Text.Markup, TaskTextMarkupErrors);
            //mEditor.OnErrorFor(x => x.Task.Images, TaskImagesErrors);
            //mEditor.OnErrorFor(x => x.Options, OptionsErrors);
            //mEditor.OnErrorFor(x => x.Options.ABCD, OptionsABCDErrors);
            //mEditor.OnErrorFor(x => x.Scoring, ScoringErrors);
            //mEditor.OnErrorFor(x => x.Scoring.MaximumScore, ScoringMaxPointsErrors);
            //mEditor.OnErrorFor(x => x.Scoring.CorrectAnswerIdx, ScoringCorrectAnswerErrors);
            mEditor.OnErrorFor(x => x, General);

            // Return the submit action for the master view model to make use of
            return Submit;
        }

        /// <summary>
        /// Tries to submit the question to the editor
        /// </summary>
        public IQuestion Submit()
        {
            // Clear all the error messages
            // You don't need to clean errors

            // Pass all the changes user has made to the editor
            mEditor.Task.Text.Content = TaskTextContent;
            mEditor.Options.ABCD = Answers.Value.ToOptionsInEditor();
            var rightAnswerIndex = Answers.Value.GetIndexesOfSelected().FirstOrDefault();
            mEditor.Scoring.CorrectAnswerIdx = rightAnswerIndex ?? -1;
            mEditor.Scoring.MaximumScore = int.TryParse(Points.Value, out var maxScore) ? maxScore : -1;

            // Return built question
            return mEditor.BuildQuestionFromEditor();
        }

        #endregion
    }

    public class ErrorHandlerDemo : BaseViewModel, IErrorCollection
    {
        public ObservableCollection<string> Errors { get; set; }

        public ErrorHandlerDemo()
        {
            Errors = new ObservableCollection<string>();
        }

        public void Add(string message)
        {
            Errors.Add(message);
        }

        public void Clear()
        {
            Errors.Clear();
        }
    }

    public class ErrorCollection : ObservableCollection<string>, IErrorCollection
    { 
    }

    public class InputFieldNEW<T> : BaseViewModel
    {
        public T Value { get; set; }

        public ErrorHandlerDemo ErrorHandler { get; set; }

        public ObservableCollection<string> ErrorList => ErrorHandler?.Errors;

        public InputFieldNEW(T value)
        {
            Value = value;
        }

        public static implicit operator T(InputFieldNEW<T> d)
        {
            return d != null ? d.Value : default;
        }

        public static implicit operator InputFieldNEW<T>(T d)
        {
            return new InputFieldNEW<T>(d);
        }

        public override string ToString()
        {
            return Value != null ? Value.ToString() : string.Empty;
        }
    }

}
