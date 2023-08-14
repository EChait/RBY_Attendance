using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models
{
    [Table("SemesterTeacherSubject")]
    public class SemesterTeacherSubject
    {
        [Key]
        [Display(Name = "Id")]
        public int? Id { get; set; }

        [Required]
        [Display(Name = "ClientId")]
        [Column(TypeName = "int")]
        public int ClientId { get; set; } = 0;

        [Required]
        [Display(Name = "SemesterId")]
        [Column(TypeName = "int")]
        public int SemesterId { get; set; } = 0;

        [Required]
        [Display(Name = "TeacherId")]
        [Column(TypeName = "int")]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; } = 0;
        public User Teacher { get; set; } = new  User();

        [Required]
        [Display(Name = "SubjectId")]
        [Column(TypeName = "int")]
        [ForeignKey("Subjects")]
        public int SubjectId { get; set; } = 0;
        public Subject Subject { get; set; } = new Subject();
    }
}
