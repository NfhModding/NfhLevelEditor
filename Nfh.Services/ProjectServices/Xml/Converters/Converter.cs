using System;
using System.Collections.Generic;
using System.Linq;
using DomainType = System.Type;
using XmlType = System.Type;

namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal class Converter : IConverter
    {
        private readonly Dictionary<(DomainType DomainType, XmlType XmlType), ITypeConverter> converters = new();

        public void RegisterConverter(DomainType domainType, XmlType xmlType, ITypeConverter converter)
        {
            converters[(domainType, xmlType)] = converter;
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
            if (toDomainConverter is null)
                toDomainConverter = supportSpecialCasesToDomain(from, to);
            if (toDomainConverter is not null)            
                return toDomainConverter.ConvertToDomain(model);
           
            var toXmlConverter = converters.GetValueOrDefault((from, to));
            if (toXmlConverter is not null)
                return toXmlConverter.ConvertToXml(model);

            throw new KeyNotFoundException("Something went wrong");
        }

        // ToDo Get rid of this...
        private ITypeConverter? supportSpecialCasesToDomain(Type from, Type to)
        {
            if (from == typeof(List<Xml.Models.Level.XmlLevelFloor>) && to == typeof(List<Domain.Models.InGame.Wall>))
            {
                return converters.Values.FirstOrDefault(c => c.GetType().Name.Contains("WallsConverter"));
            }
            if (from == typeof(List<Xml.Models.Level.XmlLevelFloor>) && to == typeof(List<Domain.Models.InGame.Floor>))
            {
                return converters.Values.FirstOrDefault(c => c.GetType().Name.Contains("FloorsConverter"));
            }
            return null;
        }
    }    
}
