namespace TeisterMask.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [XmlType("Project")]
    public class ImportProjectDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement(ElementName = "OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement(ElementName = "DueDate")]
        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public List<ImportTaskDto> Tasks { get; set; }
    }
}
