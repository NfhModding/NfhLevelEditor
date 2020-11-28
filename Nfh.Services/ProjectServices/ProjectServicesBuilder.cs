using Microsoft.Extensions.DependencyInjection;
using Nfh.Domain.Interfaces;

namespace Nfh.Services.ProjectServices
{
    public static class ProjectServicesBuilder
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddTransient<IProjectService, ProjectService>();

            return services;
        }
    }
}
