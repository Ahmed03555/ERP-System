using ERP.Infrastructure.Date;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your infrastructure services here
            // For example, you can register your DbContext, repositories, etc.
            // Example: Registering the DbContext
            services.AddDbContext<ErbDbContext>(options => { 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            // Register other services as needed

            return services;
        }
    }
}
