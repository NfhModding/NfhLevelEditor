using Nfh.Services.ProjectServices.Xml.Models.Anims;
using Nfh.Services.ProjectServices.Xml.Models.GfxData;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using Nfh.Services.ProjectServices.Xml.Models.Objects;
using Nfh.Services.ProjectServices.Xml.Models.Strings;

namespace Nfh.Services.ProjectServices.Xml.Models
{
    internal class XmlLevelData
    {
        public XmlLevelRoot LevelRoot { get; set; } = new();
        public XmlAnimsRoot AnimsRoot { get; set; } = new();
        public XmlGfxRoot GfxDataRoot { get; set; } = new();
        public XmlStringsRoot StringsRoot { get; set; } = new();
        public XmlObjectsRoot ObjectsRoot { get; set; } = new();
    }
}
