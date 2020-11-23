using Microsoft.Extensions.DependencyInjection;
using Nfh.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh
{
    internal class Startup
    {
        public IServiceProvider ServiceProvider { get; }

        public Startup()
        {
            var services = new ServiceCollection();
            configureServices(services);

            ServiceProvider = services.BuildServiceProvider(true);
        }

        private void configureServices(IServiceCollection services)
        {
            services.AddDomainServices();
            services.AddSingleton<Playground>();
        }
    }
}
