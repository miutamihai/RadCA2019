using CA20192020.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CA20192020.MVC.Controllers
{
    public class LecturerController : Controller
    {
        private int lecturerID;
        DataModel.Context context;
        public LecturerController()
        {
            context = new DataModel.Context();
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            int lastCapital = Array.FindLastIndex<char>(email.ToCharArray(), Char.IsUpper);
            string lastName = null;
            if (lastCapital >= 0)
                lastName = email.Substring(lastCapital);
            lastName = lastName.Split('@')[0];
            int index = email.IndexOf(lastName, StringComparison.Ordinal);
            string firstName = (index < 0)
                ? email
                : email.Remove(index, lastName.Length);
            firstName = firstName.Split('@')[0];
            lecturerID = context.Lecturers.ToList().Find(x => x.LastName == ' ' + lastName && x.FirstName == firstName).ID;
        }
        public ActionResult ViewAssessments()
        {
            return View(context.ModuleAssessments);
        }
        [HttpPost]
        public ActionResult CreateNewAttendanceSheet(CreateAttendanceSheet model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int moduleID = context.Modules.ToList().Find(i => i.ModuleCode == model.ModuleCode).ID;
            if (!context.ModuleDeliveries.ToList().Exists(x => x.ModuleID == moduleID && x.LecturerID == lecturerID))
            {
                TempData["ErrorMessage"] = "You cannot register attendance for this module as you do no teach it";
                return RedirectToAction("CreateAttendanceSheet");
            }
            DataModel.Classes.ModuleAttendance attendance = new DataModel.Classes.ModuleAttendance()
            {
                ID = model.ID,
                ModuleID = moduleID,
                StudentID = model.StudentID,
                StartDateTime = model.StartDateTime,
                FinishDateTime = model.FinishDateTime
            };
            if (context.ModuleAttendances.Contains(attendance))
            {
                TempData["ErrorMessage"] = "You've already recorded this attendance";
                return RedirectToAction("CreateAttendanceSheet");
            }
            context.ModuleAttendances.Add(attendance);
            context.SaveChanges();
            return RedirectToAction("AttendanceSheets");
        }
        public ActionResult AttendanceSheets()
        {
            return View(context.ModuleAttendances);
        }
        public ActionResult CreateAttendanceSheet()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(new CreateAttendanceSheet());
        }
        public ActionResult CreateAssessment()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(new DataModel.Classes.ModuleAssessment());
        }
        [HttpPost]
        public ActionResult CreateNewAssessment(DataModel.Classes.ModuleAssessment assessment)
        {
            if (context.ModuleAssessments.ToList().Contains(assessment))
            {
                TempData["ErrorMessage"] = "You have already created this assignment";
                return RedirectToAction("CreateAssessment");
            }
            int moduleID = context.Modules.ToList().Find(i => i.ModuleCode == assessment.Module.ModuleCode).ID;
            if(!context.ModuleDeliveries.ToList().Exists(i => i.ModuleID == moduleID && i.LecturerID == lecturerID))
            {
                TempData["ErrorMessage"] = "You cannot create an assessment for this module as you do not teach it";
                return RedirectToAction("CreateAssessment");
            }
            assessment.ModuleID = moduleID;
            context.ModuleAssessments.Add(assessment);
            context.SaveChanges();
            return RedirectToAction("ViewAssessments");
        }
        public ActionResult AssessmentGrades()
        {
            return View(context.StudentGrades);
        }
        public ActionResult GradeAssessment()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(new DataModel.Classes.StudentGrade());
        }
        [HttpPost]
        public ActionResult CreateGrade(DataModel.Classes.StudentGrade grade)
        {
            if (context.StudentGrades.ToList().Contains(grade))
            {
                TempData["ErrorMessage"] = "You have already graded this assignment";
                return RedirectToAction("GradeAssessment");
            }
            if (!context.ModuleDeliveries.ToList().Exists(i => i.ModuleID == grade.ModuleAssessment.ModuleID && i.LecturerID == lecturerID))
            {
                TempData["ErrorMessage"] = "You cannot grade an assessment for this module as you do not teach it";
                return RedirectToAction("CreateAssessment");
            }
            context.StudentGrades.Add(grade);
            context.SaveChanges();
            return RedirectToAction("AssessmentGrades");
        }
        public ActionResult EditGrade(int gradeID)
        {
            return RedirectToAction("GradeAssessment", new { grade = context.StudentGrades.ToList().Find(i => i.ID == gradeID)});
        }
        public ActionResult DeleteGrade(int gradeID)
        {
            var grade = context.StudentGrades.ToList().Find(i => i.ID == gradeID);
            context.StudentGrades.Remove(grade);
            context.SaveChanges();
            return RedirectToAction("AssessmentGrades");
        }
        public ActionResult ModulesTeached()
        {
            List<int> taughtModulesID = context.ModuleDeliveries.ToList().FindAll(x => x.LecturerID == lecturerID).ToList().Select(x => x.ModuleID).ToList();
            var modules = context.Modules.ToList();
            modules.RemoveAll(module => taughtModulesID.Find(id => id == module.ID) == 0);
            return View(modules);
        }
    }
}