using DatabaseAccessLib.Models;
using Microsoft.EntityFrameworkCore;


namespace DatabaseAccessLib.Data
{
    /// <summary>
    /// the database context for the Merchandise database
    /// </summary>
    public class MerchandiseConextDB: DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;   

        public DbSet<OrderDetails> OrderDetails { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=MerchandiseDB;Trusted_Connection=True;TrustServerCertificate=true;");
        }
    }
}
