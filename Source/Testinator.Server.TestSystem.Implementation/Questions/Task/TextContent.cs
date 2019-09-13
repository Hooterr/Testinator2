using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation.Questions.Task
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

        [MaxLenght(maxLenght: 500)]
        [MaxLenght(maxLenght: 400, fromVersion: 2)]
        public string Text { get; internal set; }
    }
}
