using System.ComponentModel.DataAnnotations.Schema;
using ValidaContatoApi.Domain.Enum;

namespace ValidaContatoApi.Domain.Models
{
    public class Contato : Entity
    {
        public string? Nome { get; set; }
        public bool Status { get; set; } = true;
        public SexoEnum Sexo { get; set; }
        public DateTime DataNascimento { get; set; }

        [NotMapped]
        public int Idade { get; set; }
    }
}
