namespace ProductShop.Dtos.Export
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("User")]
    public class SoldProductDto
    {
        [XmlElement(ElementName = "firstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "lastName")]
        public string LastName { get; set; }

        [XmlArray(ElementName = "soldProducts")]
        public List<UserSoldProductDto> SoldProducts { get; set; }
    }
}
