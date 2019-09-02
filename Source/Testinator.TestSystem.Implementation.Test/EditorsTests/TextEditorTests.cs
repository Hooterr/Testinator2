using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testinator.Server.TestSystem.Implementation;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.TestSystem.Abstractions.Questions.Task;
using Xunit;

namespace Testinator.TestSystem.Implementation.Test.EditorsTests
{
    public class TextEditorTests
    {
        internal static TextEditor GetEditor(int version = 1) => new TextEditorMock(version);

        [Fact]
        public void ValidateCorrectTextLength()
        {
            var editor = GetEditor();
            editor.Content = "some text";
            var operation = editor.Build();
            Assert.True(operation.Succeeded);
        }

    
        [Fact]
        public void ValidateTooLongText()
        {
            var editor = GetEditor();
            editor.Content = new string('a', 501);
            Assert.False(editor.Build().Succeeded);
        }

        [Fact]
        public void ValidateHtmlNotSupported()
        {
            var editor = GetEditor();
            editor.Markup = MarkupLanguage.Html;
            var operation = editor.Build();
            Assert.False(operation.Succeeded);
            Assert.Contains("Not supported yet.", operation.Errors);
        }


        [Fact]
        public void BuildObjcet()
        {
            var editor = GetEditor();
            editor.Content = "some task";
            editor.Markup = MarkupLanguage.PlainText;
            var operation = editor.Build();
            Assert.True(operation.Succeeded);
            Assert.Equal("some task", operation.Result.Text);
            Assert.Equal(MarkupLanguage.PlainText, operation.Result.Markup);
        }
    }

    internal class TextEditorMock : TextEditor
    {
        protected override void LoadAttributeValues()
        {
            mMaxTextLength = AttributeHelper.GetPropertyAttributeValue<TextContentMock, string, MaxLenghtAttribute, int>(x => x.Text, a => a.MaxLength, Version);
        }
        public TextEditorMock(int version) : base(version)
        {
        }

        public TextEditorMock(ITextContent objToEdit, int version) : base(objToEdit, version)
        {
        }
    }

    public class TextContentMock : ITextContent
    {

        [MaxLenght(maxLenght: 500, fromVersion: 1)]
        public string Text { get; internal set; }

        public MarkupLanguage Markup { get; internal set; }
    }

}
