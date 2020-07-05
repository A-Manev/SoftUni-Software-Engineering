using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_StudentSystem.Data.Models
{
    //HomeworkId
    //Content(string, linking to a file, not unicode)
    //ContentType(enum – can be Application, Pdf or Zip)
    //SubmissionTime
    //StudentId
    //CourseId

    public enum ContentType
    {
        Application = 1,
        Pdf = 2,
        Zip = 3
    }

    public class Homework
    {
        [Key]
        public int HomeworkId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public ContentType ContentType { get; set; }

        [Required]
        public DateTime SubmissionTime { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
