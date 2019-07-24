using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZPD.ViewModels;

namespace SZPD.Models
{
    public class Thesis
    {
        public int ID { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string EnglishSubject { get; set; }

        [Required]
        public string Objective { get; set; }

        [Required]
        public string Tasks { get; set; }

        public string Comment { get; set; }

        public string PrivateComment { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public string Category { get; set; }

        public string Student { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? CompletionDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DefenseDate { get; set; }

        public float Rate { get; set; }

        public string ReviewerCommittee { get; set; }

        public string ChairmanCommittee { get; set; }

        public string InputData { get; set; }

        public string ThesisRange { get; set; }

        public string ThesisLocation { get; set; }

        public bool Settled { get; set; }

        public string EducationProfile { get; set; }

        public string LecturerID { get; set; }

        public int AcademicYearID { get; set; }

        public int InstituteId { get; set; }

        public int FacultyId { get; set; }

        public int CourseId { get; set; }

        public int SpecializationId { get; set; }

        public virtual Lecturer Lecturer { get; set; }        
        public virtual AcademicYear AcademicYear { get; set; }

        public Thesis()
        {
        }

        public Thesis(AddThesisViewModel addThesis)
        {
            this.Subject = addThesis.Subject;
            this.EnglishSubject = addThesis.EnglishSubject;
            this.Objective = addThesis.Objective;
            this.Tasks = addThesis.Tasks;
            this.Status = addThesis.Status;
            this.Version = addThesis.Version;
            this.Category = addThesis.Category;
        }
    }
}