using ValidaContatoApi.Domain.Enum;

namespace ValidaContatoApi.Business.ViewModels
{
    public class AtualizarContatoVM
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public SexoEnum Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
