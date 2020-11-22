using System;
using System.Collections.Generic;

using DomainType = System.Type;
using XmlType = System.Type;

namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal class Converter
    {
        private readonly Dictionary<(DomainType, XmlType), ITypeConverter> converters = new();

        public Converter(
            IReadOnlyCollection<(ITypeConverter converter, DomainType domainType, XmlType xmlType)> converters)
        {
            foreach (var (converter, domainType, xmlType) in converters)
            {
                this.converters[(domainType, xmlType)] = converter;
            }
        }

        public TTo Convert<TFrom, TTo>(TFrom model)
            where TFrom : notnull
            where TTo : notnull
        {
            return (TTo)convert(typeof(TFrom), typeof(TTo), model);
        }

        private object convert(Type from, Type to, object model)
        {
            var toDomainConverter = converters.GetValueOrDefault((to, from));
            if (toDomainConverter is not null)
                return toDomainConverter.ConvertToDomain(model);

            var toXmlConverter = converters.GetValueOrDefault((from, to));
            if (toXmlConverter is not null)
                return toXmlConverter.ConvertToXml(model);

            throw new("Something went wrong");
        }
    }

    
}
