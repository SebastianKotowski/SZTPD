using System.ComponentModel.DataAnnotations;

namespace SZPD.Models
{
    public class Faculty
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}