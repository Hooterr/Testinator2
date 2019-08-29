using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface ITaskEditor //: IErrorListener
    {
        ITextEditor Text { get; }

        IImageEditor Images { get; }
    }
}
