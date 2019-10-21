namespace InternshipPortal.Migrations
{
    using InternshipPortal.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InternshipPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
       
        protected override void Seed(InternshipPortal.Models.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!(context.Roles.Any(p => p.Name == "Admin")))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
            if (!context.Roles.Any(p => p.Name == "Instructor"))
            {
                roleManager.Create(new IdentityRole("Instructor"));
            }
            if (!context.Roles.Any(p => p.Name == "Student"))
            {
                roleManager.Create(new IdentityRole("Student"));
            }
            if (!context.Roles.Any(p => p.Name == "Employer"))
            {
                roleManager.Create(new IdentityRole("Employer"));
            }

            ApplicationUser adminUser;

            if (!context.Users.Any(p => p.UserName == "admin1@internportal.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.Email = "admin1@internportal.com";
                adminUser.UserName = adminUser.Email;
                userManager.Create(adminUser, "P@ssword!");
            }
            else
            {
                adminUser = context.Users.First(p => p.UserName == "admin1@internportal.com");
            }
            if (!userManager.IsInRole(adminUser.Id, "admin"))
            {
                userManager.AddToRole(adminUser.Id, "admin");
            }

            context.SaveChanges();
        }
    }
}
