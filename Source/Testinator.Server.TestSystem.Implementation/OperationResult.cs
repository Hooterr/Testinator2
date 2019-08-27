using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public class OperationResult
    {
        public List<string> Errors { get; private set; } = new List<string>();
        public bool Succeeded { get; private set; }
        public bool Failed => !Succeeded;

        public OperationResult()
        { }

        public OperationResult(string error)
        {
            Errors.Add(error);
            Succeeded = false;
        }
        public OperationResult(string[] error)
        {
            Errors.AddRange(error);
            Succeeded = false;
        }

        public OperationResult(bool success)
        {
            Succeeded = success;
        }

        public void AddError(string msg)
        {
            Errors.Add(msg);
        }


        public static OperationResult Fail(string error) => new OperationResult(error);

        public static OperationResult Fail(string[] errors) => new OperationResult(errors);

        public static OperationResult Fail() => new OperationResult(false);

        public static OperationResult Succes() => new OperationResult(true);

    }

    public class OperationResult<TOut> : OperationResult
    {
        public readonly TOut Result;

        public OperationResult(string error) : base(error) { }


        public OperationResult(string[] errors) : base(errors) { }

        public OperationResult(TOut obj, bool success) : base(success)
        {
            Result = obj;;
        }

        public static OperationResult<TOut> Success(TOut obj) => new OperationResult<TOut>(obj, true);

    }
}
