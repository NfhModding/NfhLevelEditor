using Nfh.Dal.Xml.Models;

namespace Nfh.Dal
{
    internal interface ILevelDataUnifier
    {
        XmlLevelData UnifyWithGeneric(XmlLevelData generic, XmlLevelData level);
        XmlLevelData SeperateFromGeneric(XmlLevelData generic, XmlLevelData unified);
    }
}
