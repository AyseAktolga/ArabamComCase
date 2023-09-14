using ArabamComCase.Application.Interfaces;
using ArabamComCase.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabamComCase.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IAdvertRepository, AdvertRepository>();
            services.AddTransient<IAdvertVisitRepository, AdvertVisitRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
