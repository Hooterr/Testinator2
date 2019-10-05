using System.Collections.ObjectModel;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model that provides base functionality to the question view models that have multiple answers available
    /// </summary>
    public abstract class BaseQuestionsMultipleAnswersViewModel<TQuestion, TOptionsEditor, TScoringEditor> : BaseQuestionsViewModel<TQuestion, TOptionsEditor, TScoringEditor>
        where TOptionsEditor : IQuestionMultipleAnswersEditor
    {
        #region Public Properties

        /// <summary>
        /// The possible answers for this question as view models
        /// </summary>
        public InputField<ObservableCollection<AnswerSelectableViewModel>> Answers { get; set; }

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
        public BaseQuestionsMultipleAnswersViewModel(ApplicationSettingsViewModel settingsVM) : base(settingsVM)
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
            if (answersCount >= mEditor.Options.MaximumCount)
                return;

            // Add new answer
            Answers.Value.Add(new AnswerSelectableViewModel());
        }

        /// <summary>
        /// Removes last answer from the question
        /// </summary>
        protected void RemoveAnswer()
        {
            // Make sure we can remove the answer and still meet the requirement
            var answersCount = Answers.Value.Count;
            if (answersCount <= mEditor.Options.MinimumCount)
                return;

            // Remove the last answer
            Answers.Value.RemoveAt(answersCount - 1);
        }

        #endregion
    }
}
