using System;

namespace Xtype.Contracts;

/// <summary>Non-generic result for operations that produce no value (e.g. Save).</summary>
public sealed class OperationResult
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    public System.Exception? Exception { get; }

    private OperationResult(bool isSuccess, string? errorMessage, System.Exception? exception)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    public static OperationResult Success() => new(true, null, null);
    public static OperationResult Failure(string errorMessage, System.Exception? exception = null) => new(false, errorMessage, exception);

    public override string ToString() =>
        IsSuccess ? "Success" : $"Failure: {ErrorMessage}";
}

/// <summary>Generic result for operations that return a value (e.g. Load).</summary>
public sealed class OperationResult<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? ErrorMessage { get; }
    public System.Exception? Exception { get; }

    private OperationResult(bool isSuccess, T? value, string? errorMessage, System.Exception? exception)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    public static OperationResult<T> Success(T value) => new(true, value, null, null);
    public static OperationResult<T> Failure(string errorMessage, System.Exception? exception = null) => new(false, default, errorMessage, exception);

    public override string ToString() =>
        IsSuccess ? $"Success: {Value}" : $"Failure: {ErrorMessage}";
}
