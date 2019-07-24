using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZPD.Models
{
    public class FacultyCoursesViewModel
    {
        public Faculty Faculty { get; set; }
        public List<Courses> Courses { get; set; }
    }
}