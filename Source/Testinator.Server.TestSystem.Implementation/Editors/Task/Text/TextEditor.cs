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
        private readonly int mMaxTextLength;
        private Action<string> mDisplayError;
        private string mErrorMessgae;
        private string mText;
        private MarkupLanguage mMarkup;

        public void Add(string text, MarkupLanguage markup = MarkupLanguage.PlainText)
        {
            if (text.Length > mMaxTextLength)
            {
                var error = $"Text content is too long. Maximum text length is set to {mMaxTextLength} characters.";

                if (mDisplayError != null)
                    mDisplayError.Invoke(error);
                else
                    mErrorMessgae = error;
            }
            else
            {
                mText = text;
                mMarkup = markup;
                mErrorMessgae = string.Empty;
            }
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
            mDisplayError = action;
        }

        public OperationResult<ITextContent> Build()
        {
            // There are no errors
            if (string.IsNullOrEmpty(mErrorMessgae))
            {
                // We can return the text content
                var result = new TextContent()
                {
                    Text = mText,
                    Markup = mMarkup,
                };

                return OperationResult<ITextContent>.Success(result);
            }
            else
                return OperationResult<ITextContent>.Fail(mErrorMessgae);
        }
    }
}
