namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlObjectsTime
    {
        public static readonly XmlObjectsTime Auto = new XmlObjectsTime(-1);

        public int Amount { get; set; }

        public XmlObjectsTime(int amount)
        {
            Amount = amount;
        }
    }
}