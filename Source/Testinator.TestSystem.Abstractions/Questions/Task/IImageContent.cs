using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Testinator.TestSystem.Abstractions.Questions.Task
{
    public interface IImageContent
    {
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
