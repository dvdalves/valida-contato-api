using Microsoft.EntityFrameworkCore;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Data.Context;

public class ValidateContactContext : DbContext
{
    public ValidateContactContext(DbContextOptions<ValidateContactContext> options) : base(options) { }

    public DbSet<Contact> Contatos { get; set; }

}