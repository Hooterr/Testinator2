﻿using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation.Questions.Task
{
    /// <summary>
    /// Represents the string content of the question task
    /// </summary>
    public class TextContent : ITextContent
    {
        /// <summary>
        /// The actual content
        /// </summary>
        public string Content { get; internal set; }

        /// <summary>
        /// Describes what markup language has been used
        /// </summary>
        public MarkupLanguage Markup { get; internal set; }

        public MarkupLanguage GetMarkup()
        {
            return Markup;
        }

        public string GetText()
        {
            return Content;
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Content);
        }
    }
}