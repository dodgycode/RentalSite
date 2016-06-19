namespace RentalSite.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RentalSite.Models.PropertyListingModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.PropertyListingModel context)
        {
            //This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method
            //to avoid creating duplicate seed data.E.g.

            context.Properties.AddOrUpdate(
              p => p.Name,
              new Models.Property
              {
                  Name = "Strawberry 1",
                  PropertyId = new Guid("a588f272-86c6-4f95-8bfe-71c654a10eb4"),
                  Active = true
                }
              );

            context.Addresses.AddOrUpdate(
                a => a.AddressId,
                new Models.Address
                {
                    PropertyId = new Guid("a588f272-86c6-4f95-8bfe-71c654a10eb4"),
                    AddressId = new Guid("a588f272-86c6-4f95-8bfe-71c654a10eb4"),
                    AddressLine1 = "14a Strawberry Dale Avenue",
                    AddressLine2 = "Harrogate",
                    AddressLine3 = "",
                    AddressLine4 = "",
                    AddressLine5 = "",
                    Postcode = "HG1 5EA"
                }
                );

            context.Details.AddOrUpdate(
                d => d.DetailsId,
                new Models.Details
                {
                    PropertyId = new Guid("a588f272-86c6-4f95-8bfe-71c654a10eb4"),
                    DetailsId = new Guid("a588f272-86c6-4f95-8bfe-71c654a10eb4"),
                    NumBathrooms = 3,
                    NumBedrooms = 3,
                    NumSleeps = 8,
                    Parking = Models.ParkingType.Driveway
                }
                );

        }
    }
}
