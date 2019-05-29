using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions.Task;
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
        private TextContent mText;
        private ImageContent mImages;

        #endregion

        public ITextContent Text => mText;

        public IImageContent Images => mImages;

        public bool IsEmpty()
        {
            var isEmpty = true;
            isEmpty |= Text == null || Text.IsEmpty();
            isEmpty |= mImages == null || mImages.IsEmpty();

            return isEmpty;
        }
        
        internal void SetTextContent(TextContent content)
        {
            mText = content;
        }

        internal void SetImageContent(ImageContent content)
        {
            mImages = content;
        }
    }
}
