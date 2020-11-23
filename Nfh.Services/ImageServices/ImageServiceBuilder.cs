using Microsoft.Extensions.DependencyInjection;
using Nfh.Domain.Interfaces;

namespace Nfh.Services.ImageServices
{
    public static class ImageServiceBuilder
    {
        public static IServiceCollection AddImageServices(this IServiceCollection services)
        {
            services.AddTransient<IImageService, ImageService>();
            services.AddSingleton<IGfxPrepareService, GfxPrepareService>();
            return services;
        }
    }
}
