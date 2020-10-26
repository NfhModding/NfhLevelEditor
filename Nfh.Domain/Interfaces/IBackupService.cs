namespace Nfh.Domain.Interfaces
{
    public interface IBackupService
    {
        /// <summary>
        /// Backs up the gamedata.bnd file from the game to somewhere else.
        /// </summary>
        public void BackupGameData(string sourceGamePath, string targetBackupPath);
        /// <summary>
        /// Restores the gamedata.bnd from a source to the game.
        /// </summary>
        public void RestoreGameData(string sourceBackupPath, string targetGamePath);
    }
}
