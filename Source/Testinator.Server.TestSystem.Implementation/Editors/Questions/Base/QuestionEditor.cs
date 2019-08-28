using System;
using System.Linq;
using System.Reflection;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    public class QuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> : IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor>
        where TQuestion : BaseQuestion, new()//get rid of this new
        where TOptionsEditor : IOptionsEditor
        where TScoringEditor : IQuestionScoringEditor
    {
        /// <summary>
        /// The question we're building/editing
        /// </summary>
        private TQuestion mQuestion;

        private TaskEditor mTask;

        /// <summary>
        /// The editor for the task part of the question
        /// </summary>
        public ITaskEditor Task => mTask;

        public TOptionsEditor Options { get; private set; }

        public TScoringEditor Scoring { get; private set; }

        public OperationResult<TQuestion> Build()
        {
            var taskBuildOperation = mTask.Build();
            if (taskBuildOperation.Failed)
            {
                var questionBuildResult = OperationResult<TQuestion>.Fail();
                questionBuildResult.Merge(taskBuildOperation);
                return questionBuildResult;
            }

            mQuestion.Task = taskBuildOperation.Result;

            return OperationResult<TQuestion>.Success(mQuestion);
        }

        #region All Constructors 

        /// <summary>
        /// Always forward initialization to this constructor
        /// </summary>
        private QuestionEditor()
        {
            // Create task editor
            mTask = new TaskEditor(mQuestion.Version);

            // TODO move this somewhere 
            var optionsEditorImplementations = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.GetCustomAttributes(typeof(ImplementedInterfaceAttribute), false).Any())
                .Select(type => new
                {
                    Type = type,
                    Attribute = (ImplementedInterfaceAttribute)type.GetCustomAttribute(typeof(ImplementedInterfaceAttribute), false)
                })
                .Where(x => x.Attribute.ImplementedInterface == typeof(TOptionsEditor));

            if (optionsEditorImplementations.Count() == 0)
                throw new NotSupportedException($"{typeof(TOptionsEditor).Name} is not implemented!");

            if (optionsEditorImplementations.Count() > 1)
                throw new AmbiguousMatchException($"Multiple implementations of {typeof(TOptionsEditor).Name} were found");

            var concreteOptionsEditorType = optionsEditorImplementations.First().Type;

            Options = (TOptionsEditor)Activator.CreateInstance(concreteOptionsEditorType, mQuestion.Version);
        }

        /// <summary>
        /// Setups up the editor to edit an existing question
        /// </summary>
        /// <param name="question">Question to edit. Passing null will not create a new question but rather throw an exception</param>
        internal QuestionEditor(TQuestion question) : this()
        {
#pragma warning disable IDE0016 // Use 'throw' expression
            if (question == null)
                throw new ArgumentNullException(nameof(question),
                    "When editing cannot use a null question. If you want to create a new question call the constructor that accepts question model version.");
#pragma warning restore IDE0016 // Use 'throw' expression

            mQuestion = question;
        }

        /// <summary>
        /// Setups up the editor to create a new question using specific version number
        /// </summary>
        /// <param name="version">The version number to use. Must be from within the supported version numbers</param>
        internal QuestionEditor(int version) : this()
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version), "Version must be from within the range.");

            // Set up a new question

            // TODO use a factory method
            mQuestion = new TQuestion()
            {
                Version = version
            };
        }

        #endregion
    }
}
