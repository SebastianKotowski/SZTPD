using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using SZPD.Models;
using PagedList;
using Rotativa;

namespace SZPD.Controllers
{
    public class AcademicYearController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Main()
        {
            var academicYears = db.AcademicYear.ToList().OrderByDescending(x => x.ID);

            if(academicYears == null)
            {
                int year = DateTime.Now.Year;
                AcademicYear academicYear = new AcademicYear();

                academicYear.Year = $"{year} /  {year + 1}";
                academicYear.Status = "Nowe";

                db.AcademicYear.Add(academicYear);
                db.SaveChanges();

            }

            return View(academicYears.FirstOrDefault());
        }

        [Authorize(Users = "sz.tpd@cs.put.poznan.pl")]
        public ActionResult Index()
        {
            return View(db.AcademicYear.ToList());
        }

        
        public ActionResult Details(int? page, bool own = false, string tryb = "", string promotor = "", string AcademicYears = "")
        {
            List<string> years = (from w in db.AcademicYear orderby w.ID descending select w.Year).ToList();
            ViewBag.AcademicYears = new SelectList(years);
            ViewBag.UserPermission = false;

            var userName = User.Identity.Name;

            if ( AcademicYears == "")
            {
                AcademicYears = (from i in db.AcademicYear orderby i.ID descending select i.Year).FirstOrDefault() ;
            }
            AcademicYear academicYear = (from a in db.AcademicYear where a.Year == AcademicYears select a).FirstOrDefault();
            if (academicYear == null)
            {
                return HttpNotFound();
            }

            var academicYearId = (from a in db.AcademicYear where a.Year == AcademicYears select a.ID).FirstOrDefault();
            List < Thesis > thesis = (from t in db.Thesis where t.AcademicYearID == academicYearId select t).ToList();
            
            foreach(var t in thesis)
            {
                Lecturer lecturer = (from l in db.Users where l.Id == t.LecturerID select l).FirstOrDefault();

                t.Lecturer = lecturer;
            }

            if (userName == null && String.IsNullOrEmpty(tryb))
            {
                thesis = (thesis.Where(a => a.Category == "Inżynierskie stacjonarne")).ToList();
            }

            if (tryb != "Wszystkie" && !String.IsNullOrEmpty(tryb))
            {
                thesis = (thesis.Where(a => a.Category == tryb)).ToList();
            }          

            if (!String.IsNullOrEmpty(promotor))
            {
                string lecturer = (from l in db.Users where l.Lastname.Contains(promotor) select l.Id).FirstOrDefault();
                thesis = (thesis.Where(a => a.LecturerID == lecturer)).ToList();
            }
            else if (!String.IsNullOrEmpty(userName) && own)
            {
                string lecturer = (from l in db.Users where l.Email == userName select l.Id).FirstOrDefault();
                thesis = (thesis.Where(a => a.LecturerID == lecturer)).ToList();
            }

            if(!String.IsNullOrEmpty(userName))
            {
                Lecturer lecturer = (from user in db.Users where user.UserName == userName select user).FirstOrDefault();
                if(lecturer.AddThesisPermision != null)
                {
                    int current = DateTime.Compare(DateTime.Now, lecturer.AddThesisPermision);

                    if(current < 0)
                    {
                        ViewBag.UserPermission = true;
                    }
                }
                
            }

            List<Institute> institutes = (from f in db.Institutes select f).ToList();

            ViewBag.Institutes = new SelectList(institutes, "ID", "Name");

            int pageSize = 5;
            int pageNumber = (page ?? 1);


            ThesisInYear thesisInYear = new ThesisInYear
            {
                AcademicYear = academicYear,
                Thesis = new PagedList<Thesis>(thesis,pageNumber, pageSize)

            };

            return View(thesisInYear);
        }

        // GET: AcademicYear/Create
        [Authorize(Users = "sz.tpd@cs.put.poznan.pl")]
        public ActionResult Create()
        {
            Dictionary<string, int> Status = new Dictionary<string, int>();
            Status.Add("Nowy", 0);
            Status.Add("Zgłaszanie prac otwarte", 1);
            Status.Add("Zgłaszanie prac zakończone", 2);
            Status.Add("Zamknięty", 3);

            ViewBag.Status = Status;

            AcademicYear Year = new AcademicYear();




            var previousYear = (from a in db.AcademicYear orderby a.ID descending select a).FirstOrDefault();

            if (previousYear != null)
            {
                var stringYear = Regex.Match(previousYear.Year, "^.....");
                int year = Convert.ToInt32(previousYear.Year.Replace(stringYear.ToString(), ""));
                Year.Year = year.ToString() + "/" + (year + 1).ToString();
            }
            else
            {
                Year.Year = "2016/2017";
            }

            return View(Year);
        }

        // POST: AcademicYear/Create
        [Authorize(Users = "sz.tpd@cs.put.poznan.pl")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AcademicYear academicYear)
        {
                       
            if (ModelState.IsValid)
            {
                academicYear.Thesiss = null;
                db.AcademicYear.Add(academicYear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academicYear); 
        }

        // GET: AcademicYear/Edit/5
        [Authorize(Users = "sz.tpd@cs.put.poznan.pl")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYear academicYear = db.AcademicYear.Find(id);
            if (academicYear == null)
            {
                return HttpNotFound();
            }
            return View(academicYear);
        }

        [Authorize(Users = "sz.tpd@cs.put.poznan.pl")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AcademicYear academicYear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicYear).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academicYear);
        }

        // GET: AcademicYear/Delete/5
        [Authorize(Users = "sz.tpd@cs.put.poznan.pl")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYear academicYear = db.AcademicYear.Find(id);
            if (academicYear == null)
            {
                return HttpNotFound();
            }
            return View(academicYear);
        }

        public ActionResult ThesisRep(int academicYear, int? instituteId)
        {
            List<Thesis> thesisInYear = new List<Thesis>();
            if (instituteId != null)
            {
                ViewBag.Insttute = (from institute in db.Institutes where institute.Id == instituteId select institute.Name).FirstOrDefault();
                thesisInYear = (from t in db.Thesis where t.AcademicYearID == academicYear && t.InstituteId == instituteId select t).ToList();
            }
            else
            {
                thesisInYear = (from t in db.Thesis where t.AcademicYearID == academicYear select t).ToList();
            }

            return View("ThesisReport", thesisInYear);
        }

        public ActionResult ThesisReport(int academicYear, int? instituteId)
        {
            List<Thesis> thesisInYear = new List<Thesis>();
            if (instituteId != null)
            {
                ViewBag.Insttute = (from institute in db.Institutes where institute.Id == instituteId select institute.Name).FirstOrDefault();
                thesisInYear = (from t in db.Thesis where t.AcademicYearID == academicYear && t.InstituteId == instituteId select t).ToList();
            }
            else
            {
                thesisInYear = (from t in db.Thesis where t.AcademicYearID == academicYear select t).ToList();
            }          

            return new ViewAsPdf("ThesisReport", thesisInYear)
            {
                FileName = Server.MapPath("~Content/ListaTematow.pdf")
            };
        }

        [Authorize(Users = "sz.tpd@cs.put.poznan.pl")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AcademicYear academicYear = db.AcademicYear.Find(id);
            db.AcademicYear.Remove(academicYear);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
