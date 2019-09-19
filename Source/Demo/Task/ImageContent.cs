using System;
using System.Collections.Generic;
using System.Drawing;
using Testinator.TestSystem.Abstractions.Questions.Task;
using Testinator.TestSystem.Attributes;

namespace Demo.Task
{
    /// <summary>
    /// Represents all the images in <see cref="QuestionTask"/>
    /// </summary>
    [Serializable]
    public class ImageContent
    {
        public IList<Image> Images { get; internal set; }

    }
}
