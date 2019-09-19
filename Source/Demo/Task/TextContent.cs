using System;
using Testinator.TestSystem.Abstractions.Questions.Task;
using Testinator.TestSystem.Attributes;

namespace Demo.Task
{
    /// <summary>
    /// Represents the string content of the question task
    /// </summary>
    [Serializable]
    public class TextContent : ITextContent
    {

        /// <summary>
        /// Describes what markup language has been used
        /// </summary>
        public MarkupLanguage Markup { get; internal set; }
        public string Text { get; internal set; }
    }
}
