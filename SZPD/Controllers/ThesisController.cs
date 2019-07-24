using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SZPD.Models;
using Microsoft.AspNet.Identity;
using SZPD.ViewModels;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Rotativa;

namespace SZPD.Controllers
{
    public class ThesisController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Thesis.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thesis thesis = db.Thesis.Find(id);
            if (thesis == null)
            {
                return HttpNotFound();
            }
            return View(thesis);
        }

        [Authorize]
        public ActionResult Create()
        {
            int year = (from i in db.AcademicYear select i.ID).FirstOrDefault();
            ViewBag.LimitMessage = CheckLimits(year); ;

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int year, AddThesisViewModel addThesis)
        {
            if (ModelState.IsValid)
            {
                Thesis thesis = new Thesis(addThesis);
                thesis.AcademicYearID = year;
                thesis.LecturerID = User.Identity.GetUserId();
                thesis.CompletionDate = DateTime.Now;
                thesis.DefenseDate = DateTime.Now;
                thesis.EducationProfile = "ogólnoakademicki";

                db.Thesis.Add(thesis);
                db.SaveChanges();
                return RedirectToAction("Details", "AcademicYear");
            }

            return View(addThesis);
        }

        private List<string> CheckLimits(int year)
        {
            List<string> Result = new List<string>();

            AcademicYear academicYear = (from a in db.AcademicYear where a.ID == year select a).FirstOrDefault();

            List<string> Category = new List<string>() { "Inżynierskie stacjonarne", "Inżynierskie niestacjonarne", "Magisterskie stacjonarne", "Magisterskie niestacjonarne" };
            var user = User.Identity.GetUserId();

            foreach (var c in Category)
            {
                var thesis = (from t in db.Thesis where t.Category == c && t.LecturerID == user select t).ToList();

                int thesisCount = thesis.Count;

                if (thesisCount > 0)
                {
                    if (c == "Inżynierskie stacjonarne" && thesisCount > academicYear.LimitSI)
                    {
                        Result.Add($"Limit tematów na studia {c} został już wykożystany");
                    }
                    if (c == "Inżynierskie niestacjonarne" && thesisCount > academicYear.LimitNI)
                    {
                        Result.Add($"Limit tematów na studia {c} został już wykożystany");
                    }
                    if (c == "Magisterskie stacjonarne" && thesisCount > academicYear.LimitSM)
                    {
                        Result.Add($"Limit tematów na studia {c} został już wykożystany");
                    }
                    if (c == "Magisterskie niestacjonarne" && thesisCount > academicYear.LimitNM)
                    {
                        Result.Add($"nnLimit tematów na studia {c} został już wykożystany");
                    }
                }

            }

            return Result;
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Thesis thesis = db.Thesis.Find(id);
            if (thesis == null)
            {
                return HttpNotFound();
            }

            List<Faculty> faculties = (from f in db.Faculties select f).ToList();

            List<Courses> courses;
            if (thesis.FacultyId != 0)
            {
                courses = (from c in db.Courses where c.FacultyId == thesis.FacultyId select c).ToList();
            }
            else
            {
                courses = (from c in db.Courses where c.FacultyId == 1 select c).ToList();
            }

            List<Specialization> specializations;
            if (thesis.CourseId != 0)
            {
                specializations = (from s in db.Specializations where s.CourseId == thesis.CourseId select s).ToList();
            }
            else
            {
                specializations = (from s in db.Specializations where s.CourseId == 1 select s).ToList();
            }

            List<Institute> institutes = (from f in db.Institutes select f).ToList();

            ViewBag.Institutes = new SelectList(institutes, "ID", "Name");
            ViewBag.Faculties = new SelectList(faculties, "ID", "Name");
            ViewBag.Courses = new SelectList(courses, "ID", "Name");
            ViewBag.Specializations = new SelectList(specializations, "ID", "Name");

            return View(thesis);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Thesis thesis, int Faculty, int Kierunek, int Specjalizacja)
        {
            thesis.FacultyId = Faculty;
            thesis.CourseId = Kierunek;
            thesis.SpecializationId = Specjalizacja;

            if (ModelState.IsValid)
            {
                db.Entry(thesis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "AcademicYear");
            }
            return View(thesis);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thesis thesis = db.Thesis.Find(id);

            string academicYear = (from a in db.AcademicYear where a.ID == thesis.AcademicYearID select a.Year).FirstOrDefault();
            ViewBag.AcademicYear = academicYear;

            if (thesis == null)
            {
                return HttpNotFound();
            }
            return View(thesis);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thesis thesis = db.Thesis.Find(id);
            db.Thesis.Remove(thesis);
            db.SaveChanges();
            return RedirectToAction("Details", "AcademicYear");
        }

        [Authorize]
        public ActionResult ThesisCard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Thesis thesis = db.Thesis.Find(id);
            AcademicYear academicYear = db.AcademicYear.Find(thesis.AcademicYearID);
            string deadline = "";

            if (thesis.Category == "Inżynierskie stacjonarne")
            {
                deadline = academicYear.SITerm.Value.ToShortDateString();
            }
            if (thesis.Category == "Inżynierskie niestacjonarne")
            {
                deadline = academicYear.NITerm.Value.ToShortDateString();
            }
            if (thesis.Category == "Magisterskie stacjonarne")
            {
                deadline = academicYear.SMTerm.Value.ToShortDateString();
            }
            if (thesis.Category == "Magisterskie niestacjonarne")
            {
                deadline = academicYear.NMTerm.Value.ToShortDateString();
            }

            ViewBag.Termin = deadline;

            string faculty = (from f in db.Faculties where f.Id == thesis.FacultyId select f.Name).FirstOrDefault();
            string course = (from c in db.Courses where c.Id == thesis.CourseId select c.Name).FirstOrDefault();
            string specialization = (from s in db.Specializations where s.Id == thesis.SpecializationId select s.Name).FirstOrDefault();

            ViewBag.Faculty = faculty;
            ViewBag.Course = course;
            ViewBag.Specialization = specialization;


            List<string> studentStrigs = thesis.Student.Split(',').ToList();
            Dictionary<string, string> studentAndIndex = new Dictionary<string, string>();

            foreach (var s in studentStrigs)
            {
                var singleStudent = s.Split('|');
                studentAndIndex.Add(singleStudent[0], singleStudent[1]);
            }

            ViewBag.Students = studentAndIndex;

            Lecturer lecturer = (from l in db.Users where l.Id == thesis.LecturerID select l).FirstOrDefault();

            ViewBag.Lecturer = lecturer.FirstName + ' ' + lecturer.Lastname;

            if (thesis == null)
            {
                return HttpNotFound();
            }
            return View(thesis);
        }

        public ActionResult ThesisCardForPDF(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thesis thesis = db.Thesis.Find(id);
            AcademicYear academicYear = db.AcademicYear.Find(thesis.AcademicYearID);
            string deadline = "";

            if (thesis.Category == "Inżynierskie stacjonarne")
            {
                deadline = academicYear.SITerm.Value.ToShortDateString();
            }
            if (thesis.Category == "Inżynierskie niestacjonarne")
            {
                deadline = academicYear.NITerm.Value.ToShortDateString();
            }
            if (thesis.Category == "Magisterskie stacjonarne")
            {
                deadline = academicYear.SMTerm.Value.ToShortDateString();
            }
            if (thesis.Category == "Magisterskie niestacjonarne")
            {
                deadline = academicYear.NMTerm.Value.ToShortDateString();
            }

            ViewBag.Termin = deadline;


            string faculty = (from f in db.Faculties where f.Id == thesis.FacultyId select f.Name).FirstOrDefault();
            string course = (from c in db.Courses where c.Id == thesis.CourseId select c.Name).FirstOrDefault();
            string specialization = (from s in db.Specializations where s.Id == thesis.SpecializationId select s.Name).FirstOrDefault();

            ViewBag.Faculty = faculty;
            ViewBag.Course = course;
            ViewBag.Specialization = specialization;

            List<string> studentStrigs = thesis.Student.Split(',').ToList();
            Dictionary<string, string> studentAndIndex = new Dictionary<string, string>();

            foreach (var s in studentStrigs)
            {
                var singleStudent = s.Split('|');
                studentAndIndex.Add(singleStudent[0], singleStudent[1]);
            }

            ViewBag.Students = studentAndIndex;

            Lecturer lecturer = (from l in db.Users where l.Id == thesis.LecturerID select l).FirstOrDefault();

            ViewBag.Lecturer = lecturer.FirstName + ' ' + lecturer.Lastname;

            if (thesis == null)
            {
                return HttpNotFound();
            }
            return View(thesis);
        }

        public ActionResult ThesisCardToPDF(int? id)
        {
            return new ActionAsPdf("ThesisCardForPDF", new { id = id })
            {
                FileName = Server.MapPath("~Content/KartaPracy.pdf")
            };
        }

        public ActionResult SendRequest(string thesisName)
        {
            RequestMessage requestMessage = new RequestMessage();
            requestMessage.Thesis = thesisName;
            requestMessage.Subject = "[" + thesisName + "]";

            return View(requestMessage);
        }

        [HttpPost]
        public ActionResult SendRequest(RequestMessage requestMessage)
        {
            if (ModelState.IsValid)
            {
                RedirectToAction("Details", "AcademicYear");
            }
            return View(requestMessage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AllowAnonymous]
        public JsonResult UpdateCourses(int id)
        {
            List<Courses> courses = (from c in db.Courses where c.FacultyId == id select c).ToList();

            return Json(
                JsonConvert.SerializeObject(courses),
                JsonRequestBehavior.AllowGet
                );
        }

        [AllowAnonymous]
        public JsonResult UpdateSpecializations(int id)
        {
            List<Specialization> specializations = (from c in db.Specializations where c.CourseId == id select c).ToList();

            return Json(
                JsonConvert.SerializeObject(specializations),
                JsonRequestBehavior.AllowGet
                );
        }
    }
}
