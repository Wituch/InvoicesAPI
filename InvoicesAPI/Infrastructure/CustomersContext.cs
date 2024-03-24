using InvoicesAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoicesAPI.Infrastructure
{
    public class CustomersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Invoices;Trusted_Connection=True");
        }
    }
}
