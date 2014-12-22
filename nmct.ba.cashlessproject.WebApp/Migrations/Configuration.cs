namespace nmct.ba.cashlessproject.WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using nmct.ba.cashlessproject.WebApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<nmct.ba.cashlessproject.WebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(nmct.ba.cashlessproject.WebApp.Models.ApplicationDbContext context)
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

            string roleAdmin = "Administrator";
            string roleNormalUser = "User";
            IdentityResult roleResult;

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!RoleManager.RoleExists(roleNormalUser))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleNormalUser));
            }
            if (!RoleManager.RoleExists(roleAdmin))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleAdmin));
            }

            if (!context.Users.Any(u => u.Email.Equals("nikitalisabethgard@hotmail.com")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Lisabeth",
                    FirstName = "Nikita",
                    Email = "nikita.lisabeth@student.howest.be",
                    UserName = "nikita.lisabeth@student.howest.be",
                };
                manager.Create(user, "-Password1");
                manager.AddToRole(user.Id, roleAdmin);
            }
        }
    }
}
