using Nfh.Services.ProjectServices.Xml.Models.Anims;
using Nfh.Services.ProjectServices.Xml.Models.GfxData;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using Nfh.Services.ProjectServices.Xml.Models.Objects;
using Nfh.Services.ProjectServices.Xml.Models.Strings;

namespace Nfh.Services.ProjectServices.Xml.Models
{
    internal class LevelData
    {
        public LevelRoot LevelRoot { get; set; } = new();
        public AnimsRoot AnimsRoot { get; set; } = new();
        public GfxDataRoot GfxDataRoot { get; set; } = new();
        public StringsRoot StringsRoot { get; set; } = new();
        public ObjectsRoot ObjectsRoot { get; set; } = new();
    }
}
