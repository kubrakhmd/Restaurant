using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("Default")));
            
            return services;

        }
    }
}
