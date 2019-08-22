using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IEditorBuilder<TEditor, TQuestion>
    {
        TEditor Build();
        IEditorBuilder<TEditor, TQuestion> NewQuestion();
        IEditorBuilder<TEditor, TQuestion> SetInitialQuestion(TQuestion question);
        IEditorBuilder<TEditor, TQuestion> SetVersion(int version);
        IEditorBuilder<TEditor, TQuestion> UseNewestVersion();

    }
}
