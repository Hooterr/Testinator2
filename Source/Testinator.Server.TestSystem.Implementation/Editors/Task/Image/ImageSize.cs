using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public class ImageSize
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private ImageSize(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new NotSupportedException("Width and height can't be less than 0.");

            Width = width;
            Height = height;
        }

        public static ImageSize FromPixels(int width, int height)
        {
            return new ImageSize(width, height);
        }

    }
}
