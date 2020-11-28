using Microsoft.Extensions.DependencyInjection;
using Nfh.Dal.Repositories;
using Nfh.Dal.Xml.Converters;
using Nfh.Dal.Xml.Serializers;

namespace Nfh.Dal
{
    public static class DalBuilder
    {
        public static IServiceCollection AddDal(this IServiceCollection services)
        {
            services.AddConverters();
            services.AddSerialization();
            services.AddTransient<ISeasonPackRepository, SeasonPackRepository>();
            services.AddTransient<ILevelRepostory, LevelRepository>();
            services.AddTransient<ILevelMetaRepository, LevelMetaLoader>();
            services.AddTransient<ILevelDataRepository, LevelDataRepository>();
            services.AddTransient<ILevelDataUnifier, LevelDataUnifier>();

            return services;
        }
    }
}
