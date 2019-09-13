using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    /// <summary>
    /// Implementation of the question task
    /// </summary>
    public class QuestionTask : IQuestionTask
    {

        public ITextContent Text { get; internal set; }

        public IImageContent Images { get; internal set; }
    }
}
