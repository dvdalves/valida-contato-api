namespace ValidaContatoApi.Business.Resultados
{
    public class Resultado<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = default!;
        public int StatusCode { get; set; }
        public T Result { get; set; } = default!;

        public void ResultadoOk(string mensagem)
        {
            IsSuccess = true;
            StatusCode = 200;
            Message = mensagem;
        }

        public void ResultadoErro(int resultCode, string mensagem)
        {
            IsSuccess = false;
            StatusCode = resultCode;
            Message = mensagem;
        }
    }
}
