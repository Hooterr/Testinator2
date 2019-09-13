using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Attributes
{
    internal class MaxCollectionCountAttribute : BaseEditorAttribute
    {
        private int mMaxCount;

        public int MaxCount => mMaxCount;
        
        public MaxCollectionCountAttribute(int maxCount)
        {
            mMaxCount = maxCount;
        }
        public MaxCollectionCountAttribute(int maxCount, int fromVersion) : base(fromVersion)
        {
            mMaxCount = maxCount;
        }
    }
}
