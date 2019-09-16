using System;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Provides base functionality for any editor
    /// </summary>
    /// <typeparam name="TObjectToCreate">The type of object this editor will operate on</typeparam>
    /// <typeparam name="TInterface">The interface the implementation of the editor is hidden behind</typeparam>
    internal abstract class BaseEditor<TObjectToCreate, TInterface> : ErrorListener<TInterface>, IBuildable<TObjectToCreate>
    {
        #region Protected Members
        // TODO check if this variables can be private

        /// <summary>
        /// The question model version to use
        /// </summary>
        protected int Version { get; private set; }

        /// <summary>
        /// The object that is being edited.
        /// If in creation this should be null
        /// </summary>
        protected TObjectToCreate OriginalObject { get; private set; }

        #endregion

        #region All Constructors

        /// <summary>
        /// Initializes the editor to create a new object
        /// </summary>
        /// <param name="version">The question model version to use</param>
        protected BaseEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version), "Version must be from within the range.");

            Version = version;
            OnInitialize();
        }

        /// <summary>
        /// Initializes the editor to edit an existing object
        /// </summary>
        /// <param name="baseObject">The object to edit. NOTE: null value is allowed here, it's treated as if the caller wanted to create a new object</param>
        /// <param name="version">The question model version to use</param>
        protected BaseEditor(TObjectToCreate baseObject, int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version), "Version must be from within the range.");

            OriginalObject = baseObject;

            Version = version;

            OnInitialize();
        }

        #endregion

        #region Protected Methods

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

        #endregion

        #region Protected

        #region Virtual

        /// <summary>
        /// Called when editor is initializing
        /// </summary>
        protected virtual void OnInitialize() { }

        public virtual OperationResult<TObjectToCreate> Build()
        {
            // Remember to clear the errors
            ClearAllErrors();
            if (Validate())
            {
                var builtObject = BuildObject();
                return OperationResult<TObjectToCreate>.Success(builtObject);
            }
            else
            {
                var unhandledErrors = GetUnhandledErrors();
                var result = OperationResult<TObjectToCreate>.Fail(unhandledErrors);
                return result;
            }
        }

        /// <summary>
        /// Validates the current state of the editor.
        /// Called during build process. During this validation implementer should call HandleError/HandleErrorFor to populate error messages
        /// </summary>
        /// <returns>True if validation was successful, otherwise false</returns>
        internal virtual bool Validate()
        {
            return true;
        } 

        #endregion

        #region Abstract

        /// <summary>
        /// Builds the edited object
        /// Called only if the validation passed
        /// </summary>
        /// <returns>The object, edited or created</returns>
        protected abstract TObjectToCreate BuildObject();

        #endregion

        #endregion

    }
}
