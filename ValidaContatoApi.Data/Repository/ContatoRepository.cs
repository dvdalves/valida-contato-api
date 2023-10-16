using System.Linq.Expressions;
using ValidaContatoApi.Data.Context;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Data.Repository
{
    public class ContatoRepository : Repository<Contato>, IContatoRepository
    {
        public ContatoRepository(ValidaContatoContext context) : base(context)
        {
        }
    }
}