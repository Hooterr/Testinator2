using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions.Task;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TextEditor : ITextEditor
    {
        private readonly int mVersion;
        private TextContent mTextContent;
        private readonly int mMaxTextLength;

        public OperationResult Add(string text, MarkupLanguage markup = MarkupLanguage.PlainText)
        {
            throw new NotImplementedException();
        }

        public TextEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version));

            mVersion = version;

            mMaxTextLength = AttributeHelper.GetPropertyAttributeValue<TextContent, string, MaxLenghtAttribute, int>(x => x.Content, a => a.MaxLength, mVersion);
        }
    }
}
