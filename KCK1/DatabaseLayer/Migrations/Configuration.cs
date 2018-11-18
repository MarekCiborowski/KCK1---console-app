namespace DatabaseLayer.Migrations
{
    using DataTransferObjects.Models;
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
            Category[] category = new Category[] {
                new Category() { isSingleChoice = false, canAddOwnAnswer = false },
                new Category() { isSingleChoice = false, canAddOwnAnswer = true },
                new Category() { isSingleChoice = true, canAddOwnAnswer = false },
                new Category() { isSingleChoice = true, canAddOwnAnswer = true }
            };
            foreach (Category cat in category)
                context.categories.AddOrUpdate(t => new { t.canAddOwnAnswer, t.isSingleChoice }, cat);
        }
    }
}
