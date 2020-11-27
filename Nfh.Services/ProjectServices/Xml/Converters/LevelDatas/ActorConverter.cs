using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using System.Drawing;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
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
