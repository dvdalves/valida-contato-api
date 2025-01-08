using Microsoft.EntityFrameworkCore;
using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Business.Services;
using ValidaContatoApi.Data.Context;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Data.Repository;


namespace ValidaContatoApi.Configurations
{
    public static class DIConfiguration
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContatoService>();
            services.AddAutoMapper(typeof(Program));
        }

        public static IServiceCollection ConfigureDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ValidaContatoContext>(options =>
                                                options.UseSqlServer(configuration
                                                    .GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
