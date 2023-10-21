using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Business.Services;
using ValidaContatoApi.Data.Context;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Data.Repository;

namespace ValidaContatoApi.Tests
{
    public class TesteBase
    {
        protected ValidaContatoContext _context = default!;
        protected IMapper _mapper = default!;
        protected IServiceProvider _serviceProvider = default!;
        private string DataBaseName = "DataBaseTest" + Guid.NewGuid();


        public TesteBase()
        {
            InicializarContainer();
            InicializarContexto();
        }

        private void InicializarContexto()
        {
            _context = _serviceProvider.GetRequiredService<ValidaContatoContext>();
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }

        protected void PopularBancoDeDados<T>(ICollection<T> collection) where T : class
        {
            if (collection != null && collection.Any())
            {
                _context.AddRange(collection);
                _context.SaveChanges();
            }
        }

        private void InicializarContainer()
        {
            var serviceColection = new ServiceCollection();
            serviceColection.AddDbContext<ValidaContatoContext>(options => options.UseInMemoryDatabase(databaseName: DataBaseName));
            serviceColection.AddScoped<IContatoRepository, ContatoRepository>();
            serviceColection.AddScoped<IContatoService, ContatoService>();
            serviceColection.AddAutoMapper(typeof(ValidaContatoApi.Configurations.AutoMapper));
            _serviceProvider = serviceColection.BuildServiceProvider();
        }
    }
}
