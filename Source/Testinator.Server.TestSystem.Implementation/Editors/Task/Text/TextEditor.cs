using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        private Dictionary<string, Action<string>> mErrorHandlers;
        private Dictionary<string, string> mUnHandledErrorMessages;

        public string Content { get; set; }
        public MarkupLanguage Markup { get; set; }


        public TextEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version));

            mVersion = version;

            mMaxTextLength = AttributeHelper.GetPropertyAttributeValue<TextContent, string, MaxLenghtAttribute, int>(x => x.Text, a => a.MaxLength, mVersion);

            mErrorHandlers = new Dictionary<string, Action<string>>();
            mUnHandledErrorMessages = new Dictionary<string, string>();

            // Get all editor methods and create the error handlers map
            var methodNames = typeof(ITextEditor).GetProperties()
                              .Where(field => field.GetCustomAttributes<EditorFieldAttribute>().Any())
                              .Select(field => field.Name)
                              .ToList();

            mErrorHandlers = methodNames.ToDictionary(k => k, v => default(Action<string>));
            mUnHandledErrorMessages = methodNames.ToDictionary(k => k, v => default(string));
        }

        public OperationResult<ITextContent> Build()
        {
            var veryficationPassed = true;

            // Verify text
            if (Content.Length > mMaxTextLength)
            {
                HandleError(x => x.Content, $"Text content is too long. Maximum text length is set to {mMaxTextLength} characters.");
                veryficationPassed = false;
            }
            if(Markup == MarkupLanguage.Html)
            {
                HandleError(x => x.Markup, $"Not supported yet.");
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

                var UnhanledErrorMessages = mUnHandledErrorMessages.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
                result.AddErrors(UnhanledErrorMessages);

                // Clear the errors after that
                mUnHandledErrorMessages.Keys.ToList().ForEach(k => mUnHandledErrorMessages[k] = default);
                return result;
            }
        }

        private void HandleError(Expression<Func<ITextEditor, object>> propertyExpression, string message)
        {
            var propertyName = ExpressionHelpers.GetCorrectPropertyName(propertyExpression);
            // If there is handler for that method
            if (mErrorHandlers[propertyName] != null)
                mErrorHandlers[propertyName].Invoke(message);
            else
                mUnHandledErrorMessages[propertyName] = message;
        }

        public void OnErrorFor(Expression<Func<ITextEditor, object>> propertyExpression, Action<string> action)
        {
            var expression = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (PropertyInfo)expression.Member;

            if (propertyInfo.GetCustomAttributes<EditorFieldAttribute>().Any() == false)
                throw new ArgumentException($"This property is not an editor property.");

            mErrorHandlers[propertyInfo.Name] = action;
        }
    }
}
