namespace Nfh.Services.ProjectServices.Xml.Converters
{
    internal interface ITypeConverter
    {
        public object ConvertToDomain(object xmlModel);
        public object ConvertToXml(object domain);
    }
}
