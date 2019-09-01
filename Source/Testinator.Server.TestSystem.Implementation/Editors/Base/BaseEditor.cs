using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    internal abstract class BaseEditor<TObjectToCreate, TInterface> : ErrorListener<TInterface>, IBuildable<TObjectToCreate>
    {
        protected int Version { get; private set; }
        protected TObjectToCreate OriginalObject { get; private set; }

        protected BaseEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version), "Version must be from within the range.");

            Version = version;
            OnInitialize();
        }

        protected bool IsInEditorMode() => !IsInCreationMode();

        protected bool IsInCreationMode() => OriginalObject == null;

        // allow null for base object
        protected BaseEditor(TObjectToCreate baseObject, int version)
        {
            OriginalObject = baseObject;

            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version), "Version must be from within the range.");

            Version = version;

            OnInitialize();
        }

        protected virtual void OnInitialize() { }

        public virtual OperationResult<TObjectToCreate> Build()
        {
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

        internal virtual bool Validate()
        {
            return true;
        }

        protected abstract TObjectToCreate BuildObject();
    }
}
