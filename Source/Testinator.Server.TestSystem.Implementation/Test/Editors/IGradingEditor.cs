using Testinator.Server.TestSystem.Implementation.Attributes;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IGradingEditor
    {
        [EditorProperty]
        string Name { get; set; }

    }
}
