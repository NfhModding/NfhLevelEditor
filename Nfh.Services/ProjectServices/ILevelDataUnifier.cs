using Nfh.Services.ProjectServices.Xml.Models;

namespace Nfh.Services.ProjectServices
{
    internal interface ILevelDataUnifier
    {
        LevelData UnifyWithGeneric(LevelData generic, LevelData level);
        LevelData SeperateFromGeneric(LevelData unified);
    }
}
