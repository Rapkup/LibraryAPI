using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Implementations.Contexts
{
    public class ReposContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }


        public ReposContext(DbContextOptions<ReposContext> options) : base(options) { }

      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Book_Author");

        }

    }
}
