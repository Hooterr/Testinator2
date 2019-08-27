using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions.Task;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TextEditor : ITextEditor, IEditor<ITextContent>
    {
        private readonly int mVersion;
        private Lazy<TextContent> mTextContent = new Lazy<TextContent>(() => new TextContent());
        private readonly int mMaxTextLength;

        public OperationResult Add(string text, MarkupLanguage markup = MarkupLanguage.PlainText)
        {
            if (text.Length > mMaxTextLength)
                return OperationResult.Fail($"Text content is too long. Maximum text length is set to {mMaxTextLength} characters.");

            mTextContent.Value.Text = text;
            mTextContent.Value.Markup = markup;

            return OperationResult.Success;
        }

        public TextEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version));

            mVersion = version;

            mMaxTextLength = AttributeHelper.GetPropertyAttributeValue<TextContent, string, MaxLenghtAttribute, int>(x => x.Text, a => a.MaxLength, mVersion);
        }

        public void OnValidationError(Action<string> action)
        {
            throw new NotImplementedException();
        }

        public ITextContent Build()
        {
            return mTextContent.IsValueCreated ? mTextContent.Value : null;
        }
    }
}
