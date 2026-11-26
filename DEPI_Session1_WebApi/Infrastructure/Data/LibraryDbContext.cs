using DEPI_Session1_WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session1_WebApi.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        // relationships here are by convention (AuthorId, BookId, BorrowerId + navigation properties)
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
