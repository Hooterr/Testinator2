using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation.Questions.Task
{
    /// <summary>
    /// Represents all the images in <see cref="QuestionTask"/>
    /// </summary>
    public class ImageContent : IImageContent
    {
        #region Private Members

        private ICollection<Image> mImages;
        private const int mMaxImageCount = 5;
        #endregion


        public ICollection<Image> GetAll()
        {
            return mImages;
        }

        public void DeleteAll()
        {
            mImages = new List<Image>();
        }

        public void Delete(Image img)
        {
            mImages.Remove(img);
        }

        public void DeleteAt(int index)
        {
            throw new NotSupportedException();
        }

        public void Add(Image img)
        {
            if (mImages == null)
                mImages = new List<Image>();

            mImages.Add(img);
        }

        public int GetMaxCount()
        {
            return mMaxImageCount;
        }

        public int GetCurrentCount()
        {
            return mImages == null ? 0 : mImages.Count;
        } 
    }
}
