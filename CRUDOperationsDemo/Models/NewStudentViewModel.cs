using School.Models;
using Microsoft.AspNetCore.Mvc;

namespace School.Models
{
    public class NewStudentViewModel
    {
        public IEnumerable<NewStudentFrom> studentForms { get; set; } = new List<NewStudentFrom>();

    }

    public class NewStudentFrom
    {
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;

        public int teacherId { get; set; } = 0;

        public int subjectId { get; set; } = 0;
        public string teacherName { get; set; } = string.Empty;
        public string subjectName { get; set; } = string.Empty;
        public string schoolEmail { get; set; } = string.Empty;

    }
}
