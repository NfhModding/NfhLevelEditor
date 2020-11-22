namespace Nfh.Services.ProjectServices.Xml.Models.Common
{
    // ToDo refactor
    internal class Coord
    {
        public static Coord Zero => new Coord { X = 0, Y = 0 };

        public int X { get; set; }
        public int Y { get; set; }
        public string Value {
            get => $"{X}/{Y}";
            set {
                var parts = value.Split('/');
                X = int.Parse(parts[0]);
                Y = int.Parse(parts[1]);
            }
        }
    }
}
