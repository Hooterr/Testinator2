using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Attributes
{
    public class MaxImageSizeAttribute : BaseEditorAttribute
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public MaxImageSizeAttribute(int height, int width, int fromVersion) : base(fromVersion) 
        {
            Width = width;
            Height = height;
        }
    }
}
