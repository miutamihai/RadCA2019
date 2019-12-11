using CA20192020.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CA20192020.MVC.Controllers
{
    public class AdminController : Controller
    {
        DataModel.Context context;
        public AdminController()
        {
            context = new DataModel.Context();
        }
        public ActionResult CreateModule()
        {
            var modules = context.Modules;
            return View(modules);
        }


        [HttpPost]
        public ActionResult Save(NewModuleInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int lastCapital = Array.FindLastIndex<char>(model.LecturerFullName.ToCharArray(), Char.IsUpper);
            string lastName = null;
            if (lastCapital >= 0)
                lastName = model.LecturerFullName.Substring(lastCapital);
            string firstName = model.LecturerFullName.Split(' ')[0];
            int lecturerID = context.Lecturers.ToList().Find(x => x.LastName == ' ' + lastName && x.FirstName == firstName).ID;
            context.Modules.Add(new DataModel.Classes.Module()
            {
                ID = model.ID,
                ModuleCode = model.ModuleCode,
                ModuleName = model.ModuleName,
                Description = model.Description
            });
            context.ModuleDeliveries.Add(new DataModel.Classes.ModuleDelivery()
            {
                LecturerID = lecturerID,
                ModuleID = model.ID,
                DeliveryDay = model.DeliveryDay,
                DeliveryTime = model.DeliveryTime,
                Duration = model.Duration
            });
            context.SaveChanges();
            ViewBag.Success = true;
            return RedirectToAction("CreateModule");
        }
        [HttpPost]
        public ActionResult AddNewLecturer(DataModel.Classes.Lecturer lecturer)
        {
            if (context.Lecturers.ToList().Contains(lecturer))
            {
                TempData["ErrorMessage"] = "This lecturer is already in the list";
                return RedirectToAction("AddLecturer");
            }
            context.Lecturers.Add(lecturer);
            context.SaveChanges();
            return RedirectToAction("LecturerList");
        }
        [HttpPost]
        public ActionResult AddNewStudent(DataModel.Classes.Student student)
        {
            if (context.Students.ToList().Contains(student))
            {
                TempData["ErrorMessage"] = "This student is already in the list";
                return RedirectToAction("AddStudent");
            }
            context.Students.Add(student);
            context.SaveChanges();
            return RedirectToAction("StudentList");
        }

        public ActionResult CreateNewModule()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(new NewModuleInputModel());
        }
        public ActionResult StudentList()
        {
            return View(context.Students);
        }
        public ActionResult LecturerList()
        {
            return View(context.Lecturers);
        }
        public ActionResult AddLecturer()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(new DataModel.Classes.Lecturer());
        }
        public ActionResult AddStudent()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(new DataModel.Classes.Student());
        }
        public ActionResult EditLecturer(int lecturerID)
        {
            return RedirectToAction("AddNewLecturer", new { lecturer = context.Lecturers.ToList().Find(i => i.ID == lecturerID) });
        }
        public ActionResult DeleteLecturer(int lecturerID)
        {
            var lecturer = context.Lecturers.ToList().Find(i => i.ID == lecturerID);
            context.Lecturers.Remove(lecturer);
            context.SaveChanges();
            return RedirectToAction("LecturerList");
        }
        public ActionResult EditStudent(string studentID)
        {
            return RedirectToAction("AddNewStudent", new { student = context.Students.ToList().Find(i => i.StudentID == studentID) });
        }
        public ActionResult DeleteStudent(string studentID)
        {
            var student = context.Students.ToList().Find(i => i.StudentID == studentID);
            context.Students.Remove(student);
            context.SaveChanges();
            return RedirectToAction("StudentList");
        }
    }
}