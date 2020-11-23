using Microsoft.Extensions.DependencyInjection;
using Nfh.Domain.Interfaces;
using Nfh.Services.ProjectServices.Loaders;
using Nfh.Services.ProjectServices.Xml.Converters;
using Nfh.Services.ProjectServices.Xml.Serializers;

namespace Nfh.Services.ProjectServices
{
    public static class ProjectServicesBuilder
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddConverters();
            services.AddSerialization();
            services.AddTransient<ISeasonPackLoader, SeasonPackLoader>();
            services.AddTransient<ILevelLoader, LevelLoader>();
            services.AddTransient<ILevelMetaLoader, LevelMetaLoader>();
            services.AddTransient<ILevelDataLoader, LevelDataLoader>();
            services.AddTransient<ILevelDataUnifier, LevelDataUnifier>();
            services.AddTransient<IProjectService, ProjectService>();

            return services;
        }
    }
}
