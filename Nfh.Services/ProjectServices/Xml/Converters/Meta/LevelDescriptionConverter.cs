using Nfh.Domain.Models.Meta;
using Nfh.Services.ProjectServices.Xml.Models.Meta;

namespace Nfh.Services.ProjectServices.Xml.Converters.Meta
{
    internal class LevelDescriptionConverter : ITypeConverter
    {
        public object ConvertToDomain(object xmlModel) =>
            convertToDomain((BriefingRoot)xmlModel);

        public object ConvertToXml(object domain) =>
            convertToXml((LevelDescription)domain);

        private LevelDescription convertToDomain(BriefingRoot briefing) => new()
        {
            Title = briefing.Title,
            Hint = briefing.Hint,
            ThumbnailDescription = briefing.ThumbnailDescription,
            Description = briefing.LevelDescription,
        };

        private BriefingRoot convertToXml(LevelDescription levelDescription) => new()
        {
            Title = levelDescription.Title,
            Hint = levelDescription.Hint,
            ThumbnailDescription = levelDescription.ThumbnailDescription,
            LevelDescription = levelDescription.Description,
        };
    }
}
