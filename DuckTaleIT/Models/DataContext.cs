using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuckTaleIT.Models
{
    public class DataContext: DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<MasterClass> MasterClasses { get; set; }
        public DbSet<MasterSubject> MasterSubjects { get; set; }
        public DbSet<MasterStudent> MasterStudents { get; set; }
        public DbSet<SubjectStudentWise> SubjectStudentWises { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
