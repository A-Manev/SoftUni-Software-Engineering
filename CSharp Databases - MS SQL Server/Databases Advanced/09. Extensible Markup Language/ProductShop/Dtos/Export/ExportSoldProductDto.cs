namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;
    using System.Collections.Generic;

    [XmlType("SoldProducts")]
    public class ExportSoldProductDto
    {
        [XmlElement(ElementName = "count")]
        public int Count { get; set; }

        [XmlArray(ElementName = "products")]
        public List<ExportProductDto> Products { get; set; }
    }
}
