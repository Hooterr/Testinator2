using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{ 
    public interface ITaskEditor
    {
        OperationResult AddText(string text, MarkupLanguage markup);
        OperationResult DeleteAllImages();
        OperationResult DeleteImage(Image img);
        OperationResult DeleteImageAt(int index);
        OperationResult AddImage(Image img);

        int GetMaxCount();
        int GetCurrentCount();
    }
}
