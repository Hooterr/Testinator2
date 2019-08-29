using System;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions.Task;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TextEditor : BaseErrorListener<ITextEditor>, ITextEditor, IEditor<ITextContent>
    {
        private readonly int mVersion;
        private readonly int mMaxTextLength;

        public string Content { get; set; }
        public MarkupLanguage Markup { get; set; }


        public TextEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version));

            mVersion = version;

            mMaxTextLength = AttributeHelper.GetPropertyAttributeValue<TextContent, string, MaxLenghtAttribute, int>(x => x.Text, a => a.MaxLength, mVersion);
        }

        public OperationResult<ITextContent> Build()
        {
            var veryficationPassed = true;

            // Verify text
            if (Content.Length > mMaxTextLength)
            {
                HandleErrorFor(x => x.Content, $"Text content is too long. Maximum text length is set to {mMaxTextLength} characters.");
                veryficationPassed = false;
            }
            if(Markup == MarkupLanguage.Html)
            {
                HandleErrorFor(x => x.Markup, $"Not supported yet.");
                veryficationPassed = false;
            }

            if (veryficationPassed)
            {
                // All good, we can create the text content
                var result = new TextContent()
                {
                    Text = Content,
                    Markup = Markup,
                };

                return OperationResult<ITextContent>.Success(result);
            }
            else
            { 
                var result = OperationResult<ITextContent>.Fail();

                // Error messages that were not handled by callback are added to the
                // operation result list of errors
                var unhanledErrorMessages = GetUnhandledErrors();
                result.AddErrors(unhanledErrorMessages);

                ClearAllErrors();

                return result;
            }
        }
    }
}
