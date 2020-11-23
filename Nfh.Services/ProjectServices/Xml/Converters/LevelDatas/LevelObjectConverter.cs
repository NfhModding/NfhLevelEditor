using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Models;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using System.Drawing;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class LevelObjectConverter : TypeConverterBase<LevelObject, XmlLevelObject>
    {
        private readonly IConverter converter;

        public LevelObjectConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override LevelObject ConvertToDomain(XmlLevelObject obj) => new(obj.Name)
        {
            Layer = obj.Layer,
            Position = converter.Convert<XmlCoord, Point>(obj.Position), // ToDo nullable things in converters
                                                                         // The rest is connected later
        };

        public override XmlLevelObject ConvertToXml(LevelObject domain)
        {
            throw new System.NotImplementedException();
        }
    }
}
