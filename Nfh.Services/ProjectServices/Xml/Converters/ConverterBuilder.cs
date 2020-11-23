using Microsoft.Extensions.DependencyInjection;
using Nfh.Domain.Models.Meta;
using Nfh.Services.ProjectServices.Xml.Converters.LevelDatas;
using Nfh.Services.ProjectServices.Xml.Converters.Meta;
using Nfh.Services.ProjectServices.Xml.Models.Briefing;
using Nfh.Services.ProjectServices.Xml.Models.Common;
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
                    (new LevelDescriptionConverter(), typeof(LevelDescription), typeof(XmlBriefingRoot)),
                    (new LevelDatas.PointConverter(), typeof(Point), typeof(XmlCoord)),
                    (new LevelDatas.SizeConverter(), typeof(Size), typeof(XmlCoord)),
                };
                return new(converters);
            });
            return services;
        }
    }
}
