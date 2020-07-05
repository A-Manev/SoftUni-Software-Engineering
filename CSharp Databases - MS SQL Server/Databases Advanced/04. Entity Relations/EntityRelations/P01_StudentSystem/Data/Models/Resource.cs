using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_StudentSystem.Data.Models
{
    //ResourceId
    //Name(up to 50 characters, unicode)
    //Url(not unicode)
    //ResourceType(enum – can be Video, Presentation, Document or Other)
    //CourseId

    public enum ResourceType
    {
        Video = 1,
        Presentation = 2,
        Document = 3,
        Other = 4 
    }

    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public ResourceType ResourceType { get; set; }

        [Required]
        //[ForeignKey("Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
