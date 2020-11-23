using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal static class ConverterBuilder
    {
        public static IServiceCollection AddConverters(this IServiceCollection services)
        {
            services.AddSingleton<IConverter, Converter>(_ =>
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

                return converter;
            });

            return services;
        }
    }
}
