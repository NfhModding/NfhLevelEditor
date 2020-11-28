using Nfh.Dal.Xml.Models.Anims;
using Nfh.Dal.Xml.Models.GfxData;
using Nfh.Dal.Xml.Models.Level;
using Nfh.Dal.Xml.Models.Objects;
using Nfh.Dal.Xml.Models.Strings;

namespace Nfh.Dal.Xml.Models
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
