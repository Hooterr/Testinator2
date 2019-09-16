using System;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Editors;
using Xunit;

namespace Testinator.TestSystem.Implementation.Test
{
    public class EditorBuilderTests
    {
        [Fact]
        public void AlwaysNewInstance()
        {
            var editor1 = AllEditors.MultipleChoiceQuestion;
            var editor2 = AllEditors.MultipleChoiceQuestion;
            Assert.False(editor1.Equals(editor2));
        }

        [Fact]
        public void ChangingVersionThrowsException()
        {
            var builder = AllEditors.MultipleChoiceQuestion;
            builder.SetInitialQuestion(new MultipleChoiceQuestion()
            {
                Version = 1,
            });
            Assert.Throws<NotSupportedException>(() => builder.SetVersion(2));
        }

        [Fact]
        public void TooHighVersionThrowsException()
        {
            var builder = AllEditors.MultipleChoiceQuestion;
            var tooHighVersion = Versions.Highest + 1;
            Assert.Throws<ArgumentException>(() => builder.SetVersion(tooHighVersion));
        }

        [Fact]
        public void TooLowVersionThrowsException()
        {
            var builder = AllEditors.MultipleChoiceQuestion;
            var tooLowVersion = Versions.Lowest - 1;
            Assert.Throws<ArgumentException>(() => builder.SetVersion(tooLowVersion));
        }

        [Fact]
        public void BuildNewQuestionNoMethodsUsed()
        {
            var builder = AllEditors.MultipleChoiceQuestion;
            var result = builder.Build();
            Assert.NotNull(result);
        }

        [Fact]
        public void BuildNewQuestion()
        {
            var builder = AllEditors.MultipleChoiceQuestion;
            var result = builder
                .NewQuestion()
                .Build();
            Assert.NotNull(result);
        }

        [Fact]
        public void EditExisting()
        {
            var builder = AllEditors.MultipleChoiceQuestion;
            var result = builder
                .SetInitialQuestion(new MultipleChoiceQuestion() { Version = 1 })
                .Build();
            Assert.NotNull(result);
        }

        [Fact]
        public void EditNoLongerSupportedQuestionModelVersion()
        {
            var builder = AllEditors.MultipleChoiceQuestion;
            Assert.Throws<NotSupportedException>(() 
                => builder.SetInitialQuestion(new MultipleChoiceQuestion() { Version = Versions.Lowest - 1 }));   
        }
    }
}
