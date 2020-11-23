using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Models;
using System;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class LevelDataConverter : TypeConverterBase<Level, XmlLevelData>
    {
        private readonly SizeConverter sizeConverter;

        public LevelDataConverter(SizeConverter sizeConverter)
        {
            this.sizeConverter = sizeConverter;
        }

        public override Level convertToDomain(XmlLevelData xmlModel)
        {
            var levelRoot = xmlModel.LevelRoot;
            var level = new Level
            {
                Name = levelRoot.Name,
                AngryTime = levelRoot.AngryTime,
                Size = sizeConverter.convertToDomain(levelRoot.Size), // ToDo
                Meta = new(""), // ToDo get from briefings
                //Objects = levelRoot.Objects.Select(convertToLevelObjects).ToDictionary(v => v.Id, v => v),
                // Rooms = levelRoot.Rooms.Select(convertToRoom).ToDictionary(v => v.Id, v => v),
            };
            return new();
        }

        public override XmlLevelData convertToXml(Level domain)
        {
            throw new NotImplementedException();
        }
    }
}
