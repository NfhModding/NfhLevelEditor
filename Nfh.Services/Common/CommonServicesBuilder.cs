using Microsoft.Extensions.DependencyInjection;

namespace Nfh.Services.Common
{
    internal static class CommonServicesBuilder
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<IFolderService, FolderService>();
            services.AddTransient<IZipService, ZipService>();
            return services;
        }
    }
}
