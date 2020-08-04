namespace Cinema.DataProcessor.ImportDto
{
    using System.Collections.Generic;

    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    [XmlType("Customer")]
    public class ImportCustomerTicketDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Range(12, 110)]
        public int Age { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Balance { get; set; }

        [XmlArray("Tickets")]
        public List<ImportTicketDto> Tickets { get; set; }
    }
}
