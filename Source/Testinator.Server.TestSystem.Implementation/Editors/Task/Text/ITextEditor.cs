using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface ITextEditor : IErrorListener<ITextEditor>
    {
        [EditorField]
        string Content { get; set; }

        [EditorField]
        MarkupLanguage Markup { get; set; }
    }
}
