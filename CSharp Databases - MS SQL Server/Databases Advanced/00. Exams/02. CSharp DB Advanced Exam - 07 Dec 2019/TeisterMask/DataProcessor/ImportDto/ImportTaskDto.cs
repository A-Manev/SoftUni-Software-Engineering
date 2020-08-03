namespace TeisterMask.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    [XmlType("Task")]
    public class ImportTaskDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement(ElementName = "OpenDate")]
        public string OpenDate { get; set; }

        [Required]
        [XmlElement(ElementName = "DueDate")]
        public string DueDate { get; set; }

        [Required]
        [Range(0, 3)]
        [XmlElement(ElementName = "ExecutionType")]
        public int ExecutionType { get; set; }

        [Required]
        [Range(0, 4)]
        [XmlElement(ElementName = "LabelType")]
        public int LabelType { get; set; }
    }
}
