using System;
using System.Collections.Generic;
using System.Drawing;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions.Task;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class ImageEditor : IImageEditor, IEditor<IImageContent>
    {
        private readonly int mVersion;
        private ImageContent mImageContent;

        private readonly int mMaxImageCount;
        private readonly int mMaxImageWidth;
        private readonly int mMaxImageHeight;

        public OperationResult AddImage(Image img)
        {
            if (mImageContent.Images.Count >= mMaxImageCount)
                return OperationResult.Fail($"Image content cannot consist of more than {mMaxImageCount} images.");

            mImageContent.Images.Add(img);

            return OperationResult.Success;
        }

        public OperationResult DeleteAllImages()
        {
            mImageContent.Images.Clear();
            return OperationResult.Success;
        }

        public OperationResult DeleteImage(Image img, bool returnFailIfImageNotFound = false)
        {
            if (true == mImageContent.Images.Remove(img))
            {
                return OperationResult.Success;
            }

            if (returnFailIfImageNotFound)
                return OperationResult.Fail("Image not found in the collection");

            return OperationResult.Success;
        }

        public OperationResult DeleteImageAt(int index, bool returnFailIfImageNotFound = false)
        {
            if (mImageContent.Images.Count > index + 1)
            {
                if (returnFailIfImageNotFound)
                    return OperationResult.Fail($"There is no element at index {index}.");
                else
                    return OperationResult.Success;
            }

            mImageContent.Images.RemoveAt(index);
            return OperationResult.Success;
        }

        public int GetCurrentCount()
        {
            return mImageContent.Images.Count;
        }

        public int GetMaxCount()
        {
            return mMaxImageCount;
        }

        public ImageEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version));

            mVersion = version;
            mImageContent = new ImageContent();

            var mMaxImageCount = AttributeHelper.GetPropertyAttributeValue<ImageContent, ICollection<Image>, MaxCollectionCountAttribute, int>
                (obj => obj.Images, attr => attr.MaxCount, mVersion);

            var ImageSizeAttr = AttributeHelper.GetPropertyAttribute<ImageContent, ICollection<Image>, MaxImageSizeAttribute>
                (x => x.Images, mVersion);

            mMaxImageHeight = ImageSizeAttr.Height;
            mMaxImageWidth = ImageSizeAttr.Width;
        }

        public ImageContent AssembleContent()
        {
            // maybe some validation will be required
            return mImageContent;
        }

        public void OnValidationError(Action<string> action)
        {
            throw new NotImplementedException();
        }

        public IImageContent Build()
        {
            throw new NotImplementedException();
        }
    }
}
