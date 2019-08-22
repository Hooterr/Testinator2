using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TaskEditor : ITaskEditor
{ 
        public OperationResult AddImage(Image img)
        {
            throw new NotImplementedException();
        }

        public OperationResult AddText(string text, MarkupLanguage markup)
        {
            throw new NotImplementedException();
        }

        public OperationResult DeleteAllImages()
        {
            throw new NotImplementedException();
        }

        public OperationResult DeleteImage(Image img)
        {
            throw new NotImplementedException();
        }

        public OperationResult DeleteImageAt(int index)
        {
            throw new NotImplementedException();
        }

        public int GetCurrentCount()
        {
            throw new NotImplementedException();
        }

        public int GetMaxCount()
        {
            throw new NotImplementedException();
        }
    }
}
