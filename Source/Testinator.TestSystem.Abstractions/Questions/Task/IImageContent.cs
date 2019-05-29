using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Testinator.TestSystem.Abstractions.Questions.Task
{
    public interface IImageContent
    {
        // Get rid of the methods that alter the object (delete, add etc.)
        // Constructing the object should be done by a builder
        ICollection<Image> GetAll();
        void DeleteAll();
        void Delete(Image img);
        void DeleteAt(int index);
        bool IsEmpty();
        void Add(Image img);
        int GetMaxCount();
        int GetCurrentCount();
    }
}
