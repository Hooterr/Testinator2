using System.Drawing;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// The editor for the image part of the question
    /// </summary>
    public interface IImageEditor
    {
        /// <summary>
        /// Deletes all the images from the task
        /// </summary>
        /// <returns>Result of the operation</returns>
        OperationResult DeleteAllImages();

        /// <summary>
        /// Deletes specific image from the list
        /// </summary>
        /// <param name="img">The image to delete</param>
        /// <param name="returnFailIfImageNotFound">If set to true, not finding the image  is considered an error</param>
        /// <returns>Result of the operation</returns>
        OperationResult DeleteImage(Image img, bool returnFailIfImageNotFound = false);

        /// <summary>
        /// Deletes specific image from the list
        /// </summary>
        /// <param name="index">Index of the image to delete</param>
        /// <param name="returnFailIfImageNotFound">If set to true, not finding the image is considered an error</param>
        /// <returns>Result of the operation</returns>
        OperationResult DeleteImageAt(int index, bool returnFailIfImageNotFound = false);

        /// <summary>
        /// Adds an image to the task
        /// </summary>
        /// <param name="img">The image to add</param>
        /// <returns>Result of the operation</returns>
        OperationResult AddImage(Image img);

        /// <summary>
        /// Gets the max count of images that can be added to the task
        /// </summary>
        /// <returns>Maximum number of images that this task can consist of</returns>
        int GetMaxCount();

        /// <summary>
        /// Gets the number of images that currently is added to this task
        /// </summary>
        /// <returns>Current number of images added to this task</returns>
        int GetCurrentCount();
    }
}
