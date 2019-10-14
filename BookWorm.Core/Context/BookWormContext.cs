using BookWorm.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookWorm.Core.Context
{
    public class BookWormContext : IdentityDbContext<BookUser>
    {
        //TODO: вынести строку соединения в сборку при старте
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=BookWormDb;Username=postgres;Password=postgres");        
        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ReadTable> Readings { get; set; }
        public DbSet<Quote> Quotes { get; set; }
    }

    
}
