using Nfh.Domain.Models.Meta;
using Nfh.Services.ProjectServices.Xml.Models.Briefing;

namespace Nfh.Services.ProjectServices.Xml.Converters.Meta
{
    internal class LevelDescriptionConverter : TypeConverterBase<LevelDescription, XmlBriefingRoot>
    {
        public override LevelDescription convertToDomain(XmlBriefingRoot briefing) => new()
        {
            Title = briefing.Title,
            Hint = briefing.Hint,
            ThumbnailDescription = briefing.ThumbnailDescription,
            Description = briefing.LevelDescription,
        };

        public override XmlBriefingRoot convertToXml(LevelDescription levelDescription) => new()
        {
            Title = levelDescription.Title,
            Hint = levelDescription.Hint,
            ThumbnailDescription = levelDescription.ThumbnailDescription,
            LevelDescription = levelDescription.Description,
        };
    }
}
