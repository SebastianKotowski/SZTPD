using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZPD.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Stationary { get; set; }

        public bool Extramural { get; set; }

        public int CourseId { get; set; }
    }
}