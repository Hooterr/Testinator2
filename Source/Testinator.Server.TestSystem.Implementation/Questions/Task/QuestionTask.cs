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
