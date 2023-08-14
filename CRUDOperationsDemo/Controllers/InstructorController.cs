using School;
using School.Models;
using Microsoft.AspNetCore.Mvc;
using School.Models;
using System.Linq;

namespace School.Controllers
{
    public class InstructorController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly IWebHostEnvironment env;

        public InstructorController(SchoolDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        public IActionResult Index()
        {
            int teacherId = 0;
            InstructorViewModel instructorVM = new InstructorViewModel();
            instructorVM.subjects = new List<Subject>();
            instructorVM.tsubject = new List<Subject>();
            instructorVM.users = new List<User>();
            instructorVM.teacher = new User();

            List<User> users = new List<User>();
            List<Subject> tsubjects = new List<Subject>();

            instructorVM.subjects = _context.subjects.ToList();
            instructorVM.users = _context.users.Where(u => u.Title == "teacher").ToList();

            if (TempData["instTeacherId"] != null)
            {
                teacherId = (int)TempData["instTeacherId"];
                instructorVM.teacherId = teacherId;
                instructorVM.teacher = _context.users.FirstOrDefault(a => a.Id == teacherId);
            }
            else
            {
                //If no teacher selected default to first teacher
                instructorVM.teacher = _context.users.FirstOrDefault(a => a.Title == "teacher");
                teacherId = instructorVM.teacher.Id;
            }

            var semesterTeacherSubjects = _context.semesterTeacherSubjects.Where(sts => sts.TeacherId == teacherId).ToList();            
            instructorVM.tsubject = semesterTeacherSubjects.Select(sts => sts.Subject).ToArray();

            Console.Write(instructorVM.teacher);
            return View(instructorVM);
        }
        
        [HttpPost]
        public IActionResult Add([FromBody]SemesterTeacherSubject temp)
        {
            temp.ClientId = 1;
            temp.SemesterId = 1;

            var isExistingSTS = _context.semesterTeacherSubjects.Any(sts => sts.TeacherId == temp.TeacherId && sts.SubjectId == temp.SubjectId);
            if (!isExistingSTS)
            {
                temp.Subject = _context.subjects.FirstOrDefault(a => a.Id == temp.SubjectId);
                temp.Teacher = _context.users.FirstOrDefault(a => a.Id == temp.TeacherId);
                _context.semesterTeacherSubjects.Add(temp);
                _context.SaveChanges();
            }
            TempData["instTeacherId"] = temp.TeacherId;
            return Json(true);
        }

        public IActionResult AccessTeacher([FromBody]InstructorViewModel model)
        {
            TempData["instTeacherId"] = model.teacherId;
            return Json(true);
        }

        [HttpPost]
        public IActionResult Delete([FromBody]SemesterTeacherSubject temp)
        {
            SemesterTeacherSubject deleteItem = _context.semesterTeacherSubjects.Single(item => item.SubjectId == temp.SubjectId && item.TeacherId == temp.TeacherId);
            _context.semesterTeacherSubjects.Remove(deleteItem);
            TempData["instTeacherId"] = temp.TeacherId;
            _context.SaveChanges();
            return Json(true);
        }
    }
}
