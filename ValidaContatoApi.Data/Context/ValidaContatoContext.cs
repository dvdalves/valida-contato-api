using Microsoft.EntityFrameworkCore;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Data.Context
{
    public class ValidaContatoContext : DbContext
    {
        public ValidaContatoContext(DbContextOptions<ValidaContatoContext> options) : base(options)
        {
        }

        public DbSet<Contato> Contatos { get; set; }

    }
}
