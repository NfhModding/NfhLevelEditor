namespace Nfh.Domain.Models.InGame
{
    public class Door : LevelObject
    {
        /// <summary>
        /// The <see cref="Door"/> that the actors will walk out, if they enter this one.
        /// Can be null, if this doesn't lead anywhere.
        /// </summary>
        public Door? Exit { get; set; }
    }
}
