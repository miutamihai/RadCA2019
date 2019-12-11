using ActivityTracker;
using CA20192020.MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CA20192020.MVC.Startup))]
namespace CA20192020.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Activity.Track("CA20192020 App Started");
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var DataModelContext = new DataModel.Context();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "miuta.mihai@gmail.com";
                user.Email = "miuta.mihai@gmail.com";

                string userPWD = "Mihai16.";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            } 
            if (!roleManager.RoleExists("Lecturer"))
            {
                var role = new IdentityRole();
                role.Name = "Lecturer";
                roleManager.Create(role);
                foreach(var lecturer in DataModelContext.Lecturers)
                {
                    var user = new ApplicationUser();
                    string schoolDomain = "@mail.itsligo.ie";
                    lecturer.LastName = lecturer.LastName.TrimStart(' ');
                    user.UserName = lecturer.FirstName + lecturer.LastName + schoolDomain;
                    user.Email = user.UserName;

                    string userPWD = lecturer.FirstName + "16.";

                    var chkUser = UserManager.Create(user, userPWD);

                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, role.Name);

                    }
                }
            }

            if (!roleManager.RoleExists("Student"))
            {
                var role = new IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);

                foreach (var student in DataModelContext.Students)
                {
                    var user = new ApplicationUser();
                    string schoolDomain = "@mail.itsligo.ie";
                    user.UserName = student.StudentID + schoolDomain;
                    user.Email = user.UserName;

                    string userPWD = student.StudentID + "16.";

                    var chkUser = UserManager.Create(user, userPWD);

                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, role.Name);

                    }
                }
            }
        }
    }
}
