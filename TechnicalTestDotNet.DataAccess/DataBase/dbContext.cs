using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechnicalTestDotNet.DataAccess.DataBase.Models;

namespace TechnicalTestDotNet.DataAccess.DataBase
{
    public partial class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        #region Modelos Generales
        public DbSet<Course> Course { get; set; }
        public DbSet<Qualification> Qualification { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<User> User { get; set; }
        #endregion

        #region DTOs Generales
        //public DbSet<AsociadoProcesos> AsociadoProcesos { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("dbContext"));
            }
        }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            //Obtener Información por stored procedure
            #region DTOs Generales            
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses", "dbo");
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.ToTable("Qualifications", "dbo");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students", "dbo");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teachers", "dbo");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "dbo");
            });            
            #endregion

            //OnModelCreatingPartial(modelBuilder);
        }
    }
}
