using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public class OperationResult
    {
        public List<string> Errors { get; private set; } = new List<string>();
        public bool Succeeded => Errors.Count == 0;
        public bool Failed => !Succeeded;

        public OperationResult()
        { }

        public OperationResult(string error)
        {
            Errors.Add(error);
        }
        public OperationResult(string[] error)
        {
            Errors.AddRange(error);
        }

        public void AddError(string msg)
        {
            Errors.Add(msg);
        }

        public static OperationResult Fail(string error) => new OperationResult(error);

        public static OperationResult Fail(string[] errors) => new OperationResult(errors);

        public static OperationResult Success() => new OperationResult();

    }
}
