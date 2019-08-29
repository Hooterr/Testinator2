using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    internal abstract class BaseEditor<TObjectToCreate, TInterface> : BaseErrorListener<TInterface>
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

        protected BaseEditor(TObjectToCreate baseObject, int version)
        {
            if (baseObject == null)
                throw new ArgumentNullException(nameof(baseObject), $"When in editing mode, starting object for the editor cannot be null");

            OriginalObject = baseObject;

            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version), "Version must be from within the range.");
            Version = version;

            OnInitialize();
        }

        protected virtual void OnInitialize() { }
    }
}
