namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("Product")]
    public class UserSoldProductDto
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "price")]
        public decimal Price { get; set; }
    }
}
