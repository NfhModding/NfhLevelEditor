using System.Collections.Generic;

namespace Nfh.Domain.Models.InGame
{
    /// <summary>
    /// The different strings that can be attached to a <see cref="LevelObject"/>.
    /// This can be things like the object's name or it's description.
    /// The game uses this to translate the game to different languages.
    /// </summary>
    public class Localization
    {
        public IDictionary<string, string> Strings { get; set; }
        public string? Name
        {
            get => LookUp("name");
            set => Strings["name"] = value;
        }
        public string? Description
        {
            get => LookUp("description");
            set => Strings["description"] = value;
        }

        private string? LookUp(string key) =>
            Strings.TryGetValue(key, out var value) ? value : null;
    }
}
