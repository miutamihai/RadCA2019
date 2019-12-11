namespace CA20192020.DataModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.IO;
    using Classes;

    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context context)
        {
            using (var reader = new StreamReader(@"C:\Users\miuta\Source\Repos\rad-301-ca-2019-2020-MihaiMiuta\CA20192020.MVC\App_Data\StudentList1.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(',');
                    context.Students.AddOrUpdate(
                        new Student()
                        {
                            StudentID = line[0],
                            FirstName = line[1],
                            LastName = line[2]
                        });
                }
            }
            using (var reader = new StreamReader(@"C:\Users\miuta\Source\Repos\rad-301-ca-2019-2020-MihaiMiuta\CA20192020.MVC\App_Data\Modules.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(',');
                    context.Modules.AddOrUpdate(x => x.ID,
                        new Module()
                        {
                            ModuleCode = line[0],
                            ModuleName = line[1]
                        });
                }
            }
            using (var reader = new StreamReader(@"C:\Users\miuta\Source\Repos\rad-301-ca-2019-2020-MihaiMiuta\CA20192020.MVC\App_Data\Lecturers.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(',');
                    context.Lecturers.AddOrUpdate(x => x.ID,
                        new Lecturer()
                        {
                            FirstName = line[0],
                            LastName = line[1]
                        }
                        );
                }
            }
        }
    }
}
