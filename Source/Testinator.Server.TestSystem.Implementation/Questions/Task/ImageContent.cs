using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation.Questions.Task
{
    /// <summary>
    /// Represents all the images in <see cref="QuestionTask"/>
    /// </summary>
    public class ImageContent : IImageContent
    {
        #region Private Members

        private ICollection<Image> mImages;

        #endregion

        public ICollection<Image> GetAll()
        {
            return mImages;
        }
    }
}
