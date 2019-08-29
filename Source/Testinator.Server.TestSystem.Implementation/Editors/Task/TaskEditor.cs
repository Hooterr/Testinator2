using System;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TaskEditor : BaseEditor<IQuestionTask, ITaskEditor>, ITaskEditor, IEditor<IQuestionTask>
    {
        private TextEditor mTextEditor;
        private ImageEditor mImageEditor;


        public ITextEditor Text => mTextEditor;

        public IImageEditor Images => mImageEditor;

        public TaskEditor(int version) : base(version) { }

        public TaskEditor(IQuestionTask objToEdit, int version) : base(objToEdit, version) { }

        protected override void OnInitialize()
        {
            if (OriginalObject == null)
            {
                mTextEditor = new TextEditor(Version);
                mImageEditor = new ImageEditor(Version);
            }
            else
            {
                mTextEditor = new TextEditor(OriginalObject.Text, Version);
                mImageEditor = new ImageEditor(OriginalObject.Images, Version);
            }
        }

        public OperationResult<IQuestionTask> Build()
        {
            var textOperation = mTextEditor.Build();
            var imageOperation = mImageEditor.Build();

            // If one of the builds failed
            if(textOperation.Failed || imageOperation.Failed)
            {
                // Combine the error messages
                var taskBuildOperation = OperationResult<IQuestionTask>.Fail();
                taskBuildOperation.Merge(textOperation);
                taskBuildOperation.Merge(imageOperation);
                return taskBuildOperation;
            }

            var task = new QuestionTask()
            {
                Text = textOperation.Result,
                Images = imageOperation.Result,
            };

            return OperationResult<IQuestionTask>.Success(task);
        }
    }
}