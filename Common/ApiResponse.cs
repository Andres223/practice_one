namespace WebApplication1.Common;

public class ApiResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public object? Error { get; init; }

    private ApiResponse(bool success, T? data, object? error)
    {
        Success = success;
        Data = data;
        Error = error;
    }

    public static ApiResponse<T> SuccessResponse(T data)
        => new(true, data, null);

    public static ApiResponse<T> FailureResponse(object error)
        => new(false, default, error);

    public static ApiResponse<T> FromResult(Result<T> result)
    {
        return result.IsSuccess
            ? SuccessResponse(result.Value!)
            : FailureResponse(result.Error!);
    }
}