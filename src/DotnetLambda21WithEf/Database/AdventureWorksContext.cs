using Microsoft.EntityFrameworkCore;

namespace DotnetLambda21WithEf.Database
{
    public class AdventureWorksContext : DbContext
    {
        public AdventureWorksContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }
        public virtual DbSet<ProductModel> ProductModels { get; set; }
        public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
        public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerAddress>()
                .HasKey(x => new { x.AddressID, x.CustomerID });

            modelBuilder.Entity<Address>()
                .HasMany(e => e.CustomerAddresses)
                .WithOne(e => e.Address);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.SalesOrderHeaders)
                .WithOne(e => e.Address)
                .HasForeignKey(e => e.BillToAddressID);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.SalesOrderHeaders1)
                .WithOne(e => e.Address1)
                .HasForeignKey(e => e.ShipToAddressID);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PasswordSalt)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerAddresses)
                .WithOne(e => e.Customer);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.SalesOrderHeaders)
                .WithOne(e => e.Customer);

            modelBuilder.Entity<Product>()
                .Property(e => e.StandardCost)
                .HasColumnType("money");
            //.HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ListPrice)
                .HasColumnType("money");
            //.HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Weight).HasColumnType("numeric(8,2)");
            //.HasPrecision(8, 2);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.SalesOrderDetails)
                .WithOne(e => e.Product);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.ProductCategory1);

            modelBuilder.Entity<ProductDescription>()
                .HasMany(e => e.ProductModelProductDescriptions)
                .WithOne(e => e.ProductDescription);

            modelBuilder.Entity<ProductModel>()
                .HasMany(e => e.ProductModelProductDescriptions)
                .WithOne(e => e.ProductModel);

            modelBuilder.Entity<ProductModelProductDescription>()
                .HasKey(x => new { x.ProductDescriptionID, x.ProductModelID });

            modelBuilder.Entity<ProductModelProductDescription>()
                .Property(e => e.Culture)
                .IsFixedLength();

            modelBuilder.Entity<SalesOrderDetail>()
                .HasKey(x => new { x.SalesOrderID, x.SalesOrderDetailID });

            modelBuilder.Entity<SalesOrderDetail>()
                .Property(e => e.UnitPrice)
                .HasColumnType("money");
            //.HasPrecision(19, 4);

            modelBuilder.Entity<SalesOrderDetail>()
                .Property(e => e.UnitPriceDiscount)
                .HasColumnType("money");
            //.HasPrecision(19, 4);

            modelBuilder.Entity<SalesOrderDetail>()
                .Property(e => e.LineTotal).HasColumnType("numeric(38,6)");
            //.HasPrecision(38, 6);

            modelBuilder.Entity<SalesOrderHeader>()
                .Property(e => e.CreditCardApprovalCode)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderHeader>()
                .Property(e => e.SubTotal)
                .HasColumnType("money");
            //.HasPrecision(19, 4);

            modelBuilder.Entity<SalesOrderHeader>()
                .Property(e => e.TaxAmt)
                .HasColumnType("money");
            //.HasPrecision(19, 4);

            modelBuilder.Entity<SalesOrderHeader>()
                .Property(e => e.Freight)
                .HasColumnType("money");
            //.HasPrecision(19, 4);

            modelBuilder.Entity<SalesOrderHeader>()
                .Property(e => e.TotalDue)
                .HasColumnType("money");
            //.HasPrecision(19, 4);
        }
    }
}
