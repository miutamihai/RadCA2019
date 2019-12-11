using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CA20192020.MVC.Controllers
{
    public class StudentController : Controller
    {
        DataModel.Context context;
        public StudentController()
        {
            context = new DataModel.Context();
        }
        public ActionResult ViewAttendance()
        {
            string loggedStudentID = System.Web.HttpContext.Current.User.Identity.Name.Split('@')[0];
            var attendanceList = context.ModuleAttendances.ToList().FindAll(i => i.StudentID == loggedStudentID);
            return View(attendanceList);
        }
        public ActionResult ViewGrades()
        {
            string loggedStudentID = System.Web.HttpContext.Current.User.Identity.Name.Split('@')[0];
            var grades = context.StudentGrades.ToList().FindAll(i => i.StudentID == loggedStudentID);
            return View(grades);
        }

        public ActionResult ModuleEnrollment()
        {
            ViewBag.EnrollmentMessage = TempData["EnrollmentMessage"];
            return View(context.Modules);
        }

        public ActionResult Enroll(int moduleID)
        {
            string loggedStudentID = System.Web.HttpContext.Current.User.Identity.Name.Split('@')[0];
            int numberOfModulesAlreadyChosen = context.Enrollments.ToList().FindAll(i => i.StudentID == loggedStudentID).Count;
            if (numberOfModulesAlreadyChosen >= 6)
            {
                TempData["EnrollmentMessage"] = "You've already enrolled in the maximum number of modules (6 modules)";
                return RedirectToAction("ModuleEnrollment");
            }
            if (context.Enrollments.ToList().Exists(i => i.ModuleID == moduleID && i.StudentID == loggedStudentID))
            {
                TempData["EnrollmentMessage"] = "You've already enrolled in that module";
                return RedirectToAction("ModuleEnrollment");
            }
            context.EnrollmentApplications.Add(new DataModel.EnrollmentApplication()
            {
                StudentID = loggedStudentID,
                ModuleID = moduleID,
                DateApplied = DateTime.Today
            });
            context.SaveChanges();
            TempData["EnrollmentMessage"] = "You've successfuly enrolled for selected module";
            return RedirectToAction("ModuleEnrollment");
        }
    }
}