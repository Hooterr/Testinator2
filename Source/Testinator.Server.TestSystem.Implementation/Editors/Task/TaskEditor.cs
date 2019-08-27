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

        internal QuestionTask AssembleQuestionContent()
        {
            var textContent = mTextEditor.Build();
            var qt = new QuestionTask
            {
                Images = mImageEditor.AssembleContent(),
                Text = textContent
            };
            return qt;
        }

        public void OnValidationError(Action<string> action)
        {
            throw new NotImplementedException();
        }

        public IQuestionTask Build()
        {
            throw new NotImplementedException();
        }
    }
}