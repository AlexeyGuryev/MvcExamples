namespace ContactManager.Migrations
{
    using ContactManager.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ContactManager.Models.ApplicationDbContext";
        }

        protected override void Seed(ContactManager.Models.ApplicationDbContext context)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "user1@contoso.com",
            };
            var identityResult = um.Create(user, "Password!1");

            if (!identityResult.Succeeded)
            {
                return;
            }

            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            rm.Create(new IdentityRole("canEdit"));
            um.AddToRole(user.Id, "canEdit");

            context.Contacts.AddOrUpdate(c => c.Name, new Contact
            {
                Name = "Debra Garcia",
                Address = "1234 Main St",
                City = "Redmond",
                State = "WA",
                Zip = "10999",
                Email = "debra@example.com",
            },
            new Contact
            {
                Name = "Thorsten Weinrich",
                Address = "5678 1st Ave W",
                City = "Redmond",
                State = "WA",
                Zip = "10999",
                Email = "thorsten@example.com",
            },
            new Contact
            {
                Name = "Yuhong Li",
                Address = "9012 State st",
                City = "Redmond",
                State = "WA",
                Zip = "10999",
                Email = "yuhong@example.com",
            },
            new Contact
            {
                Name = "Jon Orton",
                Address = "3456 Maple St",
                City = "Redmond",
                State = "WA",
                Zip = "10999",
                Email = "jon@example.com",
            },
            new Contact
            {
                Name = "Diliana Alexieva-Bosseva",
                Address = "7890 2nd Ave E",
                City = "Redmond",
                State = "WA",
                Zip = "10999",
                Email = "diliana@example.com",
            });
        }
    }
}
