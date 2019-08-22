using System;
using System.Drawing;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TaskEditor : ITaskEditor
{
        private readonly int mVersion;

        private QuestionTask mTask;

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
            mTask = new QuestionTask();
        }
    }
}