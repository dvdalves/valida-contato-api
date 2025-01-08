using ValidaContatoApi.Domain.Enum;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Tests.ContatoTests
{
    public static class ContatoMoq
    {
        private static Contact ObterContatoParaOMoq(string nome)
        {
            return new Contact
            {
                BirthDate = DateTime.Parse("05-10-1995"),
                Gender = GenderEnum.Masculino,
                Name = nome,
                Status = true,
            };
        }

        private static List<Contact> ObterListaContatos(int quantidade)
        {
            var lista = new List<Contact>();

            for (int i = 0; i < quantidade; i++)
                lista.Add(ObterContatoParaOMoq($"{nameof(ObterMoq_Para_ObterTodosContatos)}{i}"));

            return lista;
        }

        public static List<Contact> ObterMoq_Para_ObterTodosContatos()
        {
            return ObterListaContatos(10);
        }

        public static List<Contact> ObterMoq_Para_AtualizarContato()
        {
            return ObterListaContatos(10);
        }

        public static List<Contact> ObterMoq_Para_ObterContatoPorId()
        {
            var lista = new List<Contact>();
            lista.Add(ObterContatoParaOMoq(nameof(ObterMoq_Para_ObterContatoPorId)));
            return lista;
        }
    }
}
