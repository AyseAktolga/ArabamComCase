using ArabamComCase.Application.Interfaces;
using ArabamComCase.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

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
