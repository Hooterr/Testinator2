using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface ITextEditor : IErrorListener<ITextEditor>
    {
        [EditorProperty]
        string Content { get; set; }

        [EditorProperty]
        MarkupLanguage Markup { get; set; }
    }
}
