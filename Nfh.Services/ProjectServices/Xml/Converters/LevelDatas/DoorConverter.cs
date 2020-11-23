using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class DoorConverter : TypeConverterBase<Door, XmlLevelDoor>
    {
        private readonly IConverter converter;

        public DoorConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override Door convertToDomain(XmlLevelDoor door) => new(door.Name)
        {
            Layer = door.Layer,
            Position = converter.Convert<XmlCoord, Point>(door.Position),
            // The rest is connected later
        };

        public override XmlLevelDoor convertToXml(Door domain)
        {
            throw new NotImplementedException();
        }
    }
}
