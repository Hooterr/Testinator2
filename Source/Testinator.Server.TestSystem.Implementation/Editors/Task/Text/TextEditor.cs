using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions.Task;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TextEditor : ITextEditor
    {
        private readonly int mVersion;
        private TextContent mTextContent;

        public OperationResult Add(string text, MarkupLanguage markup = MarkupLanguage.PlainText)
        {
            throw new NotImplementedException();
        }

        public TextEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new NullReferenceException(nameof(version));

            mVersion = version;
        }
    }
}
