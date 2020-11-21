using System;
using System.Collections.Generic;

using DomainType = System.Type;
using XmlType = System.Type;

namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal class Converter
    {
        private Dictionary<XmlType, ITypeConverter> toDomainCoverters = new();
        private Dictionary<DomainType, ITypeConverter> toXmlCoverters = new();

        public Converter(IReadOnlyCollection<(ITypeConverter converter, Type domainType, Type xmlType)> converters)
        {
            foreach (var (converter, domainType, xmlType) in converters)
            {
                toDomainCoverters[xmlType] = converter;
                toXmlCoverters[domainType] = converter;
            }
        }

        public TTo Convert<TFrom, TTo>(TFrom model)
            where TTo : class, new()
        {
            return (TTo)convert(typeof(TFrom), model);
        }

        private object convert(Type from, object model)
        {
            var toDomainConverter = toDomainCoverters.GetValueOrDefault(from);
            if (toDomainConverter is not null)
                return toDomainConverter.ConvertToDomain(model);

            var toXmlConverter = toXmlCoverters.GetValueOrDefault(from);
            if (toXmlConverter is not null)
                return toXmlConverter.ConvertToXml(model);

            throw new("Something went wrong");
        }
    }

    
}
