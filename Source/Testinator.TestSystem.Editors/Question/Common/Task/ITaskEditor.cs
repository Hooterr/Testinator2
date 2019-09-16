using Testinator.TestSystem.Abstractions;

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
        ITextEditor Text { get; }

        /// <summary>
        /// The editor for the images part of the task
        /// </summary>
        IImageEditor Images { get; }
    }
}
