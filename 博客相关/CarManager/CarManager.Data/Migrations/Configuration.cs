namespace CarManager.Data.Migrations
{
    using Core.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarManager.Data.CarDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CarManager.Data.CarDbContext context)
        {
            context.Set<Car>().AddOrUpdate(
                new Car { Name = "±¦Âí", Price = 38, CreateDate = DateTime.Now },
                new Car { Name = "°ÂµÏ", Price = 40, CreateDate = DateTime.Now },
                new Car { Name = "±£Ê±½Ý", Price = 120, CreateDate = DateTime.Now }
                );
        }
    }
}
