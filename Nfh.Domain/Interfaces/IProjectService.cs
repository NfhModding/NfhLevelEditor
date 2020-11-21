using Nfh.Domain.Models.InGame;
using Nfh.Domain.Models.Meta;
using System.Collections.Generic;

namespace Nfh.Domain.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Lists the valid projects in the given root (non-recursive).
        /// </summary>
        public IList<string> ListProjects(string rootFolder);

        /// <summary>
        /// Unpacks the gamedata.bnd from the game to some folder.
        /// </summary>
        public void CreateProject(string gameSourcePath, string targetProjectPath);

        /// <summary>
        /// Deletes the given folder of unpacked gamedata.
        /// </summary>
        public void DeleteProject(string targetProjectPath);

        /// <summary>
        /// Re-packs the gamedata.bnd and overwrites the game files with it.
        /// </summary>
        public void PatchGame(string sourceProjectPath, string targetGamePath);

        /// <summary>
        /// Loads the <see cref="SeasonPack"/> from the given (unpacked) source folder.
        /// </summary>
        public SeasonPack LoadSeasonPack(string sourcePath);

        /// <summary>
        /// Loads a given <see cref="Level"/> from the given (unpacked) source folder.
        /// </summary>
        public Level LoadLevel(string sourcePath, string levelId);

        /// <summary>
        /// Saves the given <see cref="SeasonPack"/> to the given (unpacked) source folder.
        /// </summary>
        public void SaveSeasonPack(SeasonPack seasonPack, string targetPath);

        /// <summary>
        /// Saves the given <see cref="Level"/> to the given (unpacked) source folder.
        /// </summary>
        public void SaveLevel(Level level, string targetPath);
    }
}
