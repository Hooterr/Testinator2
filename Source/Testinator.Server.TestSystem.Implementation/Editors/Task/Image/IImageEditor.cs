using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IImageEditor : IValidatable
    {
        OperationResult DeleteAllImages();
        OperationResult DeleteImage(Image img, bool returnFailIfImageNotFound = false);
        OperationResult DeleteImageAt(int index, bool returnFailIfImageNotFound = false);
        OperationResult AddImage(Image img);
        int GetMaxCount();
        int GetCurrentCount();
    }
}
