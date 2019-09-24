using System;
using Testinator.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Implementation of the task editor
    /// </summary>
    internal class TaskEditor : BaseEditor<IQuestionTask, ITaskEditor>, ITaskEditor, IBuildable<IQuestionTask>
    {
        #region Private Members

        /// <summary>
        /// Concrete text editor
        /// </summary>
        private TextEditor mTextEditor;

        /// <summary>
        /// Concrete images editor
        /// </summary>
        private ImageEditor mImageEditor;

        #endregion

        #region Public Properties

        public ITextEditor Text => mTextEditor;

        public IImageEditor Images => mImageEditor;

        #endregion

        #region All Constructors

        /// <summary>
        /// Initializes task editor to create a new task 
        /// </summary>
        /// <param name="version">The question model version to use</param>
        public TaskEditor(int version) : base(version)
        {
            mTextEditor = new TextEditor(Version);
            mImageEditor = new ImageEditor(Version);
        }

        /// <summary>
        /// Initializes task editor to edit an existing task
        /// </summary>
        /// <param name="objToEdit">The task to edit</param>
        /// <param name="version">The question model version to use</param>
        public TaskEditor(IQuestionTask objToEdit, int version) : base(objToEdit, version)
        {
            mTextEditor = new TextEditor(OriginalObject.Text, Version);
            mImageEditor = new ImageEditor(OriginalObject.Images, Version);
        }

        #endregion

        #region Overridden Methods

        protected override void CreateHandlers(IInternalErrorHandler handler)
        {
            mTextEditor.AttachErrorHandler(handler, nameof(Text));
            mImageEditor.AttachErrorHandler(handler, nameof(Images));
        }

        protected override bool Validate()
        {
            var validationPassed = true;

            if (string.IsNullOrEmpty(mTextEditor.Content) && mImageEditor.GetCurrentCount() == 0)
            {
                mErrorHandlerAdapter.HandleErrorFor(x => x, "Both text content and images cannot be empty.");
                validationPassed = false;
            }
            return validationPassed;
        }

        protected override IQuestionTask BuildObject()
        {
            // This method it no used, since we need to override the build method below
            throw new NotImplementedException("This method is not used.");
        }

        public override OperationResult<IQuestionTask> Build()
        {
           // ClearAllErrors();
            var textOperation = mTextEditor.Build();
            var imageOperation = mImageEditor.Build();

            // Check if all the builds were successful
            if (Helpers.AnyTrue(textOperation.Failed, imageOperation.Failed))
            {
                // Combine the error messages
                var result = OperationResult<IQuestionTask>.Fail()
                    .Merge(textOperation)
                    .Merge(imageOperation);

                return result;
            }
            else
            {
                // Both builds succeeded
                // Do the final validation
                if (Validate())
                {
                    // All good, create the task

                    IQuestionTask task;

                    if (IsInEditorMode())
                    {
                        task = OriginalObject;
                    }
                    else
                    {
                        task = new QuestionTask()
                        {
                            Text = textOperation.Result,
                            Images = imageOperation.Result,
                        };
                    }

                    return OperationResult<IQuestionTask>.Success(task);
                }
                else
                {
                    // Validation failed
                    return OperationResult<IQuestionTask>.Fail();
                }
            }
        } 

        #endregion
    }
}