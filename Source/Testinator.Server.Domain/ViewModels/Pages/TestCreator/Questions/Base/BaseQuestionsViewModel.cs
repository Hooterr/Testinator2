using System;
using Testinator.Core;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model that provides bare minimum functionality to every single question type view model
    /// </summary>
    public abstract class BaseQuestionsViewModel<TQuestion, TOptionsEditor, TScoringEditor> : BaseViewModel
    {
        #region Protected Members

        protected readonly ApplicationSettingsViewModel mApplicationSettings;

        /// <summary>
        /// The editor for current question
        /// </summary>
        protected IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> mEditor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The task for this question
        /// </summary>
        public InputField<string> TaskTextContent { get; set; }

        /// <summary>
        /// The points that are given for right answer
        /// </summary>
        public InputField<string> Points { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// Should be called by every question view model
        /// </summary>
        public BaseQuestionsViewModel(ApplicationSettingsViewModel settingsVM)
        {
            // Inject DI services
            mApplicationSettings = settingsVM;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Every question requires this function to be fired as soon as view model is created, to catch the editor for the question
        /// </summary>
        public abstract Func<IQuestion> InitializeEditor(IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> editor);

        /// <summary>
        /// Every question is validated and created by this function
        /// </summary>
        public abstract IQuestion Submit();

        #endregion
    }
}
