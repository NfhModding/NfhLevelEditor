using Microsoft.Extensions.DependencyInjection;
using Nfh.Domain.Interfaces;
using Nfh.Services;

namespace Nfh.Editor
{
    // TODO: This is a hack, we basically make them globals
    internal static class Services
    {
        public static IBackupService Backup { get; private set; }
        public static IGameLocator GameLocator { get; private set; }
        public static IImageService Image { get; private set; }
        public static IProjectService Project { get; private set; }

        static Services()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDomainServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Backup = serviceProvider.GetRequiredService<IBackupService>();
            GameLocator = serviceProvider.GetRequiredService<IGameLocator>();
            Image = serviceProvider.GetRequiredService<IImageService>();
            Project = serviceProvider.GetRequiredService<IProjectService>();
        }
    }
}
