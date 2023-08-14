using CRUDOperationsDemo;
using CRUDOperationsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using School.Models;

namespace School.Controllers
{
    public class NewStudentController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly IWebHostEnvironment env;

        public int weeks;
        public NewStudentController(SchoolDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        [HttpPost]

        public IActionResult Add([FromBody] NewStudentFrom std)
        {
            User newStudent = new User();
            newStudent.FirstName = std.firstName;
            newStudent.LastName = std.lastName;
            newStudent.Title = "student";
            newStudent.Email = std.schoolEmail;
            newStudent.ClientId = 1;
            newStudent.DateCreated = DateTime.Now;
            newStudent.DateModified = DateTime.Now;
            newStudent.Password = "123456";

            Enrollment newEnroll = new Enrollment();
            newEnroll.Student = std.firstName + " " + std.lastName;
            newEnroll.Subject = _context.subjects.Single(item => item.Id == std.subjectId).Name;
            newEnroll.Teacher = _context.users.Single(item => item.Id == std.teacherId).FirstName + " " + _context.users.Single(item => item.Id == std.teacherId).LastName;

            _context.users.Add(newStudent);
            _context.enrolls.Add(newEnroll);

            _context.SaveChanges();
            return Json(true);
        }
        public IActionResult Index()
        {
            NewStudentViewModel form = new NewStudentViewModel();
            List<NewStudentFrom> aaa = new List<NewStudentFrom>();

            foreach(var item in _context.semesterTeacherSubjects)
            {
                NewStudentFrom temp = new NewStudentFrom();
                temp.subjectName = _context.subjects.First(a => a.Id == item.SubjectId)?.Name ?? "";
                temp.teacherName = _context.users.First(a => a.Id == item.TeacherId).FirstName + " " + _context.users.First(a => a.Id == item.TeacherId)?.LastName ?? "";
                temp.teacherId = item.TeacherId;
                temp.subjectId = item.SubjectId;
                aaa.Add(temp);
            }

            form.studentForms = aaa;

            return View(form);
        }
    }
}
