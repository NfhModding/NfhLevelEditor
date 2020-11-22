namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal abstract class TypeConverterBase<TDomain, TXml> : ITypeConverter
        where TDomain : notnull
        where TXml : notnull
    {
        public object ConvertToDomain(object xmlModel) =>
            convertToDomain((TXml)xmlModel);

        public object ConvertToXml(object domain) =>
            convertToXml((TDomain)domain);

        public abstract TDomain convertToDomain(TXml xmlModel);
        public abstract TXml convertToXml(TDomain domain);
    }
}
