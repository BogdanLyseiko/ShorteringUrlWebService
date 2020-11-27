using Microsoft.EntityFrameworkCore;
using ShorteringUrlWebService.Models.Entities;

namespace ShorteringUrlWebService.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    }
}