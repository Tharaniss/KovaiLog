
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using KovaiLog.Entities.Models;
namespace KovaiLog.Entities.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<KovaiLogDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KovaiLogDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
