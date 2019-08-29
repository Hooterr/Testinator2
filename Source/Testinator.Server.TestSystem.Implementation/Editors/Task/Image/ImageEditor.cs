using System;
using System.Collections.Generic;
using System.Drawing;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions.Task;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Implementation of the editor for the image part of the question task
    /// </summary>
    internal class ImageEditor : BaseEditor<IImageContent, IImageEditor>, IImageEditor, IEditor<IImageContent>
    {
        private IList<Image> mImages;

        private int mMaxImageCount;
        private int mMaxImageWidth;
        private int mMaxImageHeight;

        public OperationResult AddImage(Image img)
        {
            if (mImages.Count >= mMaxImageCount)
                return OperationResult.Fail($"Image content cannot consist of more than {mMaxImageCount} images.");

            mImages.Add(img);

            return OperationResult.Success();
        }

        public OperationResult DeleteAllImages()
        {
            mImages.Clear();
            return OperationResult.Success();
        }

        public OperationResult DeleteImage(Image img, bool returnFailIfImageNotFound = false)
        {
            if (true == mImages.Remove(img))
            {
                return OperationResult.Success();
            }

            if (returnFailIfImageNotFound)
                return OperationResult.Fail("Image not found in the collection");

            return OperationResult.Success();
        }

        public OperationResult DeleteImageAt(int index, bool returnFailIfImageNotFound = false)
        {
            if (mImages.Count > index + 1)
            {
                if (returnFailIfImageNotFound)
                    return OperationResult.Fail($"There is no element at index {index}.");
                else
                    return OperationResult.Success();
            }

            mImages.RemoveAt(index);
            return OperationResult.Success();
        }

        public int GetCurrentCount()
        {
            return mImages.Count;
        }

        public int GetMaxCount()
        {
            return mMaxImageCount;
        }

        public ImageEditor(int version) : base(version) { }

        public ImageEditor(IImageContent objToEdit, int version) : base(objToEdit, version) { }

        protected override void OnInitialize()
        {
            var mMaxImageCount = AttributeHelper.GetPropertyAttributeValue<ImageContent, ICollection<Image>, MaxCollectionCountAttribute, int>
                (obj => obj.Images, attr => attr.MaxCount, Version);

            var ImageSizeAttr = AttributeHelper.GetPropertyAttribute<ImageContent, ICollection<Image>, MaxImageSizeAttribute>
                (x => x.Images, Version);

            mMaxImageHeight = ImageSizeAttr.Height;
            mMaxImageWidth = ImageSizeAttr.Width;
        }

        public OperationResult<IImageContent> Build()
        {
            var result = new ImageContent()
            {
                Images = mImages,
            };

            return OperationResult<IImageContent>.Success(result);
        }
    }
}
