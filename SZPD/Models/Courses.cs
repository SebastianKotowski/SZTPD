using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZPD.Models
{
    public class Courses
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int FacultyId { get; set; }
    }
}