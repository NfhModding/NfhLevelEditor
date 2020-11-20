namespace Nfh.Domain.Interfaces
{
    public interface IBackupService
    {
        /// <summary>
        /// True, if a backup already exists of the game.
        /// </summary>
        public bool BackupExists { get; }
        /// <summary>
        /// Backs up the gamedata.bnd file from the game to somewhere else.
        /// </summary>
        public void BackupGameData(string sourceGamePath);
        /// <summary>
        /// Restores the gamedata.bnd from a source to the game.
        /// </summary>
        public void RestoreGameData(string targetGamePath);
    }
}
