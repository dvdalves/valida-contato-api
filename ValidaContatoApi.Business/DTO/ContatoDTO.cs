namespace ValidaContatoApi.Business.DTO
{
    public class ContatoDTO
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Sexo { get; set; }
        public int Idade { get; set; }
    }
}
