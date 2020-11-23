using Microsoft.Extensions.DependencyInjection;
using Nfh.Services.BackupServices;
using Nfh.Services.Common;
using Nfh.Services.GameLocatorServices;
using Nfh.Services.ImageServices;
using Nfh.Services.ProjectServices;

namespace Nfh.Services
{
    public static class DomainServicesBuilder
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IApplicationWorkFolder, ApplicationWorkFolder>();
            services.AddHelpers();

            services.AddGameLocatorServices();
            services.AddBackupServices();
            services.AddProjectServices();
            services.AddImageServices();
        }
    }
}
