using Microsoft.Extensions.DependencyInjection;
using Nfh.Domain.Models.Meta;
using Nfh.Services.ProjectServices.Xml.Converters.Meta;
using Nfh.Services.ProjectServices.Xml.Models.Meta;
using System;
using System.Collections.Generic;

namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal static class ConverterBuilder
    {
        public static IServiceCollection AddConverters(this IServiceCollection services)
        {
            services.AddTransient<Converter>(_ =>
            {
                var converters = new List<(ITypeConverter converter, Type domainType, Type xmlType)>()
                {
                    (new LevelDescriptionConverter(), typeof(LevelDescription), typeof(BriefingRoot)),
                };
                return new(converters);
            });
            return services;
        }
    }
}
