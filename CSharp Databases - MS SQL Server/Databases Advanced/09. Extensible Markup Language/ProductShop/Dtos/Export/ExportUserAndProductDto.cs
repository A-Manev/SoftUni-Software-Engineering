namespace ProductShop.Dtos.Export
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("Users")]
    public class ExportUserAndProductDto
    {
        [XmlElement(ElementName = "count")]
        public int UsersCount { get; set; }

        [XmlArray(ElementName = "users")]
        public List<ExportUserDto> Users { get; set; }
    }
}
