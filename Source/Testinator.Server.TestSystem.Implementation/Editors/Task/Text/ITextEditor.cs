using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// The editor for text part of the task
    /// </summary>
    public interface ITextEditor : IErrorListener<ITextEditor>
    {
        /// <summary>
        /// The content of the text part of the task
        /// </summary>
        [EditorProperty]
        string Content { get; set; }

        /// <summary>
        /// The markup the content was encoded with
        /// </summary>
        [EditorProperty]
        MarkupLanguage Markup { get; set; }
    }
}
