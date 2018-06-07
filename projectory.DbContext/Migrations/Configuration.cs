using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace projectory.DbContext.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RepoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RepoDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var admin = new IdentityRole { Name = "admin" };
            var user = new IdentityRole { Name = "user" };
            var editor = new IdentityRole { Name = "editor" };


            roleManager.Create(admin);
            roleManager.Create(user);
            roleManager.Create(editor);

        }
    }
}
