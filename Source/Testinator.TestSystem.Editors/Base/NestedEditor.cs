using System;

namespace Testinator.TestSystem.Editors
{
    // TODO update comments
    /// <summary>
    /// Provides base functionality for any editor
    /// </summary>
    /// <typeparam name="TObjectToCreate">The type of object this editor will operate on</typeparam>
    /// <typeparam name="TInterface">The interface the implementation of the editor is hidden behind</typeparam>
    internal abstract class NestedEditor<TObjectToCreate, TInterface> : BaseEditor<TObjectToCreate, TInterface>, IBuildable<TObjectToCreate>
    {
        #region Protected Members

        #endregion

        #region All Constructors

        /// <summary>
        /// Initializes the editor to create a new object
        /// </summary>
        /// <param name="version">The question model version to use</param>
        protected NestedEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes the editor to edit an existing object
        /// </summary>
        /// <param name="baseObject">The object to edit</param>
        /// <param name="version">The question model version to use</param>
        protected NestedEditor(TObjectToCreate baseObject, int version) : base(baseObject, version) { }

        #endregion

        #region Public Methods

        public void AttachErrorHandler(IInternalErrorHandler handler, string parentEditorName)
        {
            ErrorHandlerAdapter = ErrorHandlerAdapter<TInterface>.NestedEditor(handler, parentEditorName);
        }

        public void SetInternalErrorHandler(IInternalErrorHandler handler)
        {
            mUnderlyingErrorHandler = handler;
        }

        protected override void CreateHandlers(IInternalErrorHandler internalHandler)
        {
            base.CreateHandlers(internalHandler);
        }

        #endregion

    }
}
