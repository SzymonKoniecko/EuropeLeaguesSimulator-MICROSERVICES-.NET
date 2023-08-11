using Logger.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Logger.API.Context
{
    public class AppLogContext : DbContext
    {
        public AppLogContext(DbContextOptions<AppLogContext> options) : base(options)
        {

        }
        public DbSet<AppLogDto> AppLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppLogDto>()
                .HasKey(al =>
                al.Id);
        }
    }
}
