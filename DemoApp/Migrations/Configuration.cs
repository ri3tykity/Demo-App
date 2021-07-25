namespace DemoApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DemoApp.Models.DemoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DemoApp.Models.DemoDbContext";
        }

        protected override void Seed(DemoApp.Models.DemoDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            //context.Authors.AddOrUpdate(new Author() { Id = 1, FirstName = "R D", LastName = "Sharma"});
            //context.Authors.AddOrUpdate(new Author() { Id = 2, FirstName = "Deepika", LastName = "Pandey" });

            //context.Books.AddOrUpdate(new Book() { Id=1, Title= "Trigonometry" });
            //context.Books.AddOrUpdate(new Book() { Id = 2, Title = "Advanced Trigonometry" });
        }
    }
}
