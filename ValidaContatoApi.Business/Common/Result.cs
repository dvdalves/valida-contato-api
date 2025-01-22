namespace ValidaContatoApi.Business.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = default!;
        public int StatusCode { get; set; }
        public T Results { get; set; } = default!;

        public void ResultadoOk(string mensagem)
        {
            IsSuccess = true;
            StatusCode = 200;
            Message = mensagem;
        }

        public void ErrorResult(int resultCode, string mensagem)
        {
            IsSuccess = false;
            StatusCode = resultCode;
            Message = mensagem;
        }
    }
}
