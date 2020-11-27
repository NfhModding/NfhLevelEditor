using Nfh.Services.ProjectServices.Xml.Models;

namespace Nfh.Services.ProjectServices
{
    internal interface ILevelDataUnifier
    {
        XmlLevelData UnifyWithGeneric(XmlLevelData generic, XmlLevelData level);
        XmlLevelData SeperateFromGeneric(XmlLevelData generic, XmlLevelData unified);
    }
}
