using ValidaContatoApi.Domain.Enum;

namespace ValidaContatoApi.Business.ViewModels
{
    public class UpdateContactVM
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public GenderEnum Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
