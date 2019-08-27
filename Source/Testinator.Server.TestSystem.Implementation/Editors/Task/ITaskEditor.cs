using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface ITaskEditor : IValidatable
    {
        ITextEditor Text { get; }

        IImageEditor Images { get; }
    }
}
