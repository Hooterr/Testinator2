using System.Collections.Generic;
using System.Drawing;
using Testinator.TestSystem.Abstractions.Questions.Task;
using Testinator.TestSystem.Editors.Attributes;

namespace Testinator.Server.TestSystem.Implementation.Questions.Task
{
    /// <summary>
    /// Represents all the images in <see cref="QuestionTask"/>
    /// </summary>
    public class ImageContent : IImageContent
    {

        [MaxCollectionCount(maxCount: 5, fromVersion: 1)]
        [MaxCollectionCount(maxCount: 1, fromVersion: 2)]
        [MaxCollectionCount(maxCount: 1, fromVersion: 4)]
        [MaxImageSize(width: 1000, height: 1000, fromVersion: 1)]
        public IList<Image> Images { get; internal set; }

    }
}
