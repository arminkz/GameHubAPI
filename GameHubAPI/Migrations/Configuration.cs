namespace GameHubAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using GameHubAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<GameHubAPI.Models.GameHubAPIContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GameHubAPI.Models.GameHubAPIContext context)
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

            context.Users.AddOrUpdate(p => p.Id,
                new User() { Id = 1, DisplayName = "arminkz", Email = "arminkz@live.com" },
                new User() { Id = 2, DisplayName = "aminsvnd", Email = "aminsvnd@gmail.com" }
            );
        }
    }
}
