using Nfh.Domain.Models.InGame;
using System.Drawing;
using Nfh.Dal.Xml.Models.Common;
using Nfh.Dal.Xml.Models.Level;

namespace Nfh.Dal.Xml.Converters.LevelDatas
{
    internal class ActorConverter : TypeConverterBase<Actor, XmlLevelActor>
    {
        private readonly IConverter converter;

        public ActorConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override Actor ConvertToDomain(XmlLevelActor actor) => new(actor.Name)
        {
            Layer = actor.Layer,
            Position = converter.Convert<XmlCoord, Point>(actor.Position),
            // The rest is connected later
        };

        public override XmlLevelActor ConvertToXml(Actor actor) => new()
        {
            Name = actor.Id,
            Layer = actor.Layer,
            Position = converter.Convert<Point, XmlCoord>(actor.Position),
            // ToDo Animation = 
        };
    }
}
