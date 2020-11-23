using Microsoft.Extensions.DependencyInjection;

namespace Nfh.Services.ProjectServices.Xml.Serializers
{
    internal static class SerializerBuilder
    {
        public static IServiceCollection AddSerialization(this IServiceCollection services)
        {
            services.AddSingleton<ISerializer, Serializer>();
            return services;
        }
    }
}
