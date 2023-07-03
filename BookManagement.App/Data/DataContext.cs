using BookManagement.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.App.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookCategory> BookCategories { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<PricePerDay> PricePerDays { get; set;}

        public DbSet<Reader> Readers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // category - book (many - many)
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            //  category - pricePerDay (one - one)
            modelBuilder.Entity<Category>()
                .HasOne(c => c.PricePerDay)
                .WithOne(p => p.Category)
                .HasForeignKey<PricePerDay>(p => p.CategoryId);

            // book - bill (many - one)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Bills)
                .WithOne(b => b.Book)
                .HasForeignKey(e => e.BookId);

            //reader - bill (many - one)
            modelBuilder.Entity<Reader>()
                .HasMany(r => r.Bills)
                .WithOne(b => b.Reader)
                .HasForeignKey(r => r.ReaderId);
        }
    }
}
