using System;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Defines the size of an image
    /// </summary>
    public class ImageSize
    {
        #region Public Properties

        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Length
        /// </summary>
        public int Height { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the image size from pixels
        /// </summary>
        /// <param name="width">The width in pixels</param>
        /// <param name="height">The height in pixels</param>
        /// <returns>A new image size object</returns>
        public static ImageSize FromPixels(int width, int height)
        {
            return new ImageSize(width, height);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the object
        /// </summary>
        /// <param name="width">The width of the image</param>
        /// <param name="height">The height of the object</param>
        public ImageSize(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new NotSupportedException("Width and height can't be less than 0.");

            Width = width;
            Height = height;
        }

        #endregion
    }
}
