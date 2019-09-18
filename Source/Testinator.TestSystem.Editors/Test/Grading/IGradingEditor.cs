using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for grading for the test
    /// </summary>
    public interface IGradingEditor : IErrorListener<IGradingEditor>
    {
        /// <summary>
        /// The name of this grading preset
        /// </summary>
        [EditorProperty]
        string Name { get; set; }

    }
}
