using Microsoft.Extensions.DependencyInjection;
using Nfh.Domain.Models.InGame;
using Nfh.Domain.Models.Meta;
using Nfh.Services.ProjectServices.Xml.Converters.LevelDatas;
using Nfh.Services.ProjectServices.Xml.Converters.Meta;
using Nfh.Services.ProjectServices.Xml.Models.Briefing;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal static class ConverterBuilder
    {
        public static IServiceCollection AddConverters(this IServiceCollection services)
        {
            services.AddSingleton<IConverter, Converter>(provider =>
            {
                var converter = new Converter();

                var converterInfos = Assembly.GetAssembly(typeof(ITypeConverter))
                    ?.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(ITypeConverter)))
                    .Where(t => !t.IsAbstract && !t.IsInterface)
                    .Where(c => c?.BaseType?.Namespace + c?.BaseType?.Name == typeof(TypeConverterBase<,>).Namespace + typeof(TypeConverterBase<,>).Name)
                    .Select(c =>
                    {
                        var genericTypes = c!.BaseType!.GetGenericArguments();
                        return (converter: c, domainType: genericTypes[0], xmlType: genericTypes[1]);
                    })
                    .ToList();

                foreach (var (converterType, domainType, xmlType) in converterInfos ?? new())
                {
                    var isEmptyCtor = !converterType.GetConstructors().First().GetParameters().Any();
                    var typeConverter = isEmptyCtor ?
                        Activator.CreateInstance(converterType, args: null) :
                        Activator.CreateInstance(converterType, args: converter);

                    converter.RegisterConverter(domainType, xmlType, (ITypeConverter)typeConverter);
                }

                converter.RegisterConverter(typeof(Actor), typeof(XmlLevelActor), new ActorConverter(converter));

                return converter;
            });

            return services;
        }
    }
}
