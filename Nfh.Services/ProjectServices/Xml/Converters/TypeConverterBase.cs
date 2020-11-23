namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal abstract class TypeConverterBase<TDomain, TXml> : ITypeConverter
        where TDomain : notnull
        where TXml : notnull
    {
        public object ConvertToDomain(object xmlModel) =>
            ConvertToDomain((TXml)xmlModel);

        public object ConvertToXml(object domain) =>
            ConvertToXml((TDomain)domain);

        public abstract TDomain ConvertToDomain(TXml xmlModel);
        public abstract TXml ConvertToXml(TDomain domain);
    }
}
