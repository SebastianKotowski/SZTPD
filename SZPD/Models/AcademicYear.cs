using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZPD.Models
{
    public class AcademicYear
    {
        public AcademicYear()
        {
            this.Thesiss = new HashSet<Thesis>();
        }

        public int ID { get; set; }
        public string Year { get; set; }
        [Required]
        public string Status { get; set; }
        public int LimitSI { get; set; }
        public int LimitNI { get; set; }
        public int LimitSM { get; set; }
        public int LimitNM { get; set; }
        [DataType(DataType.Date)]
        public DateTime? SITerm { get; set; }
        [DataType(DataType.Date)]
        public DateTime? NITerm { get; set; }
        [DataType(DataType.Date)]
        public DateTime? SMTerm { get; set; }
        [DataType(DataType.Date)]
        public DateTime? NMTerm { get; set; }


        public virtual ICollection<Thesis> Thesiss { get; set; }
    }
}