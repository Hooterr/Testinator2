using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Questions.Task;
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

        public List<string> MarkupTypes { get; } = (Enum.GetValues(typeof(MarkupLanguage)) as MarkupLanguage[]).Select(x => x.ToString()).ToList();

        public InputField<string> Markup { get; set; }

        /// <summary>
        /// The task for this question
        /// </summary>
        public InputField<string> TaskTextContent { get; set; }
        public DebugViewItemViewModel TaskErrors { get; set; }
        public DebugViewItemViewModel TaskTextErrors { get; set; }
        public DebugViewItemViewModel TaskTextContentErrors { get; set; }
        public DebugViewItemViewModel TaskTextMarkupErrors { get; set; }
        public DebugViewItemViewModel TaskImagesErrors { get; set; }

        /// <summary>
        /// The possible answers for this question as view models
        /// </summary>
        public InputField<ObservableCollection<AnswerSelectableViewModel>> Answers { get; set; }
        public DebugViewItemViewModel OptionsErrors { get; set; }
        public DebugViewItemViewModel OptionsABCDErrors { get; set; }

        /// <summary>
        /// The points that are given for right answer
        /// </summary>
        public InputField<string> Points { get; set; }
        public DebugViewItemViewModel ScoringErrors { get; set; }
        public DebugViewItemViewModel ScoringMaxPointsErrors { get; set; }
        public DebugViewItemViewModel ScoringCorrectAnswerErrors { get; set; }
        public DebugViewItemViewModel General { get; set; }

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

        public ICommand ClearAllErrorsCommand { get; private set; }

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

            // DEBUG
            ToggleDebugViewCommand = new RelayCommand(() => DebugView ^= true);
            ClearAllErrorsCommand = new RelayCommand(() =>
            {
                TaskErrors.Clear();
                TaskTextErrors.Clear();
                TaskTextContentErrors.Clear();
                TaskTextMarkupErrors.Clear();
                TaskImagesErrors.Clear();
                OptionsErrors.Clear();
                OptionsABCDErrors.Clear();
                ScoringErrors.Clear();
                ScoringMaxPointsErrors.Clear();
                ScoringCorrectAnswerErrors.Clear();
                General.Clear();
            });
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
            Markup = mEditor.Task.Text.Markup.ToString();
            // Catch all the errors and display them
            //mEditor.OnErrorFor(x => x.Task.Text.Content, TaskTextContent.ErrorMessages); 
            //mEditor.OnErrorFor(x => x.Options.ABCD, Answers.ErrorMessages);
            //mEditor.OnErrorFor(x => x.Scoring.MaximumScore, Points.ErrorMessages);

            // DEBUG
            TaskErrors = new DebugViewItemViewModel(mEditor, x => x.Task, "Task");
            TaskTextErrors = new DebugViewItemViewModel(mEditor, x => x.Task.Text, "Task.Text.");
            TaskTextContentErrors = new DebugViewItemViewModel(mEditor, x => x.Task.Text.Content, "Task.Text.Content");
            TaskTextMarkupErrors = new DebugViewItemViewModel(mEditor, x => x.Task.Text.Markup, "Task.Text.Markup");
            TaskImagesErrors = new DebugViewItemViewModel(mEditor, x => x.Task.Images, "Task.Images");
            OptionsErrors = new DebugViewItemViewModel(mEditor, x => x.Options, "Options");
            OptionsABCDErrors = new DebugViewItemViewModel(mEditor, x => x.Options.ABCD, "Options.ABCD");
            ScoringErrors = new DebugViewItemViewModel(mEditor, x => x.Scoring, "Scoring");
            ScoringMaxPointsErrors = new DebugViewItemViewModel(mEditor, x => x.Scoring.MaximumScore, "Scoring.MaxPoints");
            ScoringCorrectAnswerErrors = new DebugViewItemViewModel(mEditor, x => x.Scoring.CorrectAnswerIdx, "Scoring.CorrestAnswerIdx");
            General = new DebugViewItemViewModel(mEditor, x => x, "General");

            // Return the submit action for the master view model to make use of
            return Submit;
        }

        /// <summary>
        /// Tries to submit the question to the editor
        /// </summary>
        public IQuestion Submit()
        {
            // FOR DEBUG ONLY, DELETE LATER
            ClearAllErrorsCommand.Execute(null);
            
            // Pass all the changes user has made to the editor
            mEditor.Task.Text.Content = TaskTextContent;
            mEditor.Task.Text.Markup = (MarkupLanguage)Enum.Parse(typeof(MarkupLanguage), Markup);
            mEditor.Options.ABCD = Answers.Value.ToOptionsInEditor();
            var rightAnswerIndex = Answers.Value.GetIndexesOfSelected().FirstOrDefault();
            mEditor.Scoring.CorrectAnswerIdx = rightAnswerIndex ?? -1;
            mEditor.Scoring.MaximumScore = int.TryParse(Points.Value, out var maxScore) ? maxScore : -1;

            // Return built question
            return mEditor.BuildQuestionFromEditor();
        }

        #endregion
    }

    // FOR DEBUG, delete later
    public class DebugViewItemViewModel
    {
        public ObservableCollection<string> Errors { get; set; }

        private bool mIsEnabled;

        public bool IsEnabled
        {
            get => mIsEnabled;
            set
            {
                if (value)
                    mEditor.OnErrorFor(mPropertyExpression, Errors);
                else
                    mEditor.OnErrorFor(propertyExpression: mPropertyExpression, handler: null);
                mIsEnabled = value;
            }
        }

        public string PropertyName { get; set; }

        private QuestionEditorMultipleChoice mEditor;
        private Expression<Func<QuestionEditorMultipleChoice, object>> mPropertyExpression;

        public DebugViewItemViewModel(QuestionEditorMultipleChoice editor, Expression<Func<QuestionEditorMultipleChoice, object>> propertyExpression, string name, bool enabled = true)
        {
            mEditor = editor;
            mPropertyExpression = propertyExpression;
            PropertyName = name;
            Errors = new ObservableCollection<string>();
            IsEnabled = enabled;
        }

        public void Clear() => Errors.Clear();
    }
}
