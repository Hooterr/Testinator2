using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface ITextEditor
    {
        OperationResult Add(string text, MarkupLanguage markup = MarkupLanguage.PlainText);
    }
}
