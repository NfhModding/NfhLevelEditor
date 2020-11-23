using Microsoft.Extensions.DependencyInjection;
using Nfh.Domain.Interfaces;

namespace Nfh.Services.BackupServices
{
    public static class BackupServicesBuilder
    {
        public static IServiceCollection AddBackupServices(this IServiceCollection services)
        {
            services.AddTransient<IBackupService, BackupService>();
            return services;
        }
    }
}