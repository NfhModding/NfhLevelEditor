namespace Nfh.Dal.Xml.Converters
{
    internal interface ITypeConverter
    {
        public object ConvertToDomain(object xmlModel);
        public object ConvertToXml(object domain);
    }
}
