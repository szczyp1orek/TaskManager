namespace TaskManager.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TaskManager.Models.ApplicationDbContext";
            //
            var dbMigrator = new DbMigrator(this);

            // This is required to detect changes.
            var pendingMigrationsExist = dbMigrator.GetPendingMigrations().Any();

            if (pendingMigrationsExist)
            {
                dbMigrator.Update();
            }
            //
        }

        protected override void Seed(TaskManager.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
                //
                context.Roles.Add(role);
                
            }

            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new ApplicationUserManager(store);
                var user = new ApplicationUser { UserName = "admin@admin.com", Email= "admin@admin.com" };

                IdentityResult result=manager.Create(user, "Password123!");
                if (result.Succeeded == false) { throw new Exception(result.Errors.First()); }
                manager.AddToRole(user.Id, "Admin");
                
            }
            else
            {
                // Just for good measure, this adds the user to the role if they already
                // existed and just weren't in the role.
                var user = context.Users.Single(u => u.UserName.Equals("admin@admin.com", StringComparison.CurrentCultureIgnoreCase));
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                manager.AddToRole(user.Id, "Admin");
            }
            context.SaveChanges();
        }
    }
}
