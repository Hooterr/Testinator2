using System.Collections.Generic;
using System.Drawing;
using Testinator.TestSystem.Implementation.Questions.Task;
using Testinator.TestSystem.Abstractions.Questions.Task;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Implementation of the editor for the image part of the question task
    /// </summary>
    internal class ImageEditor : NestedEditor<IImageContent, IImageEditor>, IImageEditor, IBuildable<IImageContent>
    {
        #region Protected Members

        // These are all protected for testing purposes 

        /// <summary>
        /// The list of images that are the part of this task content
        /// </summary>
        protected IList<Image> mImages;

        /// <summary>
        /// Maximum count of images in this task
        /// </summary>
        protected int mMaxImageCount;

        /// <summary>
        /// Maximum width of a single image 
        /// </summary>
        protected int mMaxImageWidth;

        /// <summary>
        /// Maximum height of a single image
        /// </summary>
        protected int mMaxImageHeight;

        #endregion

        #region Editor Implementation

        public OperationResult AddImage(Image img)
        {
            // Check for empty image
            if (img.Width == 0 || img.Height == 0)
                return OperationResult.Fail("Image height and width cannot be 0");

            // Check for image size 
            if (img.Width > mMaxImageWidth || img.Height > mMaxImageHeight)
                return OperationResult.Fail("Image is too big");

            // Check for image count
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
            // Check if the item was found and removed
            if (true == mImages.Remove(img))
            {
                return OperationResult.Success();
            }
            
            // Item was not found then
            // Check what the user wants to do in that case
            if (returnFailIfImageNotFound)
                return OperationResult.Fail("Image not found in the collection");

            return OperationResult.Success();
        }

        public OperationResult DeleteImageAt(int index, bool returnFailIfImageNotFound = false)
        {
            // Check if the index is in range
            if (index >= mImages.Count)
            {
                // If it's not, check what the user wants to do in that case
                if (returnFailIfImageNotFound)
                    return OperationResult.Fail($"There is no element at index {index}.");
                else
                    return OperationResult.Success();
            }

            // Remove just like that
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

        #endregion

        #region All Constructors

        /// <summary>
        /// Initializes this editor to create a new object
        /// </summary>
        /// <param name="version">The question model version to use</param>
        public ImageEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes this editor to edit an existing object
        /// </summary>
        /// <param name="objToEdit">The image content to edit</param>
        /// <param name="version">The question model version to use</param>
        public ImageEditor(IImageContent objToEdit, int version) : base(objToEdit, version) { }

        #endregion

        #region Protected Methods 

        /// <summary>
        /// Called during editor initialization
        /// </summary>
        protected override void OnInitialize()
        {
            if(IsInCreationMode())
            {
                
            }
            else
            {
                
            }

            LoadAttributeValues();
        }

        /// <summary>
        /// Loads values from the attributes
        /// </summary>
        protected virtual void LoadAttributeValues()
        {
            mMaxImageCount = AttributeHelper.GetPropertyAttributeValue<ImageContent, ICollection<Image>, MaxCollectionCountAttribute, int>
                (obj => obj.Images, attr => attr.MaxCount, mVersion);

            var ImageSizeAttr = AttributeHelper.GetPropertyAttribute<ImageContent, ICollection<Image>, MaxImageSizeAttribute>
                (x => x.Images, mVersion);

            mMaxImageHeight = ImageSizeAttr.Height;
            mMaxImageWidth = ImageSizeAttr.Width;
        }


        protected override IImageContent BuildObject()
        {
            var result = new ImageContent()
            {
                Images = mImages,
            };
            return result;
        }

        protected override bool Validate()
        {
            return true;
        }

        protected override void InitializeCreateNewObject()
        {
            mImages = new List<Image>();
        }

        protected override void InitializeEditExistingObject()
        {
            mImages = new List<Image>(OriginalObject.Images);
        }

        #endregion
    }
}
