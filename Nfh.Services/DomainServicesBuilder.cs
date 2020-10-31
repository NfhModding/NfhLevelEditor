using Microsoft.Extensions.DependencyInjection;
using Nfh.Services.BackupServices;
using Nfh.Services.GameLocatorServices;

namespace Nfh.Services
{
    public static class DomainServicesBuilder
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IApplicationWorkFolder, ApplicationWorkFolder>();
            services.AddGameLocatorServices();
            services.AddBackupServices();
        }
    }
}
