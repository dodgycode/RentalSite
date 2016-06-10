namespace RentalSite.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PropertyListingModel : DbContext
    {
        // Your context has been configured to use a 'PropertyListingModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'RentalSite.Models.PropertyListingModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PropertyListingModel' 
        // connection string in the application configuration file.
        public PropertyListingModel()
            : base("name=PropertyListingModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Property> Properties { get; set; }
    }

    public class Property
    {
        public Guid PropertyId { get; set; }
        public string Name { get; set; }
        public virtual Details PropertyDetails { get; set;}
        public virtual Address PropertyAddress { get; set; }

    }

    public class Details
    {
        public Guid PropertyDetailsId { get; set; }
        
        public int NumSleeps { get; set; }
        public int NumBedrooms { get; set; }
        public int NumBathrooms { get; set; }
        public ParkingType Parking { get; set; }
    }

    public class Address
    {
        public Guid AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string Postcode { get; set; }

    }

    #region Enums
    public enum ParkingType
    {
        None,
        OnStreet,
        Garage,
        Driveway
    }
    #endregion
}