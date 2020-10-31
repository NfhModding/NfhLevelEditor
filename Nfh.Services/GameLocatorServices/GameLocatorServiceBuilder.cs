using Microsoft.Extensions.DependencyInjection;
using Nfh.Domain.Interfaces;
using System.Collections.Generic;

namespace Nfh.Services.GameLocatorServices
{
    public static class GameLocatorServiceBuilder
    {
        public static IServiceCollection AddGameLocatorServices(this IServiceCollection services)
        {
            services.AddTransient<IGameLocator, GameLocator>(_ => 
            {
                return new GameLocator(new List<IGameLocationProvider>()
                { 
                    new SteamGameLocationProvider(),
                });
            });
            return services;
        }
    }
}
