using Testinator.TestSystem.Implementation.Questions.Task;
using Testinator.TestSystem.Abstractions.Questions.Task;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The implementation of the text part of the question task
    /// </summary>
    internal class TextEditor : NestedEditor<ITextContent, ITextEditor>, ITextEditor, IBuildable<ITextContent>
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
        public TextEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes the editor to edit an existing text content
        /// </summary>
        /// <param name="objToEdit">The text content to edit</param>
        /// <param name="version">The question model version to use</param>
        public TextEditor(ITextContent objToEdit, int version) : base(objToEdit, version) { }

        #endregion

        protected virtual void LoadAttributeValues()
        {
            mMaxTextLength = AttributeHelper.GetPropertyAttributeValue<TextContent, string, MaxLenghtAttribute, int>(x => x.Text, a => a.MaxLength, mVersion);
        }

        #region Overridden Methods

        protected override void OnInitialize()
        {
            LoadAttributeValues();
        }

        protected override bool Validate()
        {
            var veryficationPassed = true;

            if(string.IsNullOrEmpty(Content))
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.Content, $"Text content cannot be empty.");
                veryficationPassed = false;
            }
            else if ((Content.Length > mMaxTextLength))
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.Content, $"Text content is too long. Maximum text length is set to {mMaxTextLength} characters.");
                veryficationPassed = false;
            }

            if (Markup != MarkupLanguage.PlainText)
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.Markup, $"Any markup other than plain text it not supported yet.");
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

        protected override void InitializeCreateNewObject()
        {
            Content = string.Empty;
            Markup = MarkupLanguage.PlainText;
        }

        protected override void InitializeEditExistingObject()
        {
            Content = OriginalObject.Text;
            Markup = OriginalObject.Markup;
        }

        #endregion
    }
}
