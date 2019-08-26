using System.Collections.Generic;
using System.Drawing;
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
        [MaxImageSize(width: 1000, height: 1000, fromVersion: 1)]
        public IList<Image> Images { get; internal set; }

        #endregion

    }
}
