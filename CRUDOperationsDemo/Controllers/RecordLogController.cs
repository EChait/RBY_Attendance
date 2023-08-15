using School;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using School.Models;
using System;

namespace School.Controllers
{
    public class RecordLogController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly IWebHostEnvironment env;
        public RecordLogController(SchoolDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        [HttpPost]
        public IActionResult ChangeDate([FromBody] DateTime date)
        {
            TempData["date"] = date;
            return Json(true);
        }

        [HttpPost]
        public IActionResult SaveRecord([FromBody]RecordLogItem[] logs)
        {
            foreach(var item in logs)
            {
                _context.studentAbsenses.Single(modelItem => modelItem.Id == item.Id).Note = item.note;

                if (item.type == "Present")
                    _context.studentAbsenses.Single(modelItem => modelItem.Id == item.Id).AbsenseType = 1;
                else if (item.type == "Late")
                    _context.studentAbsenses.Single(modelItem => modelItem.Id == item.Id).AbsenseType = 2;
                else if (item.type == "Absent Unexcused")
                    _context.studentAbsenses.Single(modelItem => modelItem.Id == item.Id).AbsenseType = 3;
                else if (item.type == "Absent Excused")
                    _context.studentAbsenses.Single(modelItem => modelItem.Id == item.Id).AbsenseType = 4;
                else if (item.type == "Mandated Absence")
                    _context.studentAbsenses.Single(modelItem => modelItem.Id == item.Id).AbsenseType = 5;
                else if (item.type == "Excused Late")
                    _context.studentAbsenses.Single(modelItem => modelItem.Id == item.Id).AbsenseType = 6;
                else if (item.type == "Infraction")
                    _context.studentAbsenses.Single(modelItem => modelItem.Id == item.Id).AbsenseType = 7;
            }

            _context.SaveChanges();
            return Json(true);
        }
        public IActionResult Index()
        {
            DateTime toDate = DateTime.Now.Date;
            if(TempData["date"] != null)
            {
                toDate = Convert.ToDateTime(TempData["date"].ToString());
            }

            List<RecordLogItem> absenses = new List<RecordLogItem>();
            RecordLogViewModel record = new RecordLogViewModel();
            record.semester = "Semester";

            var semester = _context.semesters.FirstOrDefault(s => toDate.Date >= s.FromDate && toDate.Date <= s.EndDate);
            record.semester = semester?.Name;

            record.absenseTypes.Add("Absent Excused");
            record.absenseTypes.Add("Absent Unexcused");
            record.absenseTypes.Add("Mandated Absence");
            record.absenseTypes.Add("Excused Late");
            record.absenseTypes.Add("Infraction");
            record.absenseTypes.Add("Late");
            record.absenseTypes.Add("Present");

            
            var studentAbsenses = _context.studentAbsenses.Where(a => a.AbsenseType != 1 && a.AuditDate.Date == toDate)
                .ToList();

            foreach (var item in studentAbsenses)
            {
                    RecordLogItem temp = new RecordLogItem();


                    if (item.AbsenseType == 2) temp.type = "Late"; 
                    if (item.AbsenseType == 3) temp.type = "Absent Unexcused";
                    if (item.AbsenseType == 4) temp.type = "Absent Excused"; 
                    if (item.AbsenseType == 5) temp.type = "Mandated Absence"; 
                    if (item.AbsenseType == 6) temp.type = "Excused Late"; 
                    if (item.AbsenseType == 7) temp.type = "Infraction";

                    temp.Id = item.Id;
                    temp.teacher = item.Teacher;
                    temp.note = item.Note;
                    if (temp.note == "" || temp.note == null)
                        temp.isNote = false;
                    
                    temp.subject = item.Subject;
                    var student = _context.users.Single(s => s.Id == item.StudentId);
                    temp.student = student.FirstName + " " + student.LastName;
                    temp.period = _context.subjects.FirstOrDefault(s => s.Name == item.Subject)?.PeriodCount ?? 0;

                    absenses.Add(temp);
            }

            record.date = toDate;
            record.dayOfWeek = toDate.DayOfWeek.ToString();
            record.records = absenses;


            var presentStudents = _context.studentAbsenses.Where(a => a.AuditDate.Date == toDate
                                                                 && (a.AbsenseType == 1 || a.AbsenseType == 2 || a.AbsenseType == 7))
                                                          .Select(a => a.StudentId)
                                                          .Distinct()
                                                          .ToList();

            var absentStudents = studentAbsenses.Where(sa => sa.AbsenseType == 3 || sa.AbsenseType == 4 || sa.AbsenseType == 5)
                                                        .Select(a => a.StudentId)
                                                        .Distinct()
                                                        .Where(sa => !presentStudents.Any(ps => ps == sa));

            record.totalStudentsAbsent = absentStudents.Count();
            return View(record);
        }
    }
}
