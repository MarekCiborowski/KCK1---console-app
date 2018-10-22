namespace DatabaseLayer.Migrations
{
    using DatabaseLayer.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseLayer.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseLayer.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            Category cat1 = new Category() { isSingleChoice = false, canAddOwnAnswer = false };
            Category cat2 = new Category() { isSingleChoice = false, canAddOwnAnswer = true };
            Category cat3 = new Category() { isSingleChoice = true, canAddOwnAnswer = false };
            Category cat4 = new Category() { isSingleChoice = true, canAddOwnAnswer = true };

            context.categories.AddOrUpdate(cat1);
            context.categories.AddOrUpdate(cat2);
            context.categories.AddOrUpdate(cat3);
            context.categories.AddOrUpdate(cat4);


        }
    }
}
