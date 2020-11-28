namespace Nfh.Dal.Xml.Models.Common
{
    internal class XmlCoord
    {
        public static XmlCoord Zero => new XmlCoord { X = 0, Y = 0 };

        public int X { get; set; }
        public int Y { get; set; }
    }
}
