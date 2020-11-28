using Nfh.Dal.Xml.Models.Briefing;
using Nfh.Domain.Models.Meta;

namespace Nfh.Dal.Xml.Converters.Meta
{
    internal class LevelDescriptionConverter : TypeConverterBase<LevelDescription, XmlBriefingRoot>
    {
        public override LevelDescription ConvertToDomain(XmlBriefingRoot briefing) => new()
        {
            Title = briefing.Title,
            Hint = briefing.Hint,
            ThumbnailDescription = briefing.ThumbnailDescription,
            Description = briefing.LevelDescription,
        };

        public override XmlBriefingRoot ConvertToXml(LevelDescription levelDescription) => new()
        {
            Title = levelDescription.Title,
            Hint = levelDescription.Hint,
            ThumbnailDescription = levelDescription.ThumbnailDescription,
            LevelDescription = levelDescription.Description,
        };
    }
}
