using Microsoft.Extensions.DependencyInjection;

namespace Nfh.Services.Common
{
    internal static class Builder
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<IFolderHelper, FolderHelper>();
            services.AddTransient<IZipHelper, ZipHelper>();
            return services;
        }
    }
}
