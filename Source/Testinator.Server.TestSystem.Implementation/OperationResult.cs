using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public class OperationResult
    {
        public List<string> Errors { get; private set; } = new List<string>();
        public bool Success => Errors.Count == 0;

        internal OperationResult()
        {

        }

        internal void AddError(string msg)
        {
            Errors.Add(msg);
        }
    }
}
