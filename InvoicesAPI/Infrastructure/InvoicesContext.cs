using InvoicesAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoicesAPI.Infrastructure
{
    public class InvoicesContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Invoices;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("InvoicesAPI.Entities.Invoice", b =>
            {
                b.HasOne("InvoicesAPI.Entities.Customer", "Buyer")
                    .WithMany("InvoicesAsBuyer")
                    .HasForeignKey("BuyerId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.HasOne("InvoicesAPI.Entities.Customer", "Recipient")
                    .WithMany("InvoicesAsRecipient")
                    .HasForeignKey("RecipientId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.Navigation("Buyer");

                b.Navigation("Recipient");
            });
        }
    }
}
