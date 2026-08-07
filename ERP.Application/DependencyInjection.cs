using ERP.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            
            return services;
        }
    }
}
