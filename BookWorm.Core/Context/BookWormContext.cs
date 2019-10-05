using BookWorm.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookWorm.Core.Context
{
    public class BookWormContext : DbContext
    {
        //public BookWormContext(DbContextOptions<BookWormContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=BookWormDb;Username=postgres;Password=postgres");
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ReadTable> Readings { get; set; }
        public DbSet<Quote> Quotes { get; set; }
    }

    
}
