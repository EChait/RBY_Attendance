using School;
using Microsoft.AspNetCore.Mvc;
using School.Models;

namespace School.Controllers
{
    public class StudentAbsenseController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly IWebHostEnvironment env;
        public StudentAbsenseController(SchoolDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        [HttpPost]
        public ActionResult CreateAbsense([FromBody]List<StudentAbsense> logItems)
        {
            foreach(var std in logItems)
            {
                std.AuditDate = DateTime.Now;

                var existingRecord = _context.studentAbsenses.SingleOrDefault(item => item.StudentId == std.StudentId
                                                                                    && item.Subject == std.Subject
                                                                                    && item.Teacher == std.Teacher
                                                                                    && item.Date == std.Date);
                if (existingRecord != null)
                {
                        existingRecord.AbsenseType = std.AbsenseType;
                        existingRecord.Note = std.Note;
                        existingRecord.Date = std.Date;
                }
                else
                {
                    _context.studentAbsenses.Add(std);
                }
            }
            _context.SaveChanges();
            return Json(1);
        }
    }
}
