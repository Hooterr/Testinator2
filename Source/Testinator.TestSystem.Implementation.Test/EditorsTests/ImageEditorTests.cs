using System.Collections.Generic;
using System.Drawing;
using Testinator.TestSystem.Abstractions.Questions.Task;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Attributes;
using Xunit;

namespace Testinator.TestSystem.Implementation.Test.EditorsTests
{
    public class ImageEditorTests
    {
        internal static ImageEditorMock GetEditor(int version = 1) => new ImageEditorMock(version);

        [Fact]
        public void AddImageCountExceeded()
        {
            var editor = GetEditor();
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));

            var opresult = editor.AddImage(new Bitmap(10, 10));
            Assert.True(opresult.Failed);
            Assert.Equal(5, editor.Images.Count);
        }

        [Fact]
        public void AddImageAddOne()
        {
            var editor = GetEditor();
            var image = new Bitmap(10, 10);
            editor.AddImage(image);
            Assert.Equal(1, editor.Images.Count);
            Assert.True(image.Equals(editor.Images[0]));
        }

        [Theory]
        [InlineData(1001, 10)]
        [InlineData(10, 1001)]
        [InlineData(1001, 1001)]
        public void AddImageAddTooBigWidth(int width, int height)
        {
            var editor = GetEditor();
            var image = new Bitmap(width, height);
            var opresult = editor.AddImage(image);
            Assert.True(opresult.Failed);
        }

        [Fact]
        public void DeleteAllImages()
        {
            var editor = GetEditor();
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));

            editor.DeleteAllImages();
            Assert.Equal(0, editor.Images.Count);
        }

        [Fact]
        public void DeleteImageImagePresentFaileIfNotFoundFalse()
        {
            var editor = GetEditor();
            var toDelete = new Bitmap(10, 10);
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(toDelete);
            editor.AddImage(new Bitmap(10, 10));

            var operation = editor.DeleteImage(toDelete, false);
            Assert.True(operation.Succeeded);
            Assert.DoesNotContain(toDelete, editor.Images);
            Assert.Equal(2, editor.Images.Count);
        }
        [Fact]
        public void DeleteImageImagePresentFaileIfNotFoundTrue()
        {
            var editor = GetEditor();
            var toDelete = new Bitmap(10, 10);
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(toDelete);
            editor.AddImage(new Bitmap(10, 10));

            var operation = editor.DeleteImage(toDelete, true);
            Assert.True(operation.Succeeded);
            Assert.DoesNotContain(toDelete, editor.Images);
            Assert.Equal(2, editor.Images.Count);
        }


        [Fact]
        public void DeleteImageImageNotFoundReturnFailIfNotFoundFalse()
        {
            var editor = GetEditor();
            var toDelete = new Bitmap(10, 10);
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));

            var operation = editor.DeleteImage(toDelete);
            Assert.True(operation.Succeeded);
            Assert.DoesNotContain(toDelete, editor.Images);
            Assert.Equal(2, editor.Images.Count);
        }

        [Fact]
        public void DeleteImageImageNotFoundReturnFailIfNotFoundTrue()
        {
            var editor = GetEditor();
            var toDelete = new Bitmap(10, 10);
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));

            var operation = editor.DeleteImage(toDelete, true);
            Assert.True(operation.Failed);
            Assert.DoesNotContain(toDelete, editor.Images);
            Assert.Equal(2, editor.Images.Count);
        }

        [Fact]
        public void DeleteImageAtImagePresentFaileIfNotFoundFalse()
        {
            var editor = GetEditor();
            var toDelete = new Bitmap(100, 100);
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(toDelete);
            editor.AddImage(new Bitmap(10, 10));

            var operation = editor.DeleteImageAt(1, false);
            Assert.True(operation.Succeeded);
            Assert.DoesNotContain(toDelete, editor.Images);
            Assert.Equal(2, editor.Images.Count);
        }
        [Fact]
        public void DeleteImageAtImagePresentFaileIfNotFoundTrue()
        {
            var editor = GetEditor();
            var toDelete = new Bitmap(100, 100);
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(toDelete);
            editor.AddImage(new Bitmap(10, 10));

            var operation = editor.DeleteImageAt(1, true);
            Assert.True(operation.Succeeded);
            Assert.DoesNotContain(toDelete, editor.Images);
            Assert.Equal(2, editor.Images.Count);
        }


        [Fact]
        public void DeleteImageAtImageNotFoundReturnFailIfNotFoundFalse()
        {
            var editor = GetEditor();
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));

            var operation = editor.DeleteImageAt(2, false);
            Assert.True(operation.Succeeded);
            Assert.Equal(2, editor.Images.Count);
        }

        [Fact]
        public void DeleteImageAtImageNotFoundReturnFailIfNotFoundTrue()
        {
            var editor = GetEditor();
            var toDelete = new Bitmap(10, 10);
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));

            var operation = editor.DeleteImageAt(2, true);
            Assert.True(operation.Failed);
            Assert.Equal(2, editor.Images.Count);
        }

        [Fact]
        public void GetCurrentCount()
        {
            var editor = GetEditor();
            editor.AddImage(new Bitmap(10, 10));
            editor.AddImage(new Bitmap(10, 10));
            Assert.Equal(editor.Images.Count, editor.GetCurrentCount());
        }

        [Fact]
        public void GetMaxCount()
        {
            var editor = GetEditor();
            Assert.Equal(5, editor.GetMaxCount());
        }

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
            mMaxImageCount = AttributeHelper.GetPropertyAttributeValue<ImageContentMock, ICollection<Image>, MaxCollectionCountAttribute, int>
                (obj => obj.Images, attr => attr.MaxCount, mVersion);

            var ImageSizeAttr = AttributeHelper.GetPropertyAttribute<ImageContentMock, ICollection<Image>, MaxImageSizeAttribute>
                (x => x.Images, mVersion);
            
            mMaxImageHeight = ImageSizeAttr.Height;
            mMaxImageWidth = ImageSizeAttr.Width;
        }

        public IList<Image> Images => mImages;
    }

    public class ImageContentMock : IImageContent
    {

        [MaxCollectionCount(maxCount: 5, fromVersion: 1)]
        [MaxImageSize(width: 1000, height: 1000, fromVersion: 1)]
        public IList<Image> Images { get; set; }
    }
}
