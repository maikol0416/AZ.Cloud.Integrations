using Domain.Port;
using Infrastructure.Integration;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            #region CosmosDB for mongo
            services.Configure<ConfigurateCosmosDB>(option =>
            {
                option.ConnectionString = configuration[$"{nameof(ConfigurateCosmosDB)}:{nameof(option.ConnectionString)}"];
                option.DatabaseName = configuration[$"{nameof(ConfigurateCosmosDB)}:{nameof(option.DatabaseName)}"];
            });

            services.AddSingleton<IConfigurateCosmosDB>(sp => sp.GetRequiredService<IOptions<ConfigurateCosmosDB>>().Value);

            services.AddScoped<IMainContextCosmos, MainContextCosmosDB>();
            #endregion

            services.AddScoped(typeof(ISecurityRepository), typeof(SecurityRepository));
            services.AddScoped(typeof(IRolRepository), typeof(RolRepository));
            services.AddScoped(typeof(IElevatorMovementRepository), typeof(ElevatorMovementRepository));
            services.AddScoped(typeof(IElevatorRepository), typeof(ElevatorRepository));

            return services;
        }
    }
}
