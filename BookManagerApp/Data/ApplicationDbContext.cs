using BookManagerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagerApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Book> Books => Set<Book>();
    }
}