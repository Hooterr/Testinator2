using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    public interface IGradingEditor
    {
        [EditorProperty]
        string Name { get; set; }

    }
}
