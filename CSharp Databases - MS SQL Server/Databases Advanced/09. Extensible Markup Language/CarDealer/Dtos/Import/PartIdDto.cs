namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("partId")]
    public class PartIdDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
