using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testinator.Server.TestSystem.Implementation;
using Testinator.Server.TestSystem.Implementation.Questions;
using Xunit;

namespace Testinator.TestSystem.Implementation.Test
{
    public class EditorBuilderTests
    {
        [Fact]
        public void AlwaysNewInstance()
        {
            var editor1 = Editors.MultipleChoiceQuestion;
            var editor2 = Editors.MultipleChoiceQuestion;
            Assert.False(editor1.Equals(editor2));
        }

        [Fact]
        public void ChangingVersionThrowsException()
        {
            var builder = Editors.MultipleChoiceQuestion;
            builder.SetInitialQuestion(new MultipleChoiceQuestion()
            {
                Version = 1,
            });
            Assert.Throws<NotSupportedException>(() => builder.SetVersion(2));
        }

        [Fact]
        public void TooHighVersionThrowsException()
        {
            var builder = Editors.MultipleChoiceQuestion;
            var tooHighVersion = Versions.Highest + 1;
            Assert.Throws<ArgumentException>(() => builder.SetVersion(tooHighVersion));
        }

        [Fact]
        public void TooLowVersionThrowsException()
        {
            var builder = Editors.MultipleChoiceQuestion;
            var tooLowVersion = Versions.Lowest - 1;
            Assert.Throws<ArgumentException>(() => builder.SetVersion(tooLowVersion));
        }

        [Fact]
        public void BuildNewQuestionNoMethodsUsed()
        {
            var builder = Editors.MultipleChoiceQuestion;
            var result = builder.Build();
            Assert.NotNull(result);
        }

        [Fact]
        public void BuildNewQuestion()
        {
            var builder = Editors.MultipleChoiceQuestion;
            var result = builder
                .NewQuestion()
                .Build();
            Assert.NotNull(result);
        }

        [Fact]
        public void EditExisting()
        {
            var builder = Editors.MultipleChoiceQuestion;
            var result = builder
                .SetInitialQuestion(new MultipleChoiceQuestion() { Version = 1 })
                .Build();
            Assert.NotNull(result);
        }

        [Fact]
        public void EditNoLongerSupportedQuestionModelVersion()
        {
            var builder = Editors.MultipleChoiceQuestion;
            Assert.Throws<NotSupportedException>(() 
                => builder.SetInitialQuestion(new MultipleChoiceQuestion() { Version = Versions.Lowest - 1 }));   
        }
    }
}
