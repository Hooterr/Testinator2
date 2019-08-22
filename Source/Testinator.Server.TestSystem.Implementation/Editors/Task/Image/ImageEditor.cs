using System;
using System.Collections.Generic;
using System.Drawing;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class ImageEditor : IImageEditor
    {
        private readonly int mVersion;
        private ImageContent mImageContent;
        private int mMaxImageCount;

        public OperationResult AddImage(Image img)
        {
            return null;
        }

        public OperationResult DeleteAllImages()
        {
            throw new NotImplementedException();
        }

        public OperationResult DeleteImage(Image img)
        {
            throw new NotImplementedException();
        }

        public OperationResult DeleteImageAt(int index)
        {
            throw new NotImplementedException();
        }

        public int GetCurrentCount()
        {
            throw new NotImplementedException();
        }

        public int GetMaxCount()
        {
            throw new NotImplementedException();
        }

        public ImageEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version));

            mVersion = version;
            mImageContent = new ImageContent();

            var mMaxImageCount = AttributeHelper.GetPropertyAttributeValue<ImageContent, ICollection<Image>, MaxCollectionCountAttribute, int>
                (obj => obj.Images, attr => attr.MaxCount, mVersion);
        }
    }
}
