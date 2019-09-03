using System;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions.Task;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TextEditor : BaseEditor<ITextContent, ITextEditor>, ITextEditor, IBuildable<ITextContent>
    {
        protected int mMaxTextLength;

        public string Content { get; set; }
        public MarkupLanguage Markup { get; set; }


        public TextEditor(int version) : base(version) { }

        public TextEditor(ITextContent objToEdit, int version) : base(objToEdit, version) { }

        // For testing
        protected virtual void LoadAttributeValues()
        {
            mMaxTextLength = AttributeHelper.GetPropertyAttributeValue<TextContent, string, MaxLenghtAttribute, int>(x => x.Text, a => a.MaxLength, Version);
        }

        protected override void OnInitialize()
        {
            LoadAttributeValues();
            if (IsInCreationMode())
            {
                Content = string.Empty;
                Markup = MarkupLanguage.PlainText;
            }
            else
            {
                Content = OriginalObject.Text;
                Markup = OriginalObject.Markup;
            }
        }

        internal override bool Validate()
        {
            var veryficationPassed = true;

            // Verify text
            if (Content.Length > mMaxTextLength)
            {
                HandleErrorFor(x => x.Content, $"Text content is too long. Maximum text length is set to {mMaxTextLength} characters.");
                veryficationPassed = false;
            }
            if (Markup == MarkupLanguage.Html)
            {
                HandleErrorFor(x => x.Markup, $"Not supported yet.");
                veryficationPassed = false;
            }

            return veryficationPassed;
        }

        protected override ITextContent BuildObject()
        {
            var result = new TextContent()
            {
                Text = Content,
                Markup = Markup,
            };

            return result;
        }
    }
}
