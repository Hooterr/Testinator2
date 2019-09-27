using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Editors
{
    internal abstract class MasterEditor<TObjectToCreate, TInterface> : BaseEditor<TObjectToCreate, TInterface>
    {
        #region Private

        protected ErrorListener<TInterface> mInternalErrorHandler;

        #endregion

        #region All Constructors

        public MasterEditor(int version) : base(version)
        { }

        public MasterEditor(TObjectToCreate baseObject, int version) : base(baseObject, version)
        { }

        #endregion

        protected override void OnInitialize()
        {
            mInternalErrorHandler = ErrorListener<TInterface>.GenerateNew();
            mUnderlyingErrorHandler = mInternalErrorHandler;
        }
        protected override void CreateHandlers(IInternalErrorHandler internalHandler)
        {
            base.CreateHandlers(internalHandler);
            ErrorHandlerAdapter = ErrorHandlerAdapter<TInterface>.TopLevelEditor(mUnderlyingErrorHandler);
        }
    }
}

