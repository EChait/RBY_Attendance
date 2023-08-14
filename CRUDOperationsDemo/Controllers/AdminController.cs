using CRUDOperationsDemo;
using CRUDOperationsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Controllers
{
    public class AdminController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly IWebHostEnvironment env;

        public int weeks;
        public AdminController(SchoolDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Access()
        {
            return View();
        }

        public IActionResult accessStudent(User std)
        {
            TempData["email"] = std.Email;
            User temp = _context.users.Single(modelItem => modelItem.Email == std.Email);
            if (temp.Title == "student")
                return RedirectToAction("Detail");
            else
                return RedirectToAction("Index", "Admin");
        }

        public IActionResult Detail()
        {
            string Email = TempData["email"] as string;
            StudentPortalViewModel Viewdata = new StudentPortalViewModel();
            int studentId;
            string studentName;
            List<PointSystem> points = new List<PointSystem>();
 
            studentId = _context.users.Single(a => a.Email == Email).Id;
            studentName = _context.users.Single(a => a.Email == Email).FirstName + " " + _context.users.Single(a => a.Email == Email).LastName;
            Viewdata.Date = DateTime.Now.ToShortDateString();
            Viewdata.StudentName = studentName;
            int Achrayus = 100;
            Viewdata.Semesters = new List<Semester>();

            foreach (var item in _context.semesters)
            {
                if (DateTime.Now >= item.FromDate && DateTime.Now <= item.EndDate)
                {
                    Viewdata.Current_Semester = item;
                    weeks = item.Weeks;
                }

                Viewdata.Semesters.Add(item);
            }

            if (Viewdata.Current_Semester != null)
            {
                foreach (var enroll in _context.enrolls)
                {
                    if (enroll.Student == studentName)
                    {

                        PointSystem point = new PointSystem();
                        point.StudentAbsenses = new List<StudentAbsense>();

                        point.Teacher = enroll.Teacher;
                        string subject = enroll.Subject;
                        int period = 0;

                        if (_context.subjects.SingleOrDefault(a => a.Name == subject).PeriodCount != null)
                        {
                            period = _context.subjects.Single(a => a.Name == subject).PeriodCount;

                            point.Allowed = period * 15 / 10;
                            point.SubjectName = subject;

                            foreach (var item in _context.studentAbsenses)
                            {
                                if (item.StudentId == studentId && 
                                    item.AbsenseType != 1 &&
                                    enroll.Teacher == item.Teacher &&
                                    enroll.Subject == item.Subject &&
                                    (item.AuditDate >= Viewdata.Current_Semester.FromDate && item.AuditDate <= Viewdata.Current_Semester.EndDate))
                                {
                                    point.StudentAbsenses.Add(item);
                                    switch (item.AbsenseType)
                                    {
                                        case 1:
                                            break;
                                        case 2:
                                            point.Deducated -= 1;
                                            break;
                                        case 3:
                                            point.Deducated -= 2;
                                            break;
                                        case 4:
                                            point.Deducated -= 2;
                                            point.Missed++;
                                            break;
                                    }
                                }
                            }
                            point.Deducated += 2 * point.Allowed;
                            Achrayus -= point.Deducated;
                            points.Add(point);
                        }
                    }
                }
            }
            Viewdata.Achrayus = Achrayus;
            Viewdata.PointSystems = points;

            return View(Viewdata);
        }

        public IActionResult ChangeAchrayus(int nAchrayus)
        {
            foreach (var item in _context.semesters)
            {
                if (DateTime.Now >= item.FromDate && DateTime.Now <= item.EndDate)
                {
                }
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}
