using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Attributes
{
    internal class MaxImageSizeAttribute : BaseEditorAttribute
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public MaxImageSizeAttribute(int height, int width)
        {
            Width = width;
            Height = height;
        }

        public MaxImageSizeAttribute(int height, int width, int fromVersion) : base(fromVersion) 
        {
            Width = width;
            Height = height;
        }
    }
}
