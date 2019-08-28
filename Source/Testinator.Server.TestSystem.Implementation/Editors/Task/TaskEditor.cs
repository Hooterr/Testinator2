using System;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TaskEditor : ITaskEditor, IEditor<IQuestionTask>
    {
        private readonly int mVersion;
        private readonly TextEditor mTextEditor;
        private readonly ImageEditor mImageEditor;


        public ITextEditor Text => mTextEditor;

        public IImageEditor Images => mImageEditor;

        public TaskEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version));

            mVersion = version;
            mTextEditor = new TextEditor(version);
            mImageEditor = new ImageEditor(version);
        }

        public void OnValidationError(Action<string> action)
        {
            throw new NotImplementedException();
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