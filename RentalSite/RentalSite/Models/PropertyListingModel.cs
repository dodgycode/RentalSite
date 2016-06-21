namespace RentalSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class PropertyListingModel : DbContext
    {
        #region Constructor
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
        #endregion

        #region On model create
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("Admin");

            //Map entities to tables
            modelBuilder.Entity<Property>().ToTable("Property");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Details>().ToTable("PropertyDetails");
            modelBuilder.Entity<PropertyImage>().ToTable("PropertyImages");

            //Set primary keys
            modelBuilder.Entity<Property>().HasKey(p => p.PropertyId);
            modelBuilder.Entity<Address>().HasKey(a => a.AddressId);
            modelBuilder.Entity<Details>().HasKey(d => d.DetailsId);
            modelBuilder.Entity<PropertyImage>().HasKey(i => i.PropertyImageId);

            //Configure Property columns
            modelBuilder.Entity<Property>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(80);

            //Configure Address columns
            modelBuilder.Entity<Address>()
                .Property(p => p.AddressLine1)
                .HasMaxLength(80);

            modelBuilder.Entity<Address>()
             .Property(p => p.AddressLine2)
             .HasMaxLength(80);

            modelBuilder.Entity<Address>()
             .Property(p => p.AddressLine3)
             .HasMaxLength(80);

            modelBuilder.Entity<Address>()
             .Property(p => p.AddressLine4)
             .HasMaxLength(80);

            modelBuilder.Entity<Address>()
             .Property(p => p.AddressLine5)
             .HasMaxLength(80);

            modelBuilder.Entity<Address>()
             .Property(p => p.Postcode)
             .HasMaxLength(10);

            modelBuilder.Entity<PropertyImage>()
             .Property(p => p.Title)
             .HasMaxLength(150);

            modelBuilder.Entity<PropertyImage>()
            .Property(p => p.Caption)
            .HasMaxLength(250);

            //Set Foreign Keys
            modelBuilder.Entity<Property>()
                .HasOptional<Address>(p => p.PropertyAddress)
                .WithRequired(ad => ad.CurrProperty);
            
            modelBuilder.Entity<Property>()
                .HasOptional<Details>(p => p.PropertyDetails)
                .WithRequired(d => d.CurrProperty);

            modelBuilder.Entity<Property>()
                .HasOptional<ICollection<PropertyImage>>(p => p.PropertyImages);

            modelBuilder.Entity<PropertyImage>()
                .HasRequired(p => p.CurrProperty)
                .WithMany(i => i.PropertyImages)
                .HasForeignKey(p => p.PropertyId);
        }
        #endregion

        #region DbSet entities
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Details> Details { get; set; }
        public virtual DbSet<PropertyImage> PropertyImages { get; set; }
        #endregion
    }

    #region Data classes
    /// <summary>
    /// Top level property class
    /// </summary>
    public class Property
    {
        public Guid PropertyId { get; set; }
        [Display(Name = "Property Name")]
        public string Name { get; set; }
        public virtual Details PropertyDetails { get; set; }
        public virtual Address PropertyAddress { get; set; }
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        public bool Active { get; set; }

    }

    /// <summary>
    /// Property details (e.g. number of bedrooms)
    /// </summary>
    public class Details
    {
        public Guid DetailsId { get; set; }
        public Guid PropertyId { get; set; }
        [Display(Name = "Guests")]
        public int NumSleeps { get; set; }
        [Display(Name = "Bedrooms")]
        public int NumBedrooms { get; set; }
        [Display(Name = "Bathrooms")]
        public int NumBathrooms { get; set; }
        public ParkingType Parking { get; set; }
        [Display(Name = "Property summary")]
        public string summaryText { get; set; }
        [Display(Name = "Apartment description")]
        public string apartmentText { get; set; }
        [Display(Name = "Property services")]
        public string servicesText { get; set; }
        public virtual Property CurrProperty { get; set; }
    }

    /// <summary>
    /// Property address
    /// </summary>
    public class Address
    {
        public Guid AddressId { get; set; }
        public Guid PropertyId { get; set; }
        [Display(Name = "Address line 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address line 2")]
        public string AddressLine2 { get; set; }
        [Display(Name = "Address line 3")]
        public string AddressLine3 { get; set; }
        [Display(Name = "Address line 4")]
        public string AddressLine4 { get; set; }
        [Display(Name = "Address line 5")]
        public string AddressLine5 { get; set; }
        public string Postcode { get; set; }
        public virtual Property CurrProperty { get; set; }
    }

    /// <summary>
    /// Property images. BlobRef links to Azure blob storage using AzureStorageHelper.
    /// </summary>
    public class PropertyImage
    {
        public Guid PropertyImageId { get; set; }
        public Guid PropertyId { get; set; }
        [Required]
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public virtual Property CurrProperty { get; set; }
        public bool Active { get; set; }
    }
    #endregion

    #region Enums
    public enum ParkingType
    {
        None,
        [Display(Name = "On street")]
        OnStreet,
        Garage,
        Driveway
    }

    #endregion
}