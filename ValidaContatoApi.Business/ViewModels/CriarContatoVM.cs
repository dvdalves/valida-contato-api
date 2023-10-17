using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidaContatoApi.Business.ViewModels
{
    public class CriarContatoVM
    {
        public string? Nome { get; set; }
        public int Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
