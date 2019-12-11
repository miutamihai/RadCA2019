using CA20192020.DataModel.Classes;
using Microsoft.Build.Tasks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CA20192020.DataModel
{
    public class Context : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<EnrollmentApplication> EnrollmentApplications { get; set; }
        public DbSet<ModuleAssessment> ModuleAssessments { get; set; }
        public DbSet<ModuleAttendance> ModuleAttendances { get; set; }
        public DbSet<ModuleDelivery> ModuleDeliveries { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
    }

}
