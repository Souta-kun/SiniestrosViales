namespace SiniestrosViales.Application.Common.Models;

public class ApiResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    private ApiResult() { }

    public static ApiResult SuccessResult(object? data = null, string? message = null)
        => new ApiResult
        {
            Success = true,
            Message = message,
            Data = data
        };

    public static ApiResult FailureResult(string? message = null)
        => new ApiResult
        {
            Success = false,
            Message = message
        };
}
