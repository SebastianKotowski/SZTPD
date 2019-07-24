using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZPD.Models
{
    public class RequestMessage
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }
        public string Thesis { get; set; }
        [Required]
        public string Sender { get; set; }
        public string SenderPhone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string SenderEmail { get; set; }
    }
}