using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation.Questions.Task
{
    /// <summary>
    /// Represents all the images in <see cref="QuestionTask"/>
    /// </summary>
    public class ImageContent : IImageContent
    {
        #region Private Members

        [MaxCollectionCount(maxCount: 5)]
        [MaxCollectionCount(maxCount: 1, fromVersion: 2)]
        [MaxCollectionCount(maxCount: 1, fromVersion: 4)]
        [MaxImageSize(1000, 1000, fromVersion: 1)]
        internal ICollection<Image> Images { get; set; }

        #endregion

        #region Public Methods

        public ICollection<Image> GetAll()
        {
            return Images;
        }

        #endregion

    }
}
