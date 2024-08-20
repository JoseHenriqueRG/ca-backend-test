using BillingManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillingManagementSystem.Infra.Repository
{
    public class RepositoryDBContext : DbContext
    {
        public RepositoryDBContext(DbContextOptions options) : base(options)
        {
        }

        public RepositoryDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var relativePath = @"Database.mdf";
            var absolutePath = Path.Combine(baseDirectory, relativePath);

            var connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={absolutePath};Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString);

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Billing> Billings { get; set; }
    }
}
