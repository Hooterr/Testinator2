using System.Collections.Generic;
using System.Drawing;
using Testinator.Server.TestSystem.Implementation;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.TestSystem.Abstractions.Questions.Task;
using Xunit;

namespace Testinator.TestSystem.Implementation.Test.EditorsTests
{
    public class ImageEditorTests
    {
        internal static ImageEditor GetEditor(int version = 1) => new ImageEditorMock(version);

        /*[Fact]
        public void AddImageCountExceeded()
        {
            var editor = GetEditor();
            editor.AddImage(Image.)
        }*/
    }

    internal class ImageEditorMock : ImageEditor
    {
        public ImageEditorMock(int version) : base(version)
        {
        }

        public ImageEditorMock(IImageContent objToEdit, int version) : base(objToEdit, version)
        {
        }

        protected override void LoadAttributeValues()
        {
            var mMaxImageCount = AttributeHelper.GetPropertyAttributeValue<ImageContentMock, ICollection<Image>, MaxCollectionCountAttribute, int>
                (obj => obj.Images, attr => attr.MaxCount, Version);

            var ImageSizeAttr = AttributeHelper.GetPropertyAttribute<ImageContentMock, ICollection<Image>, MaxImageSizeAttribute>
                (x => x.Images, Version);
            
            mMaxImageHeight = ImageSizeAttr.Height;
            mMaxImageWidth = ImageSizeAttr.Width;
        }
    }

    public class ImageContentMock : IImageContent
    {

        [MaxCollectionCount(maxCount: 5, fromVersion: 1)]
        [MaxImageSize(width: 1000, height: 1000, fromVersion: 1)]
        public IList<Image> Images { get; set; }
    }
}
