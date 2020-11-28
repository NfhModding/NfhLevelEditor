using Microsoft.Extensions.DependencyInjection;

namespace Nfh.Dal.Xml.Serializers
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
