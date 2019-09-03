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
    internal class ImageEditor : BaseEditor<IImageContent, IImageEditor>, IImageEditor, IBuildable<IImageContent>
    {
        protected IList<Image> mImages;

        protected int mMaxImageCount;
        protected int mMaxImageWidth;
        protected int mMaxImageHeight;

        public OperationResult AddImage(Image img)
        {
            if (img.Width == 0 || img.Height == 0)
                return OperationResult.Fail("Image height and width cannot be 0");

            if (img.Width > mMaxImageWidth || img.Height > mMaxImageHeight)
                return OperationResult.Fail("Image is too big");

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
            if (index >= mImages.Count)
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
            if(IsInCreationMode())
            {
                mImages = new List<Image>();
            }
            else
            {
                mImages = new List<Image>(OriginalObject.Images);
            }

            LoadAttributeValues();
        }

        protected virtual void LoadAttributeValues()
        {
            mMaxImageCount = AttributeHelper.GetPropertyAttributeValue<ImageContent, ICollection<Image>, MaxCollectionCountAttribute, int>
                (obj => obj.Images, attr => attr.MaxCount, Version);

            var ImageSizeAttr = AttributeHelper.GetPropertyAttribute<ImageContent, ICollection<Image>, MaxImageSizeAttribute>
                (x => x.Images, Version);

            mMaxImageHeight = ImageSizeAttr.Height;
            mMaxImageWidth = ImageSizeAttr.Width;
        }

        internal override bool Validate()
        {
            var validationPassed = true;

            if(mImages.Count > mMaxImageCount) // TODO add min count validation
            {
                // TODO add a list of general errors
                //HandleErrorFor(x => x.)
                validationPassed = false;
            }

            return validationPassed;
        }

        protected override IImageContent BuildObject()
        {
            var result = new ImageContent()
            {
                Images = mImages,
            };
            return result;
        }
    }
}
