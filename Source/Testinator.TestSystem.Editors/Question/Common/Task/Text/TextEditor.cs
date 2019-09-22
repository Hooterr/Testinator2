using Testinator.TestSystem.Implementation.Questions.Task;
using Testinator.TestSystem.Abstractions.Questions.Task;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The implementation of the text part of the question task
    /// </summary>
    internal class TextEditor : BaseEditor<ITextContent, ITextEditor>, ITextEditor, IBuildable<ITextContent>
    {
        #region Protected Members

        /// <summary>
        /// Maximum text length for the content
        /// </summary>
        protected int mMaxTextLength;

        #endregion

        #region Public Properties

        /// <summary>
        /// The text content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The markup the content was encoded with
        /// </summary>
        public MarkupLanguage Markup { get; set; }

        #endregion

        #region All Constructors

        /// <summary>
        /// Initializes the editor to create new text content
        /// </summary>
        /// <param name="version">The question model version to use</param>
        public TextEditor(int version, IInternalErrorHandler errorHandler) : base(version, errorHandler) { }

        /// <summary>
        /// Initializes the editor to edit an existing text content
        /// </summary>
        /// <param name="objToEdit">The text content to edit</param>
        /// <param name="version">The question model version to use</param>
        public TextEditor(ITextContent objToEdit, int version, IInternalErrorHandler errorHandler) : base(objToEdit, version, errorHandler) { }

        #endregion

        // For testing
        protected virtual void LoadAttributeValues()
        {
            mMaxTextLength = AttributeHelper.GetPropertyAttributeValue<TextContent, string, MaxLenghtAttribute, int>(x => x.Text, a => a.MaxLength, Version);
        }

        #region Overridden Methods

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

        protected override bool Validate()
        {
            var veryficationPassed = true;

            if(string.IsNullOrEmpty(Content))
            {
                mErrorHandlerAdapter.HandleErrorFor(x => x.Content, $"Text content cannot be empty.");
                veryficationPassed = false;
            }
            else if ((Content.Length > mMaxTextLength))
            {
                mErrorHandlerAdapter.HandleErrorFor(x => x.Content, $"Text content is too long. Maximum text length is set to {mMaxTextLength} characters.");
                veryficationPassed = false;
            }

            if (Markup == MarkupLanguage.Html)
            {
                mErrorHandlerAdapter.HandleErrorFor(x => x.Markup, $"Not supported yet.");
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

        #endregion
    }
}
