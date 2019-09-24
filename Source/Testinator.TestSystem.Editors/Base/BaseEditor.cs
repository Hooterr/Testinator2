using System;

namespace Testinator.TestSystem.Editors
{
    internal abstract class BaseEditor<TObjectToCreate, TInterface> : IBuildable<TObjectToCreate>
    {
        /// <summary>
        /// The question model version to use
        /// </summary>
        protected readonly int mVersion;

        /// <summary>
        /// The object that is being edited.
        /// If in creation this should be null
        /// </summary>
        protected TObjectToCreate OriginalObject { get; private set; }

        protected IErrorHandlerAdapter<TInterface> ErrorHandlerAdapter { get; set; }

        protected IInternalErrorHandler mUnderlyingErrorHandler;

        public BaseEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version), "Version must be from within the range.");

            mVersion = version;
        }

        public BaseEditor(TObjectToCreate baseObject, int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version), "Version must be from within the range.");

            mVersion = version;
            OriginalObject = baseObject;
        }

        #region Move this to a configuration ca
        public void Initialize()
        {
            OnInitialize();

            if (IsInCreationMode())
            {
                InitializeCreateNewObject();
                CreateNestedEditorsNewObject();
            }
            else
            {
                InitializeEditExistingObject();
                CreateNestedEditorExistingObject();
            }

            OnEditorsCreated();
          
            CreateHandlers(mUnderlyingErrorHandler);
        }

        #endregion

        /// <summary>
        /// Determines if the editor is editing an existing object
        /// </summary>
        /// <returns>True if the editor is editing an existing object, false if it's creating a new one</returns>
        protected bool IsInEditorMode() => !IsInCreationMode();

        /// <summary>
        /// Determines if the editor is creating a new object
        /// </summary>
        /// <returns>True if it's creating a new object, false if the editor is editing an existing one</returns>
        protected bool IsInCreationMode() => OriginalObject == null;

        protected virtual void CreateNestedEditorsNewObject() { }

        protected virtual void CreateNestedEditorExistingObject() { }

        protected virtual void OnEditorsCreated() { }

        protected abstract bool Validate();

        protected virtual void OnInitialize() { }

        protected virtual void InitializeCreateNewObject() { }

        protected virtual void InitializeEditExistingObject() { }

        protected virtual void CreateHandlers(IInternalErrorHandler internalHandler) { }

        public virtual OperationResult<TObjectToCreate> Build()
        {
            if (Validate())
            {
                var builtObject = BuildObject();
                return OperationResult<TObjectToCreate>.Success(builtObject);
            }
            else
            {
                return OperationResult<TObjectToCreate>.Fail();
            }
        }

        /// <summary>
        /// Builds the edited object
        /// Called only if the validation passed
        /// </summary>
        /// <returns>The object, edited or created</returns>
        protected abstract TObjectToCreate BuildObject();
    }
}
