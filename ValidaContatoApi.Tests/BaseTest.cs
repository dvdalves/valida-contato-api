using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Business.Services;
using ValidaContatoApi.Data.Context;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Data.Repository;

namespace ValidaContatoApi.Tests;

public class BaseTest
{
    protected ValidateContactContext _context = default!;
    protected IMapper _mapper = default!;
    protected IServiceProvider _serviceProvider = default!;
    private readonly string DataBaseName = "DataBaseTest" + Guid.NewGuid();


    public BaseTest()
    {
        InitializeContainer();
        InicializarContexto();
    }

    private void InicializarContexto()
    {
        _context = _serviceProvider.GetRequiredService<ValidateContactContext>();
        _mapper = _serviceProvider.GetRequiredService<IMapper>();
    }

    protected void PopulateDatabase<T>(ICollection<T> collection) where T : class
    {
        if (collection != null && collection.Any())
        {
            _context.AddRange(collection);
            _context.SaveChanges();
        }
    }

    private void InitializeContainer()
    {
        var serviceColection = new ServiceCollection();
        serviceColection.AddDbContext<ValidateContactContext>(options => options.UseInMemoryDatabase(databaseName: DataBaseName));
        serviceColection.AddScoped<IContactRepository, ContactRepository>();
        serviceColection.AddScoped<IContactService, ContactService>();
        serviceColection.AddAutoMapper(typeof(Configurations.AutoMapper));
        _serviceProvider = serviceColection.BuildServiceProvider();
    }
}
