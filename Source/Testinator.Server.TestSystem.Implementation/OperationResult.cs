﻿using System;
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

        public static OperationResult Success() => new OperationResult(true);

    }

    public class OperationResult<TOut>
    {
        public readonly TOut Result;

        public List<string> Errors { get; private set; } = new List<string>();
        public bool Succeeded { get; private set; }
        public bool Failed => !Succeeded;

        public OperationResult(string error)
        {
            Errors.Add(error);
            Succeeded = false;
        }


        public OperationResult(string[] errors)
        {
            Errors.AddRange(errors);
            Succeeded = false;
        }

        public OperationResult(TOut obj, bool success)
        {
            Result = obj;;
            Succeeded = success;
        }

        public OperationResult(bool success)
        {
            Succeeded = success;
        }

        public void Merge<T>(OperationResult<T> result)
        {
            Errors.AddRange(result.Errors);
        }

        public static OperationResult<TOut> Fail(string error) => new OperationResult<TOut>(error);

        public static OperationResult<TOut> Fail(string[] errors) => new OperationResult<TOut>(errors);

        public static OperationResult<TOut> Fail() => new OperationResult<TOut>(false);

        public static OperationResult<TOut> Success(TOut obj) => new OperationResult<TOut>(obj, true);

    }
}
