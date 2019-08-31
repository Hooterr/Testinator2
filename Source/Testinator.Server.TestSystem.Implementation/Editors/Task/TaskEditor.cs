using System;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TaskEditor : BaseEditor<IQuestionTask, ITaskEditor>, ITaskEditor, IBuildable<IQuestionTask>
    {
        private TextEditor mTextEditor;
        private ImageEditor mImageEditor;

        public ITextEditor Text => mTextEditor;

        public IImageEditor Images => mImageEditor;

        public TaskEditor(int version) : base(version) { }

        public TaskEditor(IQuestionTask objToEdit, int version) : base(objToEdit, version) { }

        protected override void OnInitialize()
        {
            if (OriginalObject == null)
            {
                mTextEditor = new TextEditor(Version);
                mImageEditor = new ImageEditor(Version);
            }
            else
            {
                mTextEditor = new TextEditor(OriginalObject.Text, Version);
                mImageEditor = new ImageEditor(OriginalObject.Images, Version);
            }
        }
        internal override bool Validate()
        {
            var validationPassed = true;
            // TODO check if both text and images are not empty
            return validationPassed;
        }

        protected override IQuestionTask BuildObject()
        {
            // Method not used
            throw new NotImplementedException("This method is not used.");
        }

        public override OperationResult<IQuestionTask> Build()
        {
            ClearAllErrors();
            var textOperation = mTextEditor.Build();
            var imageOperation = mImageEditor.Build();

            // Check if all the builds were successful
            if(Helpers.AnyTrue(textOperation.Failed, imageOperation.Failed))
            {
                var result = OperationResult<IQuestionTask>.Fail()
                    .Merge(textOperation)
                    .Merge(imageOperation);

                return result;
            }
            else
            {
                // Both builds succeeded
                // Do the final validation
                if(Validate())
                {
                    IQuestionTask task;

                    if (IsInEditorMode())
                    {
                        task = OriginalObject;
                    }
                    else
                    {
                        task = new QuestionTask()
                        {
                            Text = textOperation.Result,
                            Images = imageOperation.Result,
                        };
                    }

                    return OperationResult<IQuestionTask>.Success(task);
                }
                else
                {
                    var result = OperationResult<IQuestionTask>.Fail();
                    result.AddErrors(GetUnhandledErrors());
                    return result;
                }
            }
        }
    }
}