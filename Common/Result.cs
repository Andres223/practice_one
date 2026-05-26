namespace WebApplication1.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public ApiError? Error { get; }
    public bool IsFailure => !IsSuccess;

    public Result(bool isSucces, T? value, ApiError? error)
    {
        IsSuccess = isSucces;
        Value = value;
        Error = error;
    }
    
    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(ApiError error) => new(false, default, error);
}