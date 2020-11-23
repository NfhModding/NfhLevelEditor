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
    internal class ActorConverter : TypeConverterBase<Actor, XmlLevelActor>
    {
        private readonly IConverter converter;

        public ActorConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override Actor convertToDomain(XmlLevelActor actor) => new(actor.Name)
        {
            Layer = actor.Layer,
            Position = converter.Convert<XmlCoord, Point>(actor.Position),
            // The rest is connected later
        };

        public override XmlLevelActor convertToXml(Actor domain)
        {
            throw new NotImplementedException();
        }
    }
}
