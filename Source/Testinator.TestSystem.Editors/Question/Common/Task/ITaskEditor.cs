using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for the question task
    /// </summary>
    public interface ITaskEditor
    {
        /// <summary>
        /// The editor for the text part of the task
        /// </summary>
        [Editor]
        ITextEditor Text { get; }

        /// <summary>
        /// The editor for the images part of the task
        /// </summary>
        [Editor]
        IImageEditor Images { get; }
    }
}
