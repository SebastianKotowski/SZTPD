using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZPD.ViewModels
{
    public class AddThesisViewModel
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string EnglishSubject { get; set; }
        [Required]
        public string Objective { get; set; }
        [Required]
        public string Tasks { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Version { get; set; }
        [Required]
        public string Category { get; set; }

    }
}