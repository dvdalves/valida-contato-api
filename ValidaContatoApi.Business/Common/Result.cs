namespace ValidaContatoApi.Business.Common;

public class Result<T>
{
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = default!;
    public int StatusCode { get; set; }
    public T Data { get; set; } = default!;

    public void SuccessResult(string message)
    {
        IsSuccess = true;
        StatusCode = 200;
        Message = message;
    }

    public void ErrorResult(int statusCode, string message)
    {
        IsSuccess = false;
        StatusCode = statusCode;
        Message = message;
    }
}
