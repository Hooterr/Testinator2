using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The interface for every question's content
    /// </summary>
    public interface IQuestionTask
    {
        ITextContent Text { get; set; }
        IImageContent Images { get; set; }
        bool IsEmpty();
    }
}
