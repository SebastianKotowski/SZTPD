using System.Collections.Generic;

namespace SZPD.Models
{
    public class CourseSpecializationsViewModel
    {
        public Courses Course { get; set; }
        public List<Specialization> Specializations { get; set; }
    }
}