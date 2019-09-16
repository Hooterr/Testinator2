using Testinator.TestSystem.Editors.Attributes;

namespace Testinator.TestSystem.Editors
{
    public interface IGradingEditor
    {
        [EditorProperty]
        string Name { get; set; }

    }
}
