using Microsoft.EntityFrameworkCore;
using CheckoutConsole.Models;

namespace CheckoutConsole
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = CheckoutData.db");
        }

        public DbSet<Article>? Articles { get; set; }
        public DbSet<Customer>? Costumers { get; set; }
        public DbSet<InvoiceItem>? InvoiceItems { get; set; }   
    }
}
