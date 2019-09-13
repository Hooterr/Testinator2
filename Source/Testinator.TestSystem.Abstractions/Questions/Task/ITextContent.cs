using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions.Questions.Task
{
    public interface ITextContent
    {
        string Text { get; }
        MarkupLanguage Markup { get; }
    }
}
