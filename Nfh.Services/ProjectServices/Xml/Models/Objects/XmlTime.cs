namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlTime
    {
        public static readonly XmlTime Auto = new XmlTime(-1);

        public int Amount { get; set; }

        public XmlTime(int amount)
        {
            Amount = amount;
        }
    }
}