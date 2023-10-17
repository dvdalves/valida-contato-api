using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidaContatoApi.Business.Validations
{
    internal class ContatoValidation
    {
        public DateTime DataAtual = DateTime.Now;

        internal bool ValidarData(DateTime dataNascimento)
        {
            return dataNascimento.Date <= DataAtual.Date;
        }

        internal bool ValidaSeMaiorDeIdade(DateTime dataNascimento)
        {
            var idade = CalcularIdade(dataNascimento);

            return idade >= 18;
        }

        internal int CalcularIdade(DateTime dataNascimento)
        {
            var diferenca = DataAtual - dataNascimento;

            return (int)diferenca.TotalDays / 365;
        }
    }
}
