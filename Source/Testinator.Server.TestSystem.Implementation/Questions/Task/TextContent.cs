using Testinator.TestSystem.Abstractions.Questions.Task;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Implementation.Questions.Task
{
    /// <summary>
    /// Represents the string content of the question task
    /// </summary>
    public class TextContent : ITextContent
    {

        /// <summary>
        /// Describes what markup language has been used
        /// </summary>
        public MarkupLanguage Markup { get; internal set; } = MarkupLanguage.PlainText;

        [MaxLenght(maxLenght: 500, fromVersion: 1)]
        [MaxLenght(maxLenght: 400, fromVersion: 2)]
        public string Text { get; internal set; }
    }
}
