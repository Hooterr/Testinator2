using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Testinator.TestSystem.Abstractions.Questions.Task
{
    public interface IImageContent
    {
        IList<Image> Images { get; }
    }
}
