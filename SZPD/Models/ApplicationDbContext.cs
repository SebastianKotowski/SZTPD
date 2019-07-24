using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;


namespace SZPD.Models
{
    public class ApplicationDbContext : IdentityDbContext<Lecturer>
    {
        public ApplicationDbContext()
            : base("PortalDBContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lecturer>().ToTable("Lecturer").Property(p => p.Id).HasColumnName("Primary_key");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaim");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(p => p.Id).HasColumnName("RoleId");
        }

        public DbSet<Thesis> Thesis { get; set; }
        public DbSet<AcademicYear> AcademicYear { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Courses> Courses { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Institute> Institutes { get; set; }

    }
}