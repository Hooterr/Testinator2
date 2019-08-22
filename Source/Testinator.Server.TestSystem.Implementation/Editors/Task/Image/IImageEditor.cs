using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IImageEditor
    {
        OperationResult DeleteAllImages();
        OperationResult DeleteImage(Image img);
        OperationResult DeleteImageAt(int index);
        OperationResult AddImage(Image img);

        int GetMaxCount();
        int GetCurrentCount();
    }
}
