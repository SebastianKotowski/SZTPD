using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace SZPD.Models
{
    public class Lecturer : IdentityUser
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string LinkPassword { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EmailLinkDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastLoginDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime AddThesisPermision { get; set; }

        public virtual ICollection<Thesis> Thesiss { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Lecturer> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }

}