using Microsoft.Extensions.DependencyInjection;
using Nfh.Domain.Models.Meta;
using Nfh.Services.ProjectServices.Xml.Converters.LevelDatas;
using Nfh.Services.ProjectServices.Xml.Converters.Meta;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.Meta;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal static class ConverterBuilder
    {
        public static IServiceCollection AddConverters(this IServiceCollection services)
        {
            services.AddTransient<LevelDescriptionConverter>();

            services.AddTransient<Converter>(_ =>
            {
                var converters = new List<(ITypeConverter converter, Type domainType, Type xmlType)>()
                {
                    (new LevelDescriptionConverter(), typeof(LevelDescription), typeof(BriefingRoot)),
                    (new LevelDatas.PointConverter(), typeof(Point), typeof(Coord)),
                    (new LevelDatas.SizeConverter(), typeof(Size), typeof(Coord)),
                };
                return new(converters);
            });
            return services;
        }
    }
}
