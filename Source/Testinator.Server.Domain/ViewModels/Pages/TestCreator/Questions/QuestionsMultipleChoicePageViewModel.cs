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
    using IQuestionEditorMultipleChoice = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;
    using BaseQuestionVM = BaseQuestionsMultipleAnswersViewModel<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    /// <summary>
    /// The view model for multiple choice question page in Test Creator
    /// </summary>
    public class QuestionsMultipleChoicePageViewModel : BaseQuestionVM
    {
        #region Debug Stuff

        public bool DebugView { get; set; } = false;

        public List<string> MarkupTypes { get; } = (Enum.GetValues(typeof(MarkupLanguage)) as MarkupLanguage[]).Select(x => x.ToString()).ToList();

        public InputField<string> Markup { get; set; }
        public DebugViewItemViewModel TaskErrors { get; set; }
        public DebugViewItemViewModel TaskTextErrors { get; set; }
        public DebugViewItemViewModel TaskTextContentErrors { get; set; }
        public DebugViewItemViewModel TaskTextMarkupErrors { get; set; }
        public DebugViewItemViewModel TaskImagesErrors { get; set; }
        public DebugViewItemViewModel OptionsErrors { get; set; }
        public DebugViewItemViewModel OptionsABCDErrors { get; set; }
        public DebugViewItemViewModel ScoringErrors { get; set; }
        public DebugViewItemViewModel ScoringMaxPointsErrors { get; set; }
        public DebugViewItemViewModel ScoringCorrectAnswerErrors { get; set; }
        public DebugViewItemViewModel General { get; set; }

        public ICommand ToggleDebugViewCommand { get; private set; }

        public ICommand ClearAllErrorsCommand { get; private set; }

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

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this view model with provided editor for this question
        /// </summary>
        /// <param name="editor">The editor for this type of question containing all data</param>
        public override Func<IQuestion> InitializeEditor(IQuestionEditorMultipleChoice editor)
        {
            // Do base stuff with editor validation and catch the submit action
            base.InitializeEditor(editor);

            // Initialize every property based on current editor state
            // If we are editing existing question, editor will have it's data
            // If we are creating new one, editor will be empty but its still fine at this point
            TaskTextContent = mEditor.Task.Text.Content;
            Answers = mEditor.Options.ABCD.ToAnswerViewModels(mEditor.Options.InitialCount);
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

        private IQuestionEditorMultipleChoice mEditor;
        private Expression<Func<IQuestionEditorMultipleChoice, object>> mPropertyExpression;

        public DebugViewItemViewModel(IQuestionEditorMultipleChoice editor, Expression<Func<IQuestionEditorMultipleChoice, object>> propertyExpression, string name, bool enabled = true)
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
