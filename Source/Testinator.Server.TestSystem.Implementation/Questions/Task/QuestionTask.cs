using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    /// <summary>
    /// Implementation of the question task
    /// </summary>
    public class QuestionTask : IQuestionTask
    {
        #region Private Members

        private ITextContent mText;
        private IImageContent mImages;

        #endregion

        public ITextContent Text
        {
            get => mText;
            set => mText = value;
        }

        public IImageContent Images
        {
            get => mImages;
            set => mImages = value;
        }

        public bool IsEmpty()
        {
            var isEmpty = false;
            isEmpty |= Text == null || Text.IsEmpty();
            isEmpty |= mImages == null || mImages.IsEmpty();

            return isEmpty;
        }
    }
}
