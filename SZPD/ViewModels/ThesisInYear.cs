using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZPD.Models
{
    public class ThesisInYear
    {
        public AcademicYear AcademicYear { get; set; }
        public PagedList<Thesis> Thesis { get; set; }
    }
}